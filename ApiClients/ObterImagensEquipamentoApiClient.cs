using Cronos.Models;
using Cronos.Services;
using Cronos.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.ApiClients
{
    public class ObterImagensEquipamentoApiClient
    {
        private readonly HttpClient _client;
        private readonly ImagensEquipamentoViewModel _viewModel;
        public ObterImagensEquipamentoApiClient(ImagensEquipamentoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<List<ImagensEquipamentoComDescricaoModel>> ObterImagensEquipamentoAsync(ImagensEquipamentoComDescricaoModel imagensEquipamentoModel)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}equipamento/imagens_equipamento", imagensEquipamentoModel);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<List<ImagensEquipamentoComDescricaoModel>>(content);

                    return detalhes ?? new List<ImagensEquipamentoComDescricaoModel>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<ImagensEquipamentoComDescricaoModel>();
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.SemInternet = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }

            return new List<ImagensEquipamentoComDescricaoModel>();
        }
    }
}
