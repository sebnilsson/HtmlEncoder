using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

using Newtonsoft.Json.Linq;

namespace HtmlEncoder.Controllers {
    public class EncodeController : BaseApiController {
        // GET /api/encode/?text=ABC
        [HttpPost]
        public string Get(JObject jsonData) {
            dynamic json = jsonData;
            string type = json.type;
            string text = json.text;

            string cacheKey = string.Format("encode_type='{0}'_text='{1}'", type, text);

            string encoded = GetCachedObject(cacheKey, () => {
                Func<string, string> func;
                if(!TypeFunctions.TryGetValue(type, out func)) {
                    func = TypeFunctions[DefaultType];
                }
                return func(text);
            });

            return encoded;
        }
        
        public static Dictionary<string, string> TypesList = new Dictionary<string, string> {
            { DefaultType, "HTML-encode with 'HttpUtility.HtmlEncode'" },
            { "Attribute", "Attribute-encode with 'HttpUtility.HtmlAttributeEncode'" },
            { "JS", "JavaScript string-encode with 'HttpUtility.JavaScriptStringEncode'" },
            { "Path", "URL Path-encode with 'HttpUtility.UrlPathEncode'" },
            { "URL", "URL-encode with 'HttpUtility.UrlEncode'" },
            { "URL-Unicode", "URL Unicode-encode with 'HttpUtility.UrlEncodeUnicode'" },
        };

        private const string DefaultType = "HTML";

        private static Dictionary<string, Func<string, string>> TypeFunctions = new Dictionary<string, Func<string, string>> {
            { DefaultType.ToLowerInvariant(), (text) => HttpUtility.HtmlEncode(text) },
            { "attribute", (text) => HttpUtility.HtmlAttributeEncode(text) },
            { "js", (text) => HttpUtility.JavaScriptStringEncode(text) },
            { "path", (text) => HttpUtility.UrlPathEncode(text) },
            { "url", (text) => HttpUtility.UrlEncode(text) },
            { "url-unicode", (text) => HttpUtility.UrlEncodeUnicode(text) },
        };
    }
}