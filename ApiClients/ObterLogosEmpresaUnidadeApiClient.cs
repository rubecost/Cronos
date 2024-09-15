using Cronos.Models;
using Cronos.Services;
using Cronos.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.ApiClients
{
    internal class ObterLogosEmpresaUnidadeApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoViewModel _viewModel;
        public ObterLogosEmpresaUnidadeApiClient(ConfiguracaoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<EmpresaComUnidadeModel> EmpresaComUnidadeAsync(int idEmpresa, int idUnidade)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}unidade/detalhes_gerais?idEmpresa={idEmpresa}&idUnidade={idUnidade}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<EmpresaComUnidadeModel>(content);

                    return detalhes ??= new EmpresaComUnidadeModel();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new EmpresaComUnidadeModel();
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

            return new EmpresaComUnidadeModel();
        }
    }
}
