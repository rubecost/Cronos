using CommunityToolkit.Mvvm.Messaging;
using Cronos.Models;
using Cronos.Services;
using Newtonsoft.Json;
using Cronos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.ApiClients
{
	public class ObterNomeEmpresaUnidadeApiClient
	{
		private readonly HttpClient _client;
		private readonly InserirInformacaoEqViewModel _viewModel;
        public ObterNomeEmpresaUnidadeApiClient(InserirInformacaoEqViewModel viewModel)
		{
			_client = new HttpClient();
			_viewModel = viewModel;
		}

		public async Task<EquipamentoModel> ObterNomeEmpresaUnidadeAsync(EquipamentoModel equipamentoModel)
		{
			try
			{
				var token = await SecureStorage.GetAsync("KeyToken");

				if (string.IsNullOrEmpty(token))
				{
					throw new UnauthorizedAccessException("Usuário não autenticado.");
				}

				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				// Envia a solicitação GET
				HttpResponseMessage response = await _client.GetAsync($"{ApiSettingsService.BaseUrl}equipamento/dados_empresa?idEmpresa={equipamentoModel.ID_Empresa}&idUnidade={equipamentoModel.ID_Unidade}&idSetor={equipamentoModel.ID_Setor}");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var detalhes = JsonConvert.DeserializeObject<EquipamentoModel>(content);

					return detalhes ??= new EquipamentoModel();
				}
				else
				{
					var content = await response.Content.ReadAsStringAsync();

					throw new Exception($"Erro ao obter nome da empresa, unidade e setor: {content}");
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
