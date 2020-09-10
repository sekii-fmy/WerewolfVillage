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
        public double reliability;           //信頼度
        public OppositeTable oppositeTable;  //対応表

        public MentalAgent(ref List<Agent> newAgent, int i)
        {
            agent = newAgent[i];
            reliability = 0;
            oppositeTable = new OppositeTable();
        }
    }
}
