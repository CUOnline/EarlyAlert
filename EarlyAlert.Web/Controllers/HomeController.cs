using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EarlyAlert.Interface;
using System.Configuration;
using EarlyAlert.Web.ViewModel;
using EarlyAlert.Model;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;

namespace EarlyAlert.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseBll courseBll;
        private readonly IStudentBll studentBll;
        private readonly ITermBll termBll;
        private readonly IAccountBll accountBll;

        public HomeController(ICourseBll courseBll, IStudentBll studentBll, ITermBll termBll, IAccountBll accountBll)
        {
            this.courseBll = courseBll;
            this.studentBll = studentBll;
            this.termBll = termBll;
            this.accountBll = accountBll;
        }

        [HttpPost]
        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult("Canvas", Url.Action("ExternalLoginCallback", "Home"));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (await Authorized())
            {
                ViewBag.authorized = true;
                string id = "10430000000000016";
                ViewBag.score = ConfigurationManager.AppSettings["AlertScore"];
                var account = accountBll.GetAccount(id);
                var courses = courseBll.GetInitialCourses(ConfigurationManager.AppSettings["AlertScore"], account.Id);
                var terms = termBll.GetAllTerms();

                var term = new Term { Id = terms.First().Id };
                ViewBag.term = term.Id;

                CanvasViewModel canvas = new CanvasViewModel()
                {
                    Courses = courses,
                    Terms = terms,
                    CurrentTerm = term,
                    CurrentAccount = account,
                    CurrentScore = ConfigurationManager.AppSettings["AlertScore"],
                    Authorized = true
                };

                return View(canvas);
            }
            else
            {
                ViewBag.authorized = false;
                CanvasViewModel unauthorized = new CanvasViewModel();

                unauthorized.CurrentAccount = new Account() { Name = "Please Sign In." };
                unauthorized.CurrentTerm = new Term();
                unauthorized.Courses = new List<Courses>();
                unauthorized.Terms = new List<Term>();
                unauthorized.Authorized = false;

                return View(unauthorized);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(string termId, string score, string accountId)
        {
            if (score.IsNullOrWhiteSpace())
            {
                score = ConfigurationManager.AppSettings["AlertScore"];
            }

            var account = accountBll.GetAccount(accountId);
            var courses = courseBll.GetCourses(termId,score,accountId);
            var terms = termBll.GetAllTerms();

            var term = new Term {Id = termId};

            CanvasViewModel canvas = new CanvasViewModel()
            {
                Courses = courses,
                Terms = terms,
                CurrentTerm = term,
                CurrentAccount = account,
                Authorized = await Authorized()
            };
            
            return View(canvas);
        }

        [HttpGet]
        public ActionResult GetStudents(string id, string score, string accountId)
        {
            var canvas = new CanvasViewModel();
            
            if (score.IsNullOrWhiteSpace())
            {
                score = ConfigurationManager.AppSettings["AlertScore"];
            }

            var model = studentBll.GetStudentsforCourse(id, score, accountId);
            canvas.Students = model;

            return PartialView("_StudentsView", canvas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogout(string provider)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut("ExternalCookie");

            return RedirectToAction("Index");
        }

        private async Task<bool> Authorized()
        {
            List<string> authorizedRoles = new List<string>()
            {
                "Account Admin",
                "Enrollment Manager",
                "Help Desk",
                "Outcomes Admin",
                "Sub-Account Admin"
            };

            var authenticateResult = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie");
            if (authenticateResult != null)
            {
                var tokenClaim = authenticateResult.Identity.Claims.Where(cl => cl.Type == ClaimTypes.Role && authorizedRoles.Contains(cl.Value)).Any();// .FirstOrDefault(claim => claim.Type == "urn:token:github");
                if (tokenClaim)
                {
                    return true;
                }
            }

            return false;
        }

        // Used for XSRF protection when adding external logns
        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}