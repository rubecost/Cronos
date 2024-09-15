using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cronos.Models;
using Cronos.ViewModels;
using Cronos.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace Cronos.ApiClients
{
	public class CriarEquipamentoApiClient
	{
		private readonly HttpClient _client;
		private readonly InserirInformacaoEqViewModel _viewModel;
        public CriarEquipamentoApiClient(InserirInformacaoEqViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}

		public async Task<string> CriarEquipamentoAsync(EquipamentoModel equipamentoModel)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}equipamento/criar", equipamentoModel);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ApiResponse>(content);
					return result?.Message ?? "Equipamento criado com sucesso.";
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
				_viewModel.SemInternet = true;
				return "Erro de rede.";
			}
			catch (Exception)
			{
				return "Erro inesperado.";
			}
		}
	}
}
