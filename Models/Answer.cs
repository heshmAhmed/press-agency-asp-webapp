using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Create_date { get; set; }
    }
}