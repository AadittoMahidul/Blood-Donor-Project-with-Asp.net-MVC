using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BloodDonorManagementSystem.Models
{
    public enum BloodGroup { A_Positive = 1, B_Positive, AB_Positive, AB_Negative, O_Positive }
    public class Blood
    {      
        public int BloodId { get; set; }
        [EnumDataType(typeof(BloodGroup)), Display(Name = "Blood Group")]
        public BloodGroup BloodGroup { get; set; }
        [Required, StringLength(200), Display(Name = "Blood Details")]
        public string BloodDetails { get; set; }
        public ICollection<Donor> Donors { get; set; } = new List<Donor>();
    }
    public class Donor
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
        [Required, StringLength(150), Display(Name = "Picture")]       
        public string Picture { get; set; }
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
        [Required]
        public int BloodId { get; set; }
        [ForeignKey("BloodId")]
        public Blood Blood { get; set; }
        public ICollection<PatientDonor> PatientDonors { get; set; } = new List<PatientDonor>();
    }
    public class Patient
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
        [Required, StringLength(150)]
        public string Picture { get; set; }
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
        public ICollection<PatientDonor> PatientDonors { get; set; } = new List<PatientDonor>();
        public ICollection<HospitalPatient> HospitalPatients { get; set; } = new List<HospitalPatient>();
        //public ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
    }
    public class PatientDonor
    {
        [Key,]
        public int DonationId { get; set; }
        [Required]
        public int DonorId { get; set; }
        [ForeignKey("DonorId")]
        public Donor Donor { get; set; }
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
    public class Hospital
    {
        public int HospitalId { get; set; }
        [Required, StringLength(50), Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; }
        [Required, StringLength(50)]
        public string Address { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Location { get; set; }
        
        //[Required]
        //public int PatientId { get; set; }
        //[ForeignKey("PatientId")]
        //public Patient Patient { get; set; }
        public ICollection<HospitalPatient> HospitalPatients { get; set; } = new List<HospitalPatient>();
    }
    public class HospitalPatient
    {
        [Key, Column(Order = 0), ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Hospital Hospital { get; set; }
        public Patient Patient { get; set; }

    }
    public class BloodDonorDbContext : DbContext
    {
        public BloodDonorDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientDonor> PatientDonors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalPatient> HospitalPatients { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<BloodDonorDbContext>
    {
        protected override void Seed(BloodDonorDbContext context)
        {
            Blood b = new Blood { BloodGroup = BloodGroup.AB_Negative, BloodDetails= "A negative red blood cells can be used to treat around 40% of the population" };            
            //b.Donors.Add(new Donor { DonorName = "Imrul Kayes", Address="Mirpur", Email = "imrul@gamil.com", Phone = "01920-121412", IsAvailable=false, Picture = "1.jpg" });
            //b.Donors.Add(new Donor { DonorName = "Rahim", Address = "Uttara", Email = "rahim@gamil.com", Phone = "01920-121415", DonationDate = DateTime.Now.AddDays(-5 * 30), IsAvailable = true, Picture = "2.jpg" });
            Donor d = new Donor { DonorName = "Mahidul Molla", Address = "Mirpur", Email = "mahidul@yahoo.com", Phone = "01526-457324", DonationDate= DateTime.Now.AddDays(-6 * 30), IsAvailable = true, Picture = "1.jpg" };
            Patient p = new Patient { PatientName = "Sharif", Address = "Nilkhet", Email = "sharif@yahoo.com", Phone = "01526-457321", AdmitDate= DateTime.Now.AddDays(-6 * 30), EndDate = DateTime.Now.AddDays(-1), PaymentBill = 9890.00M, Picture = "2.jpg" };
            PatientDonor pd = new PatientDonor { Donor = d, Patient = p };
            Hospital h = new Hospital { HospitalName = "Heart Foundation", Picture = "hospital01.jpg", Address = "Mirpur-2, Dhaka", Email = "info@hfhospital.com", Location = "Mirpur, Dhaka" };
            HospitalPatient hp = new HospitalPatient { Patient = p, Hospital = h };
            context.Bloods.Add(b);
            context.Donors.Add(d);
            context.Patients.Add(p);
            context.PatientDonors.Add(pd);
            context.Hospitals.Add(h);
            context.HospitalPatients.Add(hp);
            context.SaveChanges();
        }
    }   
}