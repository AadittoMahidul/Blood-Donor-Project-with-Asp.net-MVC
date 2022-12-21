using BloodDonorManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodDonorManagementSystem.ViewModel
{
    public class DonorInputModel
    {
        public int DonorId { get; set; }
        [Required, StringLength(50), Display(Name = "Donor Name")]
        public string DonorName { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Donation Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DonationDate { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
        [Required, Display(Name = "Blood Id")]
        public int BloodId { get; set; }
    }   
}