using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class Interaction
    {
        public int Id { get; set; }
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public int ViewerId { get; set; }
        public virtual Viewer Viewer { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreateDate { get; set; }

    }
}