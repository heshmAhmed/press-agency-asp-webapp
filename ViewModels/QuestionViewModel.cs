using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ViewerId { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public DateTime CreateDate { get; set; }
    }
}