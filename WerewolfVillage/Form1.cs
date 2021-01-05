using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WerewolfVillage
{
    public partial class Form1 : Form
    {
        public static int num_AutoGame = 1;
        public static int num_villager = 15;
        public static int oneGenerationGames = 1;   //1世代毎の対戦数
        public static int villageNumbers = 1;       //同時に生成する村の数
        public static int generationNumbers = 1;    //世代数
        public static int continue_Generation = 1;  //途中開始する世代数
        public static string readFile = null;
        public static string[] AgentName = new string[num_villager];
        public static string[] AgentRole = new string[num_villager];
        public static string printText = null;
        public static string writeText = null;
        public static string resultVoteText = null;
        public static string resultVoteAndRaidText = null;
        Werewolf werewolf;

        public Form1()
        {
            InitializeComponent();
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
            Startbutton.Visible = false;

            AgentName[0] = AgentName1.Text;
            AgentName[1] = AgentName2.Text;
            AgentName[2] = AgentName3.Text;
            AgentName[3] = AgentName4.Text;
            AgentName[4] = AgentName5.Text;
            AgentName[5] = AgentName6.Text;
            AgentName[6] = AgentName7.Text;
            AgentName[7] = AgentName8.Text;
            AgentName[8] = AgentName9.Text;
            AgentName[9] = AgentName10.Text;
            AgentName[10] = AgentName11.Text;
            AgentName[11] = AgentName12.Text;
            AgentName[12] = AgentName13.Text;
            AgentName[13] = AgentName14.Text;
            AgentName[14] = AgentName15.Text;

            AgentRole[0] = AgentRole1.Text;
            AgentRole[1] = AgentRole2.Text;
            AgentRole[2] = AgentRole3.Text;
            AgentRole[3] = AgentRole4.Text;
            AgentRole[4] = AgentRole5.Text;
            AgentRole[5] = AgentRole6.Text;
            AgentRole[6] = AgentRole7.Text;
            AgentRole[7] = AgentRole8.Text;
            AgentRole[8] = AgentRole9.Text;
            AgentRole[9] = AgentRole10.Text;
            AgentRole[10] = AgentRole11.Text;
            AgentRole[11] = AgentRole12.Text;
            AgentRole[12] = AgentRole13.Text;
            AgentRole[13] = AgentRole14.Text;
            AgentRole[14] = AgentRole15.Text;

            if (GerdButton.Checked)
            {
                num_villager = 16;
                AgentName[15] = "楽天家ゲルト";
                AgentRole[15] = "村人";
            }

            readFile = ReadFile.Text;
            werewolf = new Werewolf();
            werewolf.startWerewolf();
            File.WriteAllText(@"./LogText.txt", printText);
        }

        private void numvillagecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            num_villager = Convert.ToInt32(numvillagecombo.Text);
        }

        private void ResultBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReadFile_TextChanged(object sender, EventArgs e)
        {
            readFile = ReadFile.Text;
        }

        private void AgentName1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void GerdButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void nextDayButton_Click(object sender, EventArgs e)
        {
            writeText += "\r\n \r\n";
            werewolf.nextDayStart();
            ResultBox.Text = writeText;
            File.WriteAllText(@"./LogText.txt", printText);
        }

        private void StartAutoGame_Click(object sender, EventArgs e)
        {
            MakeDirectory();
            string str = null;
            str += "Start to Game\r\n";
            string time = DateTime.Now.ToString() + "\r\n";
            str += time;
            AgentName[0] = AgentName1.Text;
            AgentName[1] = AgentName2.Text;
            AgentName[2] = AgentName3.Text;
            AgentName[3] = AgentName4.Text;
            AgentName[4] = AgentName5.Text;
            AgentName[5] = AgentName6.Text;
            AgentName[6] = AgentName7.Text;
            AgentName[7] = AgentName8.Text;
            AgentName[8] = AgentName9.Text;
            AgentName[9] = AgentName10.Text;
            AgentName[10] = AgentName11.Text;
            AgentName[11] = AgentName12.Text;
            AgentName[12] = AgentName13.Text;
            AgentName[13] = AgentName14.Text;
            AgentName[14] = AgentName15.Text;

            AgentRole[0] = AgentRole1.Text;
            AgentRole[1] = AgentRole2.Text;
            AgentRole[2] = AgentRole3.Text;
            AgentRole[3] = AgentRole4.Text;
            AgentRole[4] = AgentRole5.Text;
            AgentRole[5] = AgentRole6.Text;
            AgentRole[6] = AgentRole7.Text;
            AgentRole[7] = AgentRole8.Text;
            AgentRole[8] = AgentRole9.Text;
            AgentRole[9] = AgentRole10.Text;
            AgentRole[10] = AgentRole11.Text;
            AgentRole[11] = AgentRole12.Text;
            AgentRole[12] = AgentRole13.Text;
            AgentRole[13] = AgentRole14.Text;
            AgentRole[14] = AgentRole15.Text;
            werewolf = new Werewolf();
            werewolf.startAutoGame();
            //ResultBox.Text = writeText;
            //File.WriteAllText(@"./GameData_Village.txt", writeText);
            //File.WriteAllText(@"./LogText.txt", printText);
            //File.WriteAllText(@"./ResultVoteText.txt", resultVoteText);
            //File.WriteAllText(@"./ResultVoteAndRaidText.txt", resultVoteAndRaidText);
            str += "End to Game\r\n";
            str += DateTime.Now.ToString() + "\r\n\r\n";
            ResultBox.Text += str + "End of Output \r\n";
            File.WriteAllText(@"./StartTime.txt", str);
        }

        private void AutoGame_ValueChanged(object sender, EventArgs e)
        {
            num_AutoGame = (int)AutoGame.Value;
        }

        private void GenerationGames_ValueChanged(object sender, EventArgs e)
        {
            oneGenerationGames = (int)GenerationGames.Value;
        }

        private void VillageNum_ValueChanged(object sender, EventArgs e)
        {
            villageNumbers = (int)VillageNum.Value;
        }

        private void GenerationNum_ValueChanged(object sender, EventArgs e)
        {
            generationNumbers = (int)GenerationNum.Value;
        }

        void MakeDirectory()
        {
            if (!Directory.Exists(@"./AgentGene"))
            {
                Directory.CreateDirectory(@"./AgentGene");
            }
            if (!Directory.Exists(@"./GameData_Village"))
            {
                Directory.CreateDirectory(@"./GameData_Village");
            }
            if (!Directory.Exists(@"./LogText"))
            {
                Directory.CreateDirectory(@"./LogText");
            }
            if (!Directory.Exists(@"./ResultVoteAndRaidText"))
            {
                Directory.CreateDirectory(@"./ResultVoteAndRaidText");
            }
            if (!Directory.Exists(@"./ResultVoteText"))
            {
                Directory.CreateDirectory(@"./ResultVoteText");
            }
            if (!Directory.Exists(@"./Total_ResultOfMatch"))
            {
                Directory.CreateDirectory(@"./Total_ResultOfMatch");
            }
        }

        private void ContinueGeneration_Click(object sender, EventArgs e)
        {
            continue_Generation = (int)ContinueGeneNum.Value;
            MakeDirectory();
            string str = null;
            str += "Start to Game\r\n";
            string time = DateTime.Now.ToString() + "\r\n";
            str += time;
            AgentName[0] = AgentName1.Text;
            AgentName[1] = AgentName2.Text;
            AgentName[2] = AgentName3.Text;
            AgentName[3] = AgentName4.Text;
            AgentName[4] = AgentName5.Text;
            AgentName[5] = AgentName6.Text;
            AgentName[6] = AgentName7.Text;
            AgentName[7] = AgentName8.Text;
            AgentName[8] = AgentName9.Text;
            AgentName[9] = AgentName10.Text;
            AgentName[10] = AgentName11.Text;
            AgentName[11] = AgentName12.Text;
            AgentName[12] = AgentName13.Text;
            AgentName[13] = AgentName14.Text;
            AgentName[14] = AgentName15.Text;

            AgentRole[0] = AgentRole1.Text;
            AgentRole[1] = AgentRole2.Text;
            AgentRole[2] = AgentRole3.Text;
            AgentRole[3] = AgentRole4.Text;
            AgentRole[4] = AgentRole5.Text;
            AgentRole[5] = AgentRole6.Text;
            AgentRole[6] = AgentRole7.Text;
            AgentRole[7] = AgentRole8.Text;
            AgentRole[8] = AgentRole9.Text;
            AgentRole[9] = AgentRole10.Text;
            AgentRole[10] = AgentRole11.Text;
            AgentRole[11] = AgentRole12.Text;
            AgentRole[12] = AgentRole13.Text;
            AgentRole[13] = AgentRole14.Text;
            AgentRole[14] = AgentRole15.Text;
            werewolf = new Werewolf();
            werewolf.continueGeneration(villageNumbers, continue_Generation);
            str += "End to Game\r\n";
            str += DateTime.Now.ToString() + "\r\n\r\n";
            ResultBox.Text += str + "End of Output \r\n";
            File.WriteAllText(@"./StartTime.txt", str);
        }
    }
}
