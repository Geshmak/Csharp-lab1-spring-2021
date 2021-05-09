using ClassLibrary1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Lab1V4Kabanov
{
    class PropertyClass:IDataErrorInfo, INotifyPropertyChanged
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxMagnF"));
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
                info = value.Trim();
                
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


        public PropertyClass(ref V4MainCollection  V4mc)
        {
            V4MC = V4mc;
            list = V4MC.list;
            MaxMagnF = V4MC.MaxMagnF;
        }

        /*public PropertyClass()
        {
            //list = new List<V4Data>();
        }*/

        public void AddV4DC()
        {
            Random rand = new Random();
            V4DataCollection V4DC = new V4DataCollection(info, frequency);
            V4DC.InitRandom(nitems, (float)0.5, (float)0.5, maxvalue,minvalue, rand);
            V4MC.Add(V4DC);
            MaxValue = 0;
            MinValue = 0;
            Frequency = 0;
            Info = "";
            NItems = 0;
        }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "Frequency":
                        if (Frequency < 0)
                            msg = "freq < 0";
                        break;
                    case "Info":
                        if (Info == null)
                            msg = "Info  null";
                        else if (Info.Length == 0)
                            msg = "Info length";
                        else
                        {
                            bool flag = true;
                            foreach (V4Data elem in list)
                            {
                                if (elem.Info == Info && flag) 
                                    flag = false;
                            }
                            if (!flag) msg = "already contains";
                        }
                        
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
        
       

    }
}
