using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SimuladorWeb.Modelo
{
    public class Mensagem
    {
        public string Remetente { get; set; }
        public string Conteudo { get; set; }
        public string Uri { get; set; }
        static private HttpClient httpClient;

        static private void IniciarHttp()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:54289/");
        }

        static public  void Enviar(Mensagem _msg)
        {
            string s = "=" + JsonConvert.SerializeObject(_msg);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            IniciarHttp();
            httpClient.PostAsync("api/Mensagem/Enviar", content);
        }
    }
}