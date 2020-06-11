using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WerewolfVillage
{
    class MentalSpace
    {
        string name;

        public List<Agent> mentalList = new List<Agent>();

        public MentalSpace(string name)
        {
            this.name = name;
        }


        /// <summary>
        /// 推論
        /// 入力は状況
        /// </summary>
        public void inference()
        {

        }




        /// <summary>
        /// 対応表と信頼度の出力
        /// </summary>
        public void printList()
        {
            string str = "エージェント" + name + "'s MentalSpace \r\n";

            for (int j = 0; j < Form1.num_villager; j++)
            {
                str = str + "エージェント名:" + mentalList[j].name.ToString() + ",\r\n信頼度:"
                    + mentalList[j].reliability.ToString() + "\r\n対応表:\r\n";

                for (int k = 0; k < Form1.num_villager; k++)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        str = str + mentalList[j].oppositeTable.table[k,m].ToString() + ", ";
                    }

                    str = str + "\r\n";

                }
            }
            print(str);
        }

        public void print(string str)
        {
            File.AppendAllText(@"./LogText_Agent"+name+".txt", str + "\r\n");
            Form1.printText += str;
        }
    }
}
