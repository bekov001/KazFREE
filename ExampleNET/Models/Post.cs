using System.ComponentModel.DataAnnotations;

namespace ExampleNET.Models;

public class Post
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    public string Desc { get; set; }
    public int UserId { get; set; }

    public DateTime Time { get; set; }
}