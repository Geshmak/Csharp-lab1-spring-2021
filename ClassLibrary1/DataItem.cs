
using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace ClassLibrary1
{
    [Serializable]
    public struct DataItem:ISerializable
    {
        public void GetObjectData(SerializationInfo info,StreamingContext context)
        {
            info.AddValue("CPX", CoordPoint.X);
            info.AddValue("CPY", CoordPoint.Y);
            info.AddValue("EFC", ElectFieldCompl);
        }
        public DataItem(SerializationInfo info,StreamingContext context)
        {
            float x = info.GetSingle("CPX");
            float y = info.GetSingle("CPY");
            CoordPoint = new Vector2(x, y);
            ElectFieldCompl =(Complex)info.GetValue("EFC", typeof(Complex));
        }
        public Vector2 CoordPoint { get; set; }
        public Complex ElectFieldCompl { get; set; }
        public DataItem(Vector2 Cp, Complex Efc)
        {
            CoordPoint = Cp;
            ElectFieldCompl = Efc;
        }
        public override string ToString()
        {
            return "CoordPoint: " + CoordPoint.ToString() + " "
                + "ElectFieldCompl: " + ElectFieldCompl.ToString();
        }
        public string ToString(string format)
        {
            return "CoordPoint: " + CoordPoint.ToString() + " "
                 + "ElectFieldCompl: " + ElectFieldCompl.ToString(format) + " "
                 + "Magnitude: " + ElectFieldCompl.Magnitude.ToString(format);
        }
    }
}
