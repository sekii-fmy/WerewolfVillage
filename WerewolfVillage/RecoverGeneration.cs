using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class RecoverGeneration
    {
        int num_village = Form1.villageNumbers;
        public List<Agent> ReadGeneFile(int village, int generation, List<Agent> agents)
        {
            double[,] parameter = new double[Form1.num_villager, 94];
            string filename = "AgentGene/Gene_Village" + village + "Generation" + generation + ".txt";
            StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
            Console.Write("Village" + village + "Generarion" + generation + "\r\n");


            string text = sr.ReadLine();    //village**
            text = sr.ReadLine();
            int m = -1;
            while (text != null)
            {
                int i = 0;
                if (!text.Contains("Agent"))
                {
                    foreach(string line in text.Split(','))
                    {
                        string value = line;
                        while (value != "" && (value.Substring(0, 1) == " " || value.Substring(0, 1) == "{" || value.Substring(0, 1) == "}"))
                        {
                            value = value.Remove(0, 1);
                        }
                        if (value != "")
                        {
                            parameter[m,i] = double.Parse(value);
                            i++;
                        }
                    }

                    for(int p = 0; p < 94; p++)
                    {
                        agents[m].parameter.parameter[p] = parameter[m,p];
                    }
                }
                else
                {
                    m++;
                }
                text = sr.ReadLine();
            }


            return agents;
        }

    }
}
