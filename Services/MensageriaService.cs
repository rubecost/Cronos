using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cronos.Services
{
	public class MensageriaService : ValueChangedMessage<string>
	{
		public MensageriaService(string value) : base(value)
		{

		}
	}
}
