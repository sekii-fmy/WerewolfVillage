using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    class Village
    {
        //エージェントリスト
        public static List<Agent> agentList = new List<Agent>();

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


        public GameData startInference()
        {
            GameData gameData = CsvRead.ReadNextline();

            Form1.printText += "\r\n" + "------------------------------"
                + gameData.Day + "------------------------------" + "\r\n\r\n";
            Console.Write(gameData.Day + "\r\n");

            while (gameData.Day == "プロローグ")
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    agentList[i].mentalSpace.receiveData(gameData);
                }
                gameData = CsvRead.ReadNextline();
            }
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.dataRead();
            }
            return gameData;
        }


        public GameData nextInference(GameData preData)
        {
            GameData gameData = preData;

            Form1.printText += "\r\n" + "------------------------------" 
                + gameData.Day + "------------------------------" + "\r\n\r\n";
            Form1.writeText += "\r\n" + "------------------------------"
                + gameData.Day + "------------------------------" + "\r\n\r\n";
            Console.Write(gameData.Day + "\r\n");

            while (gameData.Day == preData.Day)
            {
                //Console.Write(gameData.SerialNum + "\r\n");
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    agentList[i].mentalSpace.receiveData(gameData);
                }
                gameData = CsvRead.ReadNextline();
                if (gameData == null)
                {
                    Console.Write("THE GAME IS OVER \r\n");
                    break;
                }
            }
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.dataRead();
            }
            return gameData;
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
