using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBucket.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public virtual List<UserPhoto> Photos { get; set; }
    }
}
