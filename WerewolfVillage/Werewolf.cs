using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WerewolfVillage
{
    class Werewolf
    {
        /// <summary>
        /// 進行
        /// </summary>
        /// <param name="num_villager"></param>
        /// <returns></returns>
        public void startwerewolf()
        {   
            //村の生成
            Village village = new Village();
            village.newVillage();

            village.makeOutput();
        }

    }
}
