using System;
using System.Collections.Generic;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionTags> QuestionTags { get; set; }
    }
}
