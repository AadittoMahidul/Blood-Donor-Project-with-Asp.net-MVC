using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonorManagementSystem.ViewModel
{
    public class HospitalInputModel
    {
        public int HospitalId { get; set; }
        [Required, StringLength(50), Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }
        [Required, Display(Name = "Hospital Picture")]
        public HttpPostedFileBase Picture { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Location { get; set; }
        //[Required, Display(Name = "Patient Id")]
        //public int PatientId { get; set; }
        public List<PatientViewModel> Patients { get; set; } = new List<PatientViewModel>();
    }
}