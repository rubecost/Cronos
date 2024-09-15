using CommunityToolkit.Mvvm.Messaging;
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
	public class ObterIdSetorApiClient
	{
		private readonly HttpClient _client;
        private readonly ConfiguracaoEmpresaViewModel _viewModel;
        public ObterIdSetorApiClient(ConfiguracaoEmpresaViewModel viewModel)
		{
			_client = new HttpClient();
            _viewModel = viewModel;
        }

		public async Task ObterIdSetorPorNomeAsync(int idEmpresa, int idUnidade, string nomeSetor)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var url = $"{ApiSettingsService.BaseUrl}setor/id_setor?idEmpresa={idEmpresa}&idUnidade={idUnidade}&nomeSetor={nomeSetor}";

				HttpResponseMessage response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var idSetor = JsonConvert.DeserializeObject<int>(content);

					if (idSetor != 0)
					{
						await SalvarIdSetorNoStorageAsync(idSetor);
					}
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();
					throw new Exception($"Erro ao obter ID do Setor: {content}");
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
		private async Task SalvarIdSetorNoStorageAsync(int id)
		{
			await SecureStorage.SetAsync("KeyIdSetor", id.ToString() ?? string.Empty);
		}
	}
}
