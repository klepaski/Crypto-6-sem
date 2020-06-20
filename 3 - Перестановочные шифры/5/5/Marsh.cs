using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XL = Microsoft.Office.Interop.Excel;


namespace _5
{
    class Marsh
    {
        static char[] characters = new char[] {'a','ą','b','c','ć','d','e','ę','f','g','h','i','j','k',
                                               'l','ł','m','n','ń','o','ó','p','r','s','ś','t','u','w',
                                               'y','z','ź','ż'};
        static int N = characters.Length;
        int[] colFx1 = new int[N];
        int[] colFx2 = new int[N];

        /*юлячистяковаалександровна    =  25
        key = 6

        msgToArray:
        [0] - юлячис
        [1] - тякова
        [2] - алекса
        [3] - ндровн
        [4] - а
        */

        public string Encrypt(string msg, int key)
        {
            ///кол-во вхождений
            for (int i = 0; i < N; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
            {
                colFx1[i] = msg.Where(el => el == characters[i]).Count();                    ///есть вхожд - вывод букву + кол-во
            }


            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key) + 1];

            for (int i = 0; i < (msg.Length / key) + 1; i++)
            {
                if (msg.Length - i * key <= key)
                {
                    msgInArray[i] = msg.Substring(i * key);
                    Console.WriteLine("msgInArray[" + i + "] = " + msgInArray[i]);
                    break;
                }
                else
                {
                    msgInArray[i] = msg.Substring(i * key, key);
                    Console.WriteLine("msgInArray[" + i + "] = " + msgInArray[i]);
                }
            }

            /*
             result = ютана + лялд + якер + чоко + ивсв + саан
            */

            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < msgInArray.Length-1; k++)
                {
                    if (msgInArray[k].Length <= i) continue;
                    result += msgInArray[k].Substring(i, 1);
                }
            }

            return result;
        }




        public string Decrypt(string msg, int key)
        {
            //ютанал
            //ялдяке
            //рчокос
            //аан

            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key) + 1];

            for (int i = 0; i < (msg.Length / key) + 1; i++)
            {
                if (msg.Length - i * key <= key)
                {
                    msgInArray[i] = msg.Substring(i * key);
                    break;
                }
                else
                {
                    msgInArray[i] = msg.Substring(i * key, key);
                }
            }
            
            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < msgInArray.Length-1; k++)
                {
                    if (msgInArray[k].Length <= i) continue;
                    result += msgInArray[k].Substring(i, 1);
                }
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
        }
    }
}
