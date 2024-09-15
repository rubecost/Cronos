using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Bases
{
	public partial class ValidatableObservableObject : ObservableObject
	{
		protected bool ValidateProperty<T>(T value, [CallerMemberName] string? propertyName = null)
		{
			var validationContext = new ValidationContext(this)
			{
				MemberName = propertyName
			};

			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateProperty(value, validationContext, validationResults);

			if (!isValid)
			{
				foreach (var validationResult in validationResults)
				{
					// Exibir mensagem de erro
					Shell.Current.DisplayAlert("Erro de Validação", validationResult.ErrorMessage, "OK");
				}
			}

			return isValid;
		}
	}
}
