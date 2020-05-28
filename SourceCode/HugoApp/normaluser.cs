using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

namespace HugoApp
{
    public partial class normaluser : Form
    {   
        private string username = "";
        private int id;
        public normaluser(int x, string y)
        {
            InitializeComponent();
            id = x;
            username = y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Llene todos los campos!");
            }
            else
            {
                try
                {
                    ConnectionBD.ExecuteNonQuery($"INSERT INTO public.address(idUser, address)" +
                                                 $" VALUES({id}, '{textBox1.Text}')");

                    MessageBox.Show("La direccion se ha guardado!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error!");
                }
            }
        }
        
        private void normaluser_Load(object sender, EventArgs e)
        {
            var addr = ConnectionBD.ExecuteQuery($"SELECT address FROM public.address" +
                                                 $" WHERE idUser = {id}");
            var addrcombo = new List<string>();
            var addrcombo2 = new List<string>();
            var addrcombo3 = new List<string>();

            foreach (DataRow dr in addr.Rows)
            {
                addrcombo.Add(dr[0].ToString());
            }
            
            foreach (DataRow dr in addr.Rows)
            {
                addrcombo2.Add(dr[0].ToString());
            }
            
            foreach (DataRow dr in addr.Rows)
            {
                addrcombo3.Add(dr[0].ToString());
            }

            comboBox1.DataSource = addrcombo;
            comboBox2.DataSource = addrcombo2;
            comboBox4.DataSource = addrcombo3;
            
            var prod = ConnectionBD.ExecuteQuery($"SELECT name FROM public.product");
            var prodcombo = new List<string>();

            foreach (DataRow dr in prod.Rows)
            {
                prodcombo.Add(dr[0].ToString());
            }

            comboBox3.DataSource = prodcombo;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var qr = $"SELECT idAddress FROM public.address WHERE" +
                     $" address = '{comboBox1.GetItemText(comboBox1.SelectedItem)}'";
            DataTable dt = ConnectionBD.ExecuteQuery(qr);
            int idd = Convert.ToInt32(dt.Rows[0][0].ToString());
            
            ConnectionBD.ExecuteNonQuery($"DELETE FROM public.address WHERE idAddress = {idd}");

            MessageBox.Show("La direccion ha sido eliminada!");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Llene todos los campos");
            }
            else
            {
                try
                {
                    var qr =
                        $"SELECT idAddress FROM public.address WHERE" +
                        $" address = '{comboBox2.GetItemText(comboBox2.SelectedItem)}'";
                    DataTable dt = ConnectionBD.ExecuteQuery(qr);
                    int id2 = Convert.ToInt32(dt.Rows[0][0].ToString());

                    //MODIFICAR DIRECCION
                    ConnectionBD.ExecuteNonQuery(
                        $"UPDATE public.address SET address = '{textBox2.Text}' WHERE idAddress = {id2}");

                    MessageBox.Show("Se ha modificado la direccion");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ha ocurrido un error");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var qr =
                    $"SELECT idAddress FROM public.address WHERE" +
                    $" address = '{comboBox4.GetItemText(comboBox4.SelectedItem)}'";
                DataTable dt = ConnectionBD.ExecuteQuery(qr);
                int id3 = Convert.ToInt32(dt.Rows[0][0].ToString());
            
                var qr2 = $"SELECT idProduct FROM public.product WHERE" +
                          $" name = '{comboBox3.GetItemText(comboBox3.SelectedItem)}'";
                DataTable dt2 = ConnectionBD.ExecuteQuery(qr2);
                int id4 = Convert.ToInt32(dt2.Rows[0][0].ToString());

                var date = ((DateTime.Now)).ToString(@"yyyy-MM-dd");;

                ConnectionBD.ExecuteNonQuery($"INSERT INTO public.appOrder(createDate, idProduct, idAddress)" +
                                             $" VALUES('{date}', {id4}, {id3})");

                MessageBox.Show("Su orden ha sido procesada");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ha ocurrido un error!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = ConnectionBD.ExecuteQuery($"SELECT ao.idOrder, ao.createDate, pr.name," +
                                                   $" au.fullname, ad.address FROM APPORDER ao, ADDRESS ad," +
                                                   $" PRODUCT pr, APPUSER au WHERE ao.idProduct = pr.idProduct AND" +
                                                   $" ao.idAddress = ad.idAddress AND ad.idUser = au.idUser AND" +
                                                   $" au.idUser = {id}");
           
                dataGridView1.DataSource = dt;
                MessageBox.Show("Datos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error.");
            }
        }
    }
}