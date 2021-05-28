using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace press_agency_asp_webapp.Models
{
    public class Editor : Actor
    {
        [MaxLength(450)]
        [Index(IsUnique = true)]
        [Required]
        public string Username { get; set; }

        public ICollection<Post> Posts { get; set; }
        
    }
}