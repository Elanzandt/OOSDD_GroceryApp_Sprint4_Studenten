namespace Grocery.Core.Models
{
    public partial class Client : Model
    {

        public Role ClientRole { get; set; } = Role.User;
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}