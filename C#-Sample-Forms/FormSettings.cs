using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Lightmode)
            {
                pictureBox1.BackgroundImage = Properties.Resources.off_button;
                this.BackColor = Lcontent;
                label1.ForeColor = Ltext;
                label2.ForeColor = Ltext;
                label3.ForeColor = Ltext;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.on_button;
                this.BackColor = Dcontent;
                label1.ForeColor = Dtext;
                label2.ForeColor = Dtext;
                label3.ForeColor = Dtext;
            }
            if (Properties.Settings.Default.Maximize)
            {
                pictureBox2.BackgroundImage = Properties.Resources.on_button;
            }
            else
            {
                pictureBox2.BackgroundImage = Properties.Resources.off_button;
            }

        }
        
        Color DactiveColor = Color.FromArgb(31, 27, 48);
        Color DmainColor = Color.FromArgb(26, 23, 40);
        Color Dcontent = Color.FromArgb(18, 18, 32);
        Color Dtext = Color.FromArgb(127, 124, 146);
        Color Dtitle = Color.FromArgb(199, 198, 205);

        Color LmainColor = System.Drawing.ColorTranslator.FromHtml("#e3e2de");
        Color LactiveColor = System.Drawing.ColorTranslator.FromHtml("#F3E4D2");
        Color Lcontent = System.Drawing.ColorTranslator.FromHtml("#d1d0cd");
        Color Ltext = Color.Black;
        Color Ltitle = Color.Black;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            if (Properties.Settings.Default.Lightmode)
            {
                pictureBox1.BackgroundImage = Properties.Resources.on_button;
                Properties.Settings.Default.Lightmode = false;
                Properties.Settings.Default.Save();
                pictureBox1.Refresh();
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.off_button;
                Properties.Settings.Default.Lightmode = true;
                Properties.Settings.Default.Save();
                pictureBox1.Refresh();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Refresh();
            if (Properties.Settings.Default.Maximize)
            {
                pictureBox2.BackgroundImage = Properties.Resources.off_button;
                Properties.Settings.Default.Maximize = false;
                Properties.Settings.Default.Save();
                pictureBox2.Refresh();
            }
            else
            {
                pictureBox2.BackgroundImage = Properties.Resources.on_button;
                Properties.Settings.Default.Maximize = true;
                Properties.Settings.Default.Save();
                pictureBox2.Refresh();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //2
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel7.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //3
            panel3.Visible = true;
            panel3.Location = new Point(12, 83);
            panel2.Visible = false;
            panel4.Visible = false;
            panel7.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //4
            panel4.Visible = true;
            panel4.Location = new Point(12, 83);
            panel3.Visible = false;
            panel2.Visible = false;
            panel7.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //7
            panel7.Visible = true;
            panel7.Location = new Point(12, 83);
            panel3.Visible = false;
            panel4.Visible = false;
            panel2.Visible = false;
        }
    }
}
