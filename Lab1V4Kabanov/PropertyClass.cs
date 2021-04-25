using ClassLibrary1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Lab1V4Kabanov
{
    class PropertyClass:IDataErrorInfo, INotifyPropertyChanged, IEnumerable<V4Data>
    {
        private V4MainCollection V4MC;
        public List<V4Data> list;
        private string info;
        private double frequency;
        private int nitems;
        private float minvalue;
        private float maxvalue;
        private Complex MaxMagn;
        public Complex MaxMagnF
        {
            get
            {
                return MaxMagn;
            }
            set
            {
                MaxMagn = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxMagnF"));
            }
        }
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info"));
            }
        }
        public double Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Frequency"));
            }
        }
        public int NItems
        {
            get
            {
                return nitems;
            }
            set
            {
                nitems = value;
               
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NItems"));
            }
        }
        public float MinValue
        {
            get
            {
                return minvalue;
            }
            set
            {
                minvalue = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinValue"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxValue"));
            }
        }
        public float MaxValue
        {
            get
            {
                return maxvalue;
            }
            set
            {
                maxvalue = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinValue"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxValue"));
            }
        }

        public string Error => throw new NotImplementedException();

        public void PCUpt(V4MainCollection V4mc)
        {
            V4MC = V4mc;
            list = V4MC.list;
            MaxMagnF = V4MC.MaxMagnF;
        }
        public PropertyClass(V4MainCollection V4mc)
        {
            V4MC = V4mc;
            list = V4MC.list;
            MaxMagnF = V4MC.MaxMagnF;
        }

        public PropertyClass()
        {
            //list = new List<V4Data>();
        }

        public void AddV4DC()
        {
            Random rand = new Random();
            V4DataCollection V4DC = new V4DataCollection(info, frequency);
            V4DC.InitRandom(nitems, (float)0.5, (float)0.5, maxvalue,minvalue, rand);
            V4MC.Add(V4DC);
        }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "Frequency":                        
                        break;
                    case "Info":
                        if (Info == null)
                            msg = "Info  null";
                        else if (Info.Length == 0)
                            msg = "Info length";
                        else if (V4MC.list.Find(x => x.Info.Contains(Info))!=null)
                            msg = "already contains";
                        break;
                    case "NItems":
                        if (NItems <= 1 || NItems >= 5)
                            msg = "NItems less than 1 or more than 5 ";
                        break;
                    case "MinValue":
                    case "MaxValue":
                        if (MinValue >= MaxValue) 
                            msg = "MinValue must be less than MaxValue";
                        break;
                    default:
                        break;
                    
                }
                return msg;
            }
        }

        

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            string line = "V4MainCollection: " + "\n";
            foreach (V4Data item in list)
            {
                line += item.ToString();
            }
            return line;
        }
        public string ToLongString(string format)
        {
            string line = "V4MainCollection: " + "\n";
            foreach (V4Data item in list)
            {
                line += item.ToLongString(format);
            }
            return line;
        }
        public IEnumerator<V4Data> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        /**/
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

    }
}
