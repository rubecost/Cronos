using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.Models;
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
    internal class ObterEmailsDeNotificacoesApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoViewModel _viewModel;
        public ObterEmailsDeNotificacoesApiClient(ConfiguracaoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<List<EmailNotificacoesModel>> ObterEmailsDeNotificacoesAsync(int idUsuario)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}usuario/emails_notificacoes?idUsuario={idUsuario}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject <List<EmailNotificacoesModel>>(content);

                    return detalhes ??= new List<EmailNotificacoesModel>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<EmailNotificacoesModel>();
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

            return new List<EmailNotificacoesModel>();
        }
    }
}
