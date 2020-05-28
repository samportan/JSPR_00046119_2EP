using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class Form1 : Form
    {
        private static int n;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        public void login()
        {
            try
            {
                string query =
                    $"SELECT * FROM public.appuser WHERE username='{textBox1.Text}' AND password='{textBox2.Text}'";

                var dt = ConnectionBD.ExecuteQuery(query);
                bool admin = Convert.ToBoolean(dt.Rows[0][4].ToString());

                if (dt.Rows.Count == 1)
                {
                    Hide();
                    if (admin)
                    {
                        new adminuser().Show();
                    }
                    else
                    {
                        var dr = dt.Rows[0][0];
                        int n = Convert.ToInt32(dr);
                        new normaluser(n, dt.Rows[0][2].ToString()).Show();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña Incorrecta");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }
        
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Hide();
        new upPassword().Show();
    }
    
    }
}