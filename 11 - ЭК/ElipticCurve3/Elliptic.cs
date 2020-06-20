using System;
using System.Threading;

namespace ElipticCurve3
{   
    class Elliptic
    {
        public static int a_1(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }

        static void Main(string[] args)
        {
            #region 1

            Point.DrawFunction();
            Console.WriteLine();

            int xx;
            double yy;
            for (xx = 716; xx < 750; xx++)
            {
                yy = Math.Pow(xx, 3) - xx + 1;
                yy = yy % 751;
                Console.Write($"({xx},{yy}); ");
            }
            
            Point P = new Point(67, 84);
            Point Q = new Point(69, 241);
            Point R = new Point(66, 199);
            int k = 11, l = 5;

            Point p1 = P * k;
            Point p2 = P + Q;
            Point p3 = p1 + (Q * l) + (-R);
            Point p4 = P + (-Q) + R;

            Console.WriteLine("\n\nЗадание 1:");
            Console.WriteLine($"kP          ({p1.x}, {p1.y})");
            Console.WriteLine($"P+Q         ({p2.x}, {p2.y})");
            Console.WriteLine($"kP+lQ-R     ({p3.x}, {p3.y})");
            Console.WriteLine($"P-Q+R       ({p4.x}, {p4.y})\n\n\n");


            #endregion 1

            #region 2

            ///ЭК:  y2 = x3 - x + 1 9mod 751)
            Point m1 = new Point(227, 299); //Ю
            Point m2 = new Point(200, 721); //Л
            Point m3 = new Point(227, 452); //Я
            Point[] M = { m1, m2, m3 };
            Point[] C = new Point[6];
            Point[] M2 = new Point[3];


            Point G = new Point(0,1);
            int d = 50;
            int j = 0;
            Point Q_ = G * d;

            Console.WriteLine("\nЗадание 2:");
            Console.WriteLine($"Тайный ключ: {d}");
            Console.WriteLine($"Открытый ключ: ({Q_.x}, {Q_.y})");

            //Зашифрование
            for (int i = 0; i<3; i++)
            {
                C[j] = G * k; j++;
                C[j] = M[i] + Q_ * k; j++;
            }

            Console.WriteLine($"Открытый текст: ({M[0].x}, {M[0].y}), ({M[1].x}, {M[1].y}), ({M[2].x}, {M[2].y})");
            Console.WriteLine($"Шифротекст:     ({C[0].x}, {C[0].y}), ({C[1].x}, {C[1].y}), ({C[2].x}, {C[2].y}), \n" +
                $"\t\t({C[3].x}, {C[3].y}), ({C[4].x}, {C[4].y}), ({C[5].x}, {C[5].y})");

            //Расшифрование
            M2[0] = C[1] + ((-C[0]) * d);
            M2[1] = C[3] + ((-C[2]) * d);
            M2[2] = C[5] + ((-C[4]) * d);

            Console.WriteLine($"Расшифр текст: ({M2[0].x}, {M2[0].y}), ({M2[1].x}, {M2[1].y}), ({M2[2].x}, {M2[2].y})\n\n\n");

            #endregion 2

            #region 3

            Point G_ = new Point(416, 55);
            int q = 13;
            Point Q_sign = G_ * d;
            int H = 215 % 13; //Ч
            Point kG = G_ * k;


            //Генерация ЭЦП
            int r = (int)kG.x % q;
            if (r == 0) Console.WriteLine("Замените параметр k");

            int t = a_1(k, q);

            int s = (H * t + d * r) % q;
            if (s == 0) Console.WriteLine("Замените параметр k");
            
            Console.WriteLine("Задание 3:");
            Console.WriteLine($"Открытый ключ Q: ({Q_sign.x}, {Q_sign.y})");
            Console.WriteLine($"Точка kG: ({kG.x}, {kG.y})");
            Console.WriteLine($"Хеш: {H}");
            Console.WriteLine($"Отсылаем стороне B (r,s) = ({r},{s})");

            //Верификация ЭЦП
            if (r < 1 || s > q) Console.WriteLine("Легитимность не подтверждена!");
            int w = a_1(s, q);
            int u1 = (w * H) % q;
            int u2 = (w * r) % q;

            Point ver = G_ * u1 + Q_sign * u2;
            int v = (int)ver.x % q;
            if (v != r) Console.WriteLine("Легитимность подтверэжена!");

            
            #endregion 3


            Console.ReadKey();

        }
    }
}
