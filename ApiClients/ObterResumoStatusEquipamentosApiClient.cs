using CommunityToolkit.Mvvm.Messaging;
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
    public class ObterResumoStatusEquipamentosApiClient
    {
        private readonly HttpClient _client;
        private readonly MainPageViewModel _viewModel;

        public ObterResumoStatusEquipamentosApiClient(MainPageViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<ResumoEquipamentosModel> ObterResumoStatusEquipamentosAsync(int idSetor)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}equipamento/resumo_status_equipamentos?idSetor={idSetor}");   

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<ResumoEquipamentosModel>(content);

                    return detalhes ??= new ResumoEquipamentosModel();               
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ResumoEquipamentosModel();
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

            return new ResumoEquipamentosModel();
        }
    }
}
