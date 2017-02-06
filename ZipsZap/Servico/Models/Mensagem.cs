using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servico.Models
{
    public class Mensagem
    {
        public string Remetente { get; set; }
        public string Conteudo { get; set; }
        public string Uri { get; set; }
    }
}