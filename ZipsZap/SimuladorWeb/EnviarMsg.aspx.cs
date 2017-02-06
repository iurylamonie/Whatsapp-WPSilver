using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimuladorWeb
{
    public partial class EnviarMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected  void ButtonEnviar_Click(object sender, EventArgs e)
        {
            Modelo.Mensagem msg = new Modelo.Mensagem
            {
                Remetente = TextBoxRemetente.Text,
                Uri = TextBoxUri.Text,
                Conteudo = TextBoxMsg.Text
            };
             Modelo.Mensagem.Enviar(msg);
        }
    }
}