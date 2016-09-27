using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using uxsoft.Share.Models.HomeViewModels;

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

        public IActionResult Index()
        {
            var vm = new IndexViewModel();

            var pwd = GetUserDirectory();
            if (!string.IsNullOrWhiteSpace(pwd))
            {
                vm.Files = Directory.EnumerateFiles(pwd);
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var uploads = GetUserDirectory();
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
            return NoContent();
        }
    }
}
