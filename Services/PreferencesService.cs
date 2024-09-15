using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Services
{
	public class PreferencesService : IPreferencesService
	{
		public async Task<string> GetIdUsuarioAsync()
		{
			return await SecureStorage.GetAsync("KeyIdUsuario") ?? string.Empty;
		}

		public async Task<string> GetIdEmpresaAsync()
		{
			return await SecureStorage.GetAsync("KeyIdEmpresa") ?? string.Empty;
		}

		public async Task<string> GetIdUnidadeAsync()
		{
			return await SecureStorage.GetAsync("KeyIdUnidade") ?? string.Empty;
		}

		public async Task<string> GetIdSetorAsync()
		{
			return await SecureStorage.GetAsync("KeyIdSetor") ?? string.Empty;
		}

		public async Task<string> GetIdPlanoAsync()
		{
			return await SecureStorage.GetAsync("KeyIdPlano") ?? string.Empty;
		}
	}
}
