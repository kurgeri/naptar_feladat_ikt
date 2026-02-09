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

            int napokFeb = DateTime.DaysInMonth(2028, 2);

            for (int i = 0; i < 45; i++)
            {
                Felhasznalok ujEsemeny = new Felhasznalok();
                ujEsemeny.felhasznalonev = f.felhasznalonev;
                ujEsemeny.esemenynev = esemenyek[random.Next(esemenyek.Count)];

                bool idopontJo = false;
                int probalkozasok = 0;

                while (!idopontJo && probalkozasok < 100)
                {
                    probalkozasok++;

                    int nap = random.Next(1, napokFeb + 1);

                    int ora = random.Next(8, 18);
                    int perc = random.Next(0, 60);

                    ujEsemeny.eidopontkezd = new DateTime(2028, 2, nap, ora, perc, 0);
                    int esemenyHossz = random.Next(30, 121);
                    ujEsemeny.eidopontveg = ujEsemeny.eidopontkezd.AddMinutes(esemenyHossz);

                    bool atfedes = false;

                    foreach (Felhasznalok l in adatok)
                    {
                        if (ujEsemeny.eidopontkezd < l.eidopontveg && ujEsemeny.eidopontveg > l.eidopontkezd)
                        {
                            atfedes = true;
                            break;
                        }
                    }
                    if (!atfedes)
                    {
                        idopontJo = true;
                    }
                }

                if (idopontJo)
                {
                    adatok.Add(ujEsemeny);
                }
            }

            for (int i = 0; i < adatok.Count - 1; i++)
            {
                for (int j = 0; j < adatok.Count - 1; j++)
                {
                    if (adatok[j].eidopontkezd > adatok[j + 1].eidopontkezd)
                    {
                        Felhasznalok temp = adatok[j];
                        adatok[j] = adatok[j + 1];
                        adatok[j + 1] = temp;
                    }
                }
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
                Console.WriteLine("Szeretné egy adott napnak az esményeit megtekinteni? (I/N)");
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
            Console.WriteLine("Jelenlegi események:");
            foreach (Felhasznalok feeline in adatok)
            {
                Console.WriteLine(feeline.ToString());
            }

            Console.WriteLine($"Jelenlegi felhasználó: {f.felhasznalonev}");

            char valasz;
            do
            {
                Console.WriteLine($"Ezzel a felhasználóval szeretne új eseményt hozzáadni?(I/N)");
                valasz = Convert.ToChar(Console.ReadLine().ToUpper());
                if (valasz != 'I' && valasz != 'N')
                {
                    Console.WriteLine("Nem megfelelő karaktert adott meg!");
                }
            } while (valasz != 'I' && valasz != 'N');


            if (valasz == 'I')
            {
                string bekertEs;
                int nap;
                string esekezd;
                string eseveg;
                Felhasznalok ujEs = new Felhasznalok();


                bool atfedes = false;
                bool idoTartamJo = true;
                do
                {
                    int oraK, percK, oraV, percV;
                    atfedes = false;
                    idoTartamJo = true;
                    do
                    {
                        Console.WriteLine("Fontos tudnialók mielőtt megadná az eseményt:\n Az esemény időpontjának 8 óra és este 8 óra között kell lennie.\n Az esemény hosszának 30 és 120 perc között kell lennie.");
                        Console.Write("Esemény neve:");
                        bekertEs = Console.ReadLine();
                        Console.Write("Melyik napon legyen az esemény?{1-29}:");
                        nap = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Esemény kezdete(óó:pp):");
                        esekezd = Console.ReadLine();
                        Console.WriteLine("Esemény vége(óó:pp):");
                        eseveg = Console.ReadLine();

                        string[] kezd = esekezd.Split(':');

                        oraK = Convert.ToInt32(kezd[0]);
                        percK = Convert.ToInt32(kezd[1]);
                        string[] veg = eseveg.Split(':');

                        oraV = Convert.ToInt32(veg[0]);
                        percV = Convert.ToInt32(veg[1]);


                        if (oraK < 8 && oraV > 20)
                        {
                            Console.WriteLine($"nem jó, add meg újra");
                        }


                    } while (oraK < 8 && oraV > 20);
                    foreach (Felhasznalok l in adatok)
                    {
                        ujEs.felhasznalonev = l.felhasznalonev;
                        ujEs.esemenynev = bekertEs;
                        ujEs.eidopontkezd = new DateTime(2028, 2, nap, oraK, percK, 0);
                        ujEs.eidopontveg = new DateTime(2028, 2, nap, oraV, percV, 0);
                        if (ujEs.eidopontkezd < l.eidopontveg && ujEs.eidopontveg > l.eidopontkezd)
                        {
                            atfedes = true;
                        }
                    }


                    if (atfedes == true)
                    {
                        Console.WriteLine("Már van egy esemény ebben az időpontban, adja meg egy későbbi időpontba.");
                    }

                    if ((ujEs.eidopontveg - ujEs.eidopontkezd).TotalMinutes > 120 || (ujEs.eidopontveg - ujEs.eidopontkezd).TotalMinutes < 30)
                    {
                        idoTartamJo = false;
                        Console.WriteLine("Túl rövid, vagy túl hosszú az esemény amit megadott!");
                    }



                }
                while (atfedes == true || idoTartamJo == false);


                adatok.Add(ujEs);


                for (int i = 0; i < adatok.Count - 1; i++)
                {
                    for (int j = 0; j < adatok.Count - 1; j++)
                    {
                        if (adatok[j].eidopontkezd > adatok[j + 1].eidopontkezd)
                        {
                            Felhasznalok temp = adatok[j];
                            adatok[j] = adatok[j + 1];
                            adatok[j + 1] = temp;
                        }
                    }
                }

                Console.WriteLine("Esemény sikeresen hozzáadva!\nNyomjon meg egy gombot, hogy viszatérjen a menübe.");
                Console.ReadLine();
                Menu(f, adatok);
            }
            else if (valasz == 'N')
            {
                Menu(f, adatok);
            }

        }

        static void Legkozelebbiesemeny(Felhasznalok f, List<Felhasznalok> adatok)
        {
            Console.Clear();
            Random rnd = new Random();
            int nap = rnd.Next(1, 29);
            int ora = rnd.Next(1, 23);
            int perc = rnd.Next(1, 59);
            DateTime maidatum = new DateTime(2028, 2, nap, ora, perc, 00);
            Console.WriteLine($"Mai dátum: {maidatum}");

            Felhasznalok legkozelebbi_Datum = new Felhasznalok();

            bool van = false;

            TimeSpan minKulonbseg = TimeSpan.MaxValue;

            foreach (Felhasznalok fe in adatok)
            {
                if (fe.eidopontkezd > maidatum)
                {
                    TimeSpan kulonbseg = fe.eidopontkezd - maidatum;

                    if (kulonbseg < minKulonbseg)
                    {
                        minKulonbseg = kulonbseg;
                        legkozelebbi_Datum = fe;
                        van = true;
                    }
                }
            }

            if (van)
            {
                Console.WriteLine($"Legközelebi esemény: {legkozelebbi_Datum}");
            }
            else
            {
                Console.WriteLine($"!");
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