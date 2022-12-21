using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonorManagementSystem.ViewModel
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Admit Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AdmitDate { get; set; }
        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Payment Bill"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public Decimal PaymentBill { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Total Days")]
        public int TotalDays
        {
            get
            {
                return EndDate.HasValue ? (EndDate.Value - AdmitDate).Days + 1 : 0;
            }
        }
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Payment
        {
            get
            {
                return this.TotalDays * PaymentBill;
            }
        }
    }
}