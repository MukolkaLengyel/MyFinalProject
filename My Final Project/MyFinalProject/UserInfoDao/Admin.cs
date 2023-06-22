namespace BitLink.UserInfoDao;

public class Admin
{
    public Admin(string login, string pass, string role)
    {
        Login = login;
        Pass = pass;
        Role = role;
    }

    public Admin()
    {
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Login { get; set; }
    public string Pass { get; set; }
    public string Role { get; set; }

    [NotMapped] public string ReturnUrl { get; set; }
}