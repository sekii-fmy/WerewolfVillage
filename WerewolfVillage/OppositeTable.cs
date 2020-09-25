﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    class OppositeTable
    {
        int num_role = 6;
        public double[,] table;

        public OppositeTable()
        {
            table = new double[Form1.num_villager, num_role];
            initialTable(ref table);
        }

        /// <summary>
        /// 対応表を初期化する
        /// </summary>
        /// <param name="table"></param>
        public void initialTable(ref double[,] table)
        {
            Random random = new System.Random();

            for(int i = 0; i < Form1.num_villager; i++)
            {
                table[i, 0] = 0.533;
                table[i, 1] = 0.067;
                table[i, 2] = 0.067;
                table[i, 3] = 0.067;
                table[i, 4] = 0.067;
                table[i, 5] = 0.200;
            }
        }



        /// <summary>
        /// 対応表の出力
        /// </summary>
        public string printOppositeTable()
        {
            string str = null;
            for (int i = 0; i < Form1.num_villager; i++)
            {
                str += "｛";
                for (int j = 0; j < num_role; j++)
                {
                    str += table[i, j].ToString() + ",";
                }
                str += "｝ \r\n";
            }
            return str;
        }




        /// <summary>
        /// 推論にあたって対応表内の調査
        /// </summary>
        /// <param name="table"></param>
        public void checkTable(ref double[,] table)
        {
            checkNumOfTable(ref table);

            
        }

        /// <summary>
        /// 対応表の値の処理
        /// 値がマイナスになった場合は0に、
        /// 全ての役職の値の総和が1になるように処理
        /// </summary>
        /// <param name="table"></param>
        public void checkNumOfTable(ref double[,] table)
        {
            for (int i = 0; i < Form1.num_villager; i++)
            {
                double total = 0;
                for (int j = 0; j < num_role; j++)
                {
                    if(table[i,j] < 0)
                    {
                        table[i, j] = 0;
                    }
                    total += table[i, j];
                }

                for (int j = 0; j < num_role; j++)
                {
                    table[i, j] =
                        Math.Round(table[i, j] / total, 3, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// 役職を名乗る人数が役職人数以下でなければならない
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool numberofRole(ref double[,] table)
        {
            return true;
        }

    }
}
