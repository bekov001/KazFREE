using System.ComponentModel.DataAnnotations;

namespace ExampleNET.Forms;

public class UserViewModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [Required(ErrorMessage = "Please choose profile image")]
    [Display(Name = "Profile Picture")]
    public IFormFile ProfileImage { get; set; }

    public string ProfilePicture { get; set; }
}