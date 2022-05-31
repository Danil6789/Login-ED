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
    public partial class sign_up : Form
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(64, 129, 214);
            button1.BackColor = Color.FromArgb(64, 129, 214);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            panel1.Cursor = Cursors.Default;
        }
        log_in log_in = null;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            log_in = new log_in();
            this.Hide();
            log_in.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (CheckUser())
            {
                return;
            }

            string login = text_login.Text;
            string password = text_pass.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"INSERT INTO register (login_user, password_user) VALUES ('{login}', '{password}')";
            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            dataBase.OpenConnection();            
       
            adapter.SelectCommand = command;
            //adapter.Fill(table); походу мусор, с ним 2 экземпляра в БЗ идет


            if (command.ExecuteNonQuery() == 1)
            {
               
                MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                log_in log_in = new log_in();
                this.Hide();
                log_in.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.CloseConnection();

           
        }

        private Boolean CheckUser()
        {
            string login = text_login.Text;
            string password = text_pass.Text;
            if (login == "" || password == "")
            {
                return true;
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"SELECT * FROM register WHERE login_user = '{login}' and password_user = '{password}'";
            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже существует", "Попробуйте ещё раз", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
                
            }
            else
            {
                return false;
            }
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            //text_pass.PasswordChar = '•'; \\\\ мусор
            text_pass.UseSystemPasswordChar = true;
            pictureBox2.Visible = false;
            text_login.MaxLength = 20;
            text_pass.MaxLength = 20;
            
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
