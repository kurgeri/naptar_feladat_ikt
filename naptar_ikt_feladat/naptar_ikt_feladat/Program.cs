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
        public bool apa;
        public bool anya;
        public DateTime idopont;
        public int idotartam; // percben
        public string esemenynev;

        public override string ToString()
        {
            return ($"{esemenynev};{idopont};{idotartam}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            Felhasznalovaltas();
            Menu();

        }

        static void Felhasznalovaltas()
        {
            Felhasznalok felhasznalo = new Felhasznalok();



            do
            {
                Console.Write("Ki használja a számító gépet? Apa/Anya: ");
                string user = Console.ReadLine();
                switch (user)

                {
                    case "Apa":
                        felhasznalo.apa = true;
                        Console.WriteLine("Szia, Apa!");
                        break;

                    case "Anya":
                        felhasznalo.anya = true;
                        Console.WriteLine("Szia, Anya!");
                        break;
                    case "apa":
                        felhasznalo.apa = true;
                        Console.WriteLine("Szia, Apa!");
                        break;
                    case "anya":
                        felhasznalo.anya = true;
                        Console.WriteLine("Szia, Anya!");
                        break;

                    default:
                        Console.WriteLine("Nem megfelelő adatot adott meg!");
                        break;
                }

            } while (felhasznalo.apa == false && felhasznalo.anya == false);
        }

        static void Menu()
        {



            char opcio;

            do
            {
                Console.Write($"N: Naptár\nÚ: Új esemény rögzítése\nL : Legközelebbi esemény\n K: Kilépés\nVálasszon a kívánt opciók közül:\nF: Felhasználó váltás: ");
                opcio = Convert.ToChar(Console.ReadLine().ToUpper());


                switch (opcio)
                {
                    case 'N':
                        Console.Clear();
                        Naptar();
                        break;
                    case 'Ú':
                        // Új események függvény
                        break;
                    case 'L':
                        // Legközelebbi esemény függvényg
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
            string[,] naptar = new string[6, 7];
            naptar[0, 0] = "H";
            naptar[0, 1] = "K";
            naptar[0, 2] = "Sz";
            naptar[0, 3] = "Cs";
            naptar[0, 4] = "P";
            naptar[0, 5] = "Szo";
            naptar[0, 6] = "V";
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
        }

        static void Kilepes()
        {
            StreamWriter kimenet = new StreamWriter("esemenyek.txt");
            kimenet.Write($"Esemény;Felhasználó;Időpont;Időtartam");

            kimenet.Flush();
            kimenet.Close();

        }


    }
}
