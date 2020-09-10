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
        public static int num_villager = 5;
        public static string readFile = null;
        public static string[] AgentName = null;
        public static string[] AgentRole = null;
        public static string printText = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
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

            Werewolf werewolf = new Werewolf();
            werewolf.startwerewolf();
            ResultBox.Text = printText;
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
    }
}
