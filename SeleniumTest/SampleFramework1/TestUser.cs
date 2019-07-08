namespace SampleFramework1
{
    public class TestUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserGender Gender { get; set; }
    }

    public enum UserGender
    {
        Male,
        Female,
        Other
    }
}