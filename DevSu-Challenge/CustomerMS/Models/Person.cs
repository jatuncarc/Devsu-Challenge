namespace CustomerMS.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Constructor opcional
        //public Persona(string nombre, string genero, int edad, string identificacion, string direccion, string telefono)
        //{
        //    Nombre = nombre;
        //    Genero = genero;
        //    Edad = edad;
        //    Identificacion = identificacion;
        //    Direccion = direccion;
        //    Telefono = telefono;
        //}

        // Constructor vacío
        public Person() { }
    }

}
