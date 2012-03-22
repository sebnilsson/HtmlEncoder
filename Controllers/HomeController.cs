using System.Web.Mvc;

namespace HtmlEncoder.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return RedirectToRoute("ShortDefault", new { action = "Encode" });
        }

        public ActionResult Encode() {
            return View();
        }

        public ActionResult Decode() {
            return View();
        }
    }
}
