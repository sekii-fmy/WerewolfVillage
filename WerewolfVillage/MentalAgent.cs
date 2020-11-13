using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class MentalAgent
    {
        public Agent agent;
        public bool alive;            //生死    
        public double reliability;           //信頼度
        public OppositeTable oppositeTable;  //対応表
        public double vote;                  //投票意思,高いプレイヤに投票する
        public double fortune;               //占い意思,高いプレイヤを占う
        public double bodyguard;             //護衛意思,高いプレイヤを護る
        public double raid;                  //襲撃意思,高いプレイヤを襲撃する
        public bool[] coRole;
        public bool[] notCoRole;

        public MentalAgent(ref List<Agent> newAgent, int i)
        {
            agent = newAgent[i];
            alive = true;
            reliability = 0.5;
            vote = 0;
            fortune = 0;
            bodyguard = 0;
            raid = 0;
            coRole = new bool[6] {false, false, false, false, false, false };
            notCoRole = new bool[6] { false, false, false, false, false, false };
            oppositeTable = new OppositeTable();
        }

        /// <summary>
        /// 各日の始まりに投票意思と占い意思はリセットする
        /// </summary>
        public void dayStart()
        {
            vote = 0;
            fortune = 0;
            bodyguard = 0;
            raid = 0;
        }
        
        /// <summary>
        /// 信頼度の値域は0-1
        /// </summary>
        public void checkReliability()
        {
            if(reliability < 0)
            {
                reliability = 0;
            }
            else if(reliability > 1)
            {
                reliability = 1;
            }
        }
    }
}
