using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace naptar_ikt_feladat
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Menu();
           
        }

        static void Menu()
        {
            Console.Write($"N: Naptár\nÚ: Új esemény rögzítése\nL : Legközelebbi esemény\n K: Kilépés\nVálasszon a kívánt opciók közül: ");
            string opcio = Console.ReadLine().ToUpper();
   

            switch(opcio)
            {
                case "N":
                    Console.Clear();
                    Naptar();
                    break;
                case "Ú":
                    // Új események függvény
                    break;
                case "L":
                    // Legközelebbi esemény függvényg
                    break;
                case "K":
                    // Kilépés függvény? (terminálni kell a számítót)
                    break;



            }

            
        }
        static void Naptar()
        {
            Console.WriteLine("        H  K  Sz Cs P Szo V");
            string[,] naptar = new string[5, 7];
            int napok = 0;
            for (int i = 0;  i < naptar.GetLength(1); i++)
            {
              
               
                for (int j = 0; j < naptar.GetLength(0); j++)
                {
                  
                }
            }


            for (int i = 0; i < naptar.GetLength(0); i++)
            {
                Console.Write($"\n{i+1}.hét\t");
                for (int j = 0; j < naptar.GetLength(1); j++)
                {
                    napok++;
                    naptar[i, j] = Convert.ToString(napok);
                    if(napok > 29)
                    {
                        naptar[i, j] = String.Empty;
                    }
                    


                    Console.Write($"{naptar[i,j]}  ");
                }
                Console.WriteLine();
            }
        }


    }
}
