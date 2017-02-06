using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IU.Modelo
{
     public class Membro
    {
        public int Grupo_id { get; set; }
        public int Usuario_id { get; set; }
        public Usuario Usuario { get; set; }

        static private HttpClient httpClient;


        static private void IniciarHttp()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://10.21.0.137/20131011110169/");
        }

        static public async Task<List<Membro>> Listar(int _grupo_id)
        {
            IniciarHttp();
            var responseMessage = await httpClient.GetAsync("api/Membro/Listar/" + _grupo_id);
            string str = responseMessage.Content.ReadAsStringAsync().Result;
            var Membros = JsonConvert.DeserializeObject<List<Membro>>(str);
            return Membros;
        }

        static public async void Adicionar(Membro _membro)
        {
            string s = "=" + JsonConvert.SerializeObject(_membro);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            IniciarHttp();
            await httpClient.PostAsync("api/Membro/Adicionar", content);
        }

        static public async void Deletar(Membro _membro)
        {
            IniciarHttp();
            await httpClient.DeleteAsync("api/Membro/Deletar/" + _membro);
        }
    }
}
