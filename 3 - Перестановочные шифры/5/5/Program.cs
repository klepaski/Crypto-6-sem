using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {

            Marsh marsh = new Marsh();
            Console.OutputEncoding = Encoding.Unicode;

            //encrypt
            int key = 6;
            String msg = "";

            long OldTicks = DateTime.Now.Ticks;

            using (StreamReader sr = new StreamReader("in.txt"))
            {
                msg = (sr.ReadToEnd());
                msg = msg.Replace(" ", "");
            }


            string result_marsh = marsh.Encrypt(msg, key);

            Console.WriteLine("\nИсходный текст:  " + msg);
            Console.WriteLine("Маршрутный шифр: " + result_marsh);
            Console.WriteLine("Расшифров текст: " + marsh.Decrypt(result_marsh, key));

            long time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("Затрачено " + time_cipher + " мс\n\n\n\n");



            /*
                юлия чистякова
                2103 725683410
            */
            Mnozh mnozh = new Mnozh();
            int[] key1 = { 2, 1, 0, 3 };
            int[] key2 = { 7, 2, 5, 6, 8, 3, 4, 1, 0 };
            OldTicks = DateTime.Now.Ticks;

            string result_mnozh = mnozh.Encrypt(msg, key1, key2);

            Console.WriteLine("Исходный текст:  " + msg);
            Console.WriteLine("Множеств шифр:   " + result_mnozh);
            Console.WriteLine("Расшифров текст: " + mnozh.Decrypt(result_mnozh, key2, key1));

            time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("Затрачено " + time_cipher + " мс");


            
            Console.ReadKey();
        }
    }
}
