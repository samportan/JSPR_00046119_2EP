using System;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class upPassword : Form
    {
        
        public upPassword()
        {
            InitializeComponent();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new Form1().Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals("") ||
                textBox3.Text.Equals("") ||
                textBox4.Text.Equals(""))
            {
                MessageBox.Show("Llene todos los campos");
            }
            else
            {
                try
                {
                    string query =
                        $"SELECT * FROM public.appuser WHERE username='{textBox1.Text}' AND password='{textBox2.Text}'";
                    var dt = ConnectionBD.ExecuteQuery(query);
                    
                    if (dt.Rows.Count == 1)
                    {
                        if (textBox3.Text.Equals(textBox4.Text))
                        {
                            ConnectionBD.ExecuteNonQuery($"UPDATE public.appuser SET password = '{textBox3.Text}' WHERE" +
                                                         $" username = '{textBox1.Text}'");
                            MessageBox.Show("Se ha actualizado la contraseña");
                            
                            Hide();
                            new Form1().Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o Contraseña Incorrecta");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ha ocurrido un error!");
                }
            }
        }
    }
}