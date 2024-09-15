using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cronos.Services
{
	public static class EmailValidadorService
	{
		private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return false;
			}

			return EmailRegex.IsMatch(email);
		}
	}
}
