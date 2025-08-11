using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Project
{
    public class Project
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string GitHubURL { get; set; }
        public string[] TechStack { get; set; }
    }
}