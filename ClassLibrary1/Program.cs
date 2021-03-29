using System;
using System.Numerics;


namespace ClassLibrary1
{
    
    class Program
    {
        public static void DataC(object sourse, DataChangedEventArgs args)
        {
            Console.WriteLine();
            Console.WriteLine(args.ToString());
        }
        static void Main(string[] args)
        {   
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory().Trim(@"bin\Debug".ToCharArray()) + @"\KEK.txt");
            string path = System.IO.Directory.GetCurrentDirectory() + @"\KEK.txt";//.Trim(@"bin\Debug\".ToCharArray()) + @"\KEK.txt";

            
            V4DataOnGrid test = new V4DataOnGrid(path);
            //Console.WriteLine(test.ToLongString("{0}"));
            






            Grid2D def0 = new Grid2D((float)1.0, (float)1.0, 5, 5);
            V4DataOnGrid TestOnGrid = new V4DataOnGrid("first", 1.0, def0);
            Random rand = new Random();
            TestOnGrid.InitRandom(0, 100, rand);
            //Console.WriteLine(TestOnGrid.ToLongString("-0-"));

            V4DataCollection TestCollection = (V4DataCollection)TestOnGrid; //преобразование типа 
            //Console.WriteLine(TestCollection.ToLongString("_0_"));
            
            V4MainCollection TestMainCollection = new V4MainCollection();
            TestMainCollection.AddDefaults();
            Console.WriteLine(TestMainCollection.ToLongString(""));

           /*
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            V4MainCollection Testlab3 = new V4MainCollection();
            Testlab3.DataChanged += DataC;
            Testlab3.Add(test);
            V4DataCollection testadd2 = new V4DataCollection("add2", 7.6);
            Testlab3.Add(testadd2);
            V4DataCollection testreplace = new V4DataCollection("rep", 5.6);
            Testlab3[0] = testreplace;
            Testlab3[0].CInfo = "TestChangeValue";
            Testlab3.Remove("TestChangeValue", 5.6);
            Console.WriteLine("-------------------------------------------------------------------");
            */
           
            Console.WriteLine("AverAll");
            Console.WriteLine(TestMainCollection.AverAll.ToString());

            Console.WriteLine("NearZero");
            foreach (DataItem i in TestMainCollection.NearZero(2))
            { 
               Console.WriteLine(i.ToString() );
            }

            Console.WriteLine("Ones");
            foreach (Vector2 i in TestMainCollection.Ones)
            {
                Console.WriteLine(i.ToString());
            }


            //Console.WriteLine(TestMainCollection.ToString());
            Complex[] obj;
            foreach (var item in TestMainCollection)
                {
                    //Console.WriteLine("\n");
                    obj = item.NearMax((float)10);
                    for (int i = 0; i < obj.Length; i++)
                    {
                        //Console.WriteLine(obj[i].ToString());
                    }
                }

            
        }
        

    }
}
