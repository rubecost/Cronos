using System;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Models;
using Cronos.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{

    public class ObterRecomendacoesNaoAtendidasApiClient
    {
        private readonly HttpClient _client;
        private readonly MainPageViewModel _viewModel;

        public ObterRecomendacoesNaoAtendidasApiClient(MainPageViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<List<RecomendacaoComEquipamentoModel>> ObterRecomendacoesNaoAtendidasAsync(int idSetor)
        {
            try
            {

                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}equipamento/recomendacoes_nao_atendidas", idSetor);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<List<RecomendacaoComEquipamentoModel>>(content);

                    return detalhes ?? new List<RecomendacaoComEquipamentoModel>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<RecomendacaoComEquipamentoModel>();
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.SemInternet = true;
                // sem internet
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }

            return new List<RecomendacaoComEquipamentoModel>();
        }
    }
}
