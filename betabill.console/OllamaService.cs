using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace betabill.console
{
    public class OllamaService
    {
        public async Task<string> SendMessage(string prompt)
        {
            var client = new HttpClient();
            var url = "http://localhost:11434/api/generate";

            var request = new
            {
                model = "mistral",
                prompt = prompt,
                stream = false
            };

            var response = await client.PostAsJsonAsync(url, request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
