using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    public enum Role
    {
        村人,         //VILLAGER
        占い師,       //SEER
        霊能者,       //MEDIUM
        狩人,         //BODYGUARD
        狂人,         //POSSESSED
        人狼,          //WEREWOLF
    }

    class Agent
    {
        public string name;
        public Role role;
        public bool alive;

        public MentalSpace mentalSpace;

        public Agent(string name, string agentRole)
        {
            this.name = name;
            alive = true;

            var roleArray = Enum.GetValues(typeof(Role));
            foreach (Role role in roleArray)
            {
                if (role.ToString() == agentRole)
                {
                    this.role = role;
                }
            }

            mentalSpace = new MentalSpace(this.name);

        }

        public string getFortune()
        {
            return mentalSpace.decideFortune();
        }

        public string getBodyGuard()
        {
            return mentalSpace.decideBodyGuard();
        }

        public string getRaid()
        {
            return mentalSpace.decideRaid();
        }

        public GameData getUtterance(GameData gameData)
        {
            return mentalSpace.makeUtterance(gameData);
        }

        public GameData getWhisper(GameData gameData)
        {
            return mentalSpace.makeWhisper(gameData);
        }

        public string getVote()
        {
            return mentalSpace.decideVote();
        }

        public void printTest()
        {
            Console.Write("name is "+ name +"\r\n");
            Console.Write("role is "+ role +"\r\n");
        }

    }
}
