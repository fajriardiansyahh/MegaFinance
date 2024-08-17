using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("ms_storage_location", Schema = "dbo")]
    public class StorageLocation
    {
        [Key]
        public string location_id { get; set; }
        public string location_name { get; set; }
    }
}
