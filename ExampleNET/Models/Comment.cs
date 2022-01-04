
using System.ComponentModel.DataAnnotations;

namespace ExampleNET.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public int ArticleId { get; set; }

    public User Author { get; set; }
    public DateTime Time { get; set; }
    public string Text { get; set; }
}