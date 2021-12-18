using KhareedLo.Models;
using KhareedLo.ViewModel.ReviewsAndComment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    public class ReviewsAndCommentsController : Controller
    {
        AppDbContext _ap;
        public ReviewsAndCommentsController(AppDbContext apdb)
        {
            _ap = apdb;
        }

        [HttpGet]
        public IActionResult GetReviews(int id)
        {
            var objj = _ap.ReviewsAndComments;

            UserClass.currentProdID = id;

            GetReviewsViewModel obj = new GetReviewsViewModel()
            {
                ReviewsAndComments = objj.ToList()
            };

            return View(obj);   
        }

        [HttpPost]
        public IActionResult GetReviews(ReviewsAndComment varr)
        {
            ReviewsAndComment obj = new ReviewsAndComment()
            {
                Comment = varr.Comment,
                Name = UserClass.name,
                ProductId = UserClass.currentProdID
            };

            _ap.ReviewsAndComments.Add(obj);

            _ap.SaveChanges();
           
            return View();
        }

        [HttpPost]
        public IActionResult DeleteReviews(int id)
        {
            bool success = false;

            var obj = _ap.ReviewsAndComments.FirstOrDefault(x => x.Id == id);

            if(obj != null)
            {
                _ap.Remove(obj);

                _ap.SaveChanges();

                success = true;
            }
            else
            {
                success = false;
            }

            if(success)
            {
                return RedirectToAction("GetReviews", "ReviewsAndComments");
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }

            
        }
    }
}
