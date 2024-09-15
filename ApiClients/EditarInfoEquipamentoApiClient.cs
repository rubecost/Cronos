using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.ApiClients
{
	/*public class EditarInfoEquipamentoApiClient
	{
		private readonly HttpClient _client;

		public EditarInfoEquipamentoApiClient()
		{
			_client = new HttpClient();
		}

		public async Task<string> EditarInfoEquipamentoAsync(EquipamentoModel equipamentoModel)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				HttpResponseMessage response = await _client.PostAsJsonAsync($"{ApiSettingsService.BaseUrl}equipamento/editar", equipamentoModel);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ApiResponse>(content);
					return result?.Message ?? "Informações editadas com sucesso.";
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
				WeakReferenceMessenger.Default.Send(new MensageriaService("AvisoErroNet"));
				return "Erro de rede.";
			}
			catch (Exception)
			{
				return "Erro inesperado.";
			}
		}
	}*/
}
