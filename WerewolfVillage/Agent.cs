using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    public enum CharactorName
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

    }
}
