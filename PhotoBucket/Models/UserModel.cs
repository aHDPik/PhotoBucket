using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBucket.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
