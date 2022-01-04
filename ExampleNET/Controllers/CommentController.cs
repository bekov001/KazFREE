using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleNET.Models;

namespace ExampleNET.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db;
        public CommentController(ApplicationDbContext context)
        {
            db = context;
        }
        // [HttpPost]
        // public async Task<IActionResult> CreateComment(int ID, Comment model)
        // {
        //     var a = await db.MyUsers.FirstAsync();
        //     return View();
        // }
    }
}