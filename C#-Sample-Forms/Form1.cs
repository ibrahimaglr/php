using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;



namespace Restaurant_Management
{
    public partial class Form1 : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse
        );

        string user_username;
        string user_token;
        string user_userrole;
        public Form1(string username,string userrole,string token,string usergender)
        {
            InitializeComponent();
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            pnlNavIndicator.Height = btnDashboard.Height;
            pnlNavIndicator.Top = btnDashboard.Top;
            pnlNavIndicator.Left = btnDashboard.Left;

            user_username = username;
            user_token = token;
            user_userrole = userrole;
            if(user_userrole =="Kullanıcı") {
                button6.Visible = false;
                btnAnalytics.Visible = false;
            }
            label4.Text = user_username;
            label2.Text = user_userrole;
            label1.Text += user_token;
            if(usergender == "1")
            {
                pictureBox2.Image = Properties.Resources.man;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.girl;

            }
            lblTabTitle.Text = "Dashboard";
            //this.pnlContent.Controls.Clear();
            FormDashboard FrmDashboard_Vrb = new FormDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.pnlContent.Controls.Add(FrmDashboard_Vrb);
            FrmDashboard_Vrb.Show();
        }

        Color DactiveColor = Color.FromArgb(31, 27, 48);
        Color DmainColor = Color.FromArgb(26, 23, 40);
        Color Dcontent = Color.FromArgb(18, 18, 32);
        Color Dtext = Color.FromArgb(127, 124, 146);
        Color Dtitle = Color.FromArgb(199, 198, 205);
        Color Dnav = Color.FromArgb(229, 99, 135);

        Color LmainColor = System.Drawing.ColorTranslator.FromHtml("#e3e2de");
        Color LactiveColor = System.Drawing.ColorTranslator.FromHtml("#d1d0cd");
        Color Lcontent = System.Drawing.ColorTranslator.FromHtml("#d1d0cd");
        Color Ltext = Color.Black;
        Color Ltitle = Color.Black;
        Color Lnav = System.Drawing.ColorTranslator.FromHtml("#212529");

        private void ButtonColorReset(Button button)
        {
            if (Properties.Settings.Default.Lightmode)
            {
                btnDashboard.BackColor = LmainColor;
                btnCustomers.BackColor = LmainColor;
                btnEmployees.BackColor = LmainColor;               
                btnAnalytics.BackColor = LmainColor;
                btnSettings.BackColor = LmainColor;
                button6.BackColor = LmainColor;
                button.BackColor = LactiveColor;
                panel_header.BackColor = LmainColor;
                panel4.BackColor = LmainColor;
                panel2.BackColor = LmainColor;
                panel3.BackColor = LmainColor;
                pnlContent.BackColor = Lcontent;
                pnlNavIndicator.BackColor = Lnav;

                label3.ForeColor = Ltitle;
                label2.ForeColor = Ltext;
                label4.ForeColor = Ltext;
                lblTabTitle.ForeColor = Ltext;
                btnAnalytics.ForeColor = Ltext;
                btnCustomers.ForeColor = Ltext;
                btnDashboard.ForeColor = Ltext;
                button3.ForeColor = Ltext;
                btnEmployees.ForeColor = Ltext;               
                button6.ForeColor = Ltext;
                btnSettings.ForeColor = Ltext;


            }
            else
            {
                btnDashboard.BackColor = DmainColor;
                btnCustomers.BackColor = DmainColor;
                btnEmployees.BackColor = DmainColor;             
                btnAnalytics.BackColor = DmainColor;
                button6.BackColor = DmainColor;
                btnSettings.BackColor = DmainColor;
                button.BackColor = DactiveColor;
                panel_header.BackColor = DmainColor;
                panel4.BackColor = DmainColor;
                panel2.BackColor = DmainColor;
                panel3.BackColor = DmainColor;
                pnlContent.BackColor = Dcontent;
                pnlNavIndicator.BackColor = Dnav;

                label3.ForeColor = Dtitle;
                label2.ForeColor = Dtext;
                label4.ForeColor = Dtext;
                lblTabTitle.ForeColor = Dtext;
                btnAnalytics.ForeColor = Dtext;
                btnCustomers.ForeColor = Dtext;
                btnDashboard.ForeColor = Dtext;
                button3.ForeColor = Dtext;
                btnEmployees.ForeColor = Dtext;             
                button6.ForeColor = Dtext;
                btnSettings.ForeColor = Dtext;
            }

        }

        private void DisposeChildForms(Form selectedForm)
        {
            try
            {
                List<Form> formsToClose = new List<Form>();
                foreach (Form form in Application.OpenForms)
                {
                    Console.WriteLine(form);
                    if (form != selectedForm && form.Name != "Form1" && form.Name != "FormLogin")
                    {
                        formsToClose.Add(form);
                    }
                }
                foreach (Form form in formsToClose)
                {

                    form.Close();
                    Console.WriteLine(form);
                }
            }
            catch(Exception) {
                MessageBox.Show("", "Makul Turizm Müşteri Kayıt Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
                Application.Restart(); }
        }
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = btnDashboard.Height;
            pnlNavIndicator.Top = btnDashboard.Top;
            pnlNavIndicator.Left = btnDashboard.Left;
            ButtonColorReset(btnDashboard);
            lblTabTitle.Text = "Gösterge Paneli";
            //this.pnlContent.Controls.Clear();
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            FormDashboard FrmDashboard_Vrb = new FormDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.pnlContent.Controls.Add(FrmDashboard_Vrb);
                FrmDashboard_Vrb.Show();

        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = btnCustomers.Height;
            pnlNavIndicator.Top = btnCustomers.Top;
            pnlNavIndicator.Left = btnCustomers.Left;
            ButtonColorReset(btnCustomers);
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            lblTabTitle.Text = "Yeni Kayıt";
            //this.pnlContent.Controls.Clear();
            Customerdemo FrmCustomer_Vrb = new Customerdemo(user_username, user_userrole, user_token) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.pnlContent.Controls.Add(FrmCustomer_Vrb);
            FrmCustomer_Vrb.Show();
        }

        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel_header_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel_header_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = btnSettings.Height;
            pnlNavIndicator.Top = btnSettings.Top;
            pnlNavIndicator.Left = btnSettings.Left;
            ButtonColorReset(btnSettings);
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            lblTabTitle.Text = "Ayarlar";
           // this.pnlContent.Controls.Clear();
            FormSettings FrmCustomer_Vrb = new FormSettings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmCustomer_Vrb.pictureBox1.Click += new EventHandler(result1);
            this.pnlContent.Controls.Add(FrmCustomer_Vrb);
            FrmCustomer_Vrb.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = button6.Height;
            pnlNavIndicator.Top = button6.Top;
            pnlNavIndicator.Left = button6.Left;
            ButtonColorReset(button6);
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            lblTabTitle.Text = "Personel Paneli";
            //this.pnlContent.Controls.Clear();
            FormEmployees FrmCustomer_Vrb = new FormEmployees(user_username,user_userrole,user_token) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.pnlContent.Controls.Add(FrmCustomer_Vrb);
            FrmCustomer_Vrb.Show();
        }
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = btnAnalytics.Height;
            pnlNavIndicator.Top = btnAnalytics.Top;
            pnlNavIndicator.Left = btnAnalytics.Left;
            ButtonColorReset(btnAnalytics);
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            lblTabTitle.Text = "Raporlar";
            //this.pnlContent.Controls.Clear();
            FormAnalytics FrmCustomer_Vrb = new FormAnalytics(user_username, user_userrole, user_token) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.pnlContent.Controls.Add(FrmCustomer_Vrb);
            FrmCustomer_Vrb.Show();
        }
        void result1(object sender, EventArgs e)
        {
            btnSettings.PerformClick();
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (!Properties.Settings.Default.Maximize)
            {
                
                if (this.WindowState != FormWindowState.Maximized)
                {
                    this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                    this.WindowState = FormWindowState.Maximized;
                    button4.BackgroundImage = Properties.Resources.minimize;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    button4.BackgroundImage = Properties.Resources.maximize_size_option;
                }
            }
            else
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    this.MaximizedBounds = Screen.FromHandle(this.Handle).Bounds;
                    this.WindowState = FormWindowState.Maximized;
                    button4.BackgroundImage = Properties.Resources.minimize;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    button4.BackgroundImage = Properties.Resources.maximize_size_option;
                }
            }        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonColorReset(btnDashboard);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            pnlNavIndicator.Height = btnEmployees.Height;
            pnlNavIndicator.Top = btnEmployees.Top;
            pnlNavIndicator.Left = btnEmployees.Left;
            ButtonColorReset(btnEmployees);
            Form selectedForm = this.ActiveMdiChild;
            DisposeChildForms(selectedForm);
            lblTabTitle.Text = "Müşteri Paneli";
           // this.pnlContent.Controls.Clear();
            FormCustomer FrmCustomer_Vrb = new FormCustomer(user_username, user_userrole, user_token) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.pnlContent.Controls.Add(FrmCustomer_Vrb);
            FrmCustomer_Vrb.Show();
        }
        WebClient wc = new WebClient();
        NameValueCollection dataToSend = new NameValueCollection();
        private void button3_Click(object sender, EventArgs e)
        {
            dataToSend["user_username"] = user_username;
            dataToSend["user_token"] = user_token;
            string GetData = Encoding.UTF8.GetString(wc.UploadValues(@"http://127.0.0.1/logout.php", dataToSend));
            //MessageBox.Show(GetData);
            //oturum kapa formlogine geri dön
            try
            {
                List<Form> formsToClose = new List<Form>();
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name != "FormLogin")
                    {
                        formsToClose.Add(form);
                    }
                    else
                    {
                        form.Show();
                    }
                }
                foreach (Form form in formsToClose)
                {
                    form.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("", "Makul Turizm Müşteri Kayıt Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Restart();
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataToSend["user_username"] = user_username;
            dataToSend["user_token"] = user_token;
            string GetData = Encoding.UTF8.GetString(wc.UploadValues(@"http://127.0.0.1/logout.php", dataToSend));
            //MessageBox.Show(GetData);
            //oturum kapa
        }
    }
}
