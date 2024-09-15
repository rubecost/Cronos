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
using Microsoft.Maui.ApplicationModel.Communication;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{
	public class ObterDetalhesApiClient
	{
		private readonly HttpClient _client;
        private readonly ConfiguracaoEmpresaViewModel _viewModel;
        public ObterDetalhesApiClient(ConfiguracaoEmpresaViewModel viewModel)
		{
			_client = new HttpClient();
            _viewModel = viewModel;
        }

		public async Task<UnidadesModel> ObterDetalhesEmpresaUnidadeAsync(UnidadesModel unidadesModel)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				// Envia a solicitação GET
				HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}unidade/detalhes?idEmpresa={unidadesModel.ID_Empresa}&idUnidade={unidadesModel.ID_Unidade}&idSetor={unidadesModel.ID_Setor}");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var detalhes = JsonConvert.DeserializeObject<UnidadesModel>(content);

					return detalhes ??= new UnidadesModel();
				}
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new UnidadesModel();
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

            return new UnidadesModel();
        }
	}
}
