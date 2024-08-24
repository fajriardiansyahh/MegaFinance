using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication.Models
{
    public class BPKBModel : BaseModel
    {
        [Key]
        [Required]
        [DisplayName("Agreement Number")]
        public string agreement_number { get; set; }
        [Required]
        [DisplayName("No. BPKB")]
        public string bpkb_no { get; set; }
        [Required]
        [DisplayName("Branch Id")]
        public string branch_id { get; set; }
        [Required]
        [DisplayName("Tanggal BPKB")]
        public DateTime bpkb_date { get; set; }
        [Required]
        [DisplayName("No. Faktur")]
        public string faktur_no { get; set; }
        [Required]
        [DisplayName("Tanggal Faktur")]
        public DateTime faktur_date { get; set; }
        [Required]
        [DisplayName("Lokasi Penyimpanan")]
        public string location_id { get; set; }
        [Required]
        [DisplayName("Nomor Polisi")]
        public string police_no { get; set; }
        [Required]
        [DisplayName("Tanggal BPKB In")]
        public DateTime bpkb_date_in { get; set; }
    }
}
