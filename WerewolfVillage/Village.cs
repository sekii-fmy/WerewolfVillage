using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    class Village
    {
        public List<Agent> agentList;                   //エージェントリスト
        int day;                                        //日付
        int serialNum;                                  //ゲームデータの通し番号
        int utterNum;                                   //発言の通し番号
        List<Agent> fortuneAgent;                       //占われたエージェント
        List<Agent> bodyGuardAgent;                     //護衛されたエージェント
        List<Agent> raidAgent;                          //襲撃されたエージェント
        List<Agent> executeAgent;                       //吊られたエージェント
        List<Agent> aliveAgent;                         //

        /// <summary>
        /// 始めに実行。村の住人の生成
        /// </summary>
        /// <param name="num_Villager"></param>
        public Village(List<Agent> villagers)
        {
            agentList = villagers;
            day = -1;
            serialNum = 1;
            utterNum = 1;
            fortuneAgent = new List<Agent>();
            bodyGuardAgent = new List<Agent>();
            raidAgent = new List<Agent>();
            executeAgent = new List<Agent>();
            aliveAgent = new List<Agent>();

            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].generateMentalSpace(Form1.AgentName[i]);
            }

            distributeRole();

            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.generateMentalAgent(agentList);
            }

        }

        /// <summary>
        /// 役職の割り振り
        /// </summary>
        public void distributeRole()
        {
            int[] roleArray = randomArray(Form1.num_villager);
            
            for(int i = 0; i < Form1.num_villager; i++)
            {
                switch (roleArray[i])
                {
                    case 1:
                        agentList[i].role = Role.占い師;
                        break;
                    case 2:
                        agentList[i].role = Role.霊能者;
                        break;
                    case 3:
                        agentList[i].role = Role.狩人;
                        break;
                    case 4:
                        agentList[i].role = Role.狂人;
                        break;
                    case 5:
                        agentList[i].role = Role.人狼;
                        break;
                    case 6:
                        agentList[i].role = Role.人狼;
                        break;
                    case 7:
                        agentList[i].role = Role.人狼;
                        break;
                    default:
                        agentList[i].role = Role.村人;
                        break;
                }
            }
        }


        public void startGame()
        {
            whisper();
            nextDay();
            fortune();

            while (game_Continue())
            {
                dayTime();
                if (!game_Continue()) break;
                night();
                nextDay();
            }
        }

        public void nextDay()
        {
            day++;
            Form1.printText += "\r\n" + "------------------------------"
                + dayNum(day) + "------------------------------" + "\r\n\r\n";
            Form1.resultVoteText += "\r\n" + "------------------------------"
                + dayNum(day) + "------------------------------" + "\r\n\r\n";
            Form1.resultVoteAndRaidText += "\r\n" + "------------------------------"
                + dayNum(day) + "------------------------------" + "\r\n\r\n";
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.dayStart();
            }
        }

        public void dayTime()
        {
            string target_Vote;

            talk();

            target_Vote = vote();
            generateGameData(new GameData
            {
                SerialNum = serialNum++.ToString(),
                Day = dayNum(day),
                Name = "処刑結果",
                Public = "白",
                Tag = "処刑結果",
                Guess = target_Vote,
            });
            execute(target_Vote);
            if (day != -1) makeOutput();
        }

        public void night()
        {
            whisper();
            fortune();
            psychic();
            bodyGuard();
            raid();
        }

        /// <summary>
        /// ゲームデータの読み込み（プロローグ）
        /// </summary>
        /// <returns></returns>
        public GameData startInference()
        {
            GameData gameData = CsvRead.ReadNextline();

            Form1.printText += "\r\n" + "------------------------------"
                + gameData.Day + "------------------------------" + "\r\n\r\n";
            Console.Write(gameData.Day + "\r\n");

            while (gameData.Day == "プロローグ")
            {
                generateGameData(gameData);
                gameData = CsvRead.ReadNextline();
            }
            for (int i = 0; i < Form1.num_villager; i++)
            {
                agentList[i].mentalSpace.dataRead();
            }
            return gameData;
        }

        /// <summary>
        /// ゲームデータの読み込み（一日目以降）
        /// </summary>
        /// <param name="preData"></param>
        /// <returns></returns>
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
                generateGameData(gameData);
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

        public void execute(string name)
        {
            getAgent(name).alive = false;
            executeAgent.Add(getAgent(name));
        }

        /// <summary>
        /// 占い
        /// </summary>
        public void fortune()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].role == Role.占い師 && agentList[i].alive)
                {
                    fortuneAgent.Insert(day,getAgent(agentList[i].getFortune()));
                    if (getAgent(fortuneAgent[day].name).role == Role.人狼)
                    {
                        generateGameData(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[i].name,
                            Public = "灰",
                            Tag = "占い",
                            Guess = Complement(agentList[i].name) + ":"
                              + Complement(fortuneAgent[day].name) + ":人狼",

                        });
                    }
                    else
                    {
                        generateGameData(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[i].name,
                            Public = "灰",
                            Tag = "占い",
                            Guess = Complement(agentList[i].name) + ":"
                              + Complement(fortuneAgent[day].name) + ":人間",

                        });
                    }
                }
            }
        }

        /// <summary>
        /// 霊能
        /// </summary>
        public void psychic()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].role == Role.霊能者 && agentList[i].alive)
                {
                    if(executeAgent.Count == 0)     return;

                    if (getAgent(executeAgent[executeAgent.Count - 1].name).role == Role.人狼)
                    {
                        generateGameData(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[i].name,
                            Public = "灰",
                            Tag = "霊能",
                            Guess = Complement(agentList[i].name) + ":"
                                + Complement(executeAgent[executeAgent.Count - 1].name) + ":人狼",

                        });
                    }
                    else
                    {
                        generateGameData(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[i].name,
                            Public = "灰",
                            Tag = "霊能",
                            Guess = Complement(agentList[i].name) + ":"
                                + Complement(executeAgent[executeAgent.Count - 1].name) + ":人間",

                        });
                    }
                }
            }
        }

        /// <summary>
        /// 狩人
        /// </summary>
        public void bodyGuard()
        {
            Form1.resultVoteAndRaidText += "BodyGuard \r\n";
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].role == Role.狩人 && agentList[i].alive)
                {
                    bodyGuardAgent.Insert(day, getAgent(agentList[i].getBodyGuard()));

                    generateGameData(new GameData
                    {
                        SerialNum = serialNum++.ToString(),
                        Day = dayNum(day),
                        Name = "狩人",
                        Public = "灰",
                        Tag = "狩人",
                        Guess = Complement(agentList[i].name) + ":"
                              + Complement(bodyGuardAgent[day].name),

                    });
                    Form1.resultVoteAndRaidText += agentList[i].name + "(" + agentList[i].role + ")" + "    →    "
                         + bodyGuardAgent[day].name + "(" + getAgent(bodyGuardAgent[day].name).role + ")" + "\r\n";
                }
            }
        }

        /// <summary>
        /// 投票
        /// 各プレイヤーから投票先を募り、最も投票されたプレイヤー名を返す
        /// 最多票が同数のとき、その中からランダムで選択する
        /// </summary>
        /// <returns></returns>
        public string vote()
        {
            string[] voteArray = new string[Form1.num_villager];
            int[] voteCountArray = new int[Form1.num_villager];
            int max_vote = 0;
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            Form1.resultVoteText += "Vote \r\n";
            Form1.resultVoteAndRaidText += "Vote \r\n";

            GameData voteData = new GameData
            {
                SerialNum = serialNum++.ToString(),
                Day = dayNum(day),
                Name = "投票",
                Public = "灰",
                Tag = "投票",
                Guess = null,
            };

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].alive)
                {
                    voteArray[i] = agentList[i].getVote();
                    voteCountArray[getAgentNum(voteArray[i])]++;

                    voteData.Guess += Complement(agentList[i].name) + ":"
                        + Complement(voteArray[i]) + " ";

                    Form1.resultVoteText += agentList[i].name + "(" + agentList[i].role + ")" + "    →    "
                         + voteArray[i] + "(" + getAgent(voteArray[i]).role + ")" + "\r\n";
                    Form1.resultVoteAndRaidText += agentList[i].name + "(" + agentList[i].role + ")" + "    →    "
                         + voteArray[i] + "(" + getAgent(voteArray[i]).role + ")" + "\r\n";

                    if (getAgent(voteArray[i]).role == Role.人狼 && agentList[i].role != Role.人狼)
                    {
                        agentList[i].resultMatch.result_VOTEtoWOLF();
                    }
                }
            }
            generateGameData(voteData);

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (voteCountArray[i] > voteCountArray[max_vote] && agentList[i].alive)
                {
                    max_vote = i;
                }
            }

            int rndInt = rnd.Next(0, Form1.num_villager);

            while (voteCountArray[rndInt] != voteCountArray[max_vote])
            {
                rndInt = rnd.Next(0, Form1.num_villager);
            }

            Form1.resultVoteText += "投票結果 : " + agentList[rndInt].name 
                + "(" + agentList[rndInt].role + ")\r\n\r\n";
            Form1.resultVoteAndRaidText += "投票結果 : " + agentList[rndInt].name
                + "(" + agentList[rndInt].role + ")\r\n\r\n";

            return agentList[rndInt].name;
        }

        /// <summary>
        /// 襲撃
        /// 人狼から襲撃先を募り、最も票が多かったプレイヤーを襲撃する
        /// 最多票が同数の時、その中からランダムで選択する
        /// </summary>
        public void raid()
        {
            string[] raidArray = new string[Form1.num_villager];
            int[] raidCountVote = new int[Form1.num_villager];
            int max_vote = 0;
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            Form1.resultVoteAndRaidText += "Raid \r\n";

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].role == Role.人狼 && agentList[i].alive)
                {
                    raidArray[i] = agentList[i].getRaid();
                    raidCountVote[getAgentNum(raidArray[i])]++;
                    Form1.resultVoteAndRaidText += agentList[i].name + "(" + agentList[i].role + ")" + "    →    "
                         + raidArray[i] + "(" + getAgent(raidArray[i]).role + ")" + "\r\n";
                }
            }

            for (int j = 0; j < Form1.num_villager; j++)
            {
                if (raidCountVote[j] > raidCountVote[max_vote] && agentList[j].alive)
                {
                    max_vote = j;
                }
            }

            int rndInt = rnd.Next(0, Form1.num_villager);

            while (raidCountVote[rndInt] != raidCountVote[max_vote])
            {
                rndInt = rnd.Next(0, Form1.num_villager);
            }

            if (bodyGuardAgent.Count <=  day || bodyGuardAgent.Count == 0)
            {
                generateGameData(new GameData
                {
                    SerialNum = serialNum++.ToString(),
                    Day = dayNum(day),
                    Name = "襲撃結果",
                    Public = "白",
                    Tag = "襲撃結果",
                    Guess = agentList[rndInt].name,

                });
                raidAgent.Add(agentList[rndInt]);
                agentList[rndInt].alive = false;
                Form1.resultVoteAndRaidText += "襲撃結果 : " + agentList[rndInt].name 
                    +"(" + agentList[rndInt].role + ")\r\n\r\n";
            }
            else if (agentList[rndInt].name == bodyGuardAgent[day].name)
            {
                generateGameData(new GameData
                {
                    SerialNum = serialNum++.ToString(),
                    Day = dayNum(day),
                    Name = "襲撃結果",
                    Public = "白",
                    Tag = "襲撃結果",
                    Guess = "失敗",

                });
                Form1.resultVoteAndRaidText += "襲撃結果 : " + agentList[rndInt].name 
                    + "(" + agentList[rndInt].role + ")(失敗)\r\n\r\n";
            }
            else
            {
                generateGameData(new GameData
                {
                    SerialNum = serialNum++.ToString(),
                    Day = dayNum(day),
                    Name = "襲撃結果",
                    Public = "白",
                    Tag = "襲撃結果",
                    Guess = agentList[rndInt].name,

                });
                raidAgent.Add(agentList[rndInt]);
                agentList[rndInt].alive = false;
                Form1.resultVoteAndRaidText += "襲撃結果 : " + agentList[rndInt].name 
                    + "(" + agentList[rndInt].role + ")\r\n\r\n";
            }
        }

        /// <summary>
        /// 囁き（赤チャット）
        /// 人狼プレイヤーから囁きを獲得する
        /// 1日に20発言するか、overを返したら囁きは終了
        /// </summary>
        public void whisper()
        {
            int[] num_TodayWhisper = new int[Form1.num_villager];
            bool[] over = new bool[Form1.num_villager];

            for(int i = 0; i < Form1.num_villager; i++)
            {
                if(agentList[i].role != Role.人狼 || !agentList[i].alive)
                {
                    over[i] = true;
                }
            }

            while (!checkAllTrue(over))
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (!over[i])
                    {
                        GameData gameData = agentList[i].getWhisper(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[i].name,
                            Public = "赤",
                        });

                        generateGameData(gameData);
                        num_TodayWhisper[i]++;

                        if (gameData.Tag == "Over" || num_TodayWhisper[i] >= 20)
                        {
                            over[i] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 会話
        /// 各プレイヤーから発話を獲得する
        /// </summary>
        public void talk()
        {
            int[] order_utter;
            int[] num_TodayUtterance = new int[Form1.num_villager];
            bool[] over = new bool[Form1.num_villager];

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (!agentList[i].alive)
                {
                    over[i] = true;
                }
            }

            while (!checkAllTrue(over))
            {
                order_utter = randomArray(Form1.num_villager);
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (!over[order_utter[i]])
                    {
                        GameData gameData = agentList[order_utter[i]].getUtterance(new GameData
                        {
                            SerialNum = serialNum++.ToString(),
                            UtterNum = utterNum++.ToString(),
                            Day = dayNum(day),
                            Name = agentList[order_utter[i]].name,
                            Tag = "",
                            Guess = "",
                            Disire = "",
                            Confirm = "",
                            Reliability = "",
                            Line = "",
                            Questtion = "",
                            Memo = "",
                            Role = "",
                            Utterance = "",

                        });

                        generateGameData(gameData);
                        num_TodayUtterance[order_utter[i]]++;

                        if (gameData.Tag == "Over" || num_TodayUtterance[order_utter[i]] >= 20)
                        {
                            over[order_utter[i]] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ゲームが継続するか判断する
        /// 生存している人狼以外の数が生存している人狼の数より大きいとき継続
        /// </summary>
        /// <returns></returns>
        public bool game_Continue()
        {
            int num_AliveVillager = 0;
            int num_AliveWolf = 0;

            for(int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].alive && agentList[i].role != Role.人狼)
                {
                    num_AliveVillager++;
                }
                else if(agentList[i].alive && agentList[i].role == Role.人狼)
                {
                    num_AliveWolf++;
                }
            }

            if (num_AliveVillager > num_AliveWolf && num_AliveWolf != 0)
            {
                return true;
            }
            else if (num_AliveWolf == 0)
            {
                gameEND(true);
                return false;
            }
            else
            {
                gameEND(false);
                return false;
            }
        }

        /// <summary>
        /// ゲーム終了時のアクション
        /// </summary>
        public void gameEND(bool winVillage)
        {           
            if (winVillage) {
                generateGameData(new GameData
                {
                    SerialNum = serialNum++.ToString(),
                    Day = "エピローグ",
                    Name = "ゲーム結果",
                    Public = "白",
                    Tag = "ゲーム結果",
                    Guess = "村勝利",
                });
                villagerWin();
            }
            else{
                generateGameData(new GameData
                {
                    SerialNum = serialNum++.ToString(),
                    Day = "エピローグ",
                    Name = "ゲーム結果",
                    Public = "白",
                    Tag = "ゲーム結果",
                    Guess = "人狼勝利",
                });
                wolfWin();
            }
            makeOutput();
        }

        /// <summary>
        /// ゲームデータのエージェントへの送信
        /// </summary>
        /// <param name="gameData"></param>
        public void generateGameData(GameData gameData)
        {
            //gameData = new TagToUtterance().convertTagToUtterance(gameData);
            for (int i = 0; i < Form1.num_villager; i++)
            {
                GameData data = new GameData
                {
                    SerialNum = gameData.SerialNum,
                    UtterNum = gameData.UtterNum,
                    Day = gameData.Day,
                    Name = gameData.Name,
                    Public = gameData.Public,
                    Tag = "",
                    Guess = "",
                    Disire = "",
                    Confirm = "",
                    Reliability = "",
                    Line = "",
                    Questtion = "",
                    Memo = "",
                    Role = "",
                    Utterance = gameData.Utterance,
                };
                if (agentList[i].alive)
                {
                    if (gameData.Public == "白")
                    {
                        agentList[i].mentalSpace.receiveData(gameData);
                        //agentList[i].mentalSpace.receiveUtteracne(data);
                    }
                    else if (gameData.Public == "灰" && gameData.Name == agentList[i].name)
                    {
                        agentList[i].mentalSpace.receiveData(gameData);
                        //agentList[i].mentalSpace.receiveUtteracne(data);
                    }
                    else if (gameData.Public == "赤" && agentList[i].role == Role.人狼)
                    {
                        agentList[i].mentalSpace.receiveData(gameData);
                        //agentList[i].mentalSpace.receiveUtteracne(data);
                    }
                }
            }
            outputGamedata(gameData);
        }


        /// <summary>
        /// キャラ名一文字の略称を開く
        /// </summary>
        public string Abbreviation(string abb)
        {
            switch (abb)
            {
                case "楽":
                    return "楽天家ゲルト";
                case "長":
                    return "村長ヴァルター";
                case "老":
                    return "老人モーリッツ";
                case "神":
                    return "神父ジムゾン";
                case "樵":
                    return "木こりトーマス";
                case "旅":
                    return "旅人ニコラス";
                case "者":
                    return "ならず者ディーター";
                case "年":
                    return "少年ペーター";
                case "妙":
                    return "少女リーザ";
                case "商":
                    return "行商人アルビン";
                case "羊":
                    return "羊飼いカタリナ";
                case "屋":
                    return "パン屋オットー";
                case "青":
                    return "青年ヨアヒム";
                case "娘":
                    return "村娘パメラ";
                case "農":
                    return "農夫ヤコブ";
                case "宿":
                    return "宿屋の女主人レジーナ";
                case "修":
                    return "シスターフリーデル";
                case "仕":
                    return "仕立て屋エルナ";
                case "書":
                    return "司書クララ";
                case "兵":
                    return "負傷兵シモン";
                default:
                    return null;
            }
        }

        public string Complement(string comp)
        {
            switch (comp)
            {
                case "楽天家ゲルト":
                    return "楽";
                case "村長ヴァルター":
                    return "長";
                case "老人モーリッツ":
                    return "老";
                case "神父ジムゾン":
                    return "神";
                case "木こりトーマス":
                    return "樵";
                case "旅人ニコラス":
                    return "旅";
                case "ならず者ディーター":
                    return "者";
                case "少年ペーター":
                    return "年";
                case "少女リーザ":
                    return "妙";
                case "行商人アルビン":
                    return "商";
                case "羊飼いカタリナ":
                    return "羊";
                case "パン屋オットー":
                    return "屋";
                case "青年ヨアヒム":
                    return "青";
                case "村娘パメラ":
                    return "娘";
                case "農夫ヤコブ":
                    return "農";
                case "宿屋の女主人レジーナ":
                    return "宿";
                case "シスターフリーデル":
                    return "修";
                case "仕立て屋エルナ":
                    return "仕";
                case "司書クララ":
                    return "書";
                case "負傷兵シモン":
                    return "兵";
                default:
                    return null;
            }
        }

        public string dayNum(int day)
        {
            switch (day)
            {
                case -1:
                    return "プロローグ";
                case -2:
                    return "エピローグ";
                default:
                    return (day + 1) + "日目";                    
            }
        }


        /// <summary>
        /// キャラネームと同じ名前の
        /// メンタルエージェントオブジェクトを返す
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Agent getAgent(string name)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (name == agentList[i].name)
                {
                    return agentList[i];
                }
            }
            return null;
        }

        public int getAgentNum(string name)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (name == agentList[i].name)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool checkAllTrue(bool[] value)
        {
            for(int i = 0; i < value.Length; i++)
            {
                if(value[i] == false)
                {
                    return false;
                }
            }

            return true;
        }

        public int[] randomArray(int max)
        {
            int[] ary = new int[max];
            for(int i = 0;i < max; i++)
            {
                ary[i] = i;
            }
            int[] ary2 = ary.OrderBy(i => Guid.NewGuid()).ToArray();

            return ary2;
        }

        public void outputGamedata(GameData gameData)
        {
            Form1.writeText +=
                  gameData.SerialNum + ","
                + gameData.UtterNum + ","
                + gameData.Day + ","
                + gameData.Name + ","
                + gameData.Public + ","
                + gameData.Tag + ","
                + gameData.Guess + ","
                + gameData.Disire + "."
                + gameData.Confirm + ","
                + gameData.Reliability + "."
                + gameData.Line + ","
                + gameData.Questtion + ","
                + gameData.Memo + ","
                + gameData.Role + ","
                + gameData.Utterance + "\r\n";

            consoleOutPut(gameData);
        }

        public void consoleOutPut(GameData gameData)
        {
            Console.Write(
                  gameData.SerialNum + ","
                + gameData.UtterNum + ","
                + gameData.Day + ","
                + gameData.Name + ","
                + gameData.Public + ","
                + gameData.Tag + ","
                + gameData.Guess + ","
                + gameData.Disire + "."
                + gameData.Confirm + ","
                + gameData.Reliability + "."
                + gameData.Line + ","
                + gameData.Questtion + ","
                + gameData.Memo + ","
                + gameData.Role + ","
                + gameData.Utterance + "\r\n");
        }

        public void villagerWin()
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if(agentList[i].role == Role.村人 || agentList[i].role == Role.占い師
                    || agentList[i].role == Role.狩人 || agentList[i].role == Role.霊能者)
                {
                    agentList[i].resultMatch.num_game++;
                    agentList[i].resultMatch.num_win++;
                    agentList[i].resultMatch.num_rolePlay[(int)agentList[i].role]++;
                    agentList[i].resultMatch.num_roleWin[(int)agentList[i].role]++;
                    agentList[i].resultMatch.result_WIN();
                    if (agentList[i].alive)
                    {
                        agentList[i].resultMatch.result_VILLAGERALIVE();
                    }
                }
                else
                {
                    agentList[i].resultMatch.num_game++;
                    agentList[i].resultMatch.num_rolePlay[(int)agentList[i].role]++;
                    agentList[i].resultMatch.result_LOSE();
                    if (agentList[i].alive)
                    {
                        agentList[i].resultMatch.result_WOLFALIVE();
                    }
                }
            }
        }

        public void wolfWin()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].role == Role.人狼 || agentList[i].role == Role.狂人)
                {
                    agentList[i].resultMatch.num_game++;
                    agentList[i].resultMatch.num_win++;
                    agentList[i].resultMatch.num_rolePlay[(int)agentList[i].role]++;
                    agentList[i].resultMatch.num_roleWin[(int)agentList[i].role]++;
                    agentList[i].resultMatch.result_WIN();
                    if (agentList[i].alive)
                    {
                        agentList[i].resultMatch.result_WOLFALIVE();
                    }
                }
                else
                {
                    agentList[i].resultMatch.num_game++;
                    agentList[i].resultMatch.num_rolePlay[(int)agentList[i].role]++;
                    agentList[i].resultMatch.result_LOSE();
                    if (agentList[i].alive)
                    {
                        agentList[i].resultMatch.result_VILLAGERALIVE();
                    }
                }
            }
        }

        public void makeOutput()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (agentList[i].alive)
                {
                    agentList[i].mentalSpace.printList();
                }
            }
        }
    }
}
