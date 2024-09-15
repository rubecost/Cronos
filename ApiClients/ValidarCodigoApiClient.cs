using Newtonsoft.Json;
using Cronos.Models;
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
	public class ValidarCodigoApiClient
	{
		private readonly HttpClient _client;
        private readonly LoginViewModel _viewModel;

        public ValidarCodigoApiClient(LoginViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}
		public async Task<string> ValidarCodigoAsync(string email, string codigo)
		{
			try
			{
				var validarRequest = new { Email = email, Codigo = codigo };

				HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}login/validar_codigo", validarRequest);
				string content = await response.Content.ReadAsStringAsync();

				var result = JsonConvert.DeserializeObject<ApiResponse>(content);

				if (result != null && result.Token != null)
				{
					await SecureStorage.SetAsync("KeyToken", result.Token.Token ?? "");
					return result.Message ?? "Resposta sem mensagem.";
				}

				return "Resposta malformada.";
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

}
