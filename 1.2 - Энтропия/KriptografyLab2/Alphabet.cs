using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XL = Microsoft.Office.Interop.Excel;

namespace KriptografyLab2
{
    class Alphabet
    {
        int[] colFx;
        double[] verSim;

        char[] korean = { 'ㅏ','ㅑ', 'ㅓ','ㅕ','ㅗ','ㅛ','ㅜ','ㅠ','ㅣ','ㅡ','ㄱ','ㄴ','ㄷ','ㄹ','ㅁ',
                          'ㅂ','ㅅ','ㅇ','ㅈ','ㅎ','ㅋ','ㅌ','ㅍ','ㅊ','ㄲ','ㄸ','ㅃ','ㅆ','ㅉ',
                          'ㅐ','ㅔ','ㅒ','ㅖ','ㅚ','ㅟ','ㅘ','ㅝ','ㅙ','ㅞ','ㅢ'};

        char[] cyrillic = {'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч',
                           'Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'};
        char[] latin = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Y', 'Z' };
        char[] binary = {'0','1'};
        
        public char[] Korean { get => korean; set => korean = value; }
        public char[] Cyrillic { get => cyrillic; set => cyrillic = value; }
        public char[] Latin { get => latin; set => latin = value; }
        public char[] Binary { get => binary; set => binary = value; }



        public double Entropy(String file,char [] alf)
        {
            colFx = new int[alf.Length]; // абвгд - 0023005

            using (StreamWriter sw = new StreamWriter("otvet.txt", true, System.Text.Encoding.Unicode))
            {
                if (alf == Korean) sw.WriteLine("\nКоличество вхождений символа:");
                for (int i = 0; i < alf.Length; i++)                                                        ///каждая буква алфавита провер. кол-во вхожд во фразе
                {
                    colFx[i] = file.ToUpper().Where(el => el == alf[i]).Count();                            ///кол-во вхожд буквы во фразе
                    if (colFx[i] != 0)
                        sw.WriteLine(colFx[i] + " " + alf[i]);                                              ///есть вхожд - вывод букву + кол-во
                }



                if (alf==Korean) sw.WriteLine("Вероятность появления символа:");
                verSim = new double[alf.Length];
                for (int i = 0; i < alf.Length; i++)
                {
                    if (colFx[i] != 0)
                    {
                        verSim[i] = Math.Round((double)colFx[i] / file.Length, 2);  ///кол-во вхожд / длина стр
                        sw.WriteLine(verSim[i] + " " + alf[i]);
                    }
                }
                double res = 0;
                for (int i = 0; i < alf.Length; i++)
                {
                    if (verSim[i] != 0)
                        res += verSim[i] * Math.Log(verSim[i], 2);
                }
                return (-res);
            }
        }



        public double kolInf(double ResEntr,int kolSimv)
        {
            return ResEntr * (double)kolSimv;
        }



        public string GetBytes(String str)
        {
            String strB = "";
            for (int i = 0; i < str.Length; i++)
            {
                strB += Convert.ToString( (int)str[i], 2 ) + " ";
            }
            return strB;
        }


        public double EfectEntropy(double verError)
        {
            /// He(A) = 1 - H(Y|X)
            /// H(Y|X) = - p log2(p) - q log2(q)
            return 1 - (-verError * Math.Log(verError, 2) - (1 - verError) * Math.Log((1 - verError), 2));
        }


       
        public void BuildGistogram(char[] alf)
        {
            XL.Application XL1 = new XL.Application();  ///объяв объект
            XL1.Workbooks.Add();                         ///доб. рабочую книгу

            XL1.ActiveSheet.Range["A1"].Value = "Символ:";
            XL1.ActiveSheet.Range["B1"].Value = "Частота:";


            int Yach = 2;
            for (int i = 0; i < colFx.Length; i++)
            {
                if (colFx[i] != 0)
                {
                    XL1.ActiveSheet.Range["A" + Yach].Value = $"{alf[i]}";
                    XL1.ActiveSheet.Range["B" + Yach].Value = Convert.ToInt32(colFx[i]);
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
            XL1.ActiveChart.Axes(XL.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Символы";

            XL1.ActiveChart.Export(@"D:\myFile.jpg");
            XL1.Visible = true;
        }
    }
}
