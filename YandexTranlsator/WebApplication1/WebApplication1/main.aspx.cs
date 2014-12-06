using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Model;

namespace WebApplication1
{
    public partial class main : System.Web.UI.Page
    {
        private ITranslatorService translatorService;
        private string langFrom { get; set; }
        private string langTo { get; set; }
        private string textToTranslate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            translatorService = new YandexTranslatorService("https://translate.yandex.net/api/v1.5/tr.json",
                "trnsl.1.1.20130824T081805Z.0f7033cdf8659e58.b401be91626a19e6f5ea549cc8f6021083d95e52");

            Dictionary<string, string> allLang = translatorService.GetAvailableLangs("uk");

            AddLanguageToControl(ddlFromLang, allLang);
        }

        private void AddLanguageToControl(DropDownList ddl, Dictionary<string, string> allLang)
        {
            foreach (var item in allLang)
            {
                ddl.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        protected void ddlFromLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            langFrom = ddl.SelectedValue;

            Dictionary<string, string> allLang = translatorService.GetAvailableLangs(langFrom);

            AddLanguageToControl(ddlToLang, allLang);
        }

        protected void ddlToLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            langTo = ddl.SelectedValue;
        }

        protected void Translate(object sender, EventArgs e)
        {
            textToTranslate = tbFrom.Text;
            string translated = translatorService.Translate(langFrom, textToTranslate, langTo);
            tbTo.Text = textToTranslate;
        }

       

    }
}