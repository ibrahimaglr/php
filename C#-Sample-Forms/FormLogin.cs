using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;

namespace Restaurant_Management
{
    public partial class FormLogin : Form
    {
        WebClient wc = new WebClient();
        NameValueCollection dataToSend = new NameValueCollection();
        public FormLogin()
        {
            InitializeComponent();
            ButtonColorReset();
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

        private void ButtonColorReset()
        {
            if (Properties.Settings.Default.Lightmode)
            {
                this.BackColor = LmainColor;
                label1.ForeColor = Ltext;
                label2.ForeColor = Ltext;
                button3.ForeColor = Ltext;
                button2.ForeColor = Ltext;
            }
            else
            {
                this.BackColor = DmainColor;
                label1.ForeColor = Dtext;
                label2.ForeColor = Dtext;
                button3.ForeColor = Dtext;
                button2.ForeColor = Dtext;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        private void button3_Click(object sender, EventArgs e)
        {
            dataToSend["user_username"] = textBox1.Text;
            dataToSend["user_password"] = textBox2.Text;
            string GetData = Encoding.UTF8.GetString(wc.UploadValues(@"http://127.0.0.1/index.php", dataToSend));
            string[] data = GetData.Split('|');
            if (GetData == "nodata")
            {
                MessageBox.Show("HATALI GİRİŞ!", "#nodata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (GetData == "dataerror")
            {
                MessageBox.Show("Birşeyler yanlış gitti tekrar deneyiniz!", "#dataerror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (GetData == "usernotfound")
            {
                MessageBox.Show("Kullanıcı Adı bulunamadı!", "#usernotfound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (GetData == "userwrongpassword")
            {
                MessageBox.Show("Kullanıcı Adı/Şifre doğru değil!", "#userwrongpassword", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (GetData == "userbanned")
            {
                MessageBox.Show("Bu Hesap Banlandı!", "#userbanned", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (GetData == "usersession")
            {
                MessageBox.Show("Oturum Zaten Açık!", "#usersession", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (data[0] == "success")
            {
                this.Hide();     
                Form1 f2 = new Form1(textBox1.Text, data[1], data[2], data[3]);
                f2.Show();
                
            }
            else
            {
                MessageBox.Show(GetData, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timer1.Enabled = true;
                label3.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int x = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            x++;
            if (x == 300) 
            { 
                timer1.Enabled = false;
                x = 0; 
                label3.Visible = false;
            }
            else
            {
                label3.Text = (300-x).ToString() + " Saniye Sonra Tekrar Deneyiniz";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Gizle";
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                checkBox1.Text = "Göster";
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
