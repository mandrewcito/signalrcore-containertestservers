namespace SignalRSample.Models.Utils{
	using System.Collections.Generic;
	using System.Linq;
	using Entities;

	public class AppSettings
    {
        public string Secret { get; set; }
    }
        public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user) {
            user.Password = null;
            return user;
        }
    }
}