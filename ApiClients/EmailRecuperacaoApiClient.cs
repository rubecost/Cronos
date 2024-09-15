using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Cronos.Services;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{
	public class EmailRecuperacaoApiClient
	{
		private readonly HttpClient _client;
        private readonly LoginViewModel _viewModel;

        public EmailRecuperacaoApiClient(LoginViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}

		public async Task<string> RecuperarSenhaAsync(string email)
		{
			try
			{
				var emailRequest = new { Email = email };

				var response = await _client.PostAsync($"{ApiSettingsService.BaseUrl}login/email_recuperacao", JsonContent.Create(emailRequest));

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ApiResponse>(content);
					return result?.Message ?? "Erro ao processar a resposta.";
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
				return "Erro inesperado";
			}
		}
	}
	public class ApiResponse
	{
		public string? Message { get; set; }
		public TokenResponse? Token { get; set; }
	}
	public class TokenResponse
	{
		public string? Token { get; set; }
	}
}
