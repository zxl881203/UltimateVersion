using System;
using System.Windows.Forms;
using NovartisTaskManager.BusinessClass;

namespace NovartisTaskManager
{
    public partial class FormLogin : Form
    {

        private DBManage dbm;
        private User u1;

        public FormLogin()
        {
            InitializeComponent();
            dbm = new DBManage();
            textBox1.Focus();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if (CheckLogin())
            {
                int usertype = u1.Type;
                this.Hide();
                switch (usertype)
                {
                    case 0:
                        FormOperate fo = new FormOperate(u1);
                        fo.Show();
                        break;
                    case 1:
                        FormManage fm = new FormManage(u1);
                        fm.Show();
                        break;
                    default:
                        FormQC1 fmq = new FormQC1(u1);
                        fmq.Show();
                        break;
                }
            }
        }

        private bool CheckLogin()

        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("输入不能为空！");
                textBox1.Focus();
                return false;
            }
            else
            {
                dbm.getConnection();
                u1 = dbm.queryUser(this.textBox1.Text);
                if (u1 == null)
                {
                    MessageBox.Show("用户不存在", "Warning", MessageBoxButtons.OK);
                    textBox1.Text = String.Empty;
                    textBox1.Focus();
                    return false;
                }
                else return true;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { 
            this.button1_Click_1(sender, e);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
