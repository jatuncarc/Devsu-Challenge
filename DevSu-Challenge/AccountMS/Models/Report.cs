using System.ComponentModel.DataAnnotations;

namespace AccountMS.Models
{
    public class Report
    {
        [Key]
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public Int64 NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public Boolean Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }


        // Constructor vacío
        public Report() { }
    }
}
