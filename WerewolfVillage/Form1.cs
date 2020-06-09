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
        public static string printText = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
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
    }
}
