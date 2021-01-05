using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class GeneOfParameter
    {
        public static int length_gene = 90;
        public double[] parameter = new double[length_gene + 4];
        static int seed = Environment.TickCount;
        static Random rnd = new System.Random(seed);

        static double k = 0;

        public GeneOfParameter()
        {
            for (int i = 0; i < length_gene; i++)
            {
                parameter[i] = rnd.NextDouble();
                parameter[length_gene] = k;
                parameter[length_gene + 1] = k;
                parameter[length_gene + 2] = k;
                parameter[length_gene + 3] = 0;
                //parameter[i] = k; //消す
            }
            k += 0.001;
        }
    }
}
