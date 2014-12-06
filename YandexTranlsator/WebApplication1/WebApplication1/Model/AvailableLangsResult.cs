using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class AvailableLangsResult
    {
        public string[] Dirs { get; set; }
        public Dictionary<string, string> Langs { get; set; }
    }
}