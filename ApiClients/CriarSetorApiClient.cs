using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Cronos.Models;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{
    public class CriarSetorApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoEmpresaViewModel _viewModel;
        public CriarSetorApiClient(ConfiguracaoEmpresaViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<string> CriarSetorAsync(CriarSetorModel criarSetorModel)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}setor/criar", criarSetorModel);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                    return result?.Message ?? "Setor criado com sucesso.";
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                    return result?.Message ?? "Erro ao processar a resposta.";
                }
            }
            catch (HttpRequestException ex)
            {
                _viewModel.SemInternet = true;
                return $"Erro de rede: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Erro inesperado: {ex.Message}";
            }
        }
    }
}
