using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ModuleFour.Model
{
    public class ServerRequest
    {
        public async Task<string> GetRequestAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responce =await client.GetAsync(url);
                if (responce.IsSuccessStatusCode)
                {
                    string content = await responce.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    return $"Error: {responce.StatusCode}";
                }
            }
        }
    }
}
