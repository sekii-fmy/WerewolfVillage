using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class ResultOfMatch
    {
        public int playerNum;
        public int num_game;
        public int num_win;
        public int[] num_rolePlay;
        public int[] num_roleWin;
        public int point;

        public ResultOfMatch(int num)
        {
            point = 0;
            playerNum = num;
            num_game = 0;
            num_win = 0;
            num_rolePlay = new int[6] { 0, 0, 0, 0, 0, 0 };
            num_roleWin = new int[6] { 0, 0, 0, 0, 0, 0 };
        }

        public void ResetResult()
        {
            point = 0;
            num_game = 0;
            num_win = 0;
            for (int i = 0; i < 6; i++)
            {
                num_rolePlay[i] = 0;
                num_roleWin[i] = 0;
            }
        }

        public void result_WIN()
        {
            point += 10;
        }

        public void result_LOSE()
        {
            point += 0;
        }

        public void result_WOLFALIVE()
        {
            point += 5;
        }

        public void result_VILLAGERALIVE()
        {
            point += 1;
        }

        public void result_VOTEtoWOLF()
        {
            point += 1;
        }
    }
}
