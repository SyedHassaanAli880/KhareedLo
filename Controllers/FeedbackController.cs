using KhareedLo.Models;
using KhareedLo.Repositories.Interfaces;
using KhareedLo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhareedLo.ViewModel;

namespace KhareedLo.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackrepository;

        
        public FeedbackController(IFeedbackRepository feedbackrepository)
        {
            _feedbackrepository = feedbackrepository;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Feedbacks feedback)
        {
            if(ModelState.IsValid)
            {
                _feedbackrepository.AddFeedback(feedback);

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
