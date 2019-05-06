using _5_1_stackoverflow.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_1_stackoverflow.web.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public bool LoggedIn { get; set; }
        public bool AlreadyLiked { get; set; }
    }
}
