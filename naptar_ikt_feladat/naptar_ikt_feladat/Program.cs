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
using System.Dynamic;

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
            return $"{felhasznalonev};\t{esemenynev};\t{eidopontkezd.ToString("yyyy.MM.dd HH:mm")};\t{eidopontveg.ToString("HH:mm")}";
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
            Console.Clear();
            string user = string.Empty;
            do
            {

                Console.Write("Ki használja a naptárat? Apa/Anya (első betű nagybetű): ");
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

        static void Strukfeltolt(Felhasznalok f)
        {

            List<Felhasznalok> adatok = new List<Felhasznalok>();
            Random random = new Random();
            StreamReader fajl = new StreamReader("esemenynevek.txt", Encoding.UTF8);
            List<string> esemenyek = new List<string>();

            while (!fajl.EndOfStream)
            {

                esemenyek.Add(fajl.ReadLine());

            }
            for (int i = 1; i < 30; i++)
            {
                int ora = random.Next(8, 20);
                int perc = random.Next(0, 60);
                int esemenyhossz = random.Next(30, 120); // percben
                int randomesemeny = random.Next(0, esemenyek.Count);
                f.esemenynev = esemenyek[randomesemeny];


                f.eidopontkezd = new DateTime(2028, 02, i, ora, perc, 0);
                int pluszora = esemenyhossz / 60;
                int pluszperc = esemenyhossz % 60;

                int vegperc = pluszperc + perc;


                if (vegperc >= 60)
                {
                    vegperc -= 60;
                    pluszora++;
                }
                int vegora = pluszora + ora;
                f.eidopontveg = new DateTime(2028, 02, i, vegora, vegperc, 0);



                adatok.Add(f);

                if (i % 2 == 0)
                {
                    ora = random.Next(8, 20);
                    perc = random.Next(0, 60);
                    esemenyhossz = random.Next(30, 120);
                    randomesemeny = random.Next(0, esemenyek.Count);
                    f.esemenynev = esemenyek[randomesemeny];
                    f.eidopontkezd = new DateTime(2028, 02, i, ora, perc, 0);
                    pluszora = esemenyhossz / 60;
                    pluszperc = esemenyhossz % 60;
                    vegperc = pluszperc + perc;


                    if (vegperc >= 60)
                    {
                        vegperc -= 60;
                        pluszora++;
                    }
                    vegora = ora + pluszora;
                    f.eidopontveg = new DateTime(2028, 02, i, vegora, vegperc, 0);




                    adatok.Add(f);
                }
                //do
                //{
                //    ora = random.Next(8, 20);
                //    perc = random.Next(0, 60);
                //    esemenyhossz = random.Next(30, 120);
                //    randomesemeny = random.Next(0, esemenyek.Count);
                //    f.esemenynev = esemenyek[randomesemeny];
                //    f.eidopontkezd = new DateTime(2028, 02, i, ora, perc, 0);
                //    pluszora = esemenyhossz / 60;
                //    pluszperc = esemenyhossz % 60;
                //    vegperc = pluszperc + perc;


                //    if (vegperc >= 60)
                //    {
                //        vegperc -= 60;
                //        pluszora++;
                //    }
                //    vegora = ora + pluszora;
                //    f.eidopontveg = new DateTime(2028, 02, i, vegora, vegperc, 0);

                //} while ((adatok[i -].eidopontkezd.Date) == (adatok[i - 2].eidopontkezd.Date) && (adatok[i - 1].eidopontkezd.Hour) == (adatok[i - 2].eidopontkezd.Hour));




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
                        Naptar(f, adatok);
                        break;
                    case 'Ú':
                        Ujesemeny(f, adatok);
                        break;
                    case 'L':
                        Legkozelebbiesemeny(f, adatok);
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
            char meg;
            do
            {
                Console.WriteLine("Szeretné egy adott napnakl az esményeit megtekinteni? (I/N)");
                meg = Convert.ToChar(Console.ReadLine().ToUpper());


               if (meg != 'N' && meg != 'I')
                {
                    Console.WriteLine("Nem jó, újra!");
                }

            } while (meg != 'N' && meg != 'I');
            int nap;
            if (meg == 'N')
            {

                Menu(f, adatok);
            }
            else if (meg == 'I')
            {

                do
                {
                    Console.WriteLine("Melyik napot szeretné megtekinteni? (1-29)");
                    nap = Convert.ToInt32(Console.ReadLine());
                    if (nap < 1 || nap > 29)
                    {
                        Console.WriteLine($"Nem jó számot adott meg!");
                    }

                } while (nap < 1 || nap > 29);

                foreach (Felhasznalok fe in adatok)
                {


                    if (fe.eidopontkezd.Day == nap)
                    {
                        int napind = adatok.IndexOf(fe);
                        Console.WriteLine($"{adatok[napind]}");
                    }
                }
                Console.WriteLine("Szeretne ehhez a naphoz új eseményt hozzáadni?(I/N)");
                meg = Convert.ToChar(Console.ReadLine());
                if (meg == 'I')
                {
                    Ujesemeny(f, adatok);
                }
                else
                {
                    Console.WriteLine("Nyomjon meg egy gombot, hogy vissza menjen a menübe");
                    Console.ReadLine();
                    Menu(f, adatok);

                }

            }

        }





        static void Ujesemeny(Felhasznalok f, List<Felhasznalok> adatok)
        {
            Console.Clear();

            Console.WriteLine($"Jelenlegi felhasználó: {f.felhasznalonev}");

            char valasz;
            do
            {
                Console.WriteLine($"Ezzel a felhasználóval szeretne új eseményt hozzáadni?(I/N)");
                valasz = Convert.ToChar(Console.ReadLine());
                if (valasz != 'I' && valasz != 'N')
                {
                    Console.WriteLine("Nem megfelelő karaktert adott meg!");
                }


            } while (valasz != 'I' && valasz != 'N');

            if (valasz == 'I')
            {
                string bekertes;
                DateTime bekertnap;
                DateTime esekezd;
                DateTime eseveg;
                foreach (Felhasznalok fe in adatok)
                {
                    do
                    {

                        Console.WriteLine("Fontos tudnialók mielőtt megadná az eseményt:\n Az esemény időpontjának 8 óra és este 8 óra között kell lennie. ");
                        Console.Write("Esemény neve:");
                        bekertes = Console.ReadLine();
                        Console.Write("Melyik nap? (Formátum: év.hónap.nap:");
                        bekertnap = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Esemény kezdete(óó:pp:mm):");
                        esekezd = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Esemény vége(óó:pp:mm):");
                        eseveg = Convert.ToDateTime(Console.ReadLine());

                        if (fe.eidopontkezd.Hour == esekezd.Hour)
                        {
                            Console.WriteLine($"Hiba! Arra az időtartamra már van esemény bejegyezve");
                        }




                    } while (fe.eidopontkezd.Hour == esekezd.Hour);
                }


                Console.WriteLine("Nyomjon meg egy gombot, hogy viszatérjen a menübe");
                Console.ReadLine();
                Menu(f, adatok);

            }


            else if (valasz == 'N')
            {
                Felhasznalovaltas(f);
            }


        }
        static void Legkozelebbiesemeny(Felhasznalok f, List<Felhasznalok> adatok)
        {
            Random rnd = new Random();
            int nap = rnd.Next(1, 29);
            int ora = rnd.Next(1, 23);
            int perc = rnd.Next(1, 59);
            DateTime maidatum = new DateTime(2028, 2, nap, ora, perc, 00);
            Console.WriteLine($"Mai dátum: {maidatum}");
            int napid;

            foreach (Felhasznalok fe in adatok)
            {
                if (fe.eidopontkezd.Day == maidatum.Day && maidatum.Hour < fe.eidopontkezd.Hour)
                {
                    napid = adatok.IndexOf(fe);
                    Console.WriteLine($"Legközelebbi esemény(ek): {adatok[napid]}");
                }
                else if (fe.eidopontkezd.Day == maidatum.Day && maidatum.Hour > fe.eidopontkezd.Hour )
                {
                    napid = adatok.IndexOf(fe);
                    Console.WriteLine($"Legközelebbi esemény(ek): {adatok[napid+1]}");
                }
            }
        
       







            






        Console.WriteLine("Nyomjon meg egy gombot, hogy viszatérjen a menübe");
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
            Console.WriteLine("Az események elmentésre kerültek az esemenyek.txt fájlba.");

        }


    }
}
