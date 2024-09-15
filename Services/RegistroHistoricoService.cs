using Cronos.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Services
{
    public class RegistroHistoricoService
    {
		private static readonly HttpClient _httpClient = new HttpClient();

		public static async Task<IEnumerable<RegistroHistoricoModel>> GetRegistroHistoricoAsync()
		{
			var response = await _httpClient.GetStringAsync("URL_DA_SUA_API");
			return JsonSerializer.Deserialize<IEnumerable<RegistroHistoricoModel>>(response);
		}
	}
}
