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
        Random rnd = new Random();
        GameData gameData;
        List<Agent> geneList = new List<Agent>();
        List<Village> villageList = new List<Village>();
        List<List<Agent>> villagers = new List<List<Agent>>();
        int oneGenerationGames = Form1.generationNumbers;
        int villageNumbers = Form1.villageNumbers;
        int generationNumbers = Form1.generationNumbers;
        static double mutation_p = 0.001;        //突然変異率

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

            while (generation < generationNumbers) {
                for (int i = 0; i < villageNumbers; i++)
                {
                    startNewGeneration(i, generation);
                }
                generation++;
                exchange++;
                selection(generation, exchange);
                if(exchange <= 10)
                {
                    exchangeGene();
                    exchange = 0;
                }
            };
        }

        /// <summary>
        /// 個体の生成
        /// </summary>
        public void generateGene()
        {
            for(int i = 0; i < 15 * villageNumbers; i++)
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
                do
                {
                    villager.Add(geneList[k]);
                    k++;
                } while (k < (i + 1) * 15);
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
                File.WriteAllText(@"./GameData_Village/Village"+ num + "_GameData" + num_NowGame + "Generation"+ generation +".txt", Form1.writeText);
                File.WriteAllText(@"./LogText/LogText"+ num + "_" + num_NowGame + "Generation" + generation + ".txt", Form1.printText);
                File.WriteAllText(@"./ResultVoteText/ResultVoteText" + num + "_" + num_NowGame + "Generation" + generation + ".txt", Form1.resultVoteText);
                File.WriteAllText(@"./ResultVoteAndRaidText/ResultVoteAndRaidText" + num + "_" + num_NowGame + "Generation" + generation + ".txt", Form1.resultVoteAndRaidText);
                Form1.printText = null;
                Form1.writeText = null;
                Form1.resultVoteText = null;
                Form1.resultVoteAndRaidText = null;
                Console.Write("Village"+ num + "_Game" + num_NowGame + "\r\n");
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
                villageList[i].agentList.Sort((a, b) => b.resultMatch.point - a.resultMatch.point);
                //スコアでソート

                villageList[i].agentList[7] = villageList[i].agentList[0];      //1位の複製
                villageList[i].agentList[8] = villageList[i].agentList[1];      //2位の複製
                villageList[i].agentList[9] = villageList[i].agentList[2];      //3位の複製

                villageList[i].agentList[10] = new Agent(cross(villageList[i].agentList, 6), villageList[i].agentList[10].resultMatch.playerNum);
                villageList[i].agentList[11] = new Agent(cross(villageList[i].agentList, 6), villageList[i].agentList[11].resultMatch.playerNum);
                villageList[i].agentList[12] = new Agent(cross(villageList[i].agentList, 6), villageList[i].agentList[12].resultMatch.playerNum);
                villageList[i].agentList[13] = new Agent(cross(villageList[i].agentList, 6), villageList[i].agentList[13].resultMatch.playerNum);
                //上位個体同士の子孫

                villageList[i].agentList[14] = new Agent(new GeneOfParameter(), villageList[i].agentList[14].resultMatch.playerNum);
                //新しい個体

                for(int k = 0; k < Form1.num_villager; k++)
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
        /// 交叉
        /// リスト内の上位rank位以上のうち2個体をランダムで選択して交叉
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rank"></param>
        public GeneOfParameter cross(List<Agent> list, int rank)
        {
            int[] crossAgent = new int[2] { rnd.Next(0, rank), rnd.Next(0, rank) };
            GeneOfParameter gene = new GeneOfParameter();
            for(int i = 0; i < GeneOfParameter.length_gene; i++)
            {
                gene.parameter[i] = list[crossAgent[rnd.Next(0, 1)]].parameter.parameter[i];
            }

            return gene;
        }
        
        /// <summary>
        /// 突然変異
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public GeneOfParameter mutation(GeneOfParameter gene)
        {
            Random rnd = new Random();
            gene.parameter[rnd.Next(0, GeneOfParameter.length_gene - 1)] = rnd.NextDouble();

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
                for (int k = 0; k < 7; k++)
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
                for (int k = 0; k < 7; k++)
                {
                    villageList[i].agentList.Add(exAgent[num]);
                    num++;
                }
            }
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
                str += "\r\n"+"point : " + result.point;
                str += "\r\n\r\n";
            }

            File.WriteAllText(@"./Total_ResultOfMatch/Result_village"+ villageNum + "Generation" + generation +".txt", str);
        }


        public void printSurvivalAgent()
        {
            string str = null;
            for (int i = 0; i < villageNumbers; i++)
            {
                str += "village" + i + "\r\n";
                for (int k = 0; k < Form1.num_villager; k++)
                {
                    str += "  Agent" + k + "\r\n";
                    str += "  { ";

                    foreach(double value in villageList[k].agentList[i].parameter.parameter){
                        str += value + ", ";
                    }

                    str += "}\r\n"; 
                }
                str += "\r\n";
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
