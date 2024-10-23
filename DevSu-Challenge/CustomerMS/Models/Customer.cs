namespace CustomerMS.Models
{
    public class Customer : Person
    {
        public string Password { get; set; }
        public Boolean State { get; set; }

        // Constructor que incluye los atributos heredados y los propios de Cliente
        //public Cliente(string nombre, string genero, int edad, string identificacion, string direccion, string telefono, string contrasena, Boolean estado)
        //    : base(nombre, genero, edad, identificacion, direccion, telefono)
        //{
        //    Contrasena = contrasena;
        //    Estado = estado;
        //}

        // Constructor vacío
        public Customer() { }
    }

}
