using Newtonsoft.Json;
using Cronos.Models;
using Cronos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ViewModels;

namespace Cronos.ApiClients
{
	public class ObterInformacoesUsuarioApiClient
	{
		private readonly HttpClient _client;
		private readonly LoginViewModel _viewModel;
        public ObterInformacoesUsuarioApiClient(LoginViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}

		public async Task<string> ObterInformacoesUsuarioAsync(string email)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}login/informacoes?email={Uri.EscapeDataString(email)}");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var listaUsuarioInformacoes = JsonConvert.DeserializeObject<List<UsuarioInformacoesModel>>(content);
					var informacaoUsuario = listaUsuarioInformacoes?.FirstOrDefault();
					
					if (informacaoUsuario != null)
					{
						await SalvarInformacoesNoStorageAsync(informacaoUsuario);
					}

					return "Informações obtidas e armazenadas com sucesso.";
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
			catch (Exception ex)
			{
				return $"Erro inesperado: {ex.Message}";
			}
		}

		private async Task SalvarInformacoesNoStorageAsync(UsuarioInformacoesModel informacoes)
		{
			await SecureStorage.SetAsync("KeyIdUsuario", informacoes.ID_Usuario.ToString() ?? string.Empty);
			await SecureStorage.SetAsync("KeyIdEmpresa", informacoes.ID_Empresa.ToString() ?? string.Empty);
			await SecureStorage.SetAsync("KeyIdUnidade", informacoes.ID_Unidade.ToString() ?? string.Empty);
			await SecureStorage.SetAsync("KeyIdSetor", informacoes.ID_Setor?.ToString() ?? string.Empty);
			await SecureStorage.SetAsync("KeyIdPlano", informacoes.ID_Plano.ToString() ?? string.Empty);
		}
	}
}
