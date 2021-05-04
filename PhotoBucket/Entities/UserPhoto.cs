using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBucket.Entities
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        virtual public UserInfo User { get; set; }
    }
}
