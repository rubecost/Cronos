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
    public class AtualizarEmailsAdicionaisNotificacaoApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoViewModel _viewModel;
        public AtualizarEmailsAdicionaisNotificacaoApiClient(ConfiguracaoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<string> AtualizarEmailsAdicionaisNotificacaoAsync(EmailsNotificacaoComUsuarioModel emailsnotificacaoModel)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}usuario/atualizar_emails_notificacoes", emailsnotificacaoModel);

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                if (response.IsSuccessStatusCode)
                {
                    return result?.Message ?? "Emails atualizados com sucesso.";
                }
                else
                {
                    return result?.Message ?? "Erro ao processar a resposta.";
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.SemInternet = true;
                return "Erro de rede. Verifique sua conexão e tente novamente.";
            }
            catch (Exception ex)
            {
                return $"Erro inesperado: {ex.Message}";
            }
        }

    }
}
