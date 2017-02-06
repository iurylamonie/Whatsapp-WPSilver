using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IU.Modelo
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdAdm { get; set; }
        static private HttpClient httpClient;


        static private void IniciarHttp()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:54289/");
        }

        static public async Task<List<Grupo>> Listar(int _usuario_id)
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Grupo/Listar/" + _usuario_id);
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            var grupos = JsonConvert.DeserializeObject<List<Grupo>>(str);
            return grupos;
        }

        static public async Task<List<Grupo>> ListarMeuGrupos(int _usuario_id)
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Grupo/ListarMeuGrupos/" + _usuario_id);
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            var grupos = JsonConvert.DeserializeObject<List<Grupo>>(str);
            return grupos;
        }

        static public async void Criar(Grupo _grupo)
        {
            string s = "=" + JsonConvert.SerializeObject(_grupo);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            IniciarHttp();
            await httpClient.PostAsync("api/Grupo/Criar", content);
        }

        static public async void Alterar(Grupo _grupo)
        {
            IniciarHttp();
            string s = "=" + JsonConvert.SerializeObject(_grupo);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("api/Grupo/Alterar/" + _grupo.Id, content);
        }

        static public async void Deletar(int _id)
        {
            IniciarHttp();
            await httpClient.DeleteAsync("api/Grupo/Deletar/" + _id);
        }

        static public async void Enviar(int id,Mensagem _msg)
        {
            string s = "=" + JsonConvert.SerializeObject(_msg);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            IniciarHttp();
            await httpClient.PostAsync("api/Grupo/Enviar/" + id, content);
        }
    }
}
