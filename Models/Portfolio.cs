using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        [Key]
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        //public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}
