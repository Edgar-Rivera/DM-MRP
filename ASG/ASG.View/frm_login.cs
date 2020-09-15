using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.IO;
using ASG.ASG.Entity;
using ASG.Properties;
using ASG.ASG.Logic;


namespace ASG
{
    public partial class frm_loggin : Form
    {
        string nombreReal; // id_usuario
        string rolUser; // rol_usuario
        string id_usuario; // nombre real
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool[] privilegios = new bool[18];
        public frm_loggin()
        {
            InitializeComponent();
            
        }
        private void timerActions()
        {
            timer1.Interval = 800;
            pictureBox1.Image = Properties.Resources.checked2;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerActions();
            timer1.Enabled = false;
            this.Hide();
            var forma = new frm_bienvenida(nombreReal, rolUser);
            forma.ShowDialog();
            frm_menu nuevo = new frm_menu(this, nombreReal, rolUser, id_usuario, pictureBox6, privilegios);
            nuevo.Show();
            textBox1.Text = "USUARIO";
            textBox1.ForeColor = Color.DimGray;
            textBox2.UseSystemPasswordChar = false;
            textBox2.Text = "CONTRASEÑA";
            textBox2.ForeColor = Color.DimGray;
            checkBox1.Checked = false;
            pictureBox1.Image = Properties.Resources.padlock__1_1;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "USUARIO")
            { 
                textBox1.Text = "";
                textBox1.ForeColor = Color.LightGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "CONTRASEÑA")
            {
                if (checkBox1.Checked == false)
                {
                    textBox2.UseSystemPasswordChar = true;
                }
                textBox2.Text = "";
                textBox2.ForeColor = Color.LightGray;
            }
           
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "USUARIO";
                textBox1.ForeColor = Color.DimGray;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "CONTRASEÑA";
                textBox2.ForeColor = Color.DimGray;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            } else
            {
                if (textBox2.Text == "CONTRASEÑA") {
                    textBox2.UseSystemPasswordChar = false;
                } else
                {
                    textBox2.UseSystemPasswordChar = true;
                }

            }
        }
        public void verimagen(PictureBox pb, string codigo)
        {
            string databasestring = "datasource=192.168.1.10;port=3306;username=root;password=1234;database=santo_tomas;Allow User Variables=True;";
            MySqlConnection connection = new MySqlConnection(databasestring);
            try
            {

                MySqlCommand cmd = new MySqlCommand("select imageUser from usuario where id_usuario = '" + codigo + "';", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "imageUser");

                DataRow dr = ds.Tables["imageUser"].Rows[0];
                if (!dr.IsNull("imageUser"))
                {
                    byte[] datos = (byte[])dr["imageUser"];
                    MemoryStream ms = new MemoryStream(datos);
                    pb.Image = Bitmap.FromStream(ms);
                }
                else
                {
                    pb.Image = Properties.Resources.user__2_1; 
                }
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.ToString());
               pb.Image = Properties.Resources.user__2_1;

            }

            connection.Close();
        }
        private void restartUI()
        {
            textBox1.Text = "USUARIO";
            textBox1.ForeColor = Color.DimGray;
            textBox2.UseSystemPasswordChar = false;
            textBox2.Text = "CONTRASEÑA";
            textBox2.ForeColor = Color.DimGray;
            checkBox1.Checked = false;
            textBox1.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int result = logicLogin.Login_Usuario(textBox1.Text.Trim(), textBox2.Text.Trim());
            if (result == 0)
            {
                MessageASG.showMessage("Usuario o contraseña incorrectos!", "Inicio de Sesion", 0);
                restartUI();

            } else if(result == 1)
            {
                nombreReal = ASG.Entity.Permisos.obtener_Nombre_Usuario();
                rolUser = ASG.Entity.Permisos.Obtener_Rol();
                id_usuario = ASG.Entity.Permisos.Obtener_Usuario();
                timerActions();
            }
            else if(result == 2)
            {
                MessageASG.showMessage("Tu cuenta ha sido desactivada!", "Inicio de Sesion", 0);
                restartUI();
            }
            else if (result == 3)
            {
                MessageASG.showMessage("El usuario ingresado no posee un rol asignado!", "Inicio de Sesion", 0);
                restartUI();
            }
        }

        // funcion que lee los datos de nombre de usuario desde la db unicamente
       
       

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {    
                if (e.KeyChar == 13)
                   button1.PerformClick();
        }

        private void frm_loggin_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void frm_loggin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void frm_loggin_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_loggin_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_loggin_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox2.Focus();
            } else if(e.KeyData == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var forma = new frm_recuperaPass();
            forma.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var forma = new frm_aboutSystem();
            forma.ShowDialog();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Up)
            {
                textBox1.Focus();
            }
            else if (e.KeyData == Keys.Down)
            {
                button1.Focus();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
