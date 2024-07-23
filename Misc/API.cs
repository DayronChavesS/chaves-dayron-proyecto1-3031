using chaves_dayron_proyecto1_3031.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace chaves_dayron_proyecto1_3031.Misc
{
    /*
     ISSUES:
     - La API suele fallar cerca de las 12 media noche cuando el DepartureDate es [HOY].
     Esto es un bug por parte del equipo de Amadeus.

     - A veces la API entrega ofertas que contienen destinos finales diferentes. He inspeccionado
       Multiples veces los segmentos de los itinerarios y no hay rastro del destino configurado
       Por el usuario. Se desconoce la razon de este comportamiento.
     */

    public class API
    {
        //Credenciales de la API
        public string API_KEY = "b8ve2FGOKculnuOWFfUZeRqnVISEacCF";
        public string API_SECRET = "6OGpAPAOxtk9b9RO";
        public async Task<string> GetToken()
        {
            //objeto que permite establecer conexiones por internet
            HttpClient client = new HttpClient();

            //configuramos el objeto para conectarse a Amadeus segun sus requisitos y documentacion
            client.BaseAddress = new Uri("https://test.api.amadeus.com/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/security/oauth2/token");
            request.Content = new StringContent($"grant_type=client_credentials&client_id={API_KEY}&client_secret={API_SECRET}",
                                                Encoding.UTF8,
                                                "application/x-www-form-urlencoded");

            //enviamos la solicitud de token para hacer consultas posteriormente
            HttpResponseMessage response = await client.SendAsync(request);

            //obtenemos la respuesta y la procesamos
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            ApiToken token = JsonConvert.DeserializeObject<ApiToken>(apiResponse);

            //regresamos el token
            return token.AccessToken;
        }

        public async Task<JObject> GetFlights(Preferences userSettigs)
        {
            /*
             Los datos obtenidos deben ser guardados en un objeto generico.
             Imposible crear un modelo 1:1 con la API, demasiada anidacion y variables.
             */
            JObject jsonObject = new JObject();

            var url = $"https://test.api.amadeus.com/v2/shopping/flight-offers";
            //https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode=SJO&destinationLocationCode=LAX&departureDate=2024-07-20&adults=1&currencyCode=CRC&max=250

            /*
             Parametros disponibles para ajustar los resultados enviados por la API,
             Segun documentacion.
             https://developers.amadeus.com/self-service/category/flights/api-doc/flight-offers-search/api-reference
             */
            var parameters = $"?originLocationCode={userSettigs.Origin}&destinationLocationCode={userSettigs.Destination}&departureDate={userSettigs.DepartureDate}&adults={userSettigs.Adults}&currencyCode=CRC&max=250&nonStop={userSettigs.NonStop.ToString().ToLower()}";
            if (userSettigs.Children != 0) { parameters += $"&children={userSettigs.Children}"; }
            if (userSettigs.Infants != 0) { parameters += $"&infants={userSettigs.Infants}"; }
            if (userSettigs.TravelClass != "") { parameters += $"&travelClass={userSettigs.TravelClass}"; }
            if (userSettigs.MaxPrice != 0) { parameters += $"&maxPrice={userSettigs.MaxPrice}"; }
            if (userSettigs.ReturnDate != "") { parameters += $"&returnDate={userSettigs.ReturnDate}"; }

            string token = await GetToken();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                //obtenemos los datos
                HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var ApiResponse = response.Content.ReadAsStringAsync().Result;
                    jsonObject = JsonConvert.DeserializeObject<JObject>(ApiResponse);
                }
            }

            //devolvemos los datos obtenidos
            return jsonObject;
        }
    }
}

/* TEST CODE
string path = AppDomain.CurrentDomain.BaseDirectory + @"Misc\LocalTestData.json";
string fileContents = System.IO.File.ReadAllText(path);

JObject jsonObject = JsonConvert.DeserializeObject<dynamic>(fileContents);
return jsonObject;
*/