using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Platforms.Windows
{
	public class DateEntryBehavior : Behavior<Entry>
	{
		private bool _isUpdating; // Flag para evitar loops infinitos
		protected override void OnAttachedTo(Entry bindable)
		{
			base.OnAttachedTo(bindable);
			if (bindable != null)
			{
				bindable.TextChanged += OnTextChanged;
			}
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			if (bindable != null)
			{
				bindable.TextChanged -= OnTextChanged;
			}

			base.OnDetachingFrom(bindable);
		}

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var entry = sender as Entry;
			if (entry == null || _isUpdating)
				return;

			var text = entry.Text;

			if (string.IsNullOrWhiteSpace(text))
				return;

			// Remove any non-numeric characters except slashes
			text = text.Replace("/", "");

			if (text.Length > 8)
				text = text.Substring(0, 8);

			// Insert slashes
			if (text.Length > 4)
			{
				text = text.Insert(4, "/");
				text = text.Insert(2, "/");
			}
			else if (text.Length > 2)
			{
				text = text.Insert(2, "/");
			}

			// Atualizar o texto somente se ele for diferente
			if (entry.Text != text)
			{
				_isUpdating = true;
				entry.Text = text;
				_isUpdating = false;
			}
		}
	}
}
