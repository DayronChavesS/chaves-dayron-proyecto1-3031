using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace chaves_dayron_proyecto1_3031.DataBase
{
    public class DBConnection
    {
        protected SqlConnection connection;
        protected void ConnectToDB()
        {
            try
            {
                //ingresamos el connection string
                connection = new SqlConnection("Data Source=(local);Initial Catalog=chaves-dayron-proyecto1-3031;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
                //abrimos la conexion
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("¡¡¡ATENCION!!!");
                Debug.WriteLine("Fallo en conexion con base de datos.");
                Debug.WriteLine("Troubleshutting:");
                Debug.WriteLine("1. Ejecute CREATE_DATABASE.sql en SQL Server Management Studio.");
                Debug.WriteLine("2. Ingrese a Visual Studio; Dirigase a Explorador de Servicios; Conectese a la nueva DB.");
                Debug.WriteLine("3. Ajuste el string de conexion en DataBase/Conexion.");
                Debug.WriteLine("StackTrace:");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {

            }
        }

        protected void DisconnectFromDB()
        {
            try
            {
                //cerramos la conexion
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("¡¡¡ATENCION!!!");
                Debug.WriteLine("Fallo en desconexion con base de datos.");
                Debug.WriteLine("Troubleshutting:");
                Debug.WriteLine("1. Ejecute CREATE_DATABASE.sql en SQL Server Management Studio.");
                Debug.WriteLine("2. Ingrese a Visual Studio; Dirigase a Explorador de Servicios; Conectese a la nueva DB.");
                Debug.WriteLine("3. Ajuste el string de conexion en DataBase/Conexion.");
                Debug.WriteLine("StackTrace:");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {

            }
        }
    }
}