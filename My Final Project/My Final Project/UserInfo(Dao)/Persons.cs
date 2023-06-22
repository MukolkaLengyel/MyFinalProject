namespace BitLink.Dao;

public class Persons
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Pass { get; set; }
    public string Role { get; set; }

    [NotMapped]
    public string ReturnUrl { get; set; }

    public Persons(string login, string pass, string role)
    {
        Login = login;
        Pass = pass;
        Role = role;
    }

    public Persons() { }

    public Persons(string returnUrl) => ReturnUrl = returnUrl;
}