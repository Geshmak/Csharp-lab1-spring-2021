using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Collections;
using System.Globalization;

namespace ClassLibrary1
{
    [Serializable]
    public class V4DataOnGrid : V4Data, IEnumerable<DataItem>
    {
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enum(Massiv, Grid);
        }
        public IEnumerator<DataItem> GetEnumerator()
        {
            return new Enum(Massiv, Grid);
        }
        public class Enum : IEnumerator<DataItem>
        {
            private Grid2D grid;
            private Complex[,] mas;
            private int x = 0, y = 0;
            public Enum(Complex[,] c, Grid2D g)
            {
                grid = g;
                mas = c;
            }

            public DataItem Current
            {
                get
                {
                    Vector2 temp = new Vector2(x * grid.StepX, y * grid.StepY);

                    return new DataItem(temp, mas[x, y]);
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                mas = null;
            }
            public void Reset()
            {
                x = 0;
                y = 0;
            }
            public bool MoveNext()
            {
                if (x == mas.GetLength(1) - 1)
                {
                    y++;
                    x = 0;
                }
                else
                    x++;
                return y < mas.GetLength(0);
            }

        }
        /*
         *Первая строка информация
         *Вторая частота
         *дальше для сетки через пробел: шагХ,шагY,кол-воX элемнтов сетки,кол-воY элементов сетки 
          потом массив веществ и действительная часть элемента через запятую 
          элементы через точку запятую 
          в первой строке первая строка массива итд
         */
        public V4DataOnGrid(string filename) : base("", 0)
        {
            FileStream fs = null;
            string[] mas, mas2;
            //Complex[,] NMassiv = 0;
            try
            {
                CultureInfo ci = new CultureInfo("ru-RU");
                fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadLine();
                if (line == null)
                    throw new Exception("empty(no Info)");
                Info = line;
                line = sr.ReadLine();
                if (line == null)
                    throw new Exception("no Frequency");
                Frequency = Convert.ToDouble(line,ci);
                line = sr.ReadLine();
                mas = line.Split(' ');
                if (mas.Length != 4)
                {
                    throw new Exception("wrong format: x,y");
                }
                Grid = new Grid2D(Convert.ToSingle(mas[0],ci), Convert.ToSingle(mas[1],ci), Convert.ToInt32(mas[2]), Convert.ToInt32(mas[3]));
                Massiv = new Complex[Grid.NumberX, Grid.NumberY];
                for (int i = 0; i < Grid.NumberX; i++)
                {
                    line = sr.ReadLine();
                    if (line == null)
                        throw new Exception("too soon");
                    mas = line.Split(';');
                    if (mas.Length != Grid.NumberY)
                    {
                        throw new Exception("wrong matrix");
                    }
                    for (int j = 0; j < Grid.NumberY; j++)
                    {
                        mas2 = mas[j].Split(',');
                        //mas2[1] = mas2[1].Remove(0, 1);
                        Massiv[i, j] = new Complex(Convert.ToSingle(mas2[0]), Convert.ToSingle(mas2[1]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        public Grid2D Grid { get; set; }
        public Complex[,] Massiv { get; set; }
        public V4DataOnGrid(string inf, double freq, Grid2D grd) : base(inf, freq)
        {
            Grid = grd;
            Massiv = new Complex[Grid.NumberX, Grid.NumberY];
        }
        public void InitRandom(double minValue, double maxValue,/*добавил сюда*/  Random rand)
        {
            //Random rand = new Random();
            int row = Massiv.GetLength(0);
            int coll = Massiv.GetLength(1);
            double real;
            double comp;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    real = rand.NextDouble() * (maxValue - minValue) + minValue;
                    comp = rand.NextDouble() * (maxValue - minValue) + minValue;
                    Massiv[i, j] = new Complex(real, comp);
                }
            }
        }
        public override string ToString()
        {
            return "V4DataOnGrid:\n Info) " + Info.ToString() + "\n Frequency) " + Frequency.ToString() +
                "\n Grid: size) " + Grid.NumberX.ToString() + " " + Grid.NumberY.ToString() +
                "\n step x,y) " + Grid.StepX.ToString() + "," + Grid.StepY.ToString();
        }
        public override string ToLongString()
        {
            string line = ToString();
            int row = Massiv.GetLength(0);
            int coll = Massiv.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    line += "\n(" + i.ToString() + "," + j.ToString() + ") - " + Massiv[i, j].ToString();
                }
                line += "\n";
            }
            return line;
        }
        public override string ToLongString(string format)
        {
            string line = "V4DataOnGrid:\n Info) " + Info.ToString() + "\n Frequency) " + Frequency.ToString(format) +
                "\n Grid: size) " + Grid.NumberX.ToString() + " " + Grid.NumberY.ToString() +
                "\n step x,y) " + Grid.StepX.ToString(format) + "," + Grid.StepY.ToString(format);
            int row = Massiv.GetLength(0);
            int coll = Massiv.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    line += "\n(" + i.ToString() + "," + j.ToString() + ") - " + Massiv[i, j].ToString(format) + " mag:   " + Massiv[i,j].Magnitude;
                }
                line += "\n";
            }
            return line;
        }

        public override Complex[] NearMax(float eps)
        {
            double Max = Massiv[0, 0].Magnitude;
            int row = Massiv.GetLength(0);
            int coll = Massiv.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    if (Max < Massiv[i, j].Magnitude)
                        Max = Massiv[i, j].Magnitude;
                }
            }
            Complex[] newmass = new Complex[5];
            int massln = 5;
            int count = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    if (Max - Massiv[i, j].Magnitude <= eps)
                    {
                        if (count >= massln)
                        {
                            massln += 5;
                            Array.Resize(ref newmass, massln);
                        }
                        newmass[count++] = Massiv[i, j];
                    }

                }
            }
            if (count != massln)
                Array.Resize(ref newmass, count);
            return newmass;
        }
        public static explicit operator V4DataCollection(V4DataOnGrid var)
        {
            V4DataCollection temp = new V4DataCollection(var.Info, var.Frequency);
            temp.Dict = new Dictionary<System.Numerics.Vector2, System.Numerics.Complex>();

            int row = var.Massiv.GetLength(0);
            int coll = var.Massiv.GetLength(1);
            Vector2 Coordinate;
            Complex Value;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    Coordinate = new Vector2(i * var.Grid.StepX, j * var.Grid.StepY);
                    Value = var.Massiv[i, j];
                    temp.Dict.Add(Coordinate, Value);
                }
            }
            return temp;
        }
    }
}
