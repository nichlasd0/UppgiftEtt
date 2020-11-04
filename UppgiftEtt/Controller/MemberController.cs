using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using UppgiftEtt.Models;


namespace UppgiftEtt.Controller
{
    public class MemberController : SurfaceController
    {
        public ActionResult RenderLogin()
        {
            return PartialView("_Login", new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
                
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("","The Username or password provided is incorrect");
                }
            }

            return CurrentUmbracoPage();
        }

        public ActionResult SubmitLogout()
        {
            TempData.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToCurrentUmbracoPage();
        }
    }
}