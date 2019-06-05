using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegracaoSpotify_AuthAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntegracaoSpotify_AuthAPI.Controllers
{
    [Route("v1/autorizacao")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private string ClientSecret = "a57c4ea32bd04c21b4ce8d2a97e49531";
        private string ClientId = "c07f39755dfd4c7494dc58226dbe71b2";

        [HttpGet]
        public async Task<ActionResult<Token>> GetToken()
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret));

            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            HttpContent content = new FormUrlEncodedContent(args);

            HttpResponseMessage resp = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            string msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Token>(msg);
        }
    }
}