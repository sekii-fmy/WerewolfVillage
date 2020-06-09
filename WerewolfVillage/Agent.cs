using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    public enum Role
    {
        VILLAGER,
        SEER,
        WEREWOLF,
        POSSESSED
    }

    class Agent
    {
        public string name;

        public Role role;

        public MentalSpace mentalSpace;

        public Agent(string name)
        {
            this.name = name;
            mentalSpace = new MentalSpace(name);
        }

        public void generateMentalAgent(ref List<Agent> agentList)
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                mentalSpace.mentalList.Add(new MentalAgent(ref agentList[i].name, ref agentList[i].role));
            }
        }

    }
}
