using ExampleNET.Forms;
using ExampleNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleNET.Controllers;

public class PostController : Controller
{
    private ApplicationDbContext db;
    

    public PostController(ApplicationDbContext context)
    {
        db = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Posts()
    {
        var posts = await db.Posts.ToListAsync();
        posts.Reverse();
        return View(posts);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(int id)
    {
        var post = await db.Posts.FirstAsync(u => u.ID == id);
        var user = await db.MyUsers.FirstAsync(u => u.Id == post.UserId);
        var comment = db.Comments.Where(u => u.ArticleId == post.ID).ToList();
        var model = new PostViewModel { post = post, user = user, comments = comment };
        // dynamic model = new ExpandoObject();
        // model.post = post;
        // model.comments = comment;
        // model.user = user;

        return View(model);
    }

    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await db.Posts.FirstAsync(u => u.ID == id);
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return RedirectToAction("Posts", "Post");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateComment(int ID, PostViewModel model)
    {
        if (model.Text != null)
        {
            var post = await db.Posts.FirstOrDefaultAsync(u => u.ID == ID);
            var author = db.MyUsers.First(u => u.Email == User.Identity.Name);
            var comment = new Comment
            {
                ArticleId = post.ID,
                Author = author,
                Time = DateTime.UtcNow,
                Text = model.Text
            };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Post", new { id = ID });
    }
    
        
    [Authorize]
    public async Task<IActionResult> Create(Post model)
    {
        if (ModelState.IsValid)
        {
            var post = new Post
            {
                Title = model.Title,
                Desc = model.Desc,
                UserId = db.MyUsers.First(u => u.Email == User.Identity.Name).Id,
                Time = DateTime.UtcNow
            };
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Post", new { id = post.ID });
        }
        // ModelState.AddModelError(nameof(model.));/**/
        return View(model);
    }
}
