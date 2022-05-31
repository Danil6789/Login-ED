using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersWinForms
{
    public partial class log_in : Form
    {
        DataBase dataBase = new DataBase();
        public log_in()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            panel1.BackColor = Color.FromArgb(64, 129, 214);
            button1.BackColor = Color.FromArgb(64, 129, 214);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            panel1.Cursor = Cursors.Default;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            //text_pass.PasswordChar = '•'; ///это тоже мусор
            text_pass.UseSystemPasswordChar = true;
            pictureBox2.Visible = false;
            text_login.MaxLength = 20;
            text_pass.MaxLength = 20;
        }
        sign_up sign = new sign_up();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            sign.ShowDialog();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = text_login.Text;
            string password = text_pass.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT login_user, password_user FROM register WHERE login_user = '{login}' and password_user = '{password}'";
            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form1 = new Form1();
                //this.Hide();
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataBase.CloseConnection();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            text_login.Text = "";
            text_pass.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            text_pass.UseSystemPasswordChar = false;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            text_pass.UseSystemPasswordChar = true;
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void text_login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void text_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
