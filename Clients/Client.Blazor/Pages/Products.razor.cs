using Catalog.Domain.Models.Response;
using Client.Blazor.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Client.Blazor.Pages
{
    public partial class Products
    {
        private List<ProductResponse> Items = new();

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        private IConfiguration Config { get; set; }

        [Inject]
        private ITokenService TokenService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var tokenResponse = await TokenService.GetTokenAsync("CatalogAPI.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(Config["ApiUrl"] + "/api/products");
            if (result.IsSuccessStatusCode)
            {
                Items = await result.Content.ReadFromJsonAsync<List<ProductResponse>>();
            }
        }
    }
}
