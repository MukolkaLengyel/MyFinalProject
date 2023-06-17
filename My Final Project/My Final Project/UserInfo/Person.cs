using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BitLink.Dao;

public sealed record Person : EntityWithId
{
    //public int Id { get; set; }

    [Required(ErrorMessage = "First Name is Required!")]
    [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long!")]
    [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters!")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is Required!")]
    [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters!")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is Required!")]
    [Range(10, 100, ErrorMessage = "Age must be between 10 and 100")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Gender is Required!")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Username is Required!")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long!")]
    [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is Required!")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$")]
    [StringLength(200, ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
    public string? Password { get; set; }

    public Person() { }

    public Person(int id, string firstName, string lastName, int age, string gender, string username, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Username = username;
        Password = password;
    }
}