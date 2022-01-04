using System.ComponentModel.DataAnnotations;
using ExampleNET.Models;

namespace ExampleNET.Forms;

public class PostViewModel
{
    public Post post { get; set; }
    public User user { get; set; }
    [Required(ErrorMessage = "Пустой комментарий не может быть опубликован")]
    public string Text { get; set; }

    public List<Comment> comments { get; set; }
}
