using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tp_8
{
    class Empleados
    {
        public string nombre;
        public string apellido;
        public string estadoCivil;
        public DateTime ingreso;
        public DateTime Nacimiento;
        public string genero;
        public double sueldoBasico;
        public string cargo;

        public int CalcularAnios(DateTime _fecha)
        {
            int anios = DateTime.Today.AddTicks(-_fecha.Ticks).Year - 1;

            return anios;
        }
        public int Jubilarse(int edad, string genero)
        {
            int anios;
            if (genero == "Femenino")
            {
                anios = 60 - edad;
            }
            else
            {
                anios = 65 - edad;
            }
            return anios;
        }
        public double CalcularSalario(int _antiguedad, double _SueldoBasico, string _cargo, string _estadoCivil)
        {

            double salario, adicional;
            Random rnd = new Random();
            int Hijos = rnd.Next(0, 5);
            if (_antiguedad <= 20)
            {
                adicional = (0.02 * _SueldoBasico) * _antiguedad;
            }
            else
            {
                adicional = 0.25 * _SueldoBasico;
            }

            if (_cargo == "Ingeniero" || _cargo == "Especialista")
            {
                adicional = adicional + adicional * 0.5;
            }

            if (Hijos > 2 && _estadoCivil == "Casado")
            {
                adicional = adicional + 5000;
            }

            salario = _SueldoBasico + adicional;
            return salario;
        }
        public void MostrarEmpleado()
        {
            Console.WriteLine("Nombre : {0}", nombre);
            Console.WriteLine("Apellido : {0}", apellido);
            Console.WriteLine("Fecha de Nacimiento: {0}", Nacimiento.ToLongDateString());
            Console.WriteLine("Estado Civil: {0}", estadoCivil);
            Console.WriteLine("Genero: {0}", genero);
            Console.WriteLine("Fecha de Ingreso: {0}", ingreso.ToLongDateString());
            Console.WriteLine("Sueldo Basico: {0}", sueldoBasico);
            Console.WriteLine("Cargo: {0}", cargo);
        }
        public Empleados(string _nombre, string _apellido, string _estado, DateTime _ingreso, DateTime _nacimiento, string _genero, double _sueldo, string _cargo)
        {
            this.nombre = _nombre;
            this.apellido = _apellido;
            this.estadoCivil = _estado;
            this.ingreso = _ingreso;
            this.Nacimiento = _nacimiento;
            this.genero = _genero;
            this.sueldoBasico = _sueldo;
            this.cargo = _cargo;
        }

        public string[] CargarArch(List<Empleados> empleado)
        {
            string[] arre = new string[] { this.apellido + ";", this.nombre + ";", this.Nacimiento.ToShortDateString() + ";", this.estadoCivil + ";",
                this.genero + ";" , this.ingreso.ToShortDateString() + ";", this.sueldoBasico + ";", this.cargo};
   
            return arre;
        }
        public string[] prueba(List<Empleados> empleado)
        {
            string[] arre = new string[] {empleado.ToString() };

            return arre;
        }

    }
}
