using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Servico.Controllers
{
    [RoutePrefix("api/Membro")]
    public class MembroController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("Listar/{grupo_id}")]
        public List<Models.Membro> Listar(int grupo_id)
        {
            Models.WhatsappDataContext dc = new Models.WhatsappDataContext();
            var r = from m in dc.Membros
                    where m.grupo_id == grupo_id
                    select m;
            return r.ToList();
        }

        [AcceptVerbs("POST")]
        [Route("Adicionar")]
        public void Adicionar([FromBody] string conteudo)
        {
            
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            Models.Membro novoMembro = JsonConvert.DeserializeObject<Models.Membro>(conteudo);
            dataContext.Membros.InsertOnSubmit(novoMembro);
            dataContext.SubmitChanges();
        }

        [AcceptVerbs("DELETE")]
        [Route("Deletar/{membro}")]
        public void Deletar(Models.Membro membro)
        {
            Models.WhatsappDataContext dataContext = new Models.WhatsappDataContext();
            var r = (from m in dataContext.Membros
                     where m.grupo_id == membro.grupo_id && m.usuario_id == membro.usuario_id
                     select m).Single();
            dataContext.Membros.DeleteOnSubmit(r);
            dataContext.SubmitChanges();
        }
    }
}
