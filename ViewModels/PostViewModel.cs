using press_agency_asp_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public InteractionViewModel Interaction { get; set; }
        public ICollection<QuestionViewModel> Qustions { get; set; }
        public bool IsSaved { get; set; } 

    }
}