using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Models
{
    public class FeedbackRepository: IFeedbackRepository
    {
        private readonly AppDbContext _appDbContext;
        public FeedbackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; 
        }

        public void AddFeedback(Feedbacks feedback)
        {
            _appDbContext.Feedbacks.Add(feedback);

            _appDbContext.SaveChanges();
        }

        public IEnumerable<Feedbacks> GetAllFeedbacks()
        {
            return _appDbContext.Feedbacks;
        }
    }
}
