using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Newtonsoft.Json;
using Cronos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Cronos.Models;

namespace Cronos.ApiClients
{
	public class SalvarImagensRelatoriosApiClient
	{
		private readonly HttpClient _client;
		private readonly InserirInformacaoEqViewModel _viewModel;
        public SalvarImagensRelatoriosApiClient(InserirInformacaoEqViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}

		public async Task<string> CriarImagensRelatoriosAsync(ImagensRelatoriosModel imagensrelatoriosModel)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}equipamento/salvar_arquivos", imagensrelatoriosModel);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ApiResponse>(content);
					return result?.Message ?? "Arquivos salvos com sucesso.";
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
