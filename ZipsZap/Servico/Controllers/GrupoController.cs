using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Servico.Controllers
{
    [RoutePrefix("api/Grupo")]
    public class GrupoController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("Listar/{usuario_id}")]
        public List<Models.Grupo> Listar(int usuario_id)
        {
            Models.WhatsappDataContext dc = new Models.WhatsappDataContext();
            List<Models.Grupo> grupos = new List<Models.Grupo>();
            List<Models.Membro> grupoPart = (from gm in dc.Membros
                                                   where gm.usuario_id == usuario_id
                                                   select gm).ToList();


            foreach (var item in grupoPart)
            {
                Models.Grupo r = (from g in dc.Grupos
                                  where g.id == item.grupo_id
                                  select g).Single();
                grupos.Add(r);
            }

            return grupos;
        }

        [AcceptVerbs("GET")]
        [Route("ListarMeuGrupos/{usuario_id}")]
        public List<Models.Grupo> ListarMeuGrupos(int usuario_id)
        {
            Models.WhatsappDataContext dc = new Models.WhatsappDataContext();
            List<Models.Grupo> grupos = (from g in dc.Grupos
                                         where g.idAdm == usuario_id
                                         select g).ToList();
            return grupos;
        }

        [AcceptVerbs("POST")]
        [Route("Criar")]
        public void Criar([FromBody] string conteudo)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Grupo grupo = JsonConvert.DeserializeObject<Models.Grupo>(conteudo);            
            dataContext.Grupos.InsertOnSubmit(grupo);
            dataContext.SubmitChanges();

            int _grupo_id = (from g in dataContext.Grupos
                            orderby g.id descending
                            select g.id).FirstOrDefault();
            Models.Membro membro = new Models.Membro { grupo_id = _grupo_id, usuario_id = grupo.idAdm };
            dataContext.Membros.InsertOnSubmit(membro);
            dataContext.SubmitChanges();
        }

        [AcceptVerbs("PUT")]
        [Route("Alterar/{id}")]
        public void Alterar(int id, [FromBody] string conteudo)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Grupo grupo = JsonConvert.DeserializeObject<Models.Grupo>(conteudo);
            Models.Grupo grupoBD = (from g in dataContext.Grupos
                                    where g.id == id
                                    select g).Single();
            grupoBD.descricao = grupo.descricao;
            dataContext.SubmitChanges();
        }

        [AcceptVerbs("DELETE")]
        [Route("Deletar/{id}")]
        public void Deletar(int id)
        {
            Models.WhatsappDataContext dc = new Models.WhatsappDataContext();
            var r = (from g in dc.Grupos
                     where g.id == id
                     select g).Single();

            var membros = from m in dc.Membros
                          where m.grupo_id == r.id
                          select m;

            dc.Membros.DeleteAllOnSubmit(membros);
            dc.Grupos.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }

        [AcceptVerbs("POST")]
        [Route("Enviar/{id}")]
        public void Enviar(int id, [FromBody] string conteudo)
        {
            Models.Mensagem msg = JsonConvert.DeserializeObject<Models.Mensagem>(conteudo);
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();

            List<Models.Membro> membrosGrupo = (from m in dataContext.Membros
                                                where m.grupo_id == id
                                                select m).ToList();

            foreach (var membro in membrosGrupo)
            {
                Models.Usuario usuario = (from u in dataContext.Usuarios
                                          where u.id == membro.usuario_id
                                          select u).Single();

                HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(usuario.uri);
                sendNotificationRequest.Method = "POST";

                string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                            "<wp:Text1>" + msg.Remetente + "</wp:Text1>" +
                            "<wp:Text2>" + msg.Conteudo + "</wp:Text2>" +
                            "<wp:Param>/Conversa.xaml?Rem=" + msg.Remetente + "&amp;Cont=" + msg.Conteudo + "</wp:Param>" +
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
}
