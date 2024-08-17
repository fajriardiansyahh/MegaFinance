using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("tr_bpkb", Schema = "dbo")]
    public class BPKB : BaseEntities
    {
        [Key]
        public string agreement_number { get; set; }
        public string bpkb_no { get; set; }
        public string branch_id { get; set; }
        public DateTime bpkb_date { get; set; }
        public string faktur_no { get; set; }
        public DateTime faktur_date { get; set; }
        public StorageLocation location_id { get; set; }
        public string police_no { get; set; }
        public DateTime bpkb_date_in { get; set; }
    }
}
