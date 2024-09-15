using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cronos.Models;

namespace Cronos.Services
{
	public class EquipamentoService 
	{

		private static readonly HttpClient _httpClient = new HttpClient();

		/*public static async Task<IEnumerable<EquipamentoModel>> GetEquipamentosAsync()
		{
			var response = await _httpClient.GetStringAsync("URL_DA_SUA_API");
			return JsonSerializer.Deserialize<IEnumerable<EquipamentoModel>>(response);
		}*/
	}
}
