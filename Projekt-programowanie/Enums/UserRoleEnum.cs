namespace Projekt_programowanie.Enums
{
    public enum UserRole
    {
        Teacher = 1,
        Student = 2,
        Admin = 3
    }

	public class UserRoleItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public static class UserRoleGetter
	{
		public static List<UserRoleItem> GetList()
		{
			return new List<UserRoleItem>
			{
				new UserRoleItem { Id = 1, Name = "Admin" },
				new UserRoleItem { Id = 2, Name = "User" },
				new UserRoleItem { Id = 3, Name = "Guest" }
			};
		}
	}
}
