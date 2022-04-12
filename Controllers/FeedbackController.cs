using KhareedLo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Identity;
using KhareedLo.Auth;
using KhareedLo.Repositories.Interfaces;

namespace KhareedLo.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IGenericRepository<Feedbacks> _genRep;

        private UserManager<ApplicationUser> _userManager;

        public FeedbackController(UserManager<ApplicationUser> um, IGenericRepository<Feedbacks> genRep)
        {
            _userManager = um;

            _genRep = genRep;
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

                //_feedbackrepository.AddFeedback(f);

                _genRep.Insert(f);

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
            var fbss = _genRep.GetAll().OrderBy(p => p.Name);

            //_feedbackrepository.GetAllFeedbacks().OrderBy(p => p.Name);

            var obj = new DisplayFeedbackViewModel()
            {
                fbs = fbss.ToList()
            };

            return View(obj);
        }

    }
}
