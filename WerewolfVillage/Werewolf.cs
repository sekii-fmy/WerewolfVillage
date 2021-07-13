using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WerewolfVillage
{
    class Werewolf
    {
        //Village village = new Village();
        static int seed = Environment.TickCount;
        static Random rnd = new Random(seed);
        GameData gameData;
        List<Agent> geneList = new List<Agent>();
        List<Village> villageList = new List<Village>();
        List<List<Agent>> villagers = new List<List<Agent>>();

        int oneGenerationGames = Form1.generationNumbers;
        int villageNumbers = Form1.villageNumbers;
        int generationNumbers = Form1.generationNumbers;

        static double mutation_p = 0.01;        //突然変異率
        static double exchangeGeneration = 5;      //交流までの世代数

        public Werewolf()
        {

        }

        /// <summary>
        /// 自己対戦の進行
        /// </summary>
        public void startAutoGame()
        {
            int generation = 0;
            int exchange = 0;
            generateGene();
            generateVillage();
            printSurvivalAgent(generation);

            while (generation < generationNumbers) {
                for (int i = 0; i < villageNumbers; i++)
                {
                    startNewGeneration(i, generation);
                }
                generation++;
                exchange++;
                selection(generation, exchange);
                if(exchange >= exchangeGeneration && generation < generationNumbers)
                {
                    exchangeGene();
                    exchange = 0;
                }
                printSurvivalAgent(generation);

            };
        }

        /// <summary>
        /// 個体の生成
        /// </summary>
        public void generateGene()
        {
            for(int i = 0; i < Form1.num_villager * villageNumbers; i++)
            {
                geneList.Add(new Agent(new GeneOfParameter(), i));
            }
        }

        /// <summary>
        /// 村の生成
        /// </summary>
        public void generateVillage()
        {
            int k = 0;
            for (int i = 0; i < villageNumbers; i++)
            {
                List<Agent> villager = new List<Agent>();
                //GeneOfParameter gene = new GeneOfParameter(); //消す
                do
                {
                    //villager.Add(new Agent(gene, (k % 15)));  //消す
                    villager.Add(geneList[k]);
                    k++;
                } while (k < (i + 1) * Form1.num_villager);
                villageList.Add(new Village(villager));
                villagers.Add(villager);
            }
        }

        /// <summary>
        /// 一つの村で世代数分対戦を行う
        /// </summary>
        /// <param name="num"></param>
        public void startNewGeneration(int num, int generation)
        {
            int num_NowGame = 0;

            while (num_NowGame < Form1.oneGenerationGames)
            {
                villageList[num] = new Village(villagers[num]);
                villageList[num].startGame();
                File.WriteAllText(@"./GameData_Village/GameData_Village"+ num + "_Generation" + generation + "_Game" + num_NowGame + ".txt", Form1.writeText);
                File.WriteAllText(@"./LogText/Logtext_Village" + num + "_Generation" + generation + "_Game" + num_NowGame + ".txt", Form1.printText);
                File.WriteAllText(@"./ResultVoteText/ResultVoteText_Village" + num + "_Generation" + generation + "_Game" + num_NowGame + ".txt", Form1.resultVoteText);
                File.WriteAllText(@"./ResultVoteAndRaidText/ResultVoteAndRaidText_Village" + num + "_Generation" + generation + "_Game" + num_NowGame + ".txt", Form1.resultVoteAndRaidText);
                Form1.printText = null;
                Form1.writeText = null;
                Form1.resultVoteText = null;
                Form1.resultVoteAndRaidText = null;
                Console.Write("Village" + num + ", Generation" + generation + ", Game" +num_NowGame + "\r\n");
                num_NowGame++;
            }
            resultsOutput(num, generation);
        }

        /// <summary>
        /// 淘汰・交差
        /// </summary>
        public void selection(int generation, int exc)
        {
            for (int i = 0; i < villageNumbers; i++)
            {
                for (int p = 0; p < Form1.num_villager; p++)
                {
                    ResultOfMatch result = villageList[i].agentList[p].resultMatch;
                    if ((result.num_game - result.num_rolePlay[4] - result.num_rolePlay[5]) != 0)
                    {
                        villageList[i].agentList[p].resultMatch.villagePoint = result.villagePoint / (result.num_game - result.num_rolePlay[4] - result.num_rolePlay[5]);
                    }
                    else
                    {
                        villageList[i].agentList[p].resultMatch.villagePoint = 0;
                    }

                    if ((result.num_rolePlay[4] + result.num_rolePlay[5]) != 0)
                    {
                        villageList[i].agentList[p].resultMatch.wolfPoint = result.wolfPoint / (result.num_rolePlay[4] + result.num_rolePlay[5]);
                    }
                    else
                    {
                        villageList[i].agentList[p].resultMatch.wolfPoint = 0;
                    }
                }
                villageList[i].agentList.Sort((a, b) => (b.resultMatch.villagePoint) - (a.resultMatch.villagePoint));
                Agent village_1st = copyAgent(villageList[i].agentList[0], villageList[i].agentList[0].resultMatch.playerNum);
                Agent village_2nd = copyAgent(villageList[i].agentList[1], villageList[i].agentList[1].resultMatch.playerNum);

                villageList[i].agentList.Sort((a, b) => (b.resultMatch.wolfPoint) - (a.resultMatch.wolfPoint));
                Agent wolf_1st = copyAgent(villageList[i].agentList[0], villageList[i].agentList[0].resultMatch.playerNum);
                Agent wolf_2nd = copyAgent(villageList[i].agentList[1], villageList[i].agentList[1].resultMatch.playerNum);
                //スコアでソート

                villageList[i].agentList[0] = village_1st;       //村ポイント1位
                //villageList[i].agentList[1] = village_2nd;       //村ポイント2位
                villageList[i].agentList[1] = wolf_1st;          //人狼ポイント1位
                //villageList[i].agentList[3] = wolf_2nd;          //人狼ポイント2位

                villageList[i].agentList[2] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[1], villageList[i].agentList[0]), villageList[i].agentList[2].resultMatch.playerNum);
                villageList[i].agentList[3] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[1], villageList[i].agentList[0]), villageList[i].agentList[3].resultMatch.playerNum);
                villageList[i].agentList[4] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[1], villageList[i].agentList[0]), villageList[i].agentList[4].resultMatch.playerNum);
                //村1位,人狼1位の子孫3個体

                /*
                villageList[i].agentList[4] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[0]), villageList[i].agentList[4].resultMatch.playerNum);
                villageList[i].agentList[5] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[0]), villageList[i].agentList[5].resultMatch.playerNum);
                villageList[i].agentList[6] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[0]), villageList[i].agentList[6].resultMatch.playerNum);
                villageList[i].agentList[7] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[2]), villageList[i].agentList[7].resultMatch.playerNum);
                villageList[i].agentList[8] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[2]), villageList[i].agentList[8].resultMatch.playerNum);
                villageList[i].agentList[9] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[2], villageList[i].agentList[2]), villageList[i].agentList[9].resultMatch.playerNum);
                //村1位,人狼1位の子孫6個体

                villageList[i].agentList[10] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[3], villageList[i].agentList[0]), villageList[i].agentList[10].resultMatch.playerNum);
                villageList[i].agentList[11] = new Agent(cross(villageList[i].agentList[0], villageList[i].agentList[3], villageList[i].agentList[3]), villageList[i].agentList[11].resultMatch.playerNum);
                //村1位,人狼2位の子孫2個体

                villageList[i].agentList[12] = new Agent(cross(villageList[i].agentList[1], villageList[i].agentList[2], villageList[i].agentList[1]), villageList[i].agentList[12].resultMatch.playerNum);
                villageList[i].agentList[13] = new Agent(cross(villageList[i].agentList[1], villageList[i].agentList[2], villageList[i].agentList[2]), villageList[i].agentList[13].resultMatch.playerNum);
                //村2位,人狼1位の子孫2個体
                
                if (rnd.Next() % 2 == 0)
                {
                    villageList[i].agentList[14] = new Agent(cross(villageList[i].agentList[1], villageList[i].agentList[3], villageList[i].agentList[1]), villageList[i].agentList[14].resultMatch.playerNum);
                }
                else
                {
                    villageList[i].agentList[14] = new Agent(cross(villageList[i].agentList[1], villageList[i].agentList[3], villageList[i].agentList[3]), villageList[i].agentList[14].resultMatch.playerNum);
                }
                //村2位,人狼2位の子孫1個体
                */

                for (int k = 0; k < Form1.num_villager; k++)
                {
                    //一定確率で、ランダムに突然変異する
                    if (rnd.NextDouble() < mutation_p)
                    {
                        villageList[i].agentList[k].parameter = mutation(villageList[i].agentList[k].parameter);
                    }
                }

                for (int m = 0; m < Form1.num_villager; m++)
                {
                    villageList[i].agentList[m].resultMatch.ResetResult();
                }
                //スコアのリセット
            }
        }

        /// <summary>
        /// 個体の複製
        /// </summary>
        public Agent copyAgent(Agent agent, int agentNum)
        {
            return new Agent(agent.parameter, agentNum);
        }

        /// <summary>
        /// 交叉
        /// 2個体の人狼側パラメータ、村人側パラメータ、共用パラメータをそれぞれ遺伝した個体を生成する
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rank"></param>
        public GeneOfParameter cross(Agent villageAgent, Agent wolfAgent, Agent common )
        {
            GeneOfParameter gene = new GeneOfParameter();
            gene.parameter[0] = wolfAgent.parameter.parameter[0];
            gene.parameter[1] = wolfAgent.parameter.parameter[1];
            gene.parameter[2] = wolfAgent.parameter.parameter[2];
            gene.parameter[3] = wolfAgent.parameter.parameter[3];
            gene.parameter[4] = wolfAgent.parameter.parameter[4];
            gene.parameter[5] = wolfAgent.parameter.parameter[5];
            gene.parameter[6] = wolfAgent.parameter.parameter[6];
            gene.parameter[7] = wolfAgent.parameter.parameter[7];
            gene.parameter[40] = wolfAgent.parameter.parameter[40];
            gene.parameter[41] = wolfAgent.parameter.parameter[41];
            gene.parameter[56] = wolfAgent.parameter.parameter[56];
            gene.parameter[65] = wolfAgent.parameter.parameter[65];
            gene.parameter[66] = wolfAgent.parameter.parameter[66];
            gene.parameter[85] = wolfAgent.parameter.parameter[85];
            gene.parameter[86] = wolfAgent.parameter.parameter[86];
            gene.parameter[87] = wolfAgent.parameter.parameter[87];

            gene.parameter[39] = villageAgent.parameter.parameter[39];
            gene.parameter[44] = villageAgent.parameter.parameter[44];
            gene.parameter[45] = villageAgent.parameter.parameter[45];
            gene.parameter[46] = villageAgent.parameter.parameter[46];
            gene.parameter[47] = villageAgent.parameter.parameter[47];
            gene.parameter[49] = villageAgent.parameter.parameter[49];
            gene.parameter[51] = villageAgent.parameter.parameter[51];
            gene.parameter[53] = villageAgent.parameter.parameter[53];
            gene.parameter[55] = villageAgent.parameter.parameter[55];
            gene.parameter[58] = villageAgent.parameter.parameter[58];
            gene.parameter[60] = villageAgent.parameter.parameter[60];
            gene.parameter[67] = villageAgent.parameter.parameter[67];
            gene.parameter[68] = villageAgent.parameter.parameter[68];
            gene.parameter[69] = villageAgent.parameter.parameter[69];
            gene.parameter[70] = villageAgent.parameter.parameter[70];
            gene.parameter[73] = villageAgent.parameter.parameter[73];
            gene.parameter[75] = villageAgent.parameter.parameter[75];
            gene.parameter[77] = villageAgent.parameter.parameter[77];
            gene.parameter[80] = villageAgent.parameter.parameter[80];
            gene.parameter[81] = villageAgent.parameter.parameter[81];
            gene.parameter[82] = villageAgent.parameter.parameter[82];
            gene.parameter[83] = villageAgent.parameter.parameter[83];
            gene.parameter[84] = villageAgent.parameter.parameter[84];

            gene.parameter[9] = common.parameter.parameter[9];
            gene.parameter[10] = common.parameter.parameter[10];
            gene.parameter[11] = common.parameter.parameter[11];
            gene.parameter[12] = common.parameter.parameter[12];
            gene.parameter[13] = common.parameter.parameter[13];
            gene.parameter[14] = common.parameter.parameter[14];
            gene.parameter[15] = common.parameter.parameter[15];
            gene.parameter[16] = common.parameter.parameter[16];
            gene.parameter[17] = common.parameter.parameter[17];
            gene.parameter[18] = common.parameter.parameter[18];
            gene.parameter[19] = common.parameter.parameter[19];
            gene.parameter[20] = common.parameter.parameter[20];
            gene.parameter[21] = common.parameter.parameter[21];
            gene.parameter[22] = common.parameter.parameter[22];
            gene.parameter[23] = common.parameter.parameter[23];
            gene.parameter[24] = common.parameter.parameter[24];
            gene.parameter[25] = common.parameter.parameter[25];
            gene.parameter[26] = common.parameter.parameter[26];
            gene.parameter[27] = common.parameter.parameter[27];
            gene.parameter[28] = common.parameter.parameter[28];
            gene.parameter[29] = common.parameter.parameter[29];
            gene.parameter[30] = common.parameter.parameter[30];
            gene.parameter[31] = common.parameter.parameter[31];
            gene.parameter[32] = common.parameter.parameter[32];
            gene.parameter[33] = common.parameter.parameter[33];
            gene.parameter[34] = common.parameter.parameter[34];
            gene.parameter[35] = common.parameter.parameter[35];
            gene.parameter[36] = common.parameter.parameter[36];
            gene.parameter[37] = common.parameter.parameter[37];
            gene.parameter[38] = common.parameter.parameter[38];
            gene.parameter[42] = common.parameter.parameter[42];
            gene.parameter[43] = common.parameter.parameter[43];
            gene.parameter[48] = common.parameter.parameter[48];
            gene.parameter[50] = common.parameter.parameter[50];
            gene.parameter[52] = common.parameter.parameter[52];
            gene.parameter[54] = common.parameter.parameter[54];
            gene.parameter[57] = common.parameter.parameter[57];
            gene.parameter[59] = common.parameter.parameter[59];
            gene.parameter[61] = common.parameter.parameter[61];
            gene.parameter[62] = common.parameter.parameter[62];
            gene.parameter[63] = common.parameter.parameter[63];
            gene.parameter[64] = common.parameter.parameter[64];
            gene.parameter[71] = common.parameter.parameter[71];
            gene.parameter[72] = common.parameter.parameter[72];
            gene.parameter[74] = common.parameter.parameter[74];
            gene.parameter[76] = common.parameter.parameter[76];
            gene.parameter[78] = common.parameter.parameter[78];
            gene.parameter[79] = common.parameter.parameter[79];

            return gene;
        }
        
        /// <summary>
        /// 突然変異
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public GeneOfParameter mutation(GeneOfParameter gene)
        {
            gene.parameter[rnd.Next(0, GeneOfParameter.length_gene)] = rnd.NextDouble();

            return gene;
        }

        /// <summary>
        /// 外の島との交流
        /// 村ごとに半数のエージェントを選び、ランダムに振り分ける
        /// </summary>
        public void exchangeGene()
        {
            int rndary;

            List<Agent> exAgent = new List<Agent>();
            for(int i = 0; i < villageNumbers; i++)
            {
                for (int k = 0; k < (Form1.num_villager / 2); k++)
                {
                    rndary = rnd.Next(0, villageList[i].agentList.Count - 1);
                    exAgent.Add(villageList[i].agentList[rndary]);
                    villageList[i].agentList.RemoveAt(rndary);
                }
            }

            exAgent = exAgent.OrderBy(i => Guid.NewGuid()).ToList();
            int num = 0;

            for (int i = 0; i < villageNumbers; i++)
            {
                for (int k = 0; k < (Form1.num_villager / 2); k++)
                {
                    villageList[i].agentList.Add(exAgent[num]);
                    num++;
                }
            }
            Console.Write("exchange\r\n");
        }



        public void resultsOutput(int villageNum, int generation)
        {
            string str = null;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                ResultOfMatch result = villageList[villageNum].agentList[i].resultMatch;
                str += "PlayerNumber = " + i + "\r\n"
                     + "num_game : " + result.num_game+ "\r\n"
                     + "num_win : " + result.num_win + "\r\n"
                     + "num_rolePlay : ";
                for(int k = 0; k < 6; k++)
                {
                    str += result.num_rolePlay[k] + ", ";
                }
                str += "\r\n"
                     + "num_roleWin : ";
                for (int k = 0; k < 6; k++)
                {
                    str += result.num_roleWin[k] + ", ";
                }
                str += "\r\n" + "VillagePoint : " + result.villagePoint;
                str += "\r\n" + "WolfPoint : " + result.wolfPoint;
                str += "\r\n\r\n";
            }
            File.WriteAllText(@"./Total_ResultOfMatch/Result_village"+ villageNum + "Generation" + generation +".txt", str);
        }


        public void printSurvivalAgent(int generation)
        {
            for (int i = 0; i < villageNumbers; i++)
            {
                string str = null;
                str += "village" + i + "\r\n";
                for (int k = 0; k < Form1.num_villager; k++)
                {
                    str += "  Agent" + villageList[i].agentList[k].resultMatch.playerNum + "\r\n";
                    str += "  { ";

                    foreach(double value in villageList[i].agentList[k].parameter.parameter){
                        str += value + ", ";
                    }

                    str += "}\r\n"; 
                }
                str += "\r\n";
                File.WriteAllText(@"./AgentGene/Gene_Village" + i + "Generation" + generation + ".txt", str);
            }
        }


        /// <summary>
        /// ゲームデータの読み込み
        /// </summary>
        public void startWerewolf()
        {

            //ゲームデータ読み込み開始
            //プロローグまで
            //gameData = village.startInference();

            //出力開始
            //village.makeOutput();
            Console.Write("end to output \r\n \r\n");
        }

        public void nextDayStart()
        {
            if (gameData == null) return;

            //次の日の読み込み開始
            //gameData = village.nextInference(gameData);

            //出力開始
            //village.makeOutput();
            Console.Write("end to output \r\n \r\n");
        }

    }
}
