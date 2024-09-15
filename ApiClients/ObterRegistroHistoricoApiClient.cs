using CommunityToolkit.Mvvm.Messaging;
using Cronos.Models;
using Cronos.Services;
using Cronos.Views;
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
    public class ObterRegistroHistoricoApiClient
    {
        private readonly HttpClient _client;
        private readonly HistoricoViewModel _viewModel;
        public ObterRegistroHistoricoApiClient(HistoricoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<List<RegistroHistoricoModel>> ObterRegistroHistoricoAsync(RegistroHistoricoModel registroHistoricoModel)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}unidade/registro_historico", registroHistoricoModel);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<List<RegistroHistoricoModel>>(content);
                    return detalhes ?? new List<RegistroHistoricoModel>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<RegistroHistoricoModel>();
                }
                else
                {
                    //var content = await response.Content.ReadAsStringAsync();
                    //throw new Exception($"Erro ao obter os dados: {content}");
                    return new List<RegistroHistoricoModel>();
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.SemInternet = true;
            }
            catch (UnauthorizedAccessException)
            {
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new LoginPage();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }

            return new List<RegistroHistoricoModel>();
        }

    }
}
