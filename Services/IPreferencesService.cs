using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Services
{
	public interface IPreferencesService
	{
		Task<string> GetIdUsuarioAsync();
		Task<string> GetIdEmpresaAsync();
		Task<string> GetIdUnidadeAsync();
		Task<string> GetIdSetorAsync();
		Task<string> GetIdPlanoAsync();
	}
}
