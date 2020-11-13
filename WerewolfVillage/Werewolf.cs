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
        Village village;
        GameData gameData;

        public Werewolf()
        {
            //村の生成
            village = new Village();
        }


        /// <summary>
        /// 進行
        /// </summary>
        /// <param name="num_villager"></param>
        /// <returns></returns>
        public void startWerewolf()
        {   

            //ゲームデータ読み込み開始
            //プロローグまで
            gameData = village.startInference();

            //出力開始
            //village.makeOutput();
            Console.Write("end to output \r\n \r\n");
        }

        public void nextDayStart()
        {
            if (gameData == null) return;
            
            //次の日の読み込み開始
            gameData = village.nextInference(gameData);

            //出力開始
            village.makeOutput();
            Console.Write("end to output \r\n \r\n");
        }



        public void startAutoGame()
        {
            int num_NowGame = 0;
            List<ResultOfMatch> resultList = new List<ResultOfMatch>();

            for(int i = 0; i < Form1.num_villager; i++)
            {
                resultList.Add(new ResultOfMatch(i));
            }

            while (num_NowGame < Form1.num_AutoGame)
            {
                resultList = new Village().startGame(resultList);
                File.WriteAllText(@"./GameData_Village/GameData_Village" + num_NowGame + ".txt", Form1.writeText);
                File.WriteAllText(@"./LogText/LogText" + num_NowGame + ".txt", Form1.printText);
                File.WriteAllText(@"./ResultVoteText/ResultVoteText" + num_NowGame + ".txt", Form1.resultVoteText);
                File.WriteAllText(@"./ResultVoteAndRaidText/ResultVoteAndRaidText" + num_NowGame + ".txt", Form1.resultVoteAndRaidText);
                Form1.printText = null;
                Form1.writeText = null;
                Form1.resultVoteText = null;
                Form1.resultVoteAndRaidText = null;
                Console.Write("Game "+ num_NowGame +"\r\n");
                num_NowGame++;
            }
            resultsOutput(resultList);
        }

        public void resultsOutput(List<ResultOfMatch> resultList)
        {
            string str = null;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                str += "PlayerNumber = " + i + "\r\n"
                     + "num_game : " + resultList[i].num_game + "\r\n"
                     + "num_win : " + resultList[i].num_win + "\r\n"
                     + "num_rolePlay : ";
                for(int k = 0; k < 6; k++)
                {
                    str += resultList[i].num_rolePlay[k] + ", ";
                }
                str += "\r\n"
                     + "num_roleWin : ";
                for (int k = 0; k < 6; k++)
                {
                    str += resultList[i].num_roleWin[k] + ", ";
                }
                str += "\r\n\r\n";
            }

            File.WriteAllText(@"./Total_ResultOfMatch/Total_ResultOfMatch.txt", str);
        }

    }
}
