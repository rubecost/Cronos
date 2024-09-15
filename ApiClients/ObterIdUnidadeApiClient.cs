using CommunityToolkit.Mvvm.Messaging;
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
	public class ObterIdUnidadeApiClient
	{
		private readonly HttpClient _client;
        private readonly ConfiguracaoEmpresaViewModel _viewModel;
        public ObterIdUnidadeApiClient(ConfiguracaoEmpresaViewModel viewModel)
		{
			_client = new HttpClient();
            _viewModel = viewModel;
        }

		public async Task ObterIdUnidadePorNomeAsync(int idEmpresa, string nomeUnidade)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var url = $"{ApiSettingsService.BaseUrl}unidade/id_unidade?idEmpresa={idEmpresa}&nomeUnidade={nomeUnidade}";

				HttpResponseMessage response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					
					var idUnidade = JsonConvert.DeserializeObject<int>(content);

					if(idUnidade != 0)
					{
						await SalvarIdUnidadeNoStorageAsync(idUnidade);
					}
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();
					throw new Exception($"Erro ao obter ID da unidade: {content}");
				}
			}
			catch (HttpRequestException ex)
			{
                _viewModel.SemInternet = true;
                throw new Exception($"Erro de rede: {ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro inesperado: {ex.Message}");
			}
		}
		private async Task SalvarIdUnidadeNoStorageAsync(int id)
		{
			await SecureStorage.SetAsync("KeyIdUnidade", id.ToString() ?? string.Empty);
		}
	}
}
