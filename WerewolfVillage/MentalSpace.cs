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
        Role myRole = Role.村人;
        bool isCO = false;
        bool isSendResultOfFortune = false;
        bool isSendResultOfPsychic = false;
        bool isSendSkip = false;
        bool isSendDisire = false;
        bool isSendEstimate = false;
        bool iswolfCO = false;
        bool isDisireCO = false;
        bool isDisireRaid = false;
        bool isSuccessGuard = false;
        bool[] receiveDisireFortune = new bool[Form1.num_villager];
        bool[] receiveDisireVote = new bool[Form1.num_villager];
        string[] wolfPlayer = new string[3];
        string[] disireCO = new string[3];
        string[] disireRaid = new string[3];
        int[] num_roleCO = new int[6] { 0, 0, 0, 0, 0, 0 };
        OppositeTable myTable = new OppositeTable();

        public class Result_fortune
        {
            public string name_seer;
            public string name_target;
            public bool isHuman;
        }

        public class Result_psychic
        {
            public string name_psychic;
            public string name_target;
            public bool isHuman;
        }

        List<MentalAgent> mentalAgentList = new List<MentalAgent>();            //メンタルエージェントリスト
        List<Result_fortune> fortuneList = new List<Result_fortune>();          //占い結果発言リスト
        List<Result_psychic> psychicList = new List<Result_psychic>();          //霊能結果発言リスト
        List<GameData> gameDataList = new List<GameData>();                     //発話リスト
        List<GameData> fortuneGameDataList = new List<GameData>();              //占い師が受け取る占い結果のリスト
        List<GameData> psychicGameDataList = new List<GameData>();              //霊能者が受け取る霊能結果のリスト
        List<string> bodyGuardList = new List<string>();                        //護衛したプレイヤのリスト
        List<GameData> result_ExecuteList = new List<GameData>();               //処刑結果のリスト
        List<GameData> result_RaidList = new List<GameData>();                  //襲撃結果のリスト


        public MentalSpace(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// メンタルエージェントの生成
        /// </summary>
        public void generateMentalAgent()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                MentalAgent mentalAgent = new MentalAgent(ref Village.agentList, i);
                mentalAgentList.Add(mentalAgent);
            }
        }

        public void dayStart()
        {
            isSendResultOfFortune = false;
            isSendResultOfPsychic = false;
            isSendDisire = false;
            isSendEstimate = false;
            isDisireRaid = false;
            isDisireCO = false;
            isSendSkip = false;
        }

        public void receiveUtteracne(GameData gameData)
        {
            GameData data = new TagToUtterance().convertUtteranceToTag(gameData);
            receiveData(data);
        }

        /// <summary>
        /// ゲームデータを受け取って、信頼度を更新する
        /// </summary>
        /// <param name="gameData"></param>
        public void receiveData(GameData gameData)
        {
            gameDataList.Add(gameData);
            if (gameData.Public == "白")
            {
                string[] values = gameData.Tag.Split(' ');
                foreach (string tag in values)
                {
                    switch (tag)
                    {
                        case "CO":
                            reliabilityCO(gameData);
                            break;
                        case "占い結果":
                            reliabilityFortune(gameData);
                            break;
                        case "霊能結果":
                            reliabilityPsychic(gameData);
                            break;
                        case "処刑結果":
                            resultOfExecution(gameData);
                            break;
                        case "襲撃結果":
                            resultOfRaid(gameData);
                            break;
                        case "信頼度":
                            inferenceReliability(gameData);
                            break;
                    }
                }
                wolfReliability();
                //信頼度が範囲を超えていたら処理する
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    mentalAgentList[i].checkReliability();
                }
            }
            else if (gameData.Public == "赤")
            {
                string[] values = gameData.Tag.Split(' ');
                foreach (string value in values)
                {
                    switch (value)
                    {
                        case "人狼CO":
                            wolfPartnerCO(gameData);
                            break;
                        case "CO希望":
                            wolf_DisireOfCO(gameData);
                            break;
                    }
                }
            }

            dataRead();

        }

        /// <summary>
        /// ゲームデータを始めから読み込み直して、対応表を更新する
        /// </summary>
        public void dataRead()
        {
            string day = "プロローグ";

            for (int i = 0; i < Form1.num_villager; i++)
            {
                mentalAgentList[i].dayStart();
                mentalAgentList[i].oppositeTable.initialTable();
            }

            foreach (GameData data in gameDataList)
            {
                if (data.Day != day)
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        mentalAgentList[i].dayStart();
                    }
                    day = data.Day;
                }

                inference(data);

            }
            intention_fortune();
            intention_BodyGuard();
            intention_Raid();
            decideOfVote();
        }


        /// <summary>
        /// 推論
        /// 入力はゲームデータ
        /// タグ（F列）を読んで対応するメソッドを開く
        /// </summary>
        public void inference(GameData gameData)
        {
            if (gameData.Public == "白")
            {
                string[] values = gameData.Tag.Split(' ');
                foreach (string tag in values)
                {
                    switch (tag)
                    {
                        case "考察":
                            inferenceGuess(gameData);
                            break;
                        case "CO":
                            inferenceCO(gameData);
                            break;
                        case "占い結果":
                            resultOfFortune(gameData);
                            break;
                        case "霊能結果":
                            resultOfPsychic(gameData);
                            break;
                        case "護衛結果":
                            resultOfGuard(gameData);
                            break;
                        case "占い先決定":
                            decisionOfFortune(gameData);
                            break;
                        case "吊り先決定":
                            decisionOfExecution(gameData);
                            break;
                        case "襲撃結果":
                            inferenceRaid(gameData);
                            break;
                        case "ゲーム結果":
                            resultOfGame(gameData);
                            break;
                        case "希望":
                            inferenceDisire(gameData);
                            break;
                        case "確認":
                            inferenceConfirm(gameData);
                            break;
                        case "ライン":
                            inferenceLine(gameData);
                            break;
                        case "質問":
                            inferenceQuestion(gameData);
                            break;
                        case "同調":
                            inferenceAgree(gameData);
                            break;
                        case "反発":
                            inferenceOppose(gameData);
                            break;
                    }
                }
            }
            else if (gameData.Public == "灰")
            {
                string[] values = gameData.Tag.Split(' ');
                foreach (string tag in values)
                {
                    switch (tag)
                    {
                        case "占い":
                            gameDataFortune(gameData);
                            break;
                        case "霊能":
                            gameDataPsychic(gameData);
                            break;
                        case "狩人":
                            gameDataBodyGuard(gameData);
                            break;
                        case "投票":
                            inferenceVote(gameData);
                            break;
                    }
                }
            }
            else if (gameData.Public == "赤")
            {
                string[] values = gameData.Tag.Split(' ');
                foreach (string value in values)
                {
                    switch (value)
                    {
                        case "襲撃希望":
                            wolf_DisireOfRaid(gameData);
                            break;
                    }
                }
            }

            checkWolfNum();
            checkPP();

            //対応表の値が範囲を超えていたら処理する
            for (int i = 0; i < Form1.num_villager; i++)
            {
                mentalAgentList[i].oppositeTable.checkNumberOfTable();
                checkNumRole(ref mentalAgentList[i].oppositeTable.table);
            }

            wolfsInference();
            checkMyRole();

        }

        /// <summary>
        /// 考察
        /// </summary>
        /// <param name="gameData"></param>
        public void reliabilityGuess(GameData gameData)
        {

        }

        /// <summary>
        /// 考察
        /// 発言者の対応表において役職値を上げる（下げる）
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceGuess(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            double soften = 1;
            int invert = 1;
            foreach(string value in values)
            {
                if (value.Contains("CO") != true)   //COの情報でない
                {
                    string[] guess = value.Split(':'); 
                    if(guess[0].Length == 1)
                    {
                        guess[0] = Abbreviation(guess[0]);
                        if (guess[1].Contains("非"))
                        {
                            invert = -1;
                            guess[1] = guess[1].Remove(0, 1);
                        }
                        if (guess[1].Contains("寄り"))
                        {
                            soften = 0.5;
                            guess[1] = guess[1].Substring(0,guess[1].Length - 2);
                        }

                        switch (guess[1])
                        {
                            case "村人":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 0] += 0.5 * invert * soften;   
                                break;
                            case "占い師":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 1] += 0.5 * invert * soften;
                                break;
                            case "霊能者":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 2] += 0.5 * invert * soften;
                                break;
                            case "狩人":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 3] += 0.5 * invert * soften;
                                break;
                            case "狂人":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 4] += 0.5 * invert * soften;
                                break;
                            case "人狼":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 5] += 0.5 * invert * soften;
                                break;
                            case "白":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 0] += 0.365 * invert * soften;   //村人 
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 1] += 0.045 * invert * soften;   //占い師
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 2] += 0.045 * invert * soften;   //霊能者
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 3] += 0.045 * invert * soften;   //狩人
                                break;
                            case "黒":
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 4] += 0.13 * invert * soften;    //狂人
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(guess[0]), 5] += 0.37 * invert * soften;    //人狼
                                break;
                            default:
                                break;

                        }

                    }
                    else if(guess[0].Length > 1)//guess[0].length!=1
                    {
                        string[] roles = guess[1].Split('-');
                        for(int i =0; i < guess[0].Length; i++)
                        {
                            for (int j = 0; j < guess[0].Length; j++)
                            {
                                switch (roles[j])
                                {
                                    case "村人":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 0] += 0.5 * invert * soften;   
                                        break;
                                    case "占い師":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 1] += 0.5 * invert * soften;
                                        break;
                                    case "霊能者":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 2] += 0.5 * invert * soften;
                                        break;
                                    case "狩人":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 3] += 0.5 * invert * soften;
                                        break;
                                    case "狂人":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 4] += 0.5 * invert * soften;
                                        break;
                                    case "人狼":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 5] += 0.5 * invert * soften;
                                        break;
                                    case "白":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 0] += 0.365 * invert * soften;   //村人 
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 1] += 0.045 * invert * soften;   //占い師
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 2] += 0.045 * invert * soften;   //霊能者
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 3] += 0.045 * invert * soften;   //狩人
                                        break;
                                    case "黒":
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 4] += 0.13 * invert * soften;    //狂人
                                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(guess[0].Substring(0, 1))), 5] += 0.37 * invert * soften;    //人狼
                                        break;
                                }

                            }
                            guess[0] = guess[0].Remove(0, 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CO
        /// それぞれの役職にCOしている人数をカウントする
        /// </summary>
        /// <param name="gameData"></param>
        public void reliabilityCO(GameData gameData)
        {
            double rolevalue = 1;
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("CO") == true)       //COの情報である
                {
                    string[] co = value.Split(':');
                    if (co[1].Contains("非"))
                    {
                        rolevalue = -1;
                        co[1] = co[1].Substring(1);
                    }
                    co[0] = Abbreviation(co[0]);
                    co[1] = co[1].Substring(0, co[1].Length - 2); //"CO"を消す
                    switch (co[1])
                    {
                        case "村人":
                            num_roleCO[0]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[0] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[0] = true;
                                }
                            }
                            break;
                        case "占い師":
                            num_roleCO[1]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[1] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[1] = true;
                                }
                            }
                            break;
                        case "霊能者":
                            num_roleCO[2]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[2] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[2] = true;
                                }
                            }
                            break;
                        case "狩人":
                            num_roleCO[3]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[3] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[3] = true;
                                }
                            }
                            break;
                        case "狂人":
                            num_roleCO[4]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[4] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[4] = true;
                                }
                            }
                            break;
                        case "人狼":
                            num_roleCO[5]++;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (rolevalue < 0)
                                {
                                    getAgent(gameData.Name).notCoRole[5] = true;
                                }
                                else
                                {
                                    getAgent(gameData.Name).coRole[5] = true;
                                }
                            }
                            break;
                        default:
                            break;

                    }
                }
            }

            //COの内容に矛盾がないか調べる
            checkCO();
        }

        /// <summary>
        /// CO
        /// COしたプレイヤの対応表において役職値を1、他を0にする
        /// 自分の対応表において役職値を上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceCO(GameData gameData)
        {
            double rolevalue = 1;
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("CO") == true)       //COの情報である
                {
                    string[] co = value.Split(':');
                    if (co[1].Contains("非"))
                    {
                        rolevalue = -1;
                        co[1] = co[1].Substring(1);
                    }
                    co[0] = Abbreviation(co[0]);
                    co[1] = co[1].Substring(0, co[1].Length - 2); //"CO"を消す
                    switch (co[1])
                    {
                        case "村人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 0] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                }
                            }
                            break;
                        case "占い師":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 1] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 1] += 1.0 * rolevalue * getAgent(gameData.Name).reliability / num_roleCO[1];
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "霊能者":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 2] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 2] += 1.0 * rolevalue * getAgent(gameData.Name).reliability / num_roleCO[2];
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "狩人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 3] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 3] += 1.0 * rolevalue * getAgent(gameData.Name).reliability / num_roleCO[3];
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "狂人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 4] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 4] += 1.0 * rolevalue * getAgent(gameData.Name).reliability / num_roleCO[4];
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "人狼":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 5] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 5] += 1.0 * rolevalue * getAgent(gameData.Name).reliability / num_roleCO[5];
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        default:
                            break;

                    }
                }
            }
        }


        /// <summary>
        /// 占い結果
        /// 誰が、誰のことを、なんと占ったかを記録する
        /// </summary>
        /// <param name="gameData"></param>
        public void reliabilityFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    fortuneList.Add(new Result_fortune { name_seer = gameData.Name, name_target = values[0], isHuman = true });
                    //占い師が自分を人間と占ったとき、その占い師の信頼度を下げる
                    //(自分が人狼のとき）
                    if (values[0] == name && (mentalAgentList[getAgentNum(name)].agent.role == Role.人狼))
                    {
                        getAgent(gameData.Name).reliability -= 0.5;
                    }
                    break;
                case "人狼":
                    fortuneList.Add(new Result_fortune { name_seer = gameData.Name, name_target = values[0], isHuman = false });

                    //占い師が自分を人狼と占ったとき、その占い師の信頼度を下げる
                    //(自分が人狼でないとき）
                    if (values[0] == name && (mentalAgentList[getAgentNum(name)].agent.role != Role.人狼))
                    {
                        getAgent(gameData.Name).reliability -= 0.5;
                    }
                    break;
            }
        }

        /// <summary>
        /// 占い結果
        /// 占い師の対尾表において占い結果に合わせて対応表を書き換える
        /// 自分の対応表において占い師の信頼度に合わせて役職値を変更する
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), 5] = 0;
                    //占い師の対応表において人狼値を0

                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (!(values[0] == name) || !(getAgentNum(name) == i))
                        {
                            mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] -= 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[1]);
                        }
                    }
                    //占い師以外の対応表において人狼値を減らす
                    break;

                case "人狼":
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), 5] = 1;     
                    //占い師の対応表において人狼値を1
                    for ( int j = 0; j < 5; j++)
                    {
                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), j] = 0;
                    }
                    //占い師の対応表において人間値を0

                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (getAgentNum(gameData.Name) != i)
                        {
                            if (!(values[0] == name) || !(getAgentNum(name) == i))
                            {
                                mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] += 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[1]);
                                //占い師以外の対応表において人狼値を上げる
                                for (int j = 0; j < 5; j++)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), j] -= 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[1]);
                                }
                                //占い師以外の対応表において人間値を下げる
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 霊能結果
        /// 各霊能者の霊能結果を記録する
        /// 霊能結果と占い結果が矛盾しないか確認する
        /// </summary>
        /// <param name="gameData"></param>
        public void reliabilityPsychic(GameData gameData)
        {
            Result_psychic result_Psychic;
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    result_Psychic = new Result_psychic { name_psychic = gameData.Name, name_target = values[0], isHuman = true };
                    psychicList.Add(result_Psychic);
                    seerpsychicReliability(result_Psychic);
                    break;

                case "人狼":
                    result_Psychic = new Result_psychic { name_psychic = gameData.Name, name_target = values[0], isHuman = false };
                    psychicList.Add(result_Psychic);
                    seerpsychicReliability(result_Psychic);
                    break;
            }
        }

        /// <summary>
        /// 霊能結果
        /// 霊能者の対尾表において霊能結果に合わせて対応表を書き換える
        /// 自分の対応表において霊能者の信頼度に合わせて役職値を変更する
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfPsychic(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), 5] = 0;
                    //霊能者の対応表において人狼値を0
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] -= 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[2]);
                    }
                    //霊能以外の対応表において人狼値を減らす
                    break;

                case "人狼":
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), 5] = 1;
                    //霊能者の対応表において人狼値を1
                    for (int i = 0; i < 5; i++)
                    {
                        getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[0]), i] = 0;
                    }
                    //霊能者の対応表において人間値を0

                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (getAgentNum(gameData.Name) == i) break;

                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] += 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[2]);
                        //占い師以外の対応表において人狼値を上げる
                        for (int j = 0; j < 5; j++)
                        {
                            mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), j] -= 0.5 * getAgent(gameData.Name).reliability * (1 / (double)num_roleCO[2]);
                        }
                        //占い師以外の対応表において人間値を下げる
                    }
                    break;
            }
        }

        /// <summary>
        /// 護衛結果
        /// 狩人の対応表において護衛対象の人狼値を0にする
        /// 狩人以外において護衛した対象の人狼値を下げる
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfGuard(GameData gameData)
        {
            foreach(string data in gameData.Guess.Split(' '))
            {
                string[] values = data.Split(':');
                if (values[0] == "護衛結果")
                {
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(values[1]), 5] = 0;
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[1]), 5] 
                            -= 1.0 * (1 / (double)num_roleCO[3]) * getAgent(gameData.Name).reliability;
                    }
                }
            }         
        }

        /// <summary>
        /// 占い先決定
        /// 占い師は決定先のプレイヤを占おうとする
        /// 占い意思の値を上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void decisionOfFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            double up_value;
            foreach (string value in values)
            {
                if (value.Contains("占い先仮決定"))
                {
                    up_value = 0.2;
                }
                else if (value.Contains("占い先本決定"))
                {
                    up_value = 0.4;
                }
                else break;             
                string[] decifortune = value.Split(':');
                if (decifortune.Length < 3)
                {
                    while(decifortune[1] != "")
                    {
                        getAgent(Abbreviation(decifortune[1])).fortune += up_value;
                        decifortune[1] = decifortune[1].Remove(0, 1);
                    }
                }
                else if (Abbreviation(values[1]) == name)
                {
                    while (decifortune[2] != "")
                    {
                        getAgent(Abbreviation(decifortune[2])).fortune += up_value;
                        decifortune[2] = decifortune[2].Remove(0, 1);
                    }
                }
            }
        }

        /// <summary>
        /// 吊り先決定
        /// 決定先のプレイヤに投票しようとする
        /// 投票意思の値を上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void decisionOfExecution(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            double up_value;
            foreach (string value in values)
            {
                if (value.Contains("吊り先仮決定"))
                {
                    up_value = 0.2;
                }
                else if (value.Contains("吊り先本決定"))
                {
                    up_value = 0.4;
                }
                else break;
                string[] decivote = value.Split(':');
                if (decivote.Length < 3)
                {
                    while (decivote[1] != "")
                    {
                        getAgent(Abbreviation(decivote[1])).vote += up_value;
                        decivote[1] = decivote[1].Remove(0, 1);
                    }
                }
                else if (Abbreviation(values[1]) == name)
                {
                    while (decivote[2] != "")
                    {
                        getAgent(Abbreviation(decivote[2])).vote += up_value;
                        decivote[2] = decivote[2].Remove(0, 1);
                    }
                }
            }
        }

        /// <summary>
        /// 投票結果
        /// 今のところ特に処理は無し？
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfVote(GameData gameData)
        {

        }

        /// <summary>
        /// 処刑結果
        /// 吊られたプレイヤを死亡扱いにする
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfExecution(GameData gameData)
        {
            mentalAgentList[getAgentNum(gameData.Guess)].alive = false;
            result_ExecuteList.Add(gameData);
        }

        /// <summary>
        /// 襲撃結果
        /// 襲われたプレイヤを死亡扱いにする
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfRaid(GameData gameData)
        {
            if(gameData.Guess != "失敗")
            {
                mentalAgentList[getAgentNum(gameData.Guess)].alive = false;
                result_RaidList.Add(gameData);
                seerReliability(gameData.Guess);
            }
            else
            {
                isSuccessGuard = true;
            }
            raidCOAgent(gameData);
        }


        /// <summary>
        /// 襲撃結果
        /// 襲撃されたプレイヤの人狼値を0
        /// 襲撃に失敗したとき、同じ相手への襲撃意思を下げる
        /// 護衛に成功したとき、同じ相手への護衛意思を少し下げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceRaid(GameData gameData)
        {
            if(gameData.Guess == "失敗")
            {
                getAgent(decideRaid()).raid -= 1.0;
                if (getAgent(name).agent.role == Role.狩人)
                {
                    getAgent(bodyGuardList[bodyGuardList.Count - 1]).bodyguard -= 0.2;
                }
                return;
            }
            else
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    mentalAgentList[i].oppositeTable.table[getAgentNum(gameData.Guess), 5] = 0;
                }
            }
        }

        /// <summary>
        /// ゲーム結果
        /// 今のところ特に処理は無し？
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfGame(GameData gameData)
        {

        }

        /// <summary>
        /// 希望
        /// 占い希望→希望先の人狼値を少し上げる（下げる）、希望先への占い意思を上げる
        /// 吊り希望→希望先への人狼値を上げる（下げる）、希望先への投票意思を上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceDisire(GameData gameData)
        {
            string[] values = gameData.Disire.Split(' ');
            foreach(string value in values)
            {
                string[] disire = value.Split(':');
                if(disire[0] == "占い希望")
                {
                    while(disire[1] != "")
                    {
                        if(disire[1].Substring(0, 1) == "非")
                        {
                            disire[1] = disire[1].Remove(0, 1);
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] -= 0.25;
                            if (gameData.Name == name)
                            {
                                mentalAgentList[getAgentNum(Abbreviation(disire[1].Substring(0, 1)))].fortune -= 0.3 * getAgent(gameData.Name).reliability;
                            }
                            disire[1] = disire[1].Remove(0, 1);
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] += 0.25;
                            if (gameData.Name == name)
                            {
                                mentalAgentList[getAgentNum(Abbreviation(disire[1].Substring(0, 1)))].fortune -= 0.3 * getAgent(gameData.Name).reliability;
                            }
                            disire[1] = disire[1].Remove(0, 1);
                        }
                    }
                }
                else if(disire[0] == "吊り希望")
                {
                    while (disire[1] != "")
                    {
                        if (disire[1].Substring(0, 1) == "非")
                        {
                            disire[1] = disire[1].Remove(0, 1);
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] -= 0.5;
                            if (gameData.Name == name)
                            {
                                mentalAgentList[getAgentNum(Abbreviation(disire[1].Substring(0, 1)))].vote -= 0.1 * getAgent(gameData.Name).reliability;
                            }
                            disire[1] = disire[1].Remove(0, 1);
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] += 0.5;
                            if (gameData.Name == name)
                            {
                                mentalAgentList[getAgentNum(Abbreviation(disire[1].Substring(0, 1)))].vote += 0.1 * getAgent(gameData.Name).reliability;
                            }
                            disire[1] = disire[1].Remove(0, 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 確認
        /// COの確認→COした役職の役職値を上げる。
        /// 占い結果,霊能結果の確認→占い先の対応する役職値を上げる
        /// 占い先決定,吊り先決定の確認→占い意思、投票意思を上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceConfirm(GameData gameData)
        {
            string[] values = gameData.Confirm.Split(' ');
            foreach(string value in values)
            {
                string[] confirm = value.Split(':');
                if (confirm[1].Contains("CO"))
                {
                    double coconfirm = 0.7;
                    if (confirm[1].Contains("非"))
                    {
                        coconfirm = 0.2;
                        confirm[1] = confirm[1].Substring(1);
                    }
                    confirm[1] = confirm[1].Remove(confirm[1].Length - 2);
                    switch (confirm[1])
                    {
                        case "村人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 0] = coconfirm;
                            break;
                        case "占い師":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 1] = coconfirm;
                            break;
                        case "霊能者":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 2] = coconfirm;
                            break;
                        case "狩人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 3] = coconfirm;
                            break;
                        case "狂人":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 4] = coconfirm;
                            break;
                        case "人狼":
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[0])), 5] = coconfirm;
                            break;
                        default:
                            break;

                    }
                }
                else
                {
                    switch (confirm[0])
                    {
                        case "占い確認":
                            if(confirm[3] == "人間")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5] 
                                    -= 1.0 * getAgent(Abbreviation(confirm[1])).reliability * (1 / (double)num_roleCO[1]);
                            }
                            else if(confirm[3] == "人狼")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    += 1.0 * getAgent(Abbreviation(confirm[1])).reliability * (1 / (double)num_roleCO[1]);
                            }
                            break;
                        case "霊能確認":
                            if (confirm[3] == "人間")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    -= 1.0 * getAgent(Abbreviation(confirm[1])).reliability * (1 / (double)num_roleCO[2]);
                            }
                            else if (confirm[3] == "人狼")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    += 1.0 * getAgent(Abbreviation(confirm[1])).reliability * (1 / (double)num_roleCO[2]);
                            }
                            break;
                        case "吊り先確認":
                            getAgent(Abbreviation(confirm[1])).vote += 0.5;
                            break;
                        case "占い先確認":
                            getAgent(Abbreviation(confirm[1])).fortune += 0.5;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 信頼度
        /// 信頼度を上げる（下げる）
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceReliability(GameData gameData)
        {
            string[] values = gameData.Reliability.Split(' ');
            foreach(string value in values)
            {
                string[] vs = value.Split(':');
                if(vs[1] == "信頼度高")
                {
                    getAgent(Abbreviation(vs[0])).reliability += 0.5;
                }
                else if(vs[1] == "信頼度低")
                {
                    getAgent(Abbreviation(vs[0])).reliability -= 0.5;
                }
            }
        }

        /// <summary>
        /// ライン
        /// ライン考察→二人の人狼値の低い方を上げる
        /// 非ライン考察→二人の人狼値の低い方を下げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceLine(GameData gameData)
        {
            string[] values = gameData.Line.Split(' ');
            foreach(string value in values)
            {
                string[] vs = value.Split(':');
                if (vs[0].Contains("非"))
                {
                    if (vs[1].Length == 2)
                    {
                        if (getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5]
                            > getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5])
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5]
                                -= getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5] * 0.2;
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5]
                                -= getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5] * 0.2;
                        }
                    }
                    

                }
                else
                {
                    if (vs[1].Length != 2)
                    {
                        if (getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5]
                            > getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5])
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5]
                                += getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5] * 0.2;
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(0, 1))), 5]
                                += getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(vs[1].Substring(1))), 5] * 0.2;

                        }
                    }
                }
            }

        }

        /// <summary>
        /// 質問
        /// 質問相手の人狼値、狂人値を少し上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceQuestion(GameData gameData)
        {
            string[] values = gameData.Confirm.Split(' ');
            foreach(string value in values)
            {
                if (value.Contains("質問") != true) break;
                string[] question = value.Split(':');
                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(question[1])), 5] += 0.1;
                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(question[1])), 4] += 0.1;
            }
        }

        /// <summary>
        /// 同調
        /// 同調相手の人狼値をわずかにさげる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceAgree(GameData gameData)
        {
            string[] values = gameData.Questtion.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("同調") == true)
                {
                    string[] agree = value.Split(':');
                    agree[1] = agree[1].Substring(0, 1);
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(agree[1])), 5] -= 0.1;
                }
            }
        }

        /// <summary>
        /// 反発
        /// 反発相手の人狼値をわずかに上げる
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceOppose(GameData gameData)
        {
            string[] values = gameData.Questtion.Split(' ');
            foreach (string value in values)
            {
                string[] oppose = value.Split(':');
                if (oppose[0].Contains("反発") == true)
                {
                    oppose[1] = oppose[1].Substring(0, 1);
                    getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(oppose[1])), 5] += 0.1;
                }
            }
        }

        /// <summary>
        /// 占い
        /// 占い師の対応表において、占った人が人狼なら人狼値1、他は0
        /// 占った人が人間なら人狼値0
        /// </summary>
        /// <param name="gameData"></param>
        public void gameDataFortune(GameData gameData)
        {
            if (!fortuneGameDataList.Contains(gameData))
            {
                fortuneGameDataList.Add(gameData);
            }

            string[] values = gameData.Guess.Split(':');
            switch (values[2])
            {
                case "人間":
                    getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), 5] = 0;
                    break;
                case "人狼":
                    getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), 5] = 1;
                    for(int i = 0; i < 5; i++)
                    {
                        getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), i ] = 0;
                    }
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 霊能
        /// 霊能者の対応表において、霊能先が人狼なら人狼値1、他は0
        /// 霊能先が人間なら人狼値0
        /// </summary>
        /// <param name="gameData"></param>
        public void gameDataPsychic(GameData gameData)
        {
            if (!psychicGameDataList.Contains(gameData))
            {
                psychicGameDataList.Add(gameData);
            }

            string[] values = gameData.Guess.Split(':');
            switch (values[2])
            {
                case "人間":
                    getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), 5] = 0;
                    break;
                case "人狼":
                    getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), 5] = 1;
                    for (int i = 0; i < 5; i++)
                    {
                        getAgent(Abbreviation(values[0])).oppositeTable.table[getAgentNum(Abbreviation(values[1])), i] = 0;
                    }
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 狩人
        /// 今のところ特に処理は無し？
        /// </summary>
        /// <param name="gameData"></param>
        public void gameDataBodyGuard(GameData gameData)
        {

        }

        /// <summary>
        /// 投票
        /// 誰がどこに投票したかを配列に記録する
        /// （ゲーム上プレイヤからは見れないデータ）
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceVote(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            string[] voted = new string[Form1.num_villager];
            foreach (string value in values)
            {
                string[] vs = value.Split(':');
                voted[getAgentNum(Abbreviation(vs[0]))] = Abbreviation(vs[1]);
            }

        }

        /// <summary>
        /// 人狼CO
        /// 人狼同士の確認
        /// 仲間の人狼のプレイヤー名を配列に加える
        /// </summary>
        /// <param name="gameData"></param>
        public void wolfPartnerCO(GameData gameData)
        {
            int i = 0;
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                string[] partnerCO = value.Split(':');
                if(value.Contains("人狼CO"))
                {
                    while (wolfPlayer[i] != null)
                    {
                        i++;
                    }
                    wolfPlayer[i] = Abbreviation(partnerCO[0]);
                }
            }
        }

        /// <summary>
        /// CO希望
        /// </summary>
        /// <param name="gameData"></param>
        public void wolf_DisireOfCO(GameData gameData)
        {
            isDisireCO = true;
            string[] values = gameData.Guess.Split(' ');
            foreach(string value in values)
            {
                string[] vs = value.Split(':');
                for(int i = 0; i < wolfPlayer.Length; i++)
                {
                    if (gameData.Name == wolfPlayer[i])
                    {
                        disireCO[i] = gameData.Guess;
                    }
                }
                if (Abbreviation(vs[0]) == name)
                {
                    myRole = stringToRole(vs[1]);
                }
            }
        }

        /// <summary>
        /// 襲撃希望
        /// </summary>
        /// <param name="gameData"></param>
        public void wolf_DisireOfRaid(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                string[] vs = value.Split(':');
                mentalAgentList[getAgentNum(Abbreviation(vs[1]))].raid += 1.0;
                for (int i = 0; i < wolfPlayer.Length; i++)
                {
                    if (Abbreviation(gameData.Name) == wolfPlayer[i])
                    {
                        disireRaid[i] = gameData.Guess;
                    }
                }
                //isDisireRaid = true;
            }
        }



        /// <summary>
        /// 人狼値(狂人値)の高いプレイヤへの投票意思を高める
        /// </summary>
        public void decideOfVote()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                mentalAgentList[i].vote += mentalAgentList[getAgentNum(name)].oppositeTable.table[i, 5] * 0.5;
                mentalAgentList[i].vote += mentalAgentList[getAgentNum(name)].oppositeTable.table[i, 4] * 0.1;
            }
        }
        
        /// <summary>
        /// 自分の役職を正しく認識している
        /// 自分の対応表において、自分の役職を１、他を０
        /// </summary>
        /// <param name="mentalAgent"></param>
        public void checkMyRole()
        {
            int num = getAgentNum(name);
            if(getAgent(name).agent.role == Role.人狼 || getAgent(name).agent.role == Role.狂人)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == (int)myRole)
                    {
                        mentalAgentList[num].oppositeTable.table[num, i] = 1;
                    }
                    else
                    {
                        mentalAgentList[num].oppositeTable.table[num, i] = 0;
                    }
                }

                for(int k = 0; k < 5; k++)
                {
                    myTable.table[num, k] = 0;
                }
                myTable.table[num, 5] = 1;
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == (int)mentalAgentList[num].agent.role)
                    {
                        mentalAgentList[num].oppositeTable.table[num, i] = 1;
                    }
                    else
                    {
                        mentalAgentList[num].oppositeTable.table[num, i] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 各役職のの人数は0にならない
        /// 各役職の人数は2以上にならない
        /// </summary>
        /// <param name="table"></param>
        public void checkNumRole(ref double[,] table)
        {
            int[] notRole = new int[6];
            int[] numRole = new int[6];

            for (int i = 0; i < Form1.num_villager; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (table[i, j] == 0)
                    {
                        notRole[j]++;
                    }
                    else if (table[i, j] == 1)
                    {
                        numRole[j]++;
                    }
                }
            }

            if (Form1.num_villager - notRole[0] < 8)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 0] += 0.1;
                }
            }
            if (Form1.num_villager - notRole[1] < 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 1] += 0.1;
                }
            }
            if (Form1.num_villager - notRole[2] < 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 2] += 0.1;
                }
            }
            if (Form1.num_villager - notRole[3] < 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 3] += 0.1;
                }
            }
            if (Form1.num_villager - notRole[4] < 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 4] += 0.1;
                }
            }
            if (Form1.num_villager - notRole[5] < 3)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    table[i, 5] += 0.1;
                }
            }
            if (numRole[0] > 7)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 0] == 1)
                    {
                        table[i, 0] -= 0.1;
                    }
                }
            }
            if (numRole[1] > 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 1] == 1)
                    {
                        table[i, 1] -= 0.1;
                    }
                }
            }
            if (numRole[2] > 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 2] == 1)
                    {
                        table[i, 2] -= 0.1;
                    }
                }
            }
            if (numRole[3] > 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 3] == 1)
                    {
                        table[i, 3] -= 0.1;
                    }
                }
            }
            if (numRole[4] > 1)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 4] == 1)
                    {
                        table[i, 4] -= 0.1;
                    }
                }
            }
            if (numRole[5] > 3)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (table[i, 5] == 1)
                    {
                        table[i, 5] -= 0.1;
                    }
                }
            }
        }

        /// <summary>
        /// 人狼の数は半数以下である
        /// 人狼値が1のプレイヤーが生存者の半数以上だった場合、
        /// 人狼値が1の生存者の人狼値を少し下げる
        /// </summary>
        public void checkWolfNum()
        {
            int num_alive = 0;
            int num_alivewolf = 0;
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if (mentalAgentList[i].alive)
                {
                    num_alive++;
                    if (mentalAgentList[0].oppositeTable.table[i, 5] == 1)
                    {
                        num_alivewolf++;
                    }
                }
            }
            if(!(num_alivewolf < num_alive * 2))
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (mentalAgentList[i].alive && mentalAgentList[0].oppositeTable.table[i, 5] == 1)
                    {
                        mentalAgentList[0].oppositeTable.table[i, 5] -= 0.1;
                    }
                }
            }
        }

        /// <summary>
        /// CO内容に矛盾があるプレイヤの信頼度を下げる
        /// </summary>
        public void checkCO()
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(mentalAgentList[i].coRole[j] && mentalAgentList[i].notCoRole[j]) //同じ役職をCO＆非COした
                    {
                        mentalAgentList[i].reliability -= 0.5;
                    }
                }

                if (mentalAgentList[i].coRole[1] && mentalAgentList[i].coRole[2]
                    || mentalAgentList[i].coRole[1] && mentalAgentList[i].coRole[3]
                    || mentalAgentList[i].coRole[2] && mentalAgentList[i].coRole[3])   //占い師、霊能者、狩人のうち2つ以上をした
                {
                    mentalAgentList[i].reliability -= 0.5;
                }
            }
        }

        
        /// <summary>
        /// 占い師の占い結果が偽と判明したとき、その占い師は偽物
        /// 偽物とわかった占い師の信頼度を0
        /// </summary>
        public void seerpsychicReliability(Result_psychic result_Psychic)
        {
            for (int i = 0; i < fortuneList.Count; i++)
            {
                //占い結果と霊能結果が異なるとき、占い師と霊能者の人狼値、狂人値を少し上げる。
                if (result_Psychic.name_target == fortuneList[i].name_target && result_Psychic.isHuman != fortuneList[i].isHuman)
                {
                    for (int j = 0; j < Form1.num_villager; j++)
                    {
                        mentalAgentList[j].oppositeTable.table[getAgentNum(result_Psychic.name_psychic), 4] += 0.1;
                        mentalAgentList[j].oppositeTable.table[getAgentNum(result_Psychic.name_psychic), 5] += 0.1;
                        mentalAgentList[j].oppositeTable.table[getAgentNum(fortuneList[i].name_seer), 4] += 0.1;
                        mentalAgentList[j].oppositeTable.table[getAgentNum(fortuneList[i].name_seer), 5] += 0.1;
                    }
                }
            }
        }

        /// <summary>
        /// 占い師の占い結果が偽と判明したとき、その占い師は偽物
        /// 偽物とわかった占い師の信頼度を0
        /// </summary>
        public void seerReliability(string agent_name)
        {
            for (int i = 0; i < fortuneList.Count; i++)
            {
                //占い結果が人狼の人が襲撃されたとき、占い師の役職値、信頼度を下げ、人狼値、狂人値を上げる
                if (fortuneList[i].name_target == agent_name && !fortuneList[i].isHuman)
                {
                    for (int j = 0; j < Form1.num_villager; j++)
                    {
                        mentalAgentList[j].oppositeTable.table[getAgentNum(fortuneList[i].name_seer), 1] -= 0.5;
                        mentalAgentList[j].oppositeTable.table[getAgentNum(fortuneList[i].name_seer), 4] += 0.5;
                        mentalAgentList[j].oppositeTable.table[getAgentNum(fortuneList[i].name_seer), 5] += 0.5;
                        getAgent(fortuneList[i].name_seer).reliability -= 0.5;
                    }
                }
            }
        }

        /// <summary>
        /// パワープレイできる状況で人狼、狂人のCOがないとき、人狼か狂人が最低一人死んでいる
        /// 死んでいる人の中で、人狼値（狂人値）が0でない人の人狼値（狂人値）を少し上げる
        /// </summary>
        public void checkPP()
        {
            int living = 0;
            int cowolf = 0;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (mentalAgentList[i].alive)
                {
                    living++;
                    if (mentalAgentList[i].coRole[4] || mentalAgentList[i].coRole[5])
                    {
                        cowolf++;
                    }
                }
            }
            if (living <= 8 && cowolf == 0)
            {
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (!mentalAgentList[i].alive && getAgent(name).oppositeTable.table[i, 4] != 0)
                    {
                        getAgent(name).oppositeTable.table[i, 4] += 0.1;
                    }
                    if (!mentalAgentList[i].alive && getAgent(name).oppositeTable.table[i, 5] != 0)
                    {
                        getAgent(name).oppositeTable.table[i, 5] += 0.1;
                    }
                }
            }
        }

        /// <summary>
        /// 人狼は真の役職持ちを優先して襲撃する
        /// 村人以外の村役職をCOしたプレイヤが襲撃されたとき、それを真として
        /// 他に生きている村役職を対抗COしたプレイヤの信頼度を下げる
        /// </summary>
        public void raidCOAgent(GameData data)
        {
            if (data.Tag == "襲撃結果" && data.Guess != "失敗")
            {
                if (getAgent(data.Guess).coRole[1])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[1])
                        {
                            mentalAgentList[getAgentNum(data.Guess)].reliability += 0.5;
                            mentalAgentList[i].reliability -= 0.5;
                        }
                    }
                }
                else if (getAgent(data.Guess).coRole[2])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[2])
                        {
                            mentalAgentList[getAgentNum(data.Guess)].reliability += 0.5;
                            mentalAgentList[i].reliability -= 0.5;
                        }
                    }
                }
                else if (getAgent(data.Guess).coRole[3])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[3])
                        {
                            mentalAgentList[getAgentNum(data.Guess)].reliability += 0.5;
                            mentalAgentList[i].reliability -= 0.5;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 人狼は真の役職持ちを優先して襲撃する
        /// 村人以外の村役職をCOしたプレイヤが襲撃されたとき、それを真として
        /// 他に生きている村役職を対抗COしたプレイヤの役職値を下げる
        /// </summary>
        /// <param name="data"></param>
        public void inferenceRaidCOAgent(GameData data)
        {
            if (data.Tag == "襲撃結果" && data.Guess != "失敗")
            {
                if (getAgent(data.Guess).coRole[1])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[1])
                        {
                            for (int k = 0; k < Form1.num_villager; k++)
                            {
                                mentalAgentList[k].oppositeTable.table[i, 1] -= 0.5;
                                mentalAgentList[k].oppositeTable.table[getAgentNum(data.Guess), 1] += 0.5;
                            }
                        }
                    }
                }
                else if (getAgent(data.Guess).coRole[2])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[2])
                        {
                            for (int k = 0; k < Form1.num_villager; k++)
                            {
                                mentalAgentList[k].oppositeTable.table[i, 2] -= 0.5;
                                mentalAgentList[k].oppositeTable.table[getAgentNum(data.Guess), 2] += 0.5;
                            }
                        }
                    }
                }
                else if (getAgent(data.Guess).coRole[3])
                {
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if (mentalAgentList[i].alive && mentalAgentList[i].coRole[3])
                        {
                            for (int k = 0; k < Form1.num_villager; k++)
                            {
                                mentalAgentList[k].oppositeTable.table[i, 3] -= 0.5;
                                mentalAgentList[k].oppositeTable.table[getAgentNum(data.Guess), 3] += 0.5;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 人狼は誰が人狼か知っている
        /// </summary>
        public void wolfsInference()
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if (wolfPlayer.Contains(mentalAgentList[i].agent.name))
                {
                    for(int k = 0; k < 5; k++)
                    {
                        myTable.table[i, k] = 0;
                    }
                    myTable.table[i, 5] = 1;
                }
                else
                {
                    myTable.table[i, 5] = 0;
                }
            }
        }


        /// <summary>
        /// 人狼値（狂人値）の高いプレイヤの信頼度を下げる
        /// 人狼値の低いプレイヤの信頼度を上げる
        /// </summary>
        public void wolfReliability()
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if(getAgent(name).oppositeTable.table[i, 5] > 0.8 || getAgent(name).oppositeTable.table[i, 4] > 0.8)
                {
                    mentalAgentList[i].reliability -= 0.3;
                }
                else if(getAgent(name).oppositeTable.table[i, 5] + getAgent(name).oppositeTable.table[i, 4] < 0.2)
                {
                    mentalAgentList[i].reliability += 0.3;
                }
            }
        }

        /// <summary>
        /// 占い意思
        /// 自分自身、およびすでに占った人は占わない
        /// 
        /// </summary>
        public void intention_fortune()
        {
            double graypoint = 0;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if(mentalAgentList[i].agent.name == name)
                {
                    mentalAgentList[i].fortune = 0;
                }
                else if (isFortuned(mentalAgentList[i].agent.name))
                {
                    mentalAgentList[i].fortune = 0;
                }
                else
                {
                    graypoint = Math.Abs(getAgent(name).oppositeTable.table[i, 0] - 0.533)
                               + Math.Abs(getAgent(name).oppositeTable.table[i, 1] - 0.067)
                               + Math.Abs(getAgent(name).oppositeTable.table[i, 2] - 0.067)
                               + Math.Abs(getAgent(name).oppositeTable.table[i, 3] - 0.067)
                               + Math.Abs(getAgent(name).oppositeTable.table[i, 4] - 0.067)
                               + Math.Abs(getAgent(name).oppositeTable.table[i, 5] - 0.200);

                    mentalAgentList[i].fortune += (1 / graypoint);
                }
            }
        }

        /// <summary>
        /// 護衛意思
        /// 役職をCOしているプレイヤと、
        /// 人狼値、狂人値が低いプレイヤの護衛意思を上げる
        /// </summary>
        public void intention_BodyGuard()
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if(mentalAgentList[i].coRole[1] || mentalAgentList[i].coRole[2])
                {
                    mentalAgentList[i].bodyguard += 0.5;
                }

                mentalAgentList[i].bodyguard += 1.0 - (getAgent(name).oppositeTable.table[i, 4] + getAgent(name).oppositeTable.table[i, 5]);
            }
        }

        /// <summary>
        /// 襲撃意思
        /// 既に役職COしているプレイヤを優先する 狩→占霊
        /// 人狼値が低い（白めに見られている）プレイヤを襲う
        /// </summary>
        public void intention_Raid()
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if ((mentalAgentList[i].coRole[3])
                    && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                    && mentalAgentList[i].alive)
                {
                    mentalAgentList[i].raid += 5.0 / num_roleCO[3];
                }
                else if ((mentalAgentList[i].coRole[1])
                    && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                    && mentalAgentList[i].alive)
                {
                    mentalAgentList[i].raid += 1.0 / num_roleCO[1];
                }
                else if ((mentalAgentList[i].coRole[2])
                    && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                    && mentalAgentList[i].alive)
                {
                    mentalAgentList[i].raid += 1.0 / num_roleCO[2];
                }

                mentalAgentList[i].raid += 1.0 - (myTable.table[i, 4] + myTable.table[i, 5]);

            }
        }


        /// <summary>
        /// ささやきを生成する
        /// （初日の夜は）誰がCOするかを相談する
        /// それ以降は、襲撃したい相手に対する襲撃希望
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public GameData makeWhisper(GameData gameData)
        {
            if (gameData.Day == "プロローグ")
            {
                if (!iswolfCO)
                {
                    iswolfCO = true;
                    gameData.Tag = "人狼CO";
                    gameData.Guess = Complement(name) + ":人狼CO ";
                }
                else if (!isDisireCO)
                {
                    gameData.Tag = "CO希望";
                    int seed = Environment.TickCount;
                    Random rnd = new Random(seed++);
                    int rndvalue = rnd.Next(0, 1000);
                    if (rndvalue < 400)
                    {
                        gameData.Guess += Complement(wolfPlayer[rnd.Next(0,2)]) + ":霊能者 ";
                    }
                    else if (rndvalue < 600)
                    {
                        gameData.Guess += Complement(wolfPlayer[rnd.Next(0, 2)]) + ":占い師 ";
                    }
                    else
                    {
                        gameData.Guess += Complement(wolfPlayer[0]) + ":村人 "
                                        + Complement(wolfPlayer[1]) + ":村人 "
                                        + Complement(wolfPlayer[2]) + ":村人 ";
                    }

                    isDisireCO = true;

                }
                else
                {
                    gameData.Tag = "Over";
                }
                return gameData;
            }
            else
            {
                if (!isDisireRaid)
                {
                    List<string> raidTarget = new List<string>();
                    Random rnd = new Random();
                    isDisireRaid = true;
                    gameData.Tag += "襲撃希望 ";
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if((mentalAgentList[i].coRole[1] || mentalAgentList[i].coRole[2]) 
                            && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                            && mentalAgentList[i].alive)
                        {                       
                            raidTarget.Add(mentalAgentList[i].agent.name);
                        }
                    }
                    if(raidTarget.Count != 0)
                    {
                        gameData.Guess = "襲撃意思:" + Complement(raidTarget[rnd.Next(0, raidTarget.Count - 1)]);
                        return gameData;
                    }

                    int min = 0;
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if((mentalAgentList[min].raid < mentalAgentList[i].raid)
                            && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                            && mentalAgentList[i].alive)
                        {
                            min = i;
                        }
                    }
                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        if ((mentalAgentList[min].raid == mentalAgentList[i].raid)
                            && !(wolfPlayer.Contains(mentalAgentList[i].agent.name))
                            && mentalAgentList[i].alive)
                        {
                            raidTarget.Add(mentalAgentList[i].agent.name);
                        }
                    }
                    if(raidTarget.Count != 0)
                    {
                        gameData.Guess = "襲撃意思:" + Complement(raidTarget[rnd.Next(0, raidTarget.Count - 1)]);
                        return gameData;
                    }
                    gameData.Guess = "襲撃意思:" + Complement(mentalAgentList[min].agent.name);
                    return gameData;
                }
                else
                {
                    gameData.Tag = "Over";
                }
                return gameData;
            }
        }

        /// <summary>
        /// 発言を生成する
        /// COしていなかったらCO
        /// 占い結果、霊能結果の報告
        /// 吊りたい相手に対する吊り希望        
        /// 人狼だと思っているプレイヤと、人狼ではないと思っているプレイヤへの考察発言
        /// </summary>
        public GameData makeUtterance(GameData gameData)
        {
            if (!isCO && gameData.Day == "1日目")
            {
                isCO = true;
                return utteranceCO(gameData);
            }
            else if ((!isSendResultOfFortune) && (getAgent(name).agent.role == Role.占い師)
                        && fortuneGameDataList.Count != 0)
            {
                isSendSkip = true;
                isSendResultOfFortune = true;
                return utteranceResultOfFortune(gameData);
            }
            else if ((!isSendResultOfPsychic) && (getAgent(name).agent.role == Role.霊能者)
                        && psychicGameDataList.Count != 0)
            {
                isSendSkip = true;
                isSendResultOfPsychic = true;
                return utteranceResultOfPsychic(gameData);
            }
            else if ((!isSendResultOfFortune) && myRole == Role.占い師)
            {
                isSendSkip = true;
                isSendResultOfFortune = true;
                return utteranceDeceiveFortune(gameData);
            }
            else if((!isSendResultOfPsychic) && myRole == Role.霊能者 && gameData.Day != "1日目")
            {
                isSendSkip = true;
                isSendResultOfPsychic = true;
                return utteranceDeceivePsychic(gameData);
            }
            else if (isSuccessGuard && (getAgent(name).agent.role == Role.狩人))
            {
                isSendSkip = true;
                isSuccessGuard = false;
                return utteranceResultOfGuard(gameData);
            }
            else if (!isSendSkip)
            {
                isSendSkip = true;
                gameData.Tag = "Skip";
                return gameData;
            }
            else if (!isSendDisire)
            {
                isSendDisire = true;
                return utteranceDisire(gameData);
            }
            else if (!isSendEstimate)
            {
                isSendEstimate = true;
                return utteranceEstimate(gameData);
            }
            else
            {
                return utteranceOver(gameData);
            }
        }

        /// <summary>
        /// COする
        /// 1日目に、占い師、霊能者は自分の役職をCO
        /// 他の役職の場合、非占、非霊CO
        /// </summary>
        public GameData utteranceCO(GameData gameData)
        {
            if(gameData.Day == "1日目")
            {
                switch (getAgent(name).agent.role)
                {
                    case Role.占い師:
                        gameData.Name = name;
                        gameData.Public = "白";
                        gameData.Tag += "CO ";
                        gameData.Guess += Complement(name) + ":占い師CO " + Complement(name) + ":非霊能者CO ";
                        return gameData;
                    case Role.霊能者:
                        gameData.Name = name;
                        gameData.Public = "白";
                        gameData.Tag += "CO ";
                        gameData.Guess += Complement(name) + ":霊能者CO " + Complement(name) + ":非占い師CO ";
                        return gameData;
                    case Role.狂人:
                        return possessedCO(gameData);
                    case Role.人狼:
                        return wolfCO(gameData);
                    default:
                        gameData.Name = name;
                        gameData.Public = "白";
                        gameData.Tag += "CO ";
                        gameData.Guess += Complement(name) + ":非占い師CO " + Complement(name) + ":非霊能者CO ";
                        return gameData;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 狂人の騙りCO
        /// {占い師、霊能者、COしない｝からランダムに選択する
        /// </summary>
        /// <param name="gameData"></param>
        public GameData possessedCO(GameData gameData)
        {
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            int rndvalue = rnd.Next(0, 1000);
            if (rndvalue < 600)
            {
                myRole = Role.占い師;
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":占い師CO " + Complement(name) + ":非霊能者CO ";
            }
            else if(rndvalue < 900)
            {
                myRole = Role.霊能者;
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":霊能者CO " + Complement(name) + ":非占い師CO ";
            }
            else
            {
                myRole = Role.村人;
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":非占い師CO " + Complement(name) + ":非霊能者CO ";
            }
            return gameData;
        }

        /// <summary>
        /// 人狼の騙りCO
        /// </summary>
        /// <returns></returns>
        public GameData wolfCO(GameData gameData)
        {
            if(myRole　== Role.占い師)
            {
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":占い師CO " + Complement(name) + ":非占い師CO ";
            }
            else if(myRole == Role.霊能者)
            {
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":霊能者CO " + Complement(name) + ":非占い師CO ";
            }
            else
            {
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "CO ";
                gameData.Guess += Complement(name) + ":非占い師CO " + Complement(name) + ":非霊能者CO ";
            }
            return gameData;
        }

        /// <summary>
        /// 占い結果の発言
        /// </summary>
        public GameData utteranceResultOfFortune(GameData gameData)
        {
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "占い結果 ";
            gameData.Guess = fortuneGameDataList[fortuneGameDataList.Count - 1].Guess.Substring(2);
            return gameData;
        }

        /// <summary>
        /// 霊能結果の発言
        /// </summary>
        public GameData utteranceResultOfPsychic(GameData gameData)
        {
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "霊能結果 ";
            gameData.Guess = psychicGameDataList[psychicGameDataList.Count - 1].Guess.Substring(2);
            return gameData;
        }

        /// <summary>
        /// 護衛に成功した場合、狩人COと護衛結果の発言
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public GameData utteranceResultOfGuard(GameData gameData)
        {
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "CO 護衛結果 ";
            gameData.Guess = Complement(name) + ":狩人CO " + "護衛結果:" + bodyGuardList[bodyGuardList.Count - 1];
            return gameData;
        }

        /// <summary>
        /// 占い師騙りCOの占い結果の発言
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public GameData utteranceDeceiveFortune(GameData gameData)
        {
            Random rnd = new Random();
            string target = decideFortune();
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "占い結果 ";
            if (wolfPlayer.Contains(target))
            {
                gameData.Guess = Complement(target) + ":人間"; 
            }
            else
            {
                if(rnd.Next(0,1000) < 300)
                {
                    gameData.Guess = Complement(target) + ":人狼";
                }
                else
                {
                    gameData.Guess = Complement(target) + ":人間";
                }
            }
            return gameData;
        }

        /// <summary>
        /// 霊能者騙りCOの霊能結果の発言
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public GameData utteranceDeceivePsychic(GameData gameData)
        {
            Random rnd = new Random();
            string target = result_ExecuteList[result_ExecuteList.Count - 1].Guess;
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "霊能結果 ";
            if (wolfPlayer.Contains(target))
            {
                gameData.Guess = Complement(target) + ":人間";
            }
            else
            {
                if (rnd.Next(0, 1000) < 300)
                {
                    gameData.Guess = Complement(target) + ":人狼";
                }
                else
                {
                    gameData.Guess = Complement(target) + ":人間";
                }
            }
            return gameData;
        }

        /// <summary>
        /// 希望の発言
        /// 人狼と最も怪しんでいるプレイヤーに対する吊り希望
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public GameData utteranceDisire(GameData gameData)
        {
            if(getAgent(name).agent.role != Role.人狼)
            {
                int max = 0;
                for (int i = 0; i< Form1.num_villager; i++)
                {
                    if(getAgent(name).oppositeTable.table[i, 5] > getAgent(name).oppositeTable.table[max, 5] && mentalAgentList[i].alive)
                    {
                        max = i;
                    }
                }
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "希望 ";
                gameData.Disire += "吊り希望:" + Complement(mentalAgentList[max].agent.name);
            }
            else
            {
                int max = 0;
                while (wolfPlayer.Contains(mentalAgentList[max].agent.name))
                {
                    max++;

                }
                for (int i = 0; i < Form1.num_villager; i++)
                {
                    if (getAgent(name).oppositeTable.table[i, 5] > getAgent(name).oppositeTable.table[max, 5] && mentalAgentList[i].alive
                        && !wolfPlayer.Contains(mentalAgentList[i].agent.name))
                    {
                        max = i;
                    }
                }
                gameData.Name = name;
                gameData.Public = "白";
                gameData.Tag += "希望 ";
                gameData.Disire += "吊り希望:" + Complement(mentalAgentList[max].agent.name);
            }            
            return gameData;
        }

        /// <summary>
        /// 考察の発言
        /// </summary>
        public GameData utteranceEstimate(GameData gameData)
        {

            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag += "考察 ";
                

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (mentalAgentList[i].agent.name == name && (getAgent(name).agent.role == Role.人狼 || getAgent(name).agent.role == Role.狂人))
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + myRole + " ";
                }
                else if (getAgent(name).oppositeTable.table[i, 0] > 0.8)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "村人 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 0] > 0.5)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "村人寄り ";
                }
                else if (getAgent(name).oppositeTable.table[i, 1] > 0.8)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "占い師 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 1] > 0.5)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "占い師寄り ";
                }
                else if (getAgent(name).oppositeTable.table[i, 2] > 0.8)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "霊能者 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 2] > 0.5)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "霊能者寄り ";
                }
                else if (getAgent(name).oppositeTable.table[i, 3] > 0.8)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "狩人 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 3] > 0.5)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "狩人寄り ";
                }
                else if (getAgent(name).oppositeTable.table[i, 4] > 0.8)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "狂人 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 4] > 0.5)
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "狂人寄り ";
                }
                else if (getAgent(name).oppositeTable.table[i, 5] > 0.8 && !(wolfPlayer.Contains(mentalAgentList[i].agent.name)))
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "人狼 ";
                }
                else if (getAgent(name).oppositeTable.table[i, 5] > 0.5 && !(wolfPlayer.Contains(mentalAgentList[i].agent.name)))
                {
                    gameData.Guess += Complement(mentalAgentList[i].agent.name) + ":" + "人狼寄り ";
                }
            }
            return gameData;
        }

        /// <summary>
        /// Over
        /// その日にこれ以上話すことがないとき、Overを返す
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public GameData utteranceOver(GameData gameData)
        {
            gameData.Name = name;
            gameData.Public = "白";
            gameData.Tag = "Over";

            return gameData;
        }

        /// <summary>
        /// 投票先を決めて、エージェント名を返す
        /// </summary>
        /// <returns></returns>
        public string decideVote()
        {
            int num_max = 0;
            while (num_max == getAgentNum(name) || !mentalAgentList[num_max].alive) num_max++;
            List<string> voteTarget = new List<string>();
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            for (int i = 0; i < Form1.num_villager; i++)
            {
                //　投票意思値が高く、自分でも人狼の仲間でもない相手
                if ((mentalAgentList[i].vote > mentalAgentList[num_max].vote) && (mentalAgentList[i].alive)
                    && (i != getAgentNum(name)) && (!wolfPlayer.Contains(mentalAgentList[i].agent.name)) )
                {
                    num_max = i;
                }
            }

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if ((mentalAgentList[i].vote == mentalAgentList[num_max].vote) && (mentalAgentList[i].alive)
                    && (i != getAgentNum(name)) && (!wolfPlayer.Contains(mentalAgentList[i].agent.name)))
                {
                    voteTarget.Add(mentalAgentList[i].agent.name);
                }
            }

            if (voteTarget.Count == 0)
            {
                return mentalAgentList[rnd.Next(0,Form1.num_villager)].agent.name;
            }

            return voteTarget[rnd.Next(0, voteTarget.Count - 1)];

        }

        /// <summary>
        /// 占い先を決めて、エージェント名を返す
        /// </summary>
        /// <returns></returns>
        public string decideFortune()
        {
            int num_max = 0;
            while (num_max == getAgentNum(name) || !mentalAgentList[num_max].alive) num_max++;
            List<string> fortuneTarget = new List<string>();
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if( (mentalAgentList[i].fortune > mentalAgentList[num_max].fortune)
                        && (i != getAgentNum(name)) && mentalAgentList[i].alive 
                        && !isFortuned(mentalAgentList[i].agent.name))
                {
                    num_max = i;
                }
            }

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if((mentalAgentList[i].fortune == mentalAgentList[num_max].fortune) 
                        && (i != getAgentNum(name)) && mentalAgentList[i].alive 
                        && !isFortuned(mentalAgentList[i].agent.name))
                {
                    fortuneTarget.Add(mentalAgentList[i].agent.name);
                }
            }

            if (fortuneTarget.Count == 0)
            {
                return mentalAgentList[rnd.Next(0, Form1.num_villager)].agent.name;
            }

            return fortuneTarget[rnd.Next(0, fortuneTarget.Count - 1)];

        }

        /// <summary>
        /// 既に占ったエージェントか確認する
        /// </summary>
        /// <param name="name_Agent"></param>
        /// <returns></returns>
        public bool isFortuned(string name_Agent)
        {
            for(int i = 0; i < fortuneGameDataList.Count; i++)
            {
                string[] values = fortuneGameDataList[i].Guess.Split(':');
                if (name_Agent == Abbreviation(values[1]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 護衛先を決めて、エージェント名を返す
        /// </summary>
        /// <returns></returns>
        public string decideBodyGuard()
        {
            int num_max = 0;
            while (num_max == getAgentNum(name) || !mentalAgentList[num_max].alive) num_max++;
            List<string> bodyguardTarget = new List<string>();
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if ((mentalAgentList[i].bodyguard > mentalAgentList[num_max].bodyguard)
                    && (i != getAgentNum(name)) && mentalAgentList[i].alive)
                {
                    num_max = i;
                }
            }

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if ((mentalAgentList[i].bodyguard == mentalAgentList[num_max].bodyguard)
                    && (i != getAgentNum(name)) && mentalAgentList[i].alive)
                {
                    bodyguardTarget.Add(mentalAgentList[i].agent.name);
                }
            }

            if (bodyguardTarget.Count == 0)
            {
                return mentalAgentList[rnd.Next(0, Form1.num_villager)].agent.name;
            }

            bodyGuardList.Add(bodyguardTarget[rnd.Next(0, bodyguardTarget.Count - 1)]);
            return bodyGuardList[bodyGuardList.Count - 1];
        }

        /// <summary>
        /// 襲撃先を決めて、エージェント名を返す
        /// </summary>
        /// <returns></returns>
        public string decideRaid()
        {
            int num_max = 0;
            while (num_max == getAgentNum(name) || !mentalAgentList[num_max].alive) num_max++;
            List<string> raidTarget = new List<string>();
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (mentalAgentList[i].raid > mentalAgentList[num_max].raid && mentalAgentList[i].alive
                    && (i != getAgentNum(name)) && (!wolfPlayer.Contains(mentalAgentList[i].agent.name)) )
                {
                    num_max = i;
                }
            }

            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (mentalAgentList[i].raid == mentalAgentList[num_max].raid && mentalAgentList[i].alive
                    && (i != getAgentNum(name)) && (!wolfPlayer.Contains(mentalAgentList[i].agent.name)))
                {
                    raidTarget.Add(mentalAgentList[i].agent.name);
                }
            }

            if (raidTarget.Count == 0)
            {
                return mentalAgentList[rnd.Next(0, Form1.num_villager)].agent.name;
            }

            return raidTarget[rnd.Next(0, raidTarget.Count - 1)];
        } 


        /// <summary>
        /// キャラ名一文字の略称を開く
        /// </summary>
        public string Abbreviation(string abb)
        {
            string charaname = null;
            switch (abb)
            {
                case "楽":
                    charaname = "楽天家ゲルト";
                    break;
                case "長":
                    charaname = "村長ヴァルター";
                    break;
                case "老":
                    charaname = "老人モーリッツ";
                    break;
                case "神":
                    charaname = "神父ジムゾン";
                    break;
                case "樵":
                    charaname = "木こりトーマス";
                    break;
                case "旅":
                    charaname = "旅人ニコラス";
                    break;
                case "者":
                    charaname = "ならず者ディーター";
                    break;
                case "年":
                    charaname = "少年ペーター";
                    break;
                case "妙":
                    charaname = "少女リーザ";
                    break;
                case "商":
                    charaname = "行商人アルビン";
                    break;
                case "羊":
                    charaname = "羊飼いカタリナ";
                    break;
                case "屋":
                    charaname = "パン屋オットー";
                    break;
                case "青":
                    charaname = "青年ヨアヒム";
                    break;
                case "娘":
                    charaname = "村娘パメラ";
                    break;
                case "農":
                    charaname = "農夫ヤコブ";
                    break;
                case "宿":
                    charaname = "宿屋の女主人レジーナ";
                    break;
                case "修":
                    charaname = "シスターフリーデル";
                    break;
                case "仕":
                    charaname = "仕立て屋エルナ";
                    break;
                case "書":
                    charaname = "司書クララ";
                    break;
                case "兵":
                    charaname = "負傷兵シモン";
                    break;
                default:
                    break;
            }
            return charaname;
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

        public Role stringToRole(string roleName)
        {
            switch (roleName)
            {
                case "村人":
                    return Role.村人;
                case "占い師":
                    return Role.占い師;
                case "霊能者":
                    return Role.霊能者;
                case "狩人":
                    return Role.狩人;
                case "狂人":
                    return Role.狂人;
                case "人狼":
                    return Role.人狼;
                default:
                    return Role.村人;
            }
        }


        /// <summary>
        /// キャラネームと同じ名前の
        /// メンタルエージェントオブジェクトを返す
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MentalAgent getAgent(string name)
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                if(name == mentalAgentList[i].agent.name)
                {
                    return mentalAgentList[i];
                }
            }
            return null;
        }

        public int getAgentNum(string name)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                if (name == mentalAgentList[i].agent.name)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 対応表と信頼度の出力
        /// </summary>
        public void printList()
        {
            string str =  "エージェント : " + name + "'s MentalSpace \r\n";
            if (name != "楽天家ゲルト")
            {
                if (getAgent(name).alive)
                {
                    str += name + "(" + getAgent(name).agent.role + ")" + "    →    "
                        + decideVote() + "(" + getAgent(decideVote()).agent.role + ")" + "\r\n";
                }

                if (getAgent(name).agent.role == Role.人狼 || getAgent(name).agent.role == Role.狂人)
                {
                    str += "MyTable: {村,  占,  霊,  狩,  狂,  狼, }\r\n";

                    for (int i = 0; i < Form1.num_villager; i++)
                    {
                        str += Complement(mentalAgentList[i].agent.name) + " ｛";
                        for (int j = 0; j < 6; j++)
                        {
                            str += myTable.table[i, j] + ",  ";
                        }
                        str += "｝ \r\n";
                    }
                    str += "\r\n";
                }

            }
            for (int j = 0; j < Form1.num_villager; j++)
            {
                str += "エージェント名:" + mentalAgentList[j].agent.name.ToString()
                        + "\r\n対応表: {村,  占,  霊,  狩,  狂,  狼, }\r\n"
                        + printOppositeTable(mentalAgentList[j].agent.name)
                        + "信頼度:" + mentalAgentList[j].reliability.ToString() + "\r\n"
                        + "vote :" + mentalAgentList[j].vote + "\r\n"
                        + "fortune :" + mentalAgentList[j].fortune + "\r\n"
                        + "bodyguard :" + mentalAgentList[j].bodyguard + "\r\n"
                        + "raid :" + mentalAgentList[j].raid + "\r\n\r\n";
            }

            str += "\r\n";
            print(str);
        }

        /// <summary>
        /// 対応表の出力
        /// </summary>
        public string printOppositeTable(string name)
        {
            double[,] table = getAgent(name).oppositeTable.getOppositeTable();
            string str = null;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                str += Complement(mentalAgentList[i].agent.name) + " ｛";

                for (int j = 0; j < 6; j++)
                {
                    str += table[i, j] + ",  ";
                }

                str += "｝ \r\n";
            }
            return str;
        }

        public string printVote()
        {
            string str = null;
            if (name != "楽天家ゲルト")
            {
                str += name + "(" + getAgent(name).agent.role + ")" + "    →    "
                         + decideVote() + "(" + getAgent(decideVote()).agent.role + ")" + "\r\n";
            }
            return str;
        }

        public string printRaid()
        {
            string str = null;
            if (name != "楽天家ゲルト")
            {
                str += name + "(" + getAgent(name).agent.role + ")" + "    →    "
                     + decideRaid() + "(" + getAgent(decideRaid()).agent.role + ")" + "\r\n";
            }
            return str;
        }

        public string printGuard()
        {
            string str = null;
            if (name != "楽天家ゲルト")
            {
                str += name + "(" + getAgent(name).agent.role + ")" + "    →    "
                     + decideBodyGuard() + "(" + getAgent(decideBodyGuard()).agent.role + ")" + "\r\n";
            }
            return str;
        }

        public void print(string str)
        {
            //File.AppendAllText(@"./LogText_Agent"+name+".txt", str + "\r\n");
            Form1.printText += str;
            //Console.Write(str);
        }
    }
}
