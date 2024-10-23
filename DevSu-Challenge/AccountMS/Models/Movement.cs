using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountMS.Models
{
    public class Movement
    {
        [Key]
        public Int64 Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
        public decimal Value { get; set; }

        public decimal Balance { get; set; }
        public Int64 AccountId { get; set; }


        // Constructor vacío
        public Movement() { }
    }
}
