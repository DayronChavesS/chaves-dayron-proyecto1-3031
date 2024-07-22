using chaves_dayron_proyecto1_3031.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace chaves_dayron_proyecto1_3031.DataBase
{
    public class DBCommand : DBConnection
    {
        /*
         Estructura general del codigo:
         -> Nos conectamos a la base de datos
         -> Inovocamos un procedimiento almacenado en SQL Server
         -> Si hay parametros (en procedimiento almacenado): 
            los agregamos uno por uno con el objeto enviado por parametro (de la funcion).
         -> Ejectuamos el procedimiento.
         -> Si el procedimiento es una consulta y retorna resultados:
            - Entramos en su ciclo hasta que el lector llegue a EOL.
            - Capturamos los datos en un objeto. Si son varias filas, creamos una lista de objetos.
         -> Nos desconectamos de la base da datos.
         */

        /*
         Aqui hay GETs, INSERTs y UPDATEs para cada una de las tablas de la base de datos.
         */

        public bool EmailExist(String email)
        {
            bool result = true;

            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpCheckEmail", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                result = reader.HasRows;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al verificar el correo electronico.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }

            return result;
        }

        public bool AuthUser(User authAttempt)
        {
            bool result = false;

            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpAuthUser", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", authAttempt.Email);
                cmd.Parameters.AddWithValue("@Password", authAttempt.Password);
                SqlDataReader reader = cmd.ExecuteReader();
                result = reader.HasRows;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al autentificar el usuario.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }

            return result;
        }

        public void InsertUser(User NewUser)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpInsertNewUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", NewUser.Name);
                cmd.Parameters.AddWithValue("@Email", NewUser.Email);
                cmd.Parameters.AddWithValue("@Password", NewUser.Password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al guardar el nuevo usuario en la base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        public void InsertFlightPreferences(User theUser)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpInsertNewPreferences", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", theUser.UserId);
                cmd.Parameters.AddWithValue("@Origin", "SJO");
                cmd.Parameters.AddWithValue("@Destination", "LAX");
                cmd.Parameters.AddWithValue("@DepartureDate", DateTime.Today.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ReturnDate", "");
                cmd.Parameters.AddWithValue("@Adults", 1);
                cmd.Parameters.AddWithValue("@Children", 0);
                cmd.Parameters.AddWithValue("@Infants", 0);
                cmd.Parameters.AddWithValue("@TravelClass", "");
                cmd.Parameters.AddWithValue("@NonStop", false);
                cmd.Parameters.AddWithValue("@MaxPrice", 0);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al guardar preferencias de vuelo predeterminadas.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        public void InsertReserve(User theUser, Flight newReserve)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpInsertNewReserve", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", theUser.UserId);
                cmd.Parameters.AddWithValue("@Origin", newReserve.departureLocation);
                cmd.Parameters.AddWithValue("@Destination", newReserve.arrivalLocation);
                cmd.Parameters.AddWithValue("@DepartureDate", newReserve.departureAt);
                cmd.Parameters.AddWithValue("@ArrivalDate", newReserve.arrivalAt);
                cmd.Parameters.AddWithValue("@Class", newReserve.cabinType);
                cmd.Parameters.AddWithValue("@Price", newReserve.grandTotal);
                cmd.Parameters.AddWithValue("@InCart", 1);
                cmd.Parameters.AddWithValue("@InHistory", 0);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al guardar la nueva reserva en la base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        public User GetUser(String email)
        {
            User user = new User();

            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpGetUser", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new User()
                    {
                        UserId = int.Parse(reader[0].ToString()),
                        Name = reader[1] + "",
                        Email = reader[2] + "",
                        Password = reader[3] + "",
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al obtener los datos del usuario de base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }

            return user;
        }

        public Preferences GetFlightPreferences(User theUser)
        {
            Preferences userSettings = new Preferences();

            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpGetPreferences", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", theUser.UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userSettings = new Preferences()
                    {
                        UserId = int.Parse(reader[0].ToString()),
                        Origin = reader[1] + "",
                        Destination = reader[2] + "",
                        DepartureDate = reader[3] + "",
                        ReturnDate = reader[4] + "",
                        Adults = int.Parse(reader[5].ToString()),
                        Children = int.Parse(reader[6].ToString()),
                        Infants = int.Parse(reader[7].ToString()),
                        TravelClass = reader[8] + "",
                        NonStop = bool.Parse(reader[9].ToString()),
                        MaxPrice = int.Parse(reader[10].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al obtener las preferencias del usuario.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }

            return userSettings;
        }

        public List<Reserve> GetReserve(User theUser)
        {
            List<Reserve> ReserveList = new List<Reserve>();

            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpGetReserve", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", theUser.UserId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reserve reserve = new Reserve()
                    {
                        RsrvId = int.Parse(reader[0].ToString()),
                        UserId = int.Parse(reader[1].ToString()),
                        Origin = reader[2] + "",
                        Destination = reader[3] + "",
                        DepartureDate = reader[4] + "",
                        ArrivalDate = reader[5] + "",
                        Class = reader[6] + "",
                        Price = int.Parse(reader[7].ToString()),
                        InCart = (bool)reader[8],
                        InHistory = (bool)reader[9]
                    };
                    ReserveList.Add(reserve);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al obtener lista de reservas de base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }

            return ReserveList;
        }

        public void UpdateUser(User newData, User oldData)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpUpdateUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = oldData.UserId;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = newData.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = newData.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = newData.Password;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al actualizar los datos del usuario en la base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        public void UpdateFlightPreferences(Preferences newPreferences)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpUpdatePreferences", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = newPreferences.UserId;
                cmd.Parameters.Add("@Origin", SqlDbType.VarChar).Value = newPreferences.Origin;
                cmd.Parameters.Add("@Destination", SqlDbType.VarChar).Value = newPreferences.Destination;
                cmd.Parameters.Add("@DepartureDate", SqlDbType.VarChar).Value = newPreferences.DepartureDate;
                cmd.Parameters.Add("@ReturnDate", SqlDbType.VarChar).Value = newPreferences.ReturnDate;
                cmd.Parameters.Add("@Adults", SqlDbType.TinyInt).Value = newPreferences.Adults;
                cmd.Parameters.Add("@Children", SqlDbType.TinyInt).Value = newPreferences.Children;
                cmd.Parameters.Add("@Infants", SqlDbType.TinyInt).Value = newPreferences.Infants;
                cmd.Parameters.Add("@TravelClass", SqlDbType.VarChar).Value = newPreferences.TravelClass;
                cmd.Parameters.Add("@NonStop", SqlDbType.Bit).Value = newPreferences.NonStop;
                cmd.Parameters.Add("@MaxPrice", SqlDbType.Int).Value = newPreferences.MaxPrice;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al actualizar las preferencias del usuario en la base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        public void UpdateReserve(Reserve theReserve)
        {
            try
            {
                ConnectToDB();
                SqlCommand cmd = new SqlCommand("stpUpdateReserve", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RsrvId", SqlDbType.BigInt).Value = theReserve.RsrvId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fallo al actualizar los datos de la reserva en la base de datos.");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                DisconnectFromDB();
            }
        }
    }
}