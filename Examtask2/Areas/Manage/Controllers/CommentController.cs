using Examtask2.DAL;
using Examtask2.Models;
using Examtask2.Utilies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Examtask2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CommentController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Comments> comments = _context.comments.ToList();
            return View(comments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Comments comments)
        {
            if (!ModelState.IsValid) return View();
            bool IsExist=_context.comments.Any(t=>t.Title.ToLower().Trim()==comments.Title.ToLower().Trim());
            if(IsExist) return View();
            comments.Image = await comments.ImageUrl.SaveFileAsync(Path.Combine(_env.WebRootPath, "assets", "img"));
           await _context.comments.AddAsync(comments);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Comments comments = _context.comments.Find(id);
            if (comments == null) return NotFound();
            FileExtension.DeleteFile(Path.Combine(_env.WebRootPath, "assets", "img", comments.Image));
            _context.comments.Remove(comments);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Comments comments=_context.comments.Find(id);
            if (comments == null) return NotFound();
            return View(comments);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,Comments comments)
        {
            if (comments.Id != id) return BadRequest();
            Comments commentsitem = _context.comments.Find(id);
            if (commentsitem == null) return NotFound();
            if (comments.ImageUrl != null)
            {
                commentsitem.ImageUrl = comments.ImageUrl;
            }
            commentsitem.Name=comments.Name;
            commentsitem.Title=comments.Title;
            commentsitem.Email=comments.Email;
            commentsitem.Raiting=comments.Raiting;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
    }
}
