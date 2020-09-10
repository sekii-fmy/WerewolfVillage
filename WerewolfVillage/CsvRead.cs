using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfVillage
{
    [System.Runtime.Serialization.DataContract]
    public class GameData
    {
        [System.Runtime.Serialization.DataMember()]
        public string SerialNum { get; set; }           //A列

        [System.Runtime.Serialization.DataMember()]
        public string UtterNum { get; set; }            //B列

        [System.Runtime.Serialization.DataMember()]
        public string Day { get; set; }                 //C列

        [System.Runtime.Serialization.DataMember()]
        public string Name { get; set; }                //D列

        [System.Runtime.Serialization.DataMember()]
        public string Public { get; set; }              //E列

        [System.Runtime.Serialization.DataMember()]
        public string Tag { get; set; }                 //F列

        [System.Runtime.Serialization.DataMember()]
        public string Guess { get; set; }               //G列

        [System.Runtime.Serialization.DataMember()]
        public string Disire { get; set; }              //H列

        [System.Runtime.Serialization.DataMember()]
        public string Confirm { get; set; }             //I列

        [System.Runtime.Serialization.DataMember()]
        public string Reliability { get; set; }         //J列

        [System.Runtime.Serialization.DataMember()]
        public string Line { get; set; }                //K列

        [System.Runtime.Serialization.DataMember()]
        public string Questtion { get; set; }           //L列

        [System.Runtime.Serialization.DataMember()]
        public string Memo { get; set; }                //M列

        [System.Runtime.Serialization.DataMember()]
        public string Role { get; set; }                //N列

        [System.Runtime.Serialization.DataMember()]
        public string Utterance { get; set; }           //O列
    }

    class CsvRead
    {
        StreamReader sr = new StreamReader(Form1.readFile, System.Text.Encoding.GetEncoding("shift_jis"));

        public GameData ReadNextline()
        {
            string line = sr.ReadLine();
            string[] values = line.Split(',');

            GameData data = new GameData
            {
                SerialNum = values[0],
                UtterNum = values[1],
                Day = values[2],
                Name = values[3],
                Public = values[4],
                Tag = values[5],
                Guess = values[6],
                Disire = values[7],
                Confirm = values[8],
                Reliability = values[9],
                Line = values[10],
                Questtion = values[11],
                Memo = values[12],
                Role = values[13],
                Utterance = values[14],
            };

            var serializer = new DataContractJsonSerializer(typeof(GameData));
            return data;
        }

    }
}
