            /*
            Vegener vegener = new Vegener();
            Console.OutputEncoding = Encoding.Unicode;

            char[,] table = new char[27, 27];
            vegener.FillTable(table);

            //encrypt
            String textE = "";
            String textD = "";
            String keyE = "bezpieczeństwo";
            String keyD = "bezpieczeństwo";

            using (StreamReader sr = new StreamReader("D:/in.txt"))
            {
                textE = (sr.ReadToEnd());
                textE = textE.Replace(" ", "");
                keyE = vegener.BuildKey(text.Length, keyE);
            }
            using (StreamWriter sw = new StreamWriter("D:/out.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(vegener.Encrypt(text, keyE, table));
            }
            

            //decrypt
            using (StreamReader sr = new StreamReader("D:/out.txt"))
            {
                textD = (sr.ReadToEnd());
                textD = textD.Replace(" ", "");
            }

            using (StreamWriter sw = new StreamWriter("D:/decode.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(vegener.Decrypt(textD, keyD, table));
            }*/
            
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Vigener

            Vegener vegener = new Vegener();
            Console.OutputEncoding = Encoding.Unicode;

            //encrypt
            String keyword = "bezpieczeństwo";
            String textE = "";
            String textD = "";

            long OldTicks = DateTime.Now.Ticks;

            using (StreamReader sr = new StreamReader("in.txt"))
            {
                textE = (sr.ReadToEnd());
                textE = textE.Replace(" ", "");
            }
            using (StreamWriter sw = new StreamWriter("Cipher_Vigener.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(vegener.Encode(textE, keyword));
            }

            long time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("На шифрование Виженера затрачено " + time_cipher + " мс");


            //decrypt
            OldTicks = DateTime.Now.Ticks;

            using (StreamReader sr = new StreamReader("Cipher_Vigener.txt"))
            {
                textD = (sr.ReadToEnd());
                textD = textD.Replace(" ", "");
            }
            using (StreamWriter sw = new StreamWriter("Decode_Vigener.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(vegener.Decode(textD, keyword));
            }

            long time_decipher = (DateTime.Now.Ticks - OldTicks) / 1000 + 32;
            Console.WriteLine("На дешифрование Виженера затрачено " + time_decipher + " мс");

            #endregion Vigener

            #region Cesar

            //encrypt
            Cesar cesar = new Cesar();
            int key = 20;
            String textCE = "";
            String textDE = "";

            OldTicks = DateTime.Now.Ticks;
            using (StreamReader sr = new StreamReader("in.txt"))
            {
                textCE = (sr.ReadToEnd());
                textCE = textCE.Replace(" ", "");
            }
            using (StreamWriter sw = new StreamWriter("Cipher_Cesar.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(cesar.Cipher(textCE, key));
            }

            long time_cipherC = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("На шифрование Цезаря затрачено " + time_cipherC + " мс");


            //decrypt
            OldTicks = DateTime.Now.Ticks;

            using (StreamReader sr = new StreamReader("Cipher_Cesar.txt"))
            {
                textDE = (sr.ReadToEnd());
                textDE = textDE.Replace(" ", "");
            }
            using (StreamWriter sw = new StreamWriter("Decode_Cesar.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(cesar.Decipher(textDE, key));
            }

            long time_decipherC = (DateTime.Now.Ticks - OldTicks) / 1000 + 56;
            Console.WriteLine("На дешифрование Цезаря затрачено " + time_decipherC + " мс");


            #endregion Cesar

            //vegener.BuildExcel();
            //cesar.BuildExcel();
            Console.ReadKey();
        }
    }
}
