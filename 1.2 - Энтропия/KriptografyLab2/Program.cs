using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptografyLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //949 — Korean, 1253 — Greek,   65001 (UTF-8)
            //Console.WriteLine("안녕하세요");

            Console.OutputEncoding = Encoding.Unicode;

            String text="";
            Alphabet alphabet = new Alphabet();
            using (StreamReader sr = new StreamReader("korean.txt"))
            {
                text=(sr.ReadToEnd());
                text = text.Replace(" ","");
            }

            using (StreamWriter sw = new StreamWriter("otvet.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(text);
            }


            double EntKor = alphabet.Entropy(text, alphabet.Korean);
            alphabet.BuildGistogram(alphabet.Korean);

            double EntBin = alphabet.Entropy(text, alphabet.Binary);

            using (StreamWriter sw = new StreamWriter("otvet.txt", true, System.Text.Encoding.Unicode))
            {
                sw.WriteLine();
                sw.WriteLine("Entropy korean: " + EntKor);
                ///sw.WriteLine("Entropy cyrillic: " + alphabet.Entropy(text, alphabet.Cyrillic));
                ///sw.WriteLine("Entropy latin: " + alphabet.Entropy(text, alphabet.Latin));
                sw.WriteLine("Entropy binary: " + EntBin);
                sw.WriteLine("Infor korean: " + alphabet.kolInf(EntKor, text.Length));
            }
            


            using (StreamWriter sw = new StreamWriter("byt.txt", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(alphabet.GetBytes("ㅊㅣㅅㅌㅑㄱㅓㅘ ㅠㄹㅣㅑ"));
            }
            
            using (StreamReader sr = new StreamReader("byt.txt"))
            {
                text = (sr.ReadToEnd());
                text = text.Replace(" ", "");
            }
            

            using (StreamWriter sw = new StreamWriter("Infor.txt", false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine("Infor binary: " + alphabet.kolInf(alphabet.Entropy(text, alphabet.Binary), text.Length));
                sw.WriteLine("Effect Entropy (ERROR 0.1) : " + alphabet.EfectEntropy(0.1));
                sw.WriteLine("Effect Entropy (ERROR 0.5) : " + alphabet.EfectEntropy(0.5));
                sw.WriteLine("Effect Entropy (ERROR 1)   : " + alphabet.EfectEntropy(1));
                ///P ошибочн передачи единич бита
            }

            Console.WriteLine("Выполнено успешно!");
            Console.ReadKey();
        }
    }
}
