using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IU.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uri { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Nome);
        }
        static private HttpClient httpClient;

        static private void IniciarHttp()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:54289/");
        }

        public static async Task<string> Verificar(string _nome)
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Usuario/Verificar/" + _nome);
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            string strNome = JsonConvert.DeserializeObject<string>(str);
            return strNome;
        }

        public static async void Cadastrar(Usuario _usuario)
        {
            string s = "=" + JsonConvert.SerializeObject(_usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            IniciarHttp();
            await httpClient.PostAsync("api/Usuario/Cadastrar", content);
        }

        public static async void AlterarUri(Usuario _usuario)
        {
            IniciarHttp();
            string s = "=" + JsonConvert.SerializeObject(_usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("api/Usuario/AlterarUri/" + _usuario.Nome, content);
        }

        public static async void AlterarNome(string nome, Usuario _usuario)
        {
            IniciarHttp();
            string s = "=" + JsonConvert.SerializeObject(_usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("api/Usuario/AlterarNome/" + nome, content);
        }

        public static async Task<List<Usuario>> Listar()
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Usuario/Listar");
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(str);
            return usuarios;
        }

        public static async Task<Usuario> Obter(string nome)
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Usuario/Obter/" + nome);
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            Usuario usuarios = JsonConvert.DeserializeObject<Usuario>(str);
            return usuarios;
        }

        public static async void Deletar(string nome)
        {
            IniciarHttp();
            await httpClient.DeleteAsync("api/Usuario/Deletar/" + nome);
        }
    }
}
