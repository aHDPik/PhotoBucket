using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoBucket.Entities;
using PhotoBucket.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBucket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PhotoContext context = new PhotoContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile myfile)
        {
            return View();
        }

        public IActionResult CreateUserPage()
        {
            return View();
        }
        private const string serverPath = @"D:\Sources\CSharp\PhotoBucket\PhotoBucket\wwwroot";
        [HttpPost]
        public async Task<IActionResult> CreateUserPage(UserModel model)
        {
            string newFileName = Path.Combine(serverPath, "Users", model.Avatar.FileName);
            using (FileStream fs = new FileStream(newFileName, FileMode.Create))
            {
                await model.Avatar.CopyToAsync(fs);
            }
            UserInfo newUser = new UserInfo()
            {
                Username = model.Username,
                Avatar = $"/Users/{model.Avatar.FileName}"
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> UserPage(int? id)
        {
            if (!id.HasValue)
                return Redirect("/Home/Index");
            UserInfo user = await context.Users.FindAsync(id.Value);
            return View(user);
        }

        public async Task<IActionResult> GetUserFile(int? id)
        {
            if (!id.HasValue)
                return Redirect("/Home/Index");
            UserInfo user = await context.Users.FindAsync(id.Value);
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine($"User Id = {user.Id}");
            writer.WriteLine($"User Name = {user.Username}");
            writer.WriteLine($"User Avatar = {user.Avatar}");
            writer.Close();
            return File(stream.ToArray(), "application/octet-stream", $"{user.Username}.txt");
        }
        [HttpPost]
        public async Task<IActionResult> AddPhoto(int? id, IFormFile[] photos)
        {
            if (!id.HasValue)
                return Redirect("/Home/Index");
            UserInfo user = await context.Users.FindAsync(id.Value);
            if (user == null)
                return Redirect("/Home/Index");
            string userDir = Path.Combine(serverPath, "Photos", user.Username);
            if (!Directory.Exists(userDir))
                Directory.CreateDirectory(userDir);
            foreach(IFormFile file in photos)
            {
                string fileName = Path.Combine(userDir, file.FileName);
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                UserPhoto newPhoto = new UserPhoto()
                {
                    Filename = file.FileName,
                    User = user
                };
                await context.Photos.AddAsync(newPhoto);
            }
            await context.SaveChangesAsync();
            return Redirect($"/Home/UserPage?id={id}");
        }
    }
}
