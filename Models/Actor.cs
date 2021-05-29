using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace press_agency_asp_webapp.Models
{
    public  class Actor
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(450)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(11)]
        public string Phone { get; set; }
        
        public virtual UserType UserType { get; set; }
        
        [Required]
        public int UserTypeId { get; set; }
        
        public DateTime CreateDate { get; set; }

    }
}