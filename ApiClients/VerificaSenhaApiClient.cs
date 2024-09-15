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
    public class VerificaSenhaApiClient
    {
        private readonly HttpClient _client;
        public VerificaSenhaApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> VerificarSenhaAsync(int idUsuario, string senha)
        {
            try
            {
                var verificarRequest = new { ID_Usuario = idUsuario, Senha = senha };

                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}login/verifica_senha", verificarRequest);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);
                    return result?.Message ?? "Resposta sem mensagem.";
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);
                    return result?.Message ?? "Erro ao processar a resposta.";
                }

            }
            catch (HttpRequestException)
            {
                throw new Exception($"Erro de rede.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
