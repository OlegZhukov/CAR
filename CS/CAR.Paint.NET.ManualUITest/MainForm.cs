using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OlegZhukov.CAR.Paint.NET;

namespace CAR.Paint.NET.ManualUITest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ShowCARDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowCARDialog();
        }

        private void ShowCARDialog()
        {
            (new CARDialog(int.Parse(textBox1.Text), int.Parse(textBox2.Text))).ShowDialog(this);
        }
    }
}
