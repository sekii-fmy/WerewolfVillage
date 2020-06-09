using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    class Village
    {
        public List<Agent> agentList = new List<Agent>();

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
        public void newVillage()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList.Add(generateAgent(i, i.ToString()));
            }

            for (int j = 0; j < Form1.num_villager; j++)
            {
                agentList[j].generateMentalAgent(ref agentList);
            }

        }

        /// <summary>
        /// 住人の生成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Agent generateAgent(int num, string name)
        {
            Agent agent = new Agent(name);
         
            //あとでvillagerの生成をいじって役職配分をランダムにする
            //いまは占、狼、狂、村、村の順に生成
            switch (num)
            {
                case 0:
                    agent.role = Role.SEER;
                    break;
                case 1:
                    agent.role = Role.WEREWOLF;
                    break;
                case 2:
                    agent.role = Role.POSSESSED;
                    break;
                default:
                    agent.role = Role.VILLAGER;
                    break;
            }
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
