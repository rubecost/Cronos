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
using Cronos.ViewModels;


namespace Cronos.ApiClients
{
    internal class ObterDadosUsuarioApiClient
    {
        private readonly HttpClient _client;
        private readonly ConfiguracaoViewModel _viewModel;

        public ObterDadosUsuarioApiClient(ConfiguracaoViewModel viewModel)
        {
            _client = new HttpClient();
            _viewModel = viewModel;
        }

        public async Task<UsuarioComTelefonesModel> ObterDadosUsuarioAsync(int IdUsuario)
        {
            try
            {
                var token = await SecureStorage.GetAsync("KeyToken");

                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}usuario/dados_usuario?idUsuario={IdUsuario}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var detalhes = JsonConvert.DeserializeObject<UsuarioComTelefonesModel>(content);

                    return detalhes ??= new UsuarioComTelefonesModel();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new UsuarioComTelefonesModel();
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.SemInternet = true;
              // Sem internet popup
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex.Message}");
            }

            return new UsuarioComTelefonesModel();
        }
    }
}
