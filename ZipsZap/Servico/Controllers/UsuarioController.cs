using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Servico.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("Verificar/{nome}")]
        public string Verificar(string nome)
        {
            try
            {
                Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
                string str = (from u in dataContext.Usuarios
                              where u.nome == nome
                              select u.nome).Single();
                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        [AcceptVerbs("POST")]
        [Route("Cadastrar")]
        public void Cadastrar([FromBody] string conteudo)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            dataContext.Usuarios.InsertOnSubmit(usuario);
            dataContext.SubmitChanges();
        }

        [AcceptVerbs("PUT")]
        [Route("AlterarUri/{nome}")]
        public void AlterarUri(string nome, [FromBody] string conteudo)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            Models.Usuario usuarioBD = (from u in dataContext.Usuarios
                                        where u.nome == nome
                                        select u).Single();
            usuarioBD.uri = usuario.uri;
            dataContext.SubmitChanges();               
        }

        [AcceptVerbs("PUT")]
        [Route("AlterarNome/{nome}")]
        public void AlterarNome(string nome, [FromBody] string conteudo)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            Models.Usuario usuarioBD = (from u in dataContext.Usuarios
                                        where u.nome == nome
                                        select u).Single();
            usuarioBD.nome = usuario.nome;
            dataContext.SubmitChanges();
        }

        [AcceptVerbs("GET")]
        [Route("Listar")]
        public List<Models.Usuario> Listar()
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            var r = from u in dataContext.Usuarios select u;
            return r.ToList();
        }

        [AcceptVerbs("GET")]
        [Route("Obter/{nome}")]
        public Models.Usuario Obter(string nome )
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            var r = (from u in dataContext.Usuarios
                     where u.nome == nome
                     select u).Single();
            return r;
        }

        [AcceptVerbs("GET")]
        [Route("Deletar/{nome}")]
        public void Deletar(string nome)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            var r = (from u in dataContext.Usuarios
                     where u.nome == nome
                     select u).Single();
            dataContext.Usuarios.DeleteOnSubmit(r);
            dataContext.SubmitChanges();
        }

    }
}
