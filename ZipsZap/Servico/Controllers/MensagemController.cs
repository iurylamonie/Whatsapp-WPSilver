using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Servico.Controllers
{
    [RoutePrefix("api/Mensagem")]
    public class MensagemController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("Enviar")]
        public void Enviar([FromBody] string conteudo)
        {
            Models.Mensagem msg = JsonConvert.DeserializeObject<Models.Mensagem>(conteudo);

            HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(msg.Uri);
            sendNotificationRequest.Method = "POST";

            string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                "<wp:Toast>" +
                        "<wp:Text1>" + msg.Remetente + "</wp:Text1>" +
                        "<wp:Text2>" + msg.Conteudo + "</wp:Text2>" +
                        "<wp:Param>/Conversa.xaml?Rem=" + msg.Remetente + "&amp;Cont=" + msg.Conteudo +"</wp:Param>" +
                   "</wp:Toast> " +
                "</wp:Notification>";

            byte[] notificationMessage = Encoding.Default.GetBytes(toastMessage);
            // Set the web request content length.
            sendNotificationRequest.ContentLength = notificationMessage.Length;
            sendNotificationRequest.ContentType = "text/xml";
            sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
            sendNotificationRequest.Headers.Add("X-NotificationClass", "2");

            using (Stream requestStream = sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }
        }
    }
}
