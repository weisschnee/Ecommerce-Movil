using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Modelos.Extras
{
    public class EnvioCorreo
    {

        public void enviarEmail(string para, string codigo)
        {
           


            string body = $@"
                <html style='background-color: #fdf8f8; font-family: Arial, sans-serif;'>
                <body style='margin: 0; padding: 0; background-color: #fdf8f8;'>
                    <div style='max-width: 600px; margin: 30px auto; background: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);'>
                        <header style='background-color: #f8c4d0; color: white; text-align: center; padding: 20px;'>
                            <h1 style='margin: 0; font-size: 28px;'>¡Gracias por registrarte!</h1>
                        </header>
                        <div style='padding: 20px; text-align: center;'>
                            <h2 style='color: #555; font-size: 22px;'>Bienvenido a AlexaShop</h2>
                            <p style='color: #777; margin: 15px 0;'>Estamos emocionados de tenerte con nosotros. Aquí está tu código único:</p>
                            <div style='font-size: 36px; font-weight: bold; color: #f8c4d0; margin: 15px 0;'>{codigo}</div>
                            <p style='color: #999; margin: 15px 0; font-size: 14px;'>Utiliza este código para completar tu registro.</p>
                        </div>
                        <footer style='background-color: #fbe8ed; text-align: center; padding: 10px; font-size: 12px; color: #666;'>
                            <p>&copy; 2024 VariedadesAlexa. Todos los derechos reservados.</p>
                        </footer>
                    </div>
                </body>
                </html>
                ";


            string mensaje = "Error al enviar este correo. Por favor verifique los datos o intente mås tarde.";
            string desde = "gallox700@gmail.com";
            string displayName = "Variedades Alexa";
            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(desde, displayName);
                mail.To.Add(para);

                mail.Subject = "Codigo De Verificación";
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(desde, "your credential here");




                client.Send(mail);
                

            }
            catch (Exception ex)
            {
               
                mensaje = ex.Message + "Algo paso";
              
            }

        }



    }
}

