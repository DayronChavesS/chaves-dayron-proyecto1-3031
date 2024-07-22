using chaves_dayron_proyecto1_3031.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chaves_dayron_proyecto1_3031.Misc
{
    public class BuildFlight
    {
        public List<Flight> BuildFlightList(JObject data)
        {
            List<Flight> listaVuelos = new List<Flight>();

            //Obtenemos el numero de vuelos en data
            int nVuelos = (int)data.SelectToken("meta.count");

            //ciclo para obtener datos de todos los vuelos
            for (int i = 0; i < nVuelos; i++)
            {
                Flight flight = new Flight();

                //dato por dato,recorremos el "Path" del Json para obtener los datos de interes.
                flight.oneWay = (bool)data.SelectToken($"data[{i}].oneWay");
                flight.numberOfBookableSeats = (int)data.SelectToken($"data[{i}].numberOfBookableSeats");

                //Obtenemos el numero de segmentos, para conseguir de primer a ultimo elemento.
                int nSegmentos = data.SelectTokens($"data[{i}].itineraries[0].segments[*]").Count() - 1;

                flight.departureLocation = (string)data.SelectToken($"data[{i}].itineraries[0].segments[0].departure.iataCode");
                DateTime tempVar = (DateTime)data.SelectToken($"data[{i}].itineraries[0].segments[0].departure.at");
                flight.departureAt = tempVar.ToString("yyyy-MM-dd");

                flight.arrivalLocation = (string)data.SelectToken($"data[{i}].itineraries[0].segments[{nSegmentos}].arrival.iataCode");
                tempVar = (DateTime)data.SelectToken($"data[{i}].itineraries[0].segments[{nSegmentos}].arrival.at");
                flight.arrivalAt = tempVar.ToString("yyyy-MM-dd");

                flight.grandTotal = (double)data.SelectToken($"data[{i}].price.grandTotal");

                flight.cabinType = (string)data.SelectToken($"data[{i}].travelerPricings[0].fareDetailsBySegment[0].cabin");
                
                //añadimos el vuelo a la lista y repetimos el ciclo
                listaVuelos.Add(flight);
            }

            return listaVuelos;
        }
    }
}