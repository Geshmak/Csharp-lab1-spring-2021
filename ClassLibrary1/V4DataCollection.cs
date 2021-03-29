using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Collections;
using System.Runtime.Serialization;

namespace ClassLibrary1
{
    [Serializable]
    public class V4DataCollection : V4Data, IEnumerable<DataItem>,ISerializable
    {
        public void GetObjectData(SerializationInfo Sinfo,StreamingContext context)
        {
            float[] x = new float[Dict.Count];
            float[] y = new float[Dict.Count];
            Complex[] comp = new Complex[Dict.Count];
            int j = 0;
            foreach (KeyValuePair<Vector2, Complex> KVPair in Dict)
            {

                x[j] = KVPair.Key.X;
                y[j] = KVPair.Key.Y;
                comp[j++] = KVPair.Value;

            }
            Sinfo.AddValue("X", x);
            Sinfo.AddValue("Y", y);
            Sinfo.AddValue("Cval", comp);
            
            Sinfo.AddValue("INF", Info);
            Sinfo.AddValue("Fr",Frequency);
        }
        public V4DataCollection(SerializationInfo Sinfo, StreamingContext context):
            base((string)Sinfo.GetValue("INF",typeof(string)), (double)Sinfo.GetValue("Fr",typeof(double)))
        {
            float[] x = (float[])Sinfo.GetValue("X", typeof(float[]));
            float[] y = (float[])Sinfo.GetValue("Y", typeof(float[]));
            Complex[] comp = (Complex[])Sinfo.GetValue("Cval", typeof(Complex[]));
            Dict = new Dictionary<Vector2, Complex>();
            for (int i=0; i < x.Length; i++)
            {
                Dict.Add(new Vector2(x[i], y[i]), comp[i]);
            }
            
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enum(Dict);
        }
        public IEnumerator<DataItem> GetEnumerator()
        {
            return new Enum(Dict);
        }
        public class Enum : IEnumerator<DataItem>
        {
            private int x = 0;
            private Dictionary<Vector2, Complex> dict;
            public Enum(Dictionary<Vector2, Complex> d)
            {
                dict = d;
            }
            object IEnumerator.Current => Current;
            public DataItem Current
            {
                get
                {
                    return new DataItem(dict.Keys.ElementAt(x), dict.Values.ElementAt(x));
                }
            }
            public void Dispose()
            {
                dict = null;
            }
            public void Reset()
            {
                x = 0;
            }
            public bool MoveNext()
            {
                x += 1;
                return x < dict.Count;
            }
        }
        public Dictionary<System.Numerics.Vector2, System.Numerics.Complex> Dict { set; get; }
        public V4DataCollection(string inf, double freq) : base(inf, freq)
        {
            this.Dict = new Dictionary<System.Numerics.Vector2, System.Numerics.Complex>();
        }
        public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue,/*добавил сюда*/ Random rand)
        {
            //Random rand = new Random();
            double x;
            double y;
            double real;
            double comp;
            Vector2 Coordinate;
            Complex Value;
            for (int count = 0; count < nItems-1; count++)
            {
                real = rand.NextDouble() * (maxValue - minValue) + minValue;
                comp = rand.NextDouble() * (maxValue - minValue) + minValue;
                x = rand.NextDouble() * xmax;
                y = rand.NextDouble() * ymax;
                Coordinate = new Vector2((float)x, (float)y);
                Value = new Complex(real, comp);
                Dict.Add(Coordinate, Value);
            }
            real = rand.NextDouble() * (maxValue - minValue) + minValue;
            comp = rand.NextDouble() * (maxValue - minValue) + minValue;
            x = 0.5;
            y = 0.5;
            Coordinate = new Vector2((float)x, (float)y);
            Value = new Complex(real, comp);
            Dict.Add(Coordinate, Value);
        }
        public override Complex[] NearMax(float eps)
        {

            double Max = 0;
            foreach (Complex comp in Dict.Values)
            {
                if (comp.Magnitude > Max)
                    Max = comp.Magnitude;
            }

            Complex[] newmass = new Complex[5];
            int massln = 5;
            int count = 0;
            foreach (Complex comp in Dict.Values)
            {
                if (Max - comp.Magnitude <= eps)
                {
                    if (count >= massln)
                    {
                        massln += 5;
                        Array.Resize(ref newmass, massln);
                    }
                    newmass[count++] = comp;
                }
            }
            if (count != massln)
                Array.Resize(ref newmass, count);
            return newmass;
        }
        public override string ToString()
        {
            return "\n\nV4DataCollection: \nInfo) " + Info.ToString() + "\nFrequency) " + Frequency.ToString() +
                "\nDictsize: " + Dict.Count.ToString() + "\n\n";
        }
        public override string ToLongString()
        {
            string line = this.ToString();
            foreach (KeyValuePair<Vector2, Complex> count in Dict)
            {
                line += " (" + count.Key.X.ToString() + "," + count.Key.Y.ToString() + ") - "
                    + count.Value.ToString() + " mag:   " + count.Value.Magnitude + "\n";
            }
            return line;
        }
        public override string ToLongString(string format)
        {
            string line = "\n\nV4DataCollection: \nInfo) " + Info.ToString() + "\nFrequency) " + Frequency.ToString(format) +
                "\nDictsize: " + Dict.Count.ToString() + "\n\n";
            foreach (KeyValuePair<Vector2, Complex> count in Dict)
            {
                line += " (" + count.Key.X.ToString(format) + "," + count.Key.Y.ToString(format) + ") - "
                    + count.Value.ToString()+" mag:   "+count.Value.Magnitude+ "\n";
            }
            return line;
        }
    }
}
