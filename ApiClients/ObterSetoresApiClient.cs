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
	public class ObterSetoresApiClient
	{
		private readonly HttpClient _client;
        private readonly ConfiguracaoEmpresaViewModel _viewModel;
        public ObterSetoresApiClient(ConfiguracaoEmpresaViewModel viewModel)
		{
			_client = new HttpClient();
            _viewModel = viewModel;
        }

		public async Task<List<string>> ObterSetoresAsync(int idEmpresa, int idUnidade)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				var url = $"{ApiSettingsService.BaseUrl}setor/setores?idEmpresa={idEmpresa}&idUnidade={idUnidade}";

				// Envia a solicitação GET
				HttpResponseMessage response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var setores = JsonConvert.DeserializeObject<List<string>>(content);

					return setores ?? new List<string>();
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();
					throw new Exception($"Erro ao obter setores: {content}");
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
	}
}
