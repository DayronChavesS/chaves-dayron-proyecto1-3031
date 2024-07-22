using chaves_dayron_proyecto1_3031.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace chaves_dayron_proyecto1_3031.Misc
{
    public class Notify
    {
        /*
         Para ver la bandeja de entrada y correos enviados,
         ver LEEME.txt en raiz de proyecto.
         (Seccion Notificaciones de Email)
         */

        //credenciales de conexion a mailtrap
        string mailtrapUser = "171ce314fc8b1a";
        string mailtrapPassword = "24ce989e221e04";
        public void SendEmail(User userData, Reserve rsrvData)
        {
            //creamos el contenido del correo electronico
            MimeMessage emailContent = new MimeMessage();

            emailContent.From.Add(new MailboxAddress("Tu Empresa de Vuelos", "tuempresa@vuelos.com"));
            emailContent.To.Add(new MailboxAddress(userData.Name, userData.Email));

            emailContent.Subject = "Confirmacion de compra.";
            emailContent.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<h1>Hola " + userData.Name + ".</h1><br/><br/>" +
                "<p>La fecha de su proximo vuelo es el " + rsrvData.DepartureDate +
                ", en el Aeropuerto " + rsrvData.Origin +
                ", con destino al Aeropuerto " + rsrvData.Destination + ".</p><br/><br/><br/><br/>" +
                "<hr/>" +
                "<p><b>Clase: " + rsrvData.Class + "</b></p>"+
                "<p><b>Precio: " + rsrvData.Price + "</b></p>" +
                "<hr/>" +
                "<br/><br/><small><i>Gracias por confiar en Tu Empresa de Vuelos.<i></small><br/>" +
                "<small><i>Este mensaje se genero el: " + DateTime.Now + ".<i></small>"
            };

            //objeto que nos permite conectarnos a servidores SMTP
            //el uso de la libreria MailKit facilita enormemente este proceso
            SmtpClient smtp = new SmtpClient();

            /*
             Nos conectamos y autentificacmos en Mailtrap segun documentacion.
             https://help.mailtrap.io/article/109-getting-started-with-mailtrap-email-testing
             */

            smtp.Connect("sandbox.smtp.mailtrap.io", 2525, false);
            smtp.Authenticate(mailtrapUser, mailtrapPassword);

            //enviamos el correo
            smtp.Send(emailContent);
            smtp.Disconnect(true);
        }
    }
}