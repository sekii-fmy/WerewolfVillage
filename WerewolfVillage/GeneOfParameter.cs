using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class GeneOfParameter
    {
        public static int length_gene = 88;
        public double[] parameter = new double[length_gene];
        static int seed = Environment.TickCount;
        static Random rnd = new System.Random(seed);

        //static double k = 0;

        public GeneOfParameter()
        {
            //k += 0.01;
            for (int i = 0; i < length_gene; i++)
            {
                parameter[i] = rnd.NextDouble();
                //parameter[i] = k; //消す
            }
        }
    }
}
