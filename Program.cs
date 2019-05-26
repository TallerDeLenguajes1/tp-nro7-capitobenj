using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsita
{
    class Program
    {
        public enum cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigador };
        static void Main(string[] args)
        {
            char key;
            List<Empleado> Agenda = new List<Empleado>();
            Empleado empleado;
            do
            {
                Console.WriteLine("1. Ver Agenda (Contactos: {0})", CantidaddeEmpleados(Agenda));
                Console.WriteLine("2. Agregar Empleado a la Agenda");
                Console.WriteLine("3. Monto Total de los Empleados de la Agenda");
                Console.WriteLine("0. Salir");
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '0':
                        break;
                    case '1':
                        MostrarAgendaConForeach(Agenda);
                        break;
                    case '2':
                        empleado = CrearEmpleado();
                        AgregarEmpleado(Agenda, empleado);
                        break;
                    /*case '3':
                        Console.WriteLine(CantidaddeEmpleados(Agenda));
                        break;*/
                    case '3':
                        Console.WriteLine(CalcularMonto(Agenda));
                        break;
                    default:
                        Console.WriteLine("Error: Dato Ingresado no coincide con las opciones predefinidas.");
                        break;
                }
            } while (key != '0');
        }
        public static int CalcularEdad(string _fecha)
        {
            int edad;
            string ano, mes, dia;
            int d1, m1, a1, d2, m2, a2;
            string fechayhora;
            fechayhora = DateTime.Now.ToString("yyyyMMdd");
            char[] aano = { fechayhora[0], fechayhora[1], fechayhora[2], fechayhora[3] };
            char[] _mes = { fechayhora[4], fechayhora[5] };
            char[] _dia = { fechayhora[6], fechayhora[7] };
            string[] charles;//fecha actual
            char[] splitter = { ' ' };
            charles = _fecha.Split(splitter);
            ano = new string(aano);//año de nac
            mes = new string(_mes);//mes de nac
            dia = new string(_dia);//dia de nac
            d1 = int.Parse(dia);
            d2 = int.Parse(charles[0]);
            m1 = int.Parse(mes);
            m2 = int.Parse(charles[1]);
            a1 = int.Parse(ano);
            a2 = int.Parse(charles[2]);
            edad = a1 - a2;
            if (m2 >= m1)
            {
                if (d2 < d1) edad--;
            }
            else edad--;
            return edad;
        }
        public struct Empleado
        {
            public string nombre;
            public string apellido;
            public string FechadeNac;
            public int Edad;
            public string EC;   //Estado civíl
            public string Sexo;
            public string FIngreso; //Fecha de ingreso a la empresa
            public int Antig; //Antigüedad del empleado en la empresa
            public int Jubil; //Años faltantes para jubilarse
            public string SB; //Sueldo Básico
            public double Sueldo;
            public int hijos;
            public string Cargo;
            public void MostrarTag()
            {
                Console.WriteLine("{0}, {1}",apellido,nombre);
                Console.WriteLine("---------------------------------------------");
            }
            public void MostrarEmpleado()
            {
                Console.WriteLine("\n================================================================================");
                Console.WriteLine("{0}, {1}", apellido, nombre);
                Console.WriteLine("\n--------------------------------------------------------------------------------");
                Console.Write("Fecha de Nacimiento: {0}\n", FechadeNac);
                Console.Write("Edad: {0}\n", Edad);
                Console.Write("Estado Civíl: {0}\n", EC);
                Console.Write("Sexo: {0}\n", Sexo);
                Console.Write("Fecha de Ingreso: {0}\n", FIngreso);
                Console.Write("Sueldo Básico: {0}\n", SB);
                Console.Write("Cargo: {0}\n", Cargo);
                Console.WriteLine("Hijos: {0}",hijos);
                Console.WriteLine("Antigüedad en la empresa: {0}", Antig);
                if (Jubil == 0) Console.WriteLine("Ya puede jubilarse.");
                else Console.WriteLine("Años restantes para jubilarse: {0}", Jubil);
                Console.WriteLine("\n--------------------------------------------------------------------------------");
                Console.WriteLine("Sueldo: {0}", Sueldo);
                Console.WriteLine("\n================================================================================");
            }
        }
        public static double CalcularAdicional(int Ant, string _SB, string cargo, string EC, int hijos)
        {
            double Adic;
            int SB;
            SB = int.Parse(_SB);
            double porano = 0.02;
            if (Ant == 0) Adic = 0;
            else if (Ant >= 20) Adic = SB * (porano * 25);
            else
            {
                Adic = SB * (porano * Ant);
            }
            if (cargo == "Ingeniero" || cargo == "Especialista") Adic *= 1.5;
            if ((EC == "Casado" || EC == "Casada") && hijos > 2)
            {
                Adic += 5000;
            }
            return Adic;
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static Empleado CrearEmpleado()
        {
            string _nombre, _apellido, _FechadeNac, _EC, _Sexo, _FIngreso, _SB;
            Empleado nuevo;
            Console.Write("Ingrese Nombre del Empleado/a: ");
            _nombre = Console.ReadLine();
            Console.Write("Ingrese Apellido del Empleado/a: ");
            _apellido = Console.ReadLine();
            Console.Write(@"Ingrese Fecha de Nacimiento del Empleado/a(Formato: 12 05 1912): ");
            _FechadeNac = Console.ReadLine();
            Console.Write("Estado Civil del Empleado/a(Formato: Soltero/a, Casado/a): ");
            _EC = Console.ReadLine();
            Console.Write("Sexo del Empleado/a(Formato: Femenino o Masculino): ");
            _Sexo = Console.ReadLine();
            Console.Write("Fecha de Ingreso del Empleado/a(Formato: 02 05 1912): ");
            _FIngreso = Console.ReadLine();
            Console.Write("Ingrese el Sueldo Basico del Empleado/a: ");
            _SB = Console.ReadLine();
            nuevo.nombre = _nombre;
            nuevo.apellido = _apellido;
            nuevo.FechadeNac = _FechadeNac;
            nuevo.Edad = CalcularEdad(_FechadeNac);
            nuevo.EC = _EC;
            nuevo.Sexo = _Sexo;
            nuevo.FIngreso = _FIngreso;
            nuevo.Antig = CalcularEdad(_FIngreso);
            nuevo.SB = _SB;
            nuevo.hijos = RandomNumber(0, 6);
            if (nuevo.Sexo == "femenino" || nuevo.Sexo == "Femenino") nuevo.Jubil = 60 - nuevo.Edad;
            else nuevo.Jubil = 65 - nuevo.Edad;
            if (nuevo.Jubil < 0) nuevo.Jubil = 0;
            nuevo.Cargo = Randomblow();
            nuevo.Sueldo = int.Parse(_SB) + CalcularAdicional(nuevo.Antig, _SB, nuevo.Cargo, nuevo.EC, nuevo.hijos);
            return nuevo;
        }
        public static void AgregarEmpleado(List<Empleado> Agenda, Empleado contacto)
        {
            Agenda.Add(contacto);
        }
        public static string Randomblow()
        {
            var value = RandomEnumValue<cargo>();
            return value.ToString();
        }
        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }
        public static void MostrarAgendaConOpcion(List<Empleado> Agenda)
        {
            int i = CantidaddeEmpleados(Agenda);
            int a = 1;
            if(i>0) Console.WriteLine("\n----------------Agenda---------------");
            foreach (Empleado emp in Agenda)
            {
                if (i>0)
                    {
                        Console.Write("{0}.",a);
                        emp.MostrarTag();
                    }
                a++;
            }
        }
        public static int CantidaddeEmpleados(List<Empleado> Agenda)
        {
            int i = 0;
            Agenda.ForEach(x => i++);
            return i;
        }
        public static double CalcularMonto(List<Empleado> Agenda)
        {
            double Monto = 0;
            Agenda.ForEach(x => Monto += x.Sueldo);
            return Monto;
        }
        public static void MostrarAgendaConForeach(List<Empleado> Agenda)
        {
            Agenda.ForEach(x => x.MostrarEmpleado());
        }
    }
}
