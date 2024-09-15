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
    public class AtualizarDadosUsuarioApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoViewModel _viewModel;
        public AtualizarDadosUsuarioApiClient(ConfiguracaoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<string> AtualizarDadosUsuarioAsync(UsuarioComTelefonesModel atualizarDadosUsuarioModel)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}usuario/atualizar_informacoes_usuario", atualizarDadosUsuarioModel);

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                if (response.IsSuccessStatusCode)
                {
                    return result?.Message ?? "Informações atualizadas com sucesso.";
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
