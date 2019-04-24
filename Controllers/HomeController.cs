using AspNetReact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace blog.AspNetReact.Controllers
{
    public class HomeController : Controller
    {
        TestDBDataContext _db = new TestDBDataContext();

        [ValidateInput(false)]
        public ActionResult Index()
        {
            //LogOnModel user = (LogOnModel)(Session["LogOnModel"]);            

            //if(user == null)
            //{
            //    return RedirectToAction("LogOn", "Logon");
            //}

            //ViewBag.Admin = user.Role == 'I' ? "Y" : user.Role == 'A' ? "Y" : "N";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GSC()
        {
            ViewBag.Message = "Loaded";

            return View();
            //return RedirectToAction("GSC", "Home");
        }

        public JsonResult GetGSCData()
        {
            using (TestDBDataContext dc = new TestDBDataContext())
            {
                List<ReturnGSCResult> data = new List<ReturnGSCResult>();
                data = dc.ReturnGSC().ToList();
                //return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}