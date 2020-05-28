using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HugoApp
{
    public partial class adminuser : Form
    {
        public adminuser()
        {
            InitializeComponent();
        }
        
       private void button1_Click(object sender, EventArgs e)
       {
           if (textBox1.Text.Equals("") ||
               textBox2.Text.Equals(""))
           {
               MessageBox.Show("Llene todos los campos");
           }
           else
           {
               try
               {
                   var sl = $"SELECT * FROM public.appuser WHERE username = '{textBox2.Text}'";
                   var dt = ConnectionBD.ExecuteQuery(sl);
                   if (dt != null)
                   {
                       if (checkBox1.Checked)
                       {
                           ConnectionBD.ExecuteNonQuery($"INSERT INTO" +
                                                        $" public.appuser(fullname, username, password," +
                                                        $" userType) VALUES('{textBox1.Text}', '{textBox2.Text}'," +
                                                        $" '{textBox2.Text}'," +
                                                        $" {true})");
                       }
                       else
                       {
                           ConnectionBD.ExecuteNonQuery($"INSERT INTO" +
                                                        $" public.appuser(fullname, username, password," +
                                                        $" userType) VALUES('{textBox1.Text}', '{textBox2.Text}'," +
                                                        $" '{textBox2.Text}'," +
                                                        $" {false})");
                       }

                       MessageBox.Show("El usuario ha sido creado");
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show("El usuario ya existe...");
               }
           }
       }

       private void adminuser_Load(object sender, EventArgs e)
       {
           var users = ConnectionBD.ExecuteQuery($"SELECT username FROM public.appuser");
           var userscombo = new List<string>();

           foreach (DataRow dr in users.Rows)
           {
               userscombo.Add(dr[0].ToString());
           }

           comboBox1.DataSource = userscombo;

           var busi = ConnectionBD.ExecuteQuery($"SELECT name FROM public.business");
           var busicombo = new List<string>();
           var busicombo2 = new List<string>();
           var busicombo3 = new List<string>();

           foreach (DataRow dr in busi.Rows)
           {
               busicombo.Add(dr[0].ToString());
           }

           comboBox2.DataSource = busicombo;

           foreach (DataRow dr in busi.Rows)
           {
               busicombo2.Add(dr[0].ToString());
           }

           comboBox3.DataSource = busicombo2;

           var prod = ConnectionBD.ExecuteQuery($"SELECT name FROM public.product");
           var prodcombo = new List<string>();

           foreach (DataRow dr in prod.Rows)
           {
               prodcombo.Add(dr[0].ToString());
           }

           comboBox4.DataSource = prodcombo;
       }


       private void button3_Click(object sender, EventArgs e)
       {
           try
           {
               var dt = ConnectionBD.ExecuteQuery($"SELECT * FROM public.appuser");
           
               dataGridView1.DataSource = dt;
               MessageBox.Show("Datos obtenidos correctamente");
           }
           catch (Exception ex)
           {
               MessageBox.Show("Ha ocurrido un error.");
           }
       }

       private void button2_Click_1(object sender, EventArgs e)
       {
           var qr = $"SELECT idUser FROM public.appuser WHERE" +
                    $" username = '{comboBox1.GetItemText(comboBox1.SelectedItem)}'";
           DataTable dt = ConnectionBD.ExecuteQuery(qr);
           int idd = Convert.ToInt32(dt.Rows[0][0].ToString());
            
           ConnectionBD.ExecuteNonQuery($"DELETE FROM public.appuser WHERE idUser = {idd}");

           MessageBox.Show("El usuario ha sido eliminado!");
       }

       private void button4_Click(object sender, EventArgs e)
       {
           if (textBox3.Text.Equals("") ||
               textBox4.Text.Equals(""))
           {
               MessageBox.Show("Llene todos los campos");
           }
           else
           {
               try
               {
                   ConnectionBD.ExecuteNonQuery($"INSERT INTO public.business(name, description)" +
                                                $" VALUES('{textBox3.Text}', '{textBox4.Text}')");

                   MessageBox.Show("Se ha creado el negocio");
               }
               catch (Exception ex)
               {
                   MessageBox.Show("Ha ocurrido un error");
               }
           }
       }

       private void button5_Click(object sender, EventArgs e)
       {
           var qr = $"SELECT idBusiness FROM public.business WHERE" +
                    $" name = '{comboBox2.GetItemText(comboBox2.SelectedItem)}'";
           DataTable dt = ConnectionBD.ExecuteQuery(qr);
           int id3 = Convert.ToInt32(dt.Rows[0][0].ToString());
            
           ConnectionBD.ExecuteNonQuery($"DELETE FROM public.business WHERE idBusiness = {id3}");

           MessageBox.Show("El negocio ha sido eliminado!");
       }

       private void button6_Click(object sender, EventArgs e)
       {
           if (textBox5.Text.Equals(""))
           {
               MessageBox.Show("Llene todos los campos");
           }
           else
           {
               try
               {
                   var qr = $"SELECT idBusiness FROM public.business WHERE" +
                            $" name = '{comboBox3.GetItemText(comboBox3.SelectedItem)}'";
                   DataTable dt = ConnectionBD.ExecuteQuery(qr);
                   int id4 = Convert.ToInt32(dt.Rows[0][0].ToString());
                   
                   ConnectionBD.ExecuteNonQuery($"INSERT INTO public.product(idBusiness, name)" +
                                                $" VALUES({id4}, '{textBox5.Text}')");

                   MessageBox.Show("Se ha guardado el producto");
               }
               catch (Exception ex)
               {
                   MessageBox.Show("Ha ocurrido un error");
               }
           }
       }
       
 private void button7_Click(object sender, EventArgs e)
 {
     var qr = $"SELECT idProduct FROM public.product WHERE name" +
              $" = '{comboBox4.GetItemText(comboBox4.SelectedItem)}'";
     DataTable dt = ConnectionBD.ExecuteQuery(qr);
     int id5 = Convert.ToInt32(dt.Rows[0][0].ToString());
            
     ConnectionBD.ExecuteNonQuery($"DELETE FROM public.product WHERE idProduct = {id5}");

     MessageBox.Show("El producto ha sido eliminado!");
 }

 private void button8_Click(object sender, EventArgs e)
 {
     try
     {
         var dt = ConnectionBD.ExecuteQuery($"SELECT ao.idOrder, ao.createDate, pr.name, au.fullname," +
                                            $" ad.address FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au WHERE" +
                                            $" ao.idProduct = pr.idProduct AND ao.idAddress = ad.idAddress AND" +
                                            $" ad.idUser = au.idUser");
           
         dataGridView2.DataSource = dt;
         MessageBox.Show("Datos obtenidos correctamente");
     }
     catch (Exception ex)
     {
         MessageBox.Show("Ha ocurrido un error.");
     }
 }
    }
}