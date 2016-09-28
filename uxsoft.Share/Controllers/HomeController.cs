using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using uxsoft.Share.Models.HomeViewModels;
using Microsoft.AspNetCore.Authorization;

namespace uxsoft.Share.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHostingEnvironment environment)
        {
            Hosting = environment;
        }

        private IHostingEnvironment Hosting { get; set; }
        private string GetUserDirectory()
        {
            var name = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var path = Path.Combine(Hosting.WebRootPath, "uploads", name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var vm = new IndexViewModel();

            var pwd = GetUserDirectory();
            if (!string.IsNullOrWhiteSpace(pwd))
            {
                vm.Files = Directory.EnumerateFiles(pwd).Select(f => Path.GetFileName(f));
            }
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var uploads = GetUserDirectory();
            if (!string.IsNullOrWhiteSpace(uploads))
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
                return Json(new { Message = "Success" });
            }
            else return StatusCode(400);
        }

        [Authorize]
        public IActionResult DeleteFile(string id)
        {
            var uploads = GetUserDirectory();
            System.IO.File.Delete(Path.Combine(uploads, id));
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Download(string id)
        {
            var uploads = GetUserDirectory();
            if (string.IsNullOrWhiteSpace(uploads))
                return NoContent();

            var file = Path.Combine(uploads, id);
            if (!System.IO.File.Exists(file))
                return NoContent();


            return File(new FileStream(file, FileMode.Open), "application/octet-stream");
        }
    }
}
