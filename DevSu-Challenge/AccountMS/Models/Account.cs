using System.ComponentModel.DataAnnotations;

namespace AccountMS.Models
{
    public class Account
    {
        [Key]
        public Int64 Number { get; set; }
        public string AccountType { get; set; }
        public decimal OpeningBalance { get; set; }
        public Boolean State { get; set; }
        public int CustomerId { get; set; }


        // Constructor vacío
        public Account() { }
    }
}
