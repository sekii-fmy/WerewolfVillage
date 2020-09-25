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
            village.makeOutput();
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

    }
}
