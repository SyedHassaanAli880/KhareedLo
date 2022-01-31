using KhareedLo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace KhareedLo.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackrepository;

        private UserManager<IdentityUser> _userManager;

        public FeedbackController(IFeedbackRepository feedbackrepository, UserManager<IdentityUser> um)
        {
            _feedbackrepository = feedbackrepository;

            _userManager = um;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Feedbacks feedback)
        {
            string LoggedInUsername = this.User.FindFirstValue(ClaimTypes.Name);

            string LoggedInUseremail = this.User.FindFirstValue(ClaimTypes.Email);

            if (ModelState.IsValid)
            {
                Feedbacks f = new Feedbacks
                {
                    Name = LoggedInUsername,
                    Email = LoggedInUseremail,
                    Message = feedback.Message,
                    ContactMe = feedback.ContactMe
                };

                _feedbackrepository.AddFeedback(f);

                return RedirectToAction("FeedbackComplete");
            }
            return View(feedback);
        }

        public IActionResult FeedbackComplete()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DisplayFeedback()
        {
            var fbss = _feedbackrepository.GetAllFeedbacks().OrderBy(p => p.Name);

            var obj = new DisplayFeedbackViewModel()
            {
                fbs = fbss.ToList()
            };

            return View(obj);
        }

    }
}
