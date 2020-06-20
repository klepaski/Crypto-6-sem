using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    class Mnozh
    {
        static char[] characters = new char[] {'a','ą','b','c','ć','d','e','ę','f','g','h','i','j','k',
                                               'l','ł','m','n','ń','o','ó','p','r','s','ś','t','u','w',
                                               'y','z','ź','ż'};

        public string Encrypt(string msg, int[] key1, int[] key2)
        {
            /*
                2 5 1 4 6 3
            5   ш и ф р   в
            2   е р т и к а
            3   л ь н о й  
            1   п е р е с т 
            4   а н о в к и 

                1 2 3 4 5 6
            1   р п т е е с
            2   т е а и р к
            3   н л   о ь й
            4   о а и в н к
            5   ф ш в р и

             */
             
            //int[] key1 = { 1, 4, 0, 3, 5, 2 };
            //int[] key2 = { 4, 1, 2, 0, 3 };

            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key1.Length) + 1];

            for (int i = 0; i < (msg.Length / key1.Length) + 1; i++)
            {
                if (msg.Length - i * key1.Length <= key1.Length)
                {
                    msgInArray[i] = msg.Substring(i * key1.Length);
                    Console.WriteLine("msgInArray[" + i + "] = " + msgInArray[i]);
                    break;
                }
                else
                {
                    msgInArray[i] = msg.Substring(i * key1.Length, key1.Length);
                    Console.WriteLine("msgInArray[" + i + "] = " + msgInArray[i]);
                }
            }
            char[,] res = new char[key1.Length, key2.Length];
            
            for (int i = 0; i < key1.Length; i++)
                for (int k = 0; k < key2.Length; k++)
                {
                    //if (msgInArray[k].Length <= i && k==key2.Length-1) continue;
                    res[key1[i], key2[k]] = msgInArray[k][i];
                }

            for (int i = 0; i < key1.Length; i++)
                for (int k = 0; k < key2.Length; k++)
                {
                    result += res[i, k];
                }
            result = result.Replace("\0", "");
            return result;
        }


        public string Decrypt(string msg, int[] key1, int[] key2)
        {

            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key1.Length) + 1];


            for (int i = 0; i < (msg.Length / key1.Length) + 1; i++)
            {
                if (msg.Length - i * key1.Length <= key1.Length)
                {
                    msgInArray[i] = msg.Substring(i * key1.Length);
                    break;
                }
                else
                {
                    msgInArray[i] = msg.Substring(i * key1.Length, key1.Length);
                }
            }
            char[,] res = new char[key1.Length, key2.Length];


            for (int i = 0; i < key1.Length; i++)
                for (int k = 0; k < key2.Length; k++)
                {
                    res[i,k] = msgInArray[key2[k]][key1[i]];
                }

            for (int i = 0; i < key1.Length; i++)
                for (int k = 0; k < key2.Length; k++)
                {
                    result += res[i, k];
                }

            result = result.Replace("\0", "");
            return result;

        }
    }
}
