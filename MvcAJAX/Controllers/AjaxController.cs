using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcAJAX.Data;
using MvcAJAX.Models;
using MvcAJAX.ViewModels;

namespace MvcAJAX.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AjaxController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Search(string Title)
        {
            var movie = await _context.Movie.Where(m => m.Title!.Contains(Title)).ToListAsync();
            return Json(movie);
        }

        public IActionResult Movies()
        {
            return View();
        }

        public async Task<JsonResult> GetMovies()
        {
            var movie = await _context.Movie.ToListAsync();
            return Json(movie);
        }

        [HttpPost]
        public async Task<JsonResult> Create([Bind("Id","Title", "ReleaseDate", "Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return Json(movie);
            }
            return new JsonResult("Failed");
        }
    }
}
