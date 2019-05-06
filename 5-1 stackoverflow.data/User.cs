using System;
using System.Collections.Generic;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Likes> Likes { get; set; }
    }
}
