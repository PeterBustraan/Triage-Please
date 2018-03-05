using System.Collections;
using System.IO;
using System;

namespace Complications
{
    public sealed class DiseaseCoder
    {
        //make sure I'm not bogging down ram anymore than I already will 
        private static readonly DiseaseCoder _instance;
        public static DiseaseCoder SingleTon { get { return _instance; } }

        private static ArrayList Diseases = new ArrayList();

        static DiseaseCoder()
        {
            _instance = new DiseaseCoder();
        }

        private DiseaseCoder () { Init(); }

        private void Init()
        {
            //TODO convert to SQL input later
            using (var csv = new StreamReader(@"Data/Sample Diseases.csv"))
            {
                while (!csv.EndOfStream)
                {
                    var line = csv.ReadLine();
                    var values = line.Split(',');

                    Diseases.Add(new int[] { int.Parse(values[1]), int.Parse(values[2]) });
                }
            }
        }

        public int[] getAilment()
        {
            Random rnd = new Random();
            int[] survivalStats;

            int selected = rnd.Next(Diseases.Count);

            survivalStats = (int[])Diseases[selected];

            return survivalStats;
        } 
    }
}
