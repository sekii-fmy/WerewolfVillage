using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class MentalAgent
    {
        public string name;             //名前
        public string role;             //役職
        public double reliability;      //信頼度
        public OppositeTable oppositeTable; 

        public MentalAgent(ref string name, ref Role role)
        {
            oppositeTable = new OppositeTable();
        }
    }
}
