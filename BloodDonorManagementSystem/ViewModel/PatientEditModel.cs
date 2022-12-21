using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodDonorManagementSystem.ViewModel
{
    public class PatientEditModel
    {
        public int PatientId { get; set; }
        [Required, StringLength(50), Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Admit Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AdmitDate { get; set; }
        [Column(TypeName = "date"), Display(Name = "End Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Required, Column(TypeName = "money"), Display(Name = "Payment Bill"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public Decimal PaymentBill { get; set; }
        public string CurrentPicture { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
    }
}