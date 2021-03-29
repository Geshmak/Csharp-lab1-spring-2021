using System;
using System.ComponentModel;
using System.Numerics;


namespace ClassLibrary1
{
    [Serializable]

    public abstract class V4Data : INotifyPropertyChanged
    {
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void PropertyC(object source, string pname)
        {
            PropertyChanged?.Invoke(source, new PropertyChangedEventArgs(pname));
        }
        public string Info;
        public double Frequency;
        public V4Data(string inf, double freq)
        {
            this.Info = inf;
            this.Frequency = freq;
        }
        public abstract Complex[] NearMax(float eps);
        public abstract string ToLongString();
        public abstract override string ToString();
        public abstract string ToLongString(string format);
        
        public string CInfo
        {
            get { return Info; }
            set
            {
                Info = value;
                PropertyC(this,"СInfo");
            }
        }
        public double CFrequency
        {
            get { return Frequency; }
            set
            {
                Frequency = value;
                PropertyC(this, "CFrequency");
            }
        }
    }
}
