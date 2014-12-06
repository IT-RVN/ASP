using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Model
{
    public class TranslateResult
    {
        public string Code { get; set; }
        public string Lang { get; set; }
        public string[] Text { get; set; }
    }
}