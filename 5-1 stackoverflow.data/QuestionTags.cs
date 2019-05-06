using System;
using System.Collections.Generic;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public class QuestionTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}
