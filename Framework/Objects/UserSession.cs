using Framework.Enums;

namespace Framework.Models
{
	public class UserSession
	{
		public Guid Guid { get; set; }
		public SystemRole SystemRole { get; set; } = SystemRole.User;
	}
}
