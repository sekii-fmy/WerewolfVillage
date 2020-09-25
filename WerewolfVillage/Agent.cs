using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{

    public enum Role
    {
        村人,
        占い師,
        霊能者,
        狩人,
        狂人,
        人狼
    }

    class Agent
    {
        public string name;
        public Role role;

        public MentalSpace mentalSpace;

        public Agent(string name, string agentRole)
        {
            this.name = name;

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

        public void printTest()
        {
            Console.Write("name is "+ name +"\r\n");
            Console.Write("role is "+ role +"\r\n");
        }

    }
}
