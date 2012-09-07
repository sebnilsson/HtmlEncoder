using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

using Newtonsoft.Json.Linq;

namespace HtmlEncoder.Controllers {
    public class DecodeController : BaseApiController {
        // GET /api/decode/?text=ABC
        [HttpPost]
        public string Get(JObject jsonData) {
            dynamic json = jsonData;
            string type = json.type;
            string text = json.text;

            string cacheKey = string.Format("type='{0}'_text='{1}'", type, text);

            string decoded = GetCachedObject(cacheKey, () => {
                Func<string, string> func;
                if(!TypeFunctions.TryGetValue(type, out func)) {
                    func = TypeFunctions[DefaultType];
                }
                return func(text);
            });

            return decoded;
        }

        public static Dictionary<string, string> TypesList = new Dictionary<string, string> {
            { DefaultType, "HTML-decode with 'HttpUtility.HtmlDecode'" },
            { "URL", "URL-decode with 'HttpUtility.UrlDecode'" },
        };

        private const string DefaultType = "HTML";

        private static Dictionary<string, Func<string, string>> TypeFunctions = new Dictionary<string, Func<string, string>> {
            { DefaultType.ToLowerInvariant(), (text) => HttpUtility.HtmlDecode(text) },
            { "url", (text) => HttpUtility.UrlDecode(text) },
        };
    }
}