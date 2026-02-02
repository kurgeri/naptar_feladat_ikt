using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Configuration;
using System.Security.Permissions;

namespace naptar_ikt_feladat
{

    struct Felhasznalok
    {
        public string felhasznalonev;
        public DateTime idopont;
        public int idotartam; // percben
        public string esemenynev;
        public List<Felhasznalok> esemenyek;


        public override string ToString()
        {
            return ($"{felhasznalonev};{esemenynev};{idopont};{idotartam}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Felhasznalovaltas();


        }

        static void Felhasznalovaltas()
        {
            Felhasznalok felhasznalo = new Felhasznalok();

            string user = string.Empty;
         
            do
            {

                Console.Write("Ki használja a számító gépet? Apa/Anya (első betű nagybetű): ");
                user = Console.ReadLine();
                switch (user)

                {
                    case "Apa":
                        felhasznalo.felhasznalonev = "Apa";
                        Console.WriteLine("Szia, Apa!");
                        break;

                    case "Anya":
                        felhasznalo.felhasznalonev = "Anya";
                        Console.WriteLine("Szia, Anya!");
                        break;


                    default:
                        Console.WriteLine("Nem megfelelő adatot adott meg!");
                        break;
                }

            } while (user == string.Empty && user != "Apa" && user != "Anya");
            Menu();
        }

        static void Menu()
        {
            

           
            char opcio;
            
            Console.Clear();
            Console.WriteLine($"Szia ");
            do
            {
                Console.Write($"N: Naptár\nÚ: Új esemény rögzítése\nL : Legközelebbi esemény\nK: Kilépés\nF: Felhasználó váltás\nVálasszon a kívánt opciók közül: ");
                opcio = Convert.ToChar(Console.ReadLine().ToUpper());


                switch (opcio)
                {
                    case 'N':
                        Console.Clear();
                        Naptar();
                        break;
                    case 'Ú':
                        Ujesemeny();
                        break;
                    case 'L':
                        Legkozelebbiesemeny();
                        break;
                    case 'K':
                        Kilepes();
                        break;
                    case 'F':
                        Felhasznalovaltas();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Rossz adat");
                        break;




                }
            } while (opcio != 'N' && opcio != 'Ú' && opcio != 'L' && opcio != 'K' && opcio != 'F');



        }
        static void Naptar()
        {
            string[,] naptar = new string[5, 7];
            int napok = 0;



            for (int i = 1; i < naptar.GetLength(0); i++)
            {

                Console.Write($"\n{i}.hét\t");
                for (int j = 0; j < naptar.GetLength(1); j++)
                {
                    napok++;
                    naptar[i, j] = Convert.ToString(napok);
                    if (napok > 29)
                    {
                        naptar[i, j] = String.Empty;
                    }



                    Console.Write($"{naptar[i, j]}  ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Nyomjon meg egy gombot, hogy visszamenjen a menübe!");
            Console.ReadLine();
            Menu();
        }

        static void Kilepes()
        {
            StreamWriter kimenet = new StreamWriter("esemenyek.txt");
            kimenet.Write($"");

            kimenet.Flush();
            kimenet.Close();

        }


        static void Ujesemeny()
        {


        }
        static void Legkozelebbiesemeny()
        {

        }


    }
}
