using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1
{
    [Table("Shippers")]
    public class Shipper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipperID { get; set; }
        [MaxLength(40)]
        public string CompanyName { get; set; }
        [MaxLength(24)]
        public string Phone { get; set; }
    }
}