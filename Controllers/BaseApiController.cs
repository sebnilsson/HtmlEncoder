using System;
using System.Web;
using System.Web.Http;

namespace HtmlEncoder.Controllers {
    public abstract class BaseApiController : ApiController {
        internal T GetCachedObject<T>(string cacheKey, Func<T> getObjectFunc) {
            object cachedItem = HttpContext.Current.Cache[cacheKey];
            if(cachedItem != null && (cachedItem is T)) {
                return (T)cachedItem;
            }

            T fetchedObject = getObjectFunc();
            if(fetchedObject != null) {
            HttpContext.Current.Cache[cacheKey] = fetchedObject;
            }

            return fetchedObject;
        }
    }
}