using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tp_8
{
    class Program
    {

        static void Main(string[] args)
        {
            Empleados nuevo;
            List<Empleados> empleado = new List<Empleados>();

            int dia, mes, anio;
            DateTime Ingreso, Nacimeinro;
            double sueldo;
            Random rnd = new Random();
            //Declaros los arreglos.
            string[] Cargo = new string[] { "Auxiliar", "Administrativo", "Ingeniero", "Especialista", "Investigador" };
            string[] Genero = new string[] { "Femenino", "Masculino" };
            string[] EstadoCivil = new string[] { "Soltero", "Casado" };
            string[] nombreMas = new string[] { "Juan", "Luis", "Andres", "German", "Mario", "Facundo" };
            string[] nombreFem = new string[] { "Luciana", "Maria", "Florencia", "Yesica", "Pamela", "Ana" };
            string[] apellido = new string[] { "Gomez", "Gonzales", "Medina", "Fernadez", "Rojas" };

            string cargo, genero, estado, nomb, apell;
            for (int i = 0; i < 20; i++)
            {
                //Asigno fecha de Nacimiento.
                dia = rnd.Next(1, 28);
                mes = rnd.Next(1, 12);
                anio = rnd.Next(1960, 2001);
                Nacimeinro = new DateTime(anio, mes, dia);

                //Asigo fecha de Ingreso.
                dia = rnd.Next(1, 28);
                mes = rnd.Next(1, 12);
                anio = rnd.Next(1990, 2018);
                Ingreso = new DateTime(anio, mes, dia);

                //Asigno Genero , estado Civil y cargo.
                cargo = Cargo[rnd.Next(0, 5)];
                genero = Genero[rnd.Next(0, 2)];
                estado = EstadoCivil[rnd.Next(0, 2)];

                //Asigno nombre segun su genero y asigno apellido.
                if (genero == "Femenino")
                {
                    nomb = nombreFem[rnd.Next(0, 6)];
                }
                else
                {
                    nomb = nombreMas[rnd.Next(0, 6)];
                }
                apell = apellido[rnd.Next(0, 5)];
                sueldo = 15000;

                nuevo = new Empleados(nomb, apell, estado, Ingreso, Nacimeinro, genero, sueldo, cargo);
                empleado.Add(nuevo);
            }
            int cont = 0;
            double MontoTotal = 0;
            foreach (Empleados empl in empleado)
            {
                Console.WriteLine("Empleado {0}", cont + 1);
                empl.MostrarEmpleado();
                Console.WriteLine("Antiguedad: {0}", empl.CalcularAnios(empl.ingreso));
                Console.WriteLine("Edad : {0}", empl.CalcularAnios(empl.Nacimiento));
                Console.WriteLine("Años para Jubilarce: {0}", empl.Jubilarse(empl.CalcularAnios(empl.Nacimiento), empl.genero));
                Console.WriteLine("Salario: {0}", empl.CalcularSalario(empl.CalcularAnios(empl.ingreso), empl.sueldoBasico, empl.cargo, empl.estadoCivil));
                cont++;
                MontoTotal = MontoTotal + empl.CalcularSalario(empl.CalcularAnios(empl.ingreso), empl.sueldoBasico, empl.cargo, empl.estadoCivil);
                Console.WriteLine("-----------------------------------");
            }
            Console.WriteLine("Cntidad de empleados: {0}", cont);
            Console.WriteLine("Monto total = {0}", MontoTotal);
           
            //Trabajo con Archivos.
            string ruta = @"E:\Repositorio\tp-nro8-Mario1294\Tp_8\Archivo.csv";
            string[] destino;
            string backup;

            int opc = 0;
            int inicio = 0;
            int fin = 5;
            do
            {
                Console.WriteLine("\nElija una opcion: \n1)Cargar 5 empleados al archivo.csv \n2)Hacer Backup. \n3)Salir.");
                Console.Write("Seleccione una opcion: ");
                opc = Int32.Parse(Console.ReadLine());
                switch (opc)
                {
                    //Cargar 5 empleados al archivo csv.
                    case 1:
                        if (!File.Exists(ruta))
                        {
                            Console.WriteLine("Archivo Creado.");
                            File.AppendAllText(ruta, "Apellido ; Nombre ; Nacimiento; Estado Civil; Genero; Ingreso; Sueldo Basico; Cargo; \n");
                        }
                        int k = 0;
                        int j = 0;

                        string[] arre = new string[] { };

                        foreach (Empleados empl in empleado)
                        {
                            arre = empl.CargarArch(empleado);
                            k = 0;
                            if ((j >= inicio) && (j < fin) && (fin <= 20))
                            {
                                while (k < 8)
                                {
                                    File.AppendAllText(ruta, arre[k]);
                                    k++;
                                }
                                File.AppendAllText(ruta, "\n");
                            }

                            j++;
                        }
                        if (fin > 20)
                        {
                            Console.WriteLine("\n---------------------------------------------------");
                            Console.WriteLine("Ya no empleados para cargar, seleccione otra opcion");
                            Console.WriteLine("---------------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("\n---------------------------------------------------");
                            Console.WriteLine("Empleados cargado con exito.");
                            Console.WriteLine("---------------------------------------------------");
                        }
                        inicio = inicio + 5;
                        fin = fin + 5;
                        break;

                    //Backup del archivo.csv
                    case 2:
                        Console.WriteLine("\n---------------------------------------------------");
                        destino = Directory.GetLogicalDrives();
                        string[] disco = new string[] { };
                        int i = 0;
                        Console.WriteLine("Unidades disponibles: ");
                        foreach (string str in destino)
                        {
                            Console.WriteLine($"{i+1}) {str}");
                            i++;
                        }
                        Console.Write("Seleccione la unidad para hacer Backup: ");

                        try
                        {
                            
                            int opc2 = Int32.Parse(Console.ReadLine());
                            opc2 = opc2 - 1;
                            
                            if (opc2 < i)
                            {
                                backup = ($@"{destino[opc2]}BackUpAgenda");
                                if (!Directory.Exists(backup))
                                {
                                    Console.WriteLine("Copia de seguridad realizada con exito en {0}.", backup);
                                    Directory.CreateDirectory(backup);
                                    File.Copy(ruta, backup + @"\archivo.bk");
                                }
                                else
                                {
                                    if (!File.Exists(backup + @"\archivo.bk"))
                                    {
                                        Console.WriteLine("Copia de seguridad realizada con exito en {0}.", backup);
                                        File.Copy(ruta, backup + @"\archivo.bk");
                                    }
                                    else
                                    {
                                        int ban = 0;
                                        int num = 1;
                                        while (ban != 1)
                                        {
                                            if (!File.Exists(backup + $@"\archivo({num}).bk"))
                                            {
                                                File.Copy(ruta, backup + $@"\archivo({num}).bk");
                                                ban = 1;
                                            }
                                            num++;
                                        }
                                        Console.WriteLine("\n---------------------------------------------------");
                                        Console.WriteLine("Copia de seguridad realizada con exito en {0}.", backup);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n---------------------------------------------------");
                                Console.WriteLine("La opción ingresada es incorrecta, intente de nuevo.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ocurrio un ERROR intente de nuevo.");
                        }
                        Console.WriteLine("---------------------------------------------------");
                        break;
                }
              
            } while (opc != 3);
   
        }
    }
}

