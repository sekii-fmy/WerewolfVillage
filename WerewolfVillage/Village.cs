using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    class Village
    {
        public static List<Agent> agentList = new List<Agent>();

        /// <summary>
        /// 発話オブジェクト
        /// </summary>
        public class Utterance
        {
            public string name;                    //発話者
            public string utterance;               //発話内容
        }

        /// <summary>
        /// 始めに実行。村の住人の生成
        /// </summary>
        /// <param name="num_Villager"></param>
        public Village()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList.Add(generateAgent(Form1.AgentName[i], Form1.AgentRole[i]));
            }

            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.generateMentalAgent();
            }

        }

        /// <summary>
        /// 住人の生成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Agent generateAgent(string name, string agentRole)
        {
            Agent agent = new Agent(name,agentRole);
            return agent;
        }


        public void makeOutput()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.printList();
            }
        }
    }
}
