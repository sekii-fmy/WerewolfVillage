using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    public enum CharaName
    {
        楽天家ゲルト,
        村長ヴァルター,
        老人モーリッツ,
        神父ジムゾン,
        木こりトーマス,
        旅人ニコラス,
        ならず者ディーター,
        少年ペーター,
        少女リーザ,
        行商人アルビン,
        羊飼いカタリナ,
        パン屋オットー,
        青年ヨアヒム,
        村娘パメラ,
        農夫ヤコブ,
        宿屋の女主人レジーナ,
        シスターフリーデル,
        仕立て屋エルナ,
        司書クララ,
        負傷兵シモン
    }

    public enum Role
    {
        VILLAGER,
        SEER,
        PHYCHIC,
        HUNTER,
        WEREWOLF,
        POSSESSED
    }

    class Agent
    {
        public CharaName name;
        public Role role;

        public MentalSpace mentalSpace;

        public Agent(string name, string agentRole)
        {
            var nameArray = Enum.GetValues(typeof(CharaName));
            foreach (CharaName charaName in nameArray)
            {
                if (role.ToString() == name)
                {
                    this.name = charaName;
                }
            }

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

    }
}
