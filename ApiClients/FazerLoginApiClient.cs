using Cronos.Views;
using Newtonsoft.Json;
using System;
using Cronos.Services;
using Cronos.ApiClients;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{
	public class FazerLoginApiClient
	{
		private readonly HttpClient _client;
        private readonly LoginViewModel _viewModel;

        public FazerLoginApiClient(LoginViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}
		public async Task<string> FazerLoginAsync(string email, string senha)
		{
			try
			{
				var loginRequest = new { Email = email, Senha = senha };

				var response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}login/autenticacao", loginRequest);

				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();

					var result = JsonConvert.DeserializeObject<ApiResponse>(content);

					if (result != null && result.Token != null)
					{
						await SecureStorage.SetAsync("KeyToken", result.Token.Token ?? "");

						return result.Message ?? "Resposta sem mensagem.";
					}

					return "Resposta malformada.";
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
				return ""; 
			}
			catch (Exception)
			{
				return "Erro desconhecido tente novamente!";
			}
		}
	}
}