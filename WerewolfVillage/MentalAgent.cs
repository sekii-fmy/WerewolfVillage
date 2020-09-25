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
        public double vote;                  //投票意思,高いプレイヤに投票しようとする
        public double fortune;               //占い意思,高いプレイヤを占う

        public MentalAgent(ref List<Agent> newAgent, int i)
        {
            agent = newAgent[i];
            alive = true;
            reliability = 0.5;
            vote = 0;
            fortune = 0;
            oppositeTable = new OppositeTable();
        }

    }
}
