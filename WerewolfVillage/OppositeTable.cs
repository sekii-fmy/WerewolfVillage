using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class OppositeTable
    {
        int num_role = 4;
        public double[,] table;

        public OppositeTable()
        {
            table = new double[Form1.num_villager, num_role];
            randomInitialTable(ref table);
        }

        /// <summary>
        /// 対応表を（今は）ランダムに初期化する
        /// </summary>
        /// <param name="table"></param>
        public void randomInitialTable(ref double[,] table)
        {
            Random random = new System.Random();

            for(int i = 0; i < Form1.num_villager; i++)
            {
                table[i, random.Next(0, num_role-1)] = 1.0;
            }
        }

        /// <summary>
        /// 推論にあたって対応表内の調査
        /// </summary>
        /// <param name="table"></param>
        public void checkTable(ref double[,] table)
        {
            if (!numberofRole(ref table))
            {

            }
        }

        /// <summary>
        /// 役職を名乗る人数が役職人数以下でなければならない
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool numberofRole(ref double[,] table)
        {
            for(int i = 0; i < Form1.num_villager; i++)
            {
                for(int j = 0; j < table.GetLength(i); j++)
                {

                }
            }

            return true;
        }

    }
}
