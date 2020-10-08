﻿using System;
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
        int[] num_roleCO = new int[6] { 0, 0, 0, 0, 0, 0 };

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

        List<MentalAgent> mentalAgentList = new List<MentalAgent>();     //メンタルエージェントリスト
        List<Result_fortune> fortuneList = new List<Result_fortune>();          //占い結果
        List<Result_psychic> psychicList = new List<Result_psychic>();          //霊能結果
        List<GameData> gameDataList = new List<GameData>();              //発話リスト


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
            }
            int k = 0;
            Console.Write(name + "\r\n");

            foreach(GameData data in gameDataList)
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
        }


        /// <summary>
        /// 推論
        /// 入力はゲームデータ
        /// タグ（F列）を読んで対応するメソッドを開く
        /// </summary>
        public void inference(GameData gameData)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                mentalAgentList[i].dayStart();
            }


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
                            fortune(gameData);
                            break;
                        case "狩人":
                            hunter(gameData);
                            break;
                        case "投票":
                            inferenceVote(gameData);
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

            checkMyRole();
            decideOfVote();

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
                    else //guess[0].length!=1
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
                            num_roleCO[0]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 0] += rolevalue;
                            for(int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                }
                            }
                            break;
                        case "占い師":
                            num_roleCO[1]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 1] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 1] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "霊能者":
                            num_roleCO[2]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 2] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 2] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "狩人":
                            num_roleCO[3]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 3] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 3] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "狂人":
                            num_roleCO[4]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 4] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 4] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 0] -= 0.5;
                                }
                            }
                            break;
                        case "人狼":
                            num_roleCO[5]++;
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(co[0]), 5] = rolevalue;
                            for (int i = 0; i < Form1.num_villager; i++)
                            {
                                if (getAgentNum(gameData.Name) != i)
                                {
                                    mentalAgentList[i].oppositeTable.table[getAgentNum(co[0]), 5] += 1.0 * rolevalue * getAgent(gameData.Name).reliability;
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
                    break;
                case "人狼":
                    fortuneList.Add(new Result_fortune { name_seer = gameData.Name, name_target = values[0], isHuman = false });

                    //占い師が自分を人狼と占ったとき、その占い師の信頼度を下げる
                    //(自分が人狼でないとき）
                    if (values[0] == name && mentalAgentList[getAgentNum(name)].agent.role != Role.人狼)
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

                    for(int i = 0; i < Form1.num_villager; i++)
                    {
                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] -= 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[1]);
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
                        if (getAgentNum(gameData.Name) == i) break;

                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] += 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[1]);
                        //占い師以外の対応表において人狼値を上げる
                        for (int j = 0; j < 5; j++)
                        {
                            mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), j] -= 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[1]);
                        }
                        //占い師以外の対応表において人間値を下げる
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
                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] -= 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[2]);
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

                        mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), 5] += 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[2]);
                        //占い師以外の対応表において人狼値を上げる
                        for (int j = 0; j < 5; j++)
                        {
                            mentalAgentList[i].oppositeTable.table[getAgentNum(values[0]), j] -= 0.5 * getAgent(gameData.Name).reliability * (1 / num_roleCO[2]);
                        }
                        //占い師以外の対応表において人間値を下げる
                    }
                    break;
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
        }

        /// <summary>
        /// 襲撃結果
        /// 襲われたプレイヤを死亡扱いにする
        /// </summary>
        /// <param name="gameData"></param>
        public void resultOfRaid(GameData gameData)
        {            
            mentalAgentList[getAgentNum(gameData.Guess)].alive = false;
            seerReliability(gameData.Guess);
        }


        /// <summary>
        /// 襲撃結果
        /// 襲撃されたプレイヤの人狼値を0
        /// </summary>
        /// <param name="gameData"></param>
        public void inferenceRaid(GameData gameData)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                mentalAgentList[i].oppositeTable.table[getAgentNum(gameData.Guess), 5] = 0;
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
        /// 占い希望→希望先の人狼値を少し上げる（下げる）
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
                            disire[1] = disire[1].Remove(0, 1);
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] += 0.25;
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
                            disire[1] = disire[1].Remove(0, 1);
                        }
                        else
                        {
                            getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(disire[1].Substring(0, 1))), 5] += 0.5;
                            if (gameData.Name == name)
                            {
                                mentalAgentList[getAgentNum(Abbreviation(disire[1].Substring(0, 1)))].vote += 0.2;
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
                                    -= 0.3 * getAgent(Abbreviation(confirm[1])).reliability;
                            }
                            else if(confirm[3] == "人狼")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    += 0.3 * getAgent(Abbreviation(confirm[1])).reliability;
                            }
                            break;
                        case "霊能確認":
                            if (confirm[3] == "人間")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    -= 0.3 * getAgent(Abbreviation(confirm[1])).reliability;
                            }
                            else if (confirm[3] == "人狼")
                            {
                                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(confirm[2])), 5]
                                    += 0.3 * getAgent(Abbreviation(confirm[1])).reliability;
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
                    if (vs[1].Length != 2) break;
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
                else
                {
                    if (vs[1].Length != 2) break;
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
            foreach(string value in values)
            {
                if (value.Contains("同調") != true) break;
                string[] agree = value.Split(':');
                agree[1] = agree[1].Substring(0, 1);
                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(agree[1])), 5] -= 0.1;
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
                if (oppose[0].Contains("反発") != true) break;
                oppose[1] = oppose[1].Substring(0,1);
                getAgent(gameData.Name).oppositeTable.table[getAgentNum(Abbreviation(oppose[1])), 5] += 0.1;
            }
        }

        /// <summary>
        /// 占い
        /// 占い師の対応表において、占った人が人狼なら人狼値1、他は0
        /// 占った人が人間なら人狼値0
        /// </summary>
        /// <param name="gameData"></param>
        public void fortune(GameData gameData)
        {
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
        /// 狩人
        /// 今のところ特に処理は無し？
        /// </summary>
        /// <param name="gameData"></param>
        public void hunter(GameData gameData)
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
            if(num_alivewolf > num_alive * 2)
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
        /// パワープレイできる状況で人狼、狂人のCOがないとき、人狼か狂人が最低一人死んでいる
        /// 死んでいる人の中で、人狼値（狂人値）が0でない人の人狼値（狂人値）を少し上げる
        /// </summary>
        public void checkPP()
        {
            int living = 0;
            int cowolf = 0;
            for(int i = 0; i < Form1.num_villager; i++)
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
            if(living <= 8 && cowolf == 0)
            {
                for(int i = 0; i<Form1.num_villager; i++)
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
            int vote_max = 0;
            while (!mentalAgentList[vote_max].alive || mentalAgentList[vote_max].agent.name == name)
            {
                vote_max++;
            }

            for (int j = 0; j < Form1.num_villager; j++)
            {
                if(mentalAgentList[vote_max].vote < mentalAgentList[j].vote && mentalAgentList[j].alive &&mentalAgentList[j].agent.name != name)
                {
                    vote_max = j;
                }

                str += "エージェント名:" + mentalAgentList[j].agent.name.ToString() + ",\r\n信頼度:"
                    + mentalAgentList[j].reliability.ToString() + "\r\n対応表:\r\n" + mentalAgentList[j].oppositeTable.printOppositeTable()
                    + "vote :" + mentalAgentList[j].vote + "\r\n";

            }

            if(name != "楽天家ゲルト")
            {
                str += "投票先 : " + mentalAgentList[vote_max].agent.name + "\r\n";
                if (getAgent(name).alive)
                {
                    Form1.writeText += name + "    →    " + mentalAgentList[vote_max].agent.name + "\r\n";
                }
            }
            print(str);
        }

        public void print(string str)
        {
            File.AppendAllText(@"./LogText_Agent"+name+".txt", str + "\r\n");
            Form1.printText += str;
            //Console.Write(str);
        }
    }
}
