using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XL = Microsoft.Office.Interop.Excel;


namespace _3
{
    class Cesar
    {

        static char[] alphabet = new char[] {'a','ą','b','c','ć','d','e','ę','f','g','h','i','j','k',
                                             'l','ł','m','n','ń','o','ó','p','r','s','ś','t','u','w',
                                             'y','z','ź','ż'};
        static int N = alphabet.Length;
        string al = new string(alphabet);

        int[] colFx1 = new int[N];
        int[] colFx2 = new int[N];


        public string Cipher(string input, int key)
        {
            string result = "";
            char[] charArray = input.ToCharArray();
            string ch = new string(charArray);

            ///кол-во вхождений
            for (int i = 0; i < N; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
            {
                colFx1[i] = input.Where(el => el == alphabet[i]).Count();                                          ///есть вхожд - вывод букву + кол-во
            }


            for (int i = 0; i < ch.Length; i++)
            {
                if (al.Contains(charArray[i].ToString()))
                {
                    for (int j = 0; j < al.Length; j++)
                    {
                        if (ch[i] == al[j])
                        {
                            result += ((al[(j + key)%N]).ToString());
                            break;
                        }
                    }
                }
                else
                {
                    result += (input[i].ToString());
                }
            }
            return result;
        }

        public string Decipher(string input, int key)
        {
            string result = "";
            char[] charArray = input.ToCharArray();
            string ch = new string(charArray);

            ///кол-во вхождений
            for (int i = 0; i < N; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
            {
                colFx2[i] = input.Where(el => el == alphabet[i]).Count();                                          ///есть вхожд - вывод букву + кол-во
            }

            for (int i = 0; i < ch.Length; i++)
            {
                if (al.Contains(charArray[i].ToString()))
                {
                    for (int j = 0; j < al.Length; j++)
                    {
                        if (ch[i] == al[j])
                        {
                            result += ((al[(j + N - key)%N]).ToString());
                            break;
                        }
                    }
                }
                else
                {
                    result += (charArray[i].ToString());
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
                    XL1.ActiveSheet.Range["A" + Yach].Value = $"{alphabet[i]}";
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
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Исходные символы Цезаря";

            //XL1.ActiveChart.Export(@"D:\myFile.jpg");
            XL1.Visible = true;

            //-------------------------------------------

            XL1.Sheets.Add();
            XL1.ActiveSheet.Range["A1"].Value = "Зашифрованный символ Цезаря:";
            XL1.ActiveSheet.Range["B1"].Value = "Частота:";

            Yach = 2;
            for (int i = 0; i < colFx2.Length; i++)
            {
                if (colFx2[i] != 0)
                {
                    XL1.ActiveSheet.Range["A" + Yach].Value = $"{alphabet[i]}";
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

            //XL1.ActiveChart.Export(@"D:\myFile.jpg");
            XL1.Visible = true;
        }

    }
}
