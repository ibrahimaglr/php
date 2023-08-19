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
    public partial class FormRegister : Form
    {
        string user_username;
        string user_userrole;
        string user_token;
        WebClient wc = new WebClient();
        NameValueCollection dataToSend = new NameValueCollection();
        public FormRegister(string username, string userrole, string token)
        {
            user_username = username;
            user_userrole = userrole;
            user_token = token;
            InitializeComponent();
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
                label3.ForeColor = Ltext;
                label4.ForeColor = Ltext;
                label5.ForeColor = Ltext;
                label6.ForeColor = Ltext;
                button3.ForeColor = Ltext;
                textBox1.ForeColor = Ltext;
                textBox2.ForeColor = Ltext;
                textBox3.ForeColor = Ltext;
                checkBox1.ForeColor = Ltext;
                comboBox1.ForeColor = Ltext;
                comboBox1.BackColor = Lcontent;
                //button2.ForeColor = Ltext;
            }
            else
            {
                this.BackColor = DmainColor;
                label1.ForeColor = Dtext;
                label2.ForeColor = Dtext;
                label3.ForeColor = Dtext;
                label4.ForeColor = Dtext;
                label5.ForeColor = Dtext;
                label6.ForeColor = Dtext;
                button3.ForeColor = Dtext;
                textBox1.ForeColor = Dtext;
                textBox2.ForeColor = Dtext;
                textBox3.ForeColor = Dtext;
                checkBox1.ForeColor = Dtext;
                comboBox1.ForeColor = Dtext;
                comboBox1.BackColor = Dcontent;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox1.Text != " " && textBox2.Text != "" && textBox2.Text != " " && textBox2.Text == textBox3.Text 
                && textBox1.Text.Length >= 4 && textBox2.Text.Length >= 8 && comboBox1.Text != "" 
                && (pictureBox1.BackgroundImage != null || pictureBox2.BackgroundImage != null))
            {
                dataToSend["uuser_username"] = user_username;
                dataToSend["user_token"] = user_token;
                dataToSend["uuser_role"] = user_userrole;

                dataToSend["user_username"] = textBox1.Text;
                dataToSend["user_password"] = textBox2.Text;

                dataToSend["user_role"] = comboBox1.Text;
                if (pictureBox3.Image != null)
                {
                    dataToSend["user_usergender"] = "0"; 
                }
                else if (pictureBox2.Image != null)
                {
                    dataToSend["user_usergender"] = "1";             
                }
                dataToSend["datatype"] = "CREATE";


                string GetData = Encoding.UTF8.GetString(wc.UploadValues(@"http://127.0.0.1/register.php", dataToSend));
                MessageBox.Show(GetData);
                //formu kapa ve succes screen
            }
            else {
                lblerror.Text = ""; 
                if (textBox1.Text == "" || textBox1.Text == " " || textBox1.Text.Length < 4)
                {
                    lblerror.Text += "- Kullanıcı adı en az 4 karakter uzunluğunda olmalıdır...\n";
                }
                if (textBox2.Text == "" || textBox2.Text == " "|| textBox2.Text.Length < 8)
                {
                    lblerror.Text += "- Şifre en az 8 karakter uzunluğunda olmalıdır...\n";
                }
                if (textBox2.Text != textBox3.Text)
                {
                    lblerror.Text += "- Şifreler uyuşmuyor...\n";
                    checkBox1.Checked = true;
                }
                if (comboBox1.Text == "")
                {
                    lblerror.Text += "- Kullanıcı rölü seçiniz...\n";

                }
                if (pictureBox3.Image == null && pictureBox2.Image == null)
                {
                    lblerror.Text += "- Kullanıcı simgesi seçiniz...\n";
                }

                lblerror.Left = (this.ClientSize.Width - lblerror.Width) / 2;              
                lblerror.Visible = true;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.bg2;
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image = null;
                pictureBox3.Refresh();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.bg2;
            if (pictureBox3.Image != null)
            {
                pictureBox2.Image = null;
                pictureBox2.Refresh();

            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            ButtonColorReset();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;

            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
