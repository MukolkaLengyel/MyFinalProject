using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BitLink.UserInfoDao;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BitLink.Logic;

// our own encryption class
public static class LoginExtensions
{
    // hash password
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = sha256.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashedBytes);
    }

    public static async Task SignIn(HttpContext context, AdminDto dbAdmin)
    {
        await context.SignInAsync(
            new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new(ClaimTypes.Name, dbAdmin.Login),
                    new(ClaimTypes.Role, dbAdmin.Role),
                }, CookieAuthenticationDefaults.AuthenticationScheme)));
    }

    internal static Task SignIn(HttpContext httpContext, Admin dbAdmin)
    {
        throw new NotImplementedException();
    }
}