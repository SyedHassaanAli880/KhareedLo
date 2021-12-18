using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo.Models
{
    public interface IFeedbackRepository
    {
        void AddFeedback(Feedbacks feedback);

        IEnumerable<Feedbacks> GetAllFeedbacks();
    }
}
