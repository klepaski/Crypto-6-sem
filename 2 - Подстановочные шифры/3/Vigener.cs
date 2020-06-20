//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XL = Microsoft.Office.Interop.Excel;

namespace _3
{
    class Vegener
    {
        static char[] characters = new char[] {'a','ą','b','c','ć','d','e','ę','f','g','h','i','j','k',
                                               'l','ł','m','n','ń','o','ó','p','r','s','ś','t','u','w',
                                               'y','z','ź','ż'};
        static int N = characters.Length;
        int[] colFx1 = new int[N];
        int[] colFx2 = new int[N];


        public string Encode(string input, string keyword)
        {
            string result = "";
            int keyword_index = 0;

            ///кол-во вхождений
            for (int i = 0; i < N; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
            {
                colFx1[i] = input.Where(el => el == characters[i]).Count();                                          ///есть вхожд - вывод букву + кол-во
            }

            foreach (char symbol in input)
            {
                int c = (Array.IndexOf(characters, symbol) +
                    Array.IndexOf(characters, keyword[keyword_index])) % N; ///y=x+k(modN)

                result += characters[c];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }
            return result;
        }


        public string Decode(string input, string keyword)
        {
            string result = "";
            int keyword_index = 0;

            ///кол-во вхождений
            for (int i = 0; i < N; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
            {
                colFx2[i] = input.Where(el => el == characters[i]).Count();                                          ///есть вхожд - вывод букву + кол-во
            }

            foreach (char symbol in input)
            {
                int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N; ///x=y-k(modN)

                result += characters[p];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }
            return result;
        }




        public void BuildExcel()
        {
            XL.Application XL1 = new XL.Application();  ///объяв объект
            XL1.Workbooks.Add();                         ///доб. рабочую книгу

            XL1.ActiveSheet.Range["A1"].Value = "Исходный символ:";
            XL1.ActiveSheet.Range["B1"].Value = "Частота:";

            int Yach = 2;
            for (int i = 0; i < colFx1.Length; i++)
            {
                if (colFx1[i] != 0)
                {
                    XL1.ActiveSheet.Range["A" + Yach].Value = $"{characters[i]}";
                    XL1.ActiveSheet.Range["B" + Yach].Value = Convert.ToInt32(colFx1[i]);
                    Yach++;
                }
            }
            XL1.Charts.Add();   ///доб. новую диаграмму
            XL1.ActiveChart.ChartType = XL.XlChartType.xlColumnClustered;///столбчатая

            //подписи по оси X
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).HasTitle = true;
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Частота появления";

            //подписи по оси Y
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).HasTitle = true;
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Исходные символы Виженера";
            
            XL1.Visible = true;

            //-------------------------------------------

            XL1.Sheets.Add();
            XL1.ActiveSheet.Range["A1"].Value = "Зашифрованный символ Виженера:";
            XL1.ActiveSheet.Range["B1"].Value = "Частота:";

            Yach = 2;
            for (int i = 0; i < colFx2.Length; i++)
            {
                if (colFx2[i] != 0)
                {
                    XL1.ActiveSheet.Range["A" + Yach].Value = $"{characters[i]}";
                    XL1.ActiveSheet.Range["B" + Yach].Value = Convert.ToInt32(colFx2[i]);
                    Yach++;
                }
            }

            XL1.Charts.Add();   ///доб. новую диаграмму
            XL1.ActiveChart.ChartType = XL.XlChartType.xlColumnClustered;///столбчатая

            //подписи по оси X
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).HasTitle = true;
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Частота появления";

            //подписи по оси Y
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).HasTitle = true;
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Шифросимволы Цезаря";
            
            XL1.Visible = true;
        }




        /*
        public void FillTable(char[,] table)
        {
            for (int i = 1; i <= 26; i++)
                table[i, 1] = table[1, i] = Convert.ToChar('A' + i - 1);

            for (int i = 2; i <= 26; i++)
                for (int j = 2; j <= 26; j++)
                    if (table[i - 1, j].Equals('Z'))
                        table[i, j] = 'A';
                    else
                        table[i, j] = Convert.ToChar(table[i - 1, j] + 1);
        }

        public string BuildKey(int msgLenght, string key)
        {
            while (key.Length < msgLenght)
            {
                if (key.Length < msgLenght - key.Length)
                    key += key;
                else key += key.Substring(0, msgLenght - key.Length);
            }

            return key;
        }

        public int[] CalculatePosition(string str)
        {
            char[] strChar = str.ToCharArray();
            int[] pos = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                pos[i] = strChar[i] - 'A';
                Console.WriteLine(pos[i]);
            }

            return pos;
        }



        public string Encrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            int[] messagePos = CalculatePosition(message);
            int[] keyPos = CalculatePosition(key);

            for (int i = 0; i < keyPos.Length; i++)
            {
                cipher += table[keyPos[i], messagePos[i]];
            }
            return cipher;
        }




        public string Decrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            char[] messageChar = message.ToCharArray();
            int[] keyPos = CalculatePosition(key);

            for (int i = 0; i < keyPos.Length; i++)
            {

                for (int j = 1; j <= 26; j++)
                {
                    if (table[keyPos[i], j].Equals(messageChar[i]))
                    {
                        cipher += table[1, j];
                        break;
                    }
                }
            }
            return cipher;
        }
        */
    }
}
