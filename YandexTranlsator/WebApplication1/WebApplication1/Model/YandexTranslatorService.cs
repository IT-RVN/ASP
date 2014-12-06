using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace WebApplication1.Model
{
    public class YandexTranslatorService : ITranslatorService
    {
        private const string TRANSLATE_METHOD = "translate";
        private const string GET_LANGS_METHOD = "getLangs";

        private readonly string _baseUrl;
        private readonly string _key;

        #region Constructors

        public YandexTranslatorService(string baseUrl, string key)
        {
            this._baseUrl = baseUrl;
            this._key = key;
        }

        #endregion

        #region ITranslatorService Members

        public string Translate(string sourceLang, string sourceText, string targetLanguage)
        {
            // Add query string parameters            
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty); // HttpUtility class belongs to System.Web assembly
            queryString["key"] = this._key;
            queryString["lang"] = string.Format("{0}-{1}", sourceLang, targetLanguage);
            queryString["text"] = sourceText;

            // Build request Uri
            UriBuilder uriBuilder = new UriBuilder(this._baseUrl + "/" + TRANSLATE_METHOD);
            uriBuilder.Query = queryString.ToString();


            HttpClient client = new HttpClient();

            // Send GET request
            Task<HttpResponseMessage> task = client.GetAsync(uriBuilder.ToString());
            HttpResponseMessage message = task.Result;

            //// Get content from the request
            //Task<string> contentTask = message.Content.ReadAsStringAsync();
            //string content = contentTask.Result;

            // Parse content as JSON
            Task<TranslateResult> jsonTask = message.Content.ReadAsAsync<TranslateResult>(); // Needs Newtonsoft.Json assembly
            TranslateResult jsonResult = jsonTask.Result;

            if (jsonResult.Code != "200")
            {
                throw new Exception(string.Format("Translation from {0} to {1} returned and error with code {2}", sourceLang, targetLanguage, jsonResult.Code));
            }

            return string.Join(@"\n", jsonResult.Text);
        }

        public Dictionary<string, string> GetAvailableLangs(string lang)
        {
            // Add query string parameters            
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty); // HttpUtility class belongs to System.Web assembly
            queryString["key"] = this._key;
            queryString["ui"] = lang;

            UriBuilder uriBuilder = new UriBuilder(this._baseUrl + "/" + GET_LANGS_METHOD);
            uriBuilder.Query = queryString.ToString();


            HttpClient client = new HttpClient();

            // Send GET request
            Task<HttpResponseMessage> task = client.GetAsync(uriBuilder.ToString());
            HttpResponseMessage message = task.Result;

            // Get content from the request
            Task<AvailableLangsResult> contentTask = message.Content.ReadAsAsync<AvailableLangsResult>();
            AvailableLangsResult content = contentTask.Result;

            return content.Langs;

        }

        public List<Tuple<string, string>> GetTranslateDirs()
        {
            // Add query string parameters            
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty); // HttpUtility class belongs to System.Web assembly
            queryString["key"] = this._key;

            UriBuilder uriBuilder = new UriBuilder(this._baseUrl + "/" + GET_LANGS_METHOD);
            uriBuilder.Query = queryString.ToString();


            HttpClient client = new HttpClient();

            // Send GET request
            Task<HttpResponseMessage> task = client.GetAsync(uriBuilder.ToString());
            HttpResponseMessage message = task.Result;

            // Get content from the request
            Task<AvailableLangsResult> contentTask = message.Content.ReadAsAsync<AvailableLangsResult>();
            AvailableLangsResult content = contentTask.Result;

            var dirs = content.Dirs.Select(r =>
            {
                string[] s = r.Split('-');
                return Tuple.Create<string, string>(s[0], s[1]);
            })
                .ToList();
            return dirs;
        }

        public string DetectLang(string[] text)
        {
            throw new NotImplementedException();
        }

        #endregion ITranslatorService Members
    }
}