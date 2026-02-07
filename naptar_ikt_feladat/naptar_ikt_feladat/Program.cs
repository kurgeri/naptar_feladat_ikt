using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Configuration;
using System.Security.Permissions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace naptar_ikt_feladat
{

    struct Felhasznalok
    {
        public string felhasznalonev;
        public DateTime eidopontkezd;
        public DateTime eidopontveg;
        public string esemenynev;



        public override string ToString()
        {
            return ($"{felhasznalonev};{esemenynev};{eidopontkezd};{eidopontveg}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Felhasznalok f = new Felhasznalok();
            Felhasznalovaltas(f);
        }

        static void Felhasznalovaltas(Felhasznalok f)
        {
            string user = string.Empty;
            do
            {

                Console.Write("Ki használja a számító gépet? Apa/Anya (első betű nagybetű): ");
                user = Console.ReadLine();
                switch (user)
                {
                    case "Apa":
                        f.felhasznalonev = "Apa";
                        Console.WriteLine("Szia, Apa!");
                        break;
                    case "Anya":
                        f.felhasznalonev = "Anya";
                        Console.WriteLine("Szia, Anya!");
                        break;
                    default:
                        Console.WriteLine("Nem megfelelő adatot adott meg!");
                        break;
                }

            } while (user != "Apa" && user != "Anya");
            Strukfeltolt(f);
        }

        static void Strukfeltolt (Felhasznalok f)
        {
            
            List<Felhasznalok> adatok = new List<Felhasznalok>();
            Random random = new Random();
            for (int i = 1; i < 29; i++)
            {
                int ora = random.Next(8, 21);
                int perc = random.Next(0, 60);
                int esemenyhossz = random.Next(30, 120);
                f.esemenynev = $"{i}. esemeny";

                f.eidopontkezd = new DateTime(2028, 02, i, ora, perc, 0);
                int pluszora = esemenyhossz / 60;
                int pluszperc = esemenyhossz % 60;

                int vegperc = pluszperc + perc;
                if (vegperc >= 60)
                {
                    vegperc -= 60;
                    pluszora++;
                }
                f.eidopontveg = new DateTime(2028, 02, i, ora + pluszora, vegperc, 0);
                adatok.Add(f);
            }

            Menu(f, adatok);
        }


        static void Menu(Felhasznalok f, List<Felhasznalok> adatok)
        {
            Console.Clear();    
            char opcio;
            Console.WriteLine($"Szia {f.felhasznalonev}! ");
            do
            {
                Console.Write($"N: Naptár\nÚ: Új esemény rögzítése\nL : Legközelebbi esemény\nK: Kilépés\nF: Felhasználó váltás\nVálasszon a kívánt opciók közül: ");
                opcio = Convert.ToChar(Console.ReadLine().ToUpper());


                switch (opcio)
                {
                    case 'N':
                        Console.Clear();
                        Naptar(f,adatok);
                        break;
                    case 'Ú':
                        Ujesemeny();
                        break;
                    case 'L':
                        Legkozelebbiesemeny();
                        break;
                    case 'K':
                        Kilepes(adatok);
                        break;
                    case 'F':
                        Felhasznalovaltas(f);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Rossz adat");
                        break;




                }
            } while (opcio != 'N' && opcio != 'Ú' && opcio != 'L' && opcio != 'K' && opcio != 'F');



        }
        static void Naptar(Felhasznalok f, List<Felhasznalok> adatok)
        {

            string[,] naptar = new string[5, 7];
            int napok = 0;

            Console.Write($"\t\tH\tK\tSz\tCs\tP\tSzo\t");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("V");
           
            Console.ResetColor();

            for (int i = 0; i < naptar.GetLength(0); i++)
            {

                Console.Write($"\n{i + 1}.hét\t");
                for (int j = 0; j < naptar.GetLength(1); j++)
                {

                    naptar[i, j] = Convert.ToString(napok);
                    if (napok > 29 || napok == 0)
                    {
                        naptar[i, j] = String.Empty;
                    }
                    if (j == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                        napok++;
                  



                    Console.Write($"\t{naptar[i, j]}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Szeretné egy adott napnakl az esményeit megtekinteni? (I/N)");
            char meg = Convert.ToChar(Console.ReadLine());
            int nap;
            if(meg == 'N')
            {
               
                Menu(f, adatok);
            }
            else if(meg == 'I')
            {
                
                do
                {
                    Console.WriteLine("Melyik napot szeretné megtekinteni? (1-29)");
                    nap = Convert.ToInt32(Console.ReadLine());
                    if (nap < 1 || nap > 29)
                    {
                        Console.WriteLine($"Nem jó számot adott meg!");
                    }

                } while(nap < 1 || nap > 29);

               foreach (Felhasznalok fe in adatok)
                {
                    if(fe.eidopontkezd.Day == (nap-1))
                    {
                        Console.WriteLine(adatok[nap - 1]);
                    }
                }
            }
            Console.WriteLine("Nyomjon meg egy gombot, hogy vissza menjen a menübe");
            Console.ReadLine();
            Menu(f, adatok);

        }
            

        static void Kilepes(List<Felhasznalok> adatok)
        {
          
            
            StreamWriter kimenet = new StreamWriter("esemenyek.txt");
            foreach (Felhasznalok fe in adatok)
            {
                kimenet.WriteLine($"{fe.ToString()}");
            }

            kimenet.Flush();
            kimenet.Close();

        }


        static void Ujesemeny()
        {


        }
        static void Legkozelebbiesemeny()
        {
            Random rnd = new Random();
            int nap = rnd.Next(1, 29);
            DateTime maidatum = new DateTime(2028, 2, nap);

        }


    }
}
