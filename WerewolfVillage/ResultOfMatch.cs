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

        public ResultOfMatch(int num)
        {
            playerNum = num;
            num_game = 0;
            num_win = 0;
            num_rolePlay = new int[6] { 0, 0, 0, 0, 0, 0 };
            num_roleWin = new int[6] { 0, 0, 0, 0, 0, 0 };
        }
    }
}
