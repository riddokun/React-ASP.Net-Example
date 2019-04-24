using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using AspNetReact.Models;

namespace HRG.NATripPass.Management.Views.LogOn
{
    public class LogOnController : Controller
    {
        TestDBDataContext _cxt = new TestDBDataContext();

        public ActionResult LogOn()
        {
            LogOnModel model = new LogOnModel();
            try
            {
                if (Request.Cookies["HRGGBTWeb"] != null)
                {
                    model.UserName = Request.Cookies["HRGGBTWeb"].Values["User"];
                    model.Password = Request.Cookies["HRGGBTWeb"].Values["Pwd"];
                    model.RememberMe = true;

                    model = model.GetUser(model, true);
                    if (model != null)
                    {
                        PrepareUserSession(model);
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        return RedirectToAction("Index", "AccountMng");
                    }
                    else { ModelState.AddModelError("", "Login data is incorrect!"); }

                }
            }
            catch { }
            return View();
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            TempData["msg"] = "<script>alert('Test');</script>";
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model = model.GetUser(model, false);
                if (model != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    PrepareUserSession(model);
                    ViewBag.Admin = model.Role == 'I' ? "Y"
                        : model.Role == 'A' ? "Y" : "N";

                    try
                    {
                        if (model.RememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("HRGGBTWeb");
                            cookie.Values.Add("User", model.UserName);
                            cookie.Values.Add("Pwd", model.Password);
                            cookie.Expires = DateTime.Now.AddMonths(3);
                            Response.Cookies.Add(cookie);
                        }
                    }
                    catch { }

                    if (ViewBag.Admin == "Y")
                    {
                        return RedirectToAction("Index", "AccountMng");
                    }

                    return RedirectToAction("Search", "ApprovalSearch");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateInput(true)]//, ValidateInput(false)]
        public ActionResult Register(RegisterViewModel model, FormCollection frmcol) //string returnUrl
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebUser _wu = new WebUser();
                    _wu = _cxt.WebUsers.Where(s => s.UserID == model.Username).SingleOrDefault();

                    //check to avoid duplicate Logins
                    if (_wu != null)
                    {
                        RegisterViewModel log = new RegisterViewModel();
                        ModelState.AddModelError("", "This Username is already taken. Please choose another!");
                        return View(log);
                    }
                    else
                    {
                        //Encyption
                        LogOnModel log = new LogOnModel();
                        model.Password = log.Encode(model.Password);

                        ///Insert into WebUsers, Assign WebUsers to Account based off of domain and
                        ///defaults Role and Active
                        _cxt.uspInsertWebUser(model.Username.ToString(), model.Email, model.Password);

                        //Document user account being created
                        //Create logging for the website                    
                        return RedirectToAction("LogOn", "LogOn");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Something went wrong, please try again!");
                    return RedirectToAction("Register", "LogOn");
                }
            }
            else
            {
                ModelState.AddModelError("","");
                return View(model);
            }
        }

        public JsonResult EmailExists(string Email)
        {
            string eDomain = Email.Substring(Email.IndexOf("@")).ToUpper();
            string[] recommendedDomains = { "@HRGWORLDWIDE.COM", "@HRGTEC.COM", "@AMEXGBT.COM" };
            var message = "Does Not Exists";
            foreach (string recEmail in recommendedDomains)
            {
                if (eDomain.Contains(recEmail))
                {
                    message = "Exists";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        private void PrepareUserSession(LogOnModel model)
        {
            Session["LogOnModel"] = new LogOnModel()
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role
            };

            /* Logging the user action
             * try
            {
                AspNetReact.Models.LogOnModel usermng = (AspNetReact.Models.LogOnModel)(Session["LogOnModel"]);
                _cmn.InsertLoggingIntoTable(0, "WebLogon", "LOW", "",
                                "Following User Logged into site: " + usermng.UserName
                                , false);
            }
            catch { }*/
        }

        public ActionResult LogOnAfterLogOut()
        {
            return View("LogOn");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn"); //Redirect("LogOnAfterLogOut");
        }

    }
}
