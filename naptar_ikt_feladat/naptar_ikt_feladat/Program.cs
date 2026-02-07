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
        
            f = Felhasznalovaltas(f);
         




        }

        static Felhasznalok Felhasznalovaltas(Felhasznalok f)
        {
            Felhasznalok felhasznalo = f;

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

            } while (user != "Apa" && user != "Anya");

            Menu(felhasznalo);
            return felhasznalo;
           
        }

        static void Menu(Felhasznalok f)
        {

            Felhasznalok fe = f;
           
            char opcio;
            List<Felhasznalok> adatok = new List<Felhasznalok>();

         

            Random random = new Random();




            for (int i = 1; i < 29; i++)
            {
                int ora = random.Next(8, 21);
                int perc = random.Next(0, 60);
                int esemenyhossz = random.Next(30, 120);

                fe.eidopontkezd = new DateTime(2028, 02, i, ora, perc, 0);
                int pluszora = esemenyhossz / 60;
                int pluszperc = esemenyhossz % 60;

                int vegperc = pluszperc + perc;
                if (vegperc >= 60)
                {
                    vegperc -= 60;
                    pluszora++;
                }
                fe.eidopontveg = new DateTime(2028, 02, i, ora+pluszora, vegperc, 0);
                adatok.Add(fe);
                Console.WriteLine(fe.ToString());

            }

    
           

            



           

            
            Console.WriteLine($"Szia {fe.felhasznalonev}! ");
            do
            {
                Console.Write($"N: Naptár\nÚ: Új esemény rögzítése\nL : Legközelebbi esemény\nK: Kilépés\nF: Felhasználó váltás\nVálasszon a kívánt opciók közül: ");
                opcio = Convert.ToChar(Console.ReadLine().ToUpper());


                switch (opcio)
                {
                    case 'N':
                        Console.Clear();
                        Naptar(fe);
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
                        Felhasznalovaltas(fe);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Rossz adat");
                        break;




                }
            } while (opcio != 'N' && opcio != 'Ú' && opcio != 'L' && opcio != 'K' && opcio != 'F');



        }
        static void Naptar(Felhasznalok f)
        {
            
            string[,] naptar = new string[5, 7];
            int napok = 0;

            Console.WriteLine("\t\tH\tK\tSz\tCs\tP\tSzo\tV");

            for (int i = 0; i < naptar.GetLength(0); i++)
            {

                Console.Write($"\n{i+1}.hét\t");
                for (int j = 0; j < naptar.GetLength(1); j++)
                {
                
                    naptar[i, j] = Convert.ToString(napok);
                    if (napok > 29 || napok == 0)
                    {
                        naptar[i, j] = String.Empty;
                    }
                    napok++;



                    Console.Write($"\t{naptar[i, j]}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Nyomjon meg egy gombot, hogy visszamenjen a menübe!");
            Console.ReadLine();
            Menu(f);
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
            Random rnd = new Random();
            int nap = rnd.Next(1, 29);
            DateTime maidatum = new DateTime(2028, 2, nap);

        }


    }
}
