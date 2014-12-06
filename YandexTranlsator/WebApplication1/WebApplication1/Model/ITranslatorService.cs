using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    interface ITranslatorService
    {
        string Translate(string sourceLang, string sourceText, string targetLanguage);
        Dictionary<string, string> GetAvailableLangs(string lang);
        List<Tuple<string, string>> GetTranslateDirs();

        string DetectLang(string[] text);
    }
}
