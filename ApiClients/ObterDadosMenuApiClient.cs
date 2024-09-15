using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cronos.Models;

namespace Cronos.ApiClients
{
    internal class ObterDadosMenuApiClient
    {
        private readonly HttpClient _client;

        public ObterDadosMenuApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<EmpresaComUsuarioModel> ObterDadosMenuApiClientAsync(int IdUsuario)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}personalizar/menu_superior?idUsuario={IdUsuario}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<EmpresaComUsuarioModel>(content);

                    return detalhes ??= new EmpresaComUsuarioModel();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    throw new Exception($"Erro ao obter os dados: {content}");
                }

            }
            catch (HttpRequestException ex)
            {

                throw new Exception($"Erro de rede: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
