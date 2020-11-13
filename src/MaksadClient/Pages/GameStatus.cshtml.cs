using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Risk.Shared;

namespace MaksadClient.Pages
{
    public class GameStatusModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public GameStatusModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public string ServerName { get; set; }

        public async Task OnGetAsync()
        {
            var client = httpClientFactory.CreateClient();
            Status = await client.GetFromJsonAsync<GameStatus>($"{ServerName}/status");
        }

        public GameStatus Status { get; set; }

        public IEnumerable<BoardTerritory> Board { get; set; }


        public async Task<IActionResult> OnPostStartGameAsync(string server, string secretCode)
        {
            var client = httpClientFactory.CreateClient();
            await client.PostAsJsonAsync($"{server}/startgame", new StartGameRequest { SecretCode = secretCode });
            return Redirect($"gameStatus/?serverName={server}");
        }
    }
}
