using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace ASG
{
    public partial class frm_envios : Form
    {
        string nameUs;
        string rolUs;
        string usuario;
        Point DragCursor;
        Point DragForm;
        bool Dragging;  
        string idSucursal;
        string nombreSucursal;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_envios(string nameUser, string rolUser, string user, string sucursal, string nombre)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            idSucursal = sucursal;
            nombreSucursal = nombre;
            stripMenu();
            label19.Text = label19.Text + "" + nameUs;
            /*
            comboBox5.SelectedIndex = 1;
            //cargaSucursales();
            comboBox1.Text = nombreSucursal;
            cargaDatos();*/
        }
        private void cargaSucursales()
        {
           /* try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }*/
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Ver Detalle de Factura");
            mymenu.Items[1].Name = "ColView";
        }
        private void my_menu_Itemclicked(object sender, ToolStripItemClickedEventArgs e)
        {
           /* if (flag == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Visible = false;
                    flag = true;
                }
                else if (e.ClickedItem.Name == "ColEdit")
                {
                    // IMPRMIR EL ENVIO
                    mymenu.Visible = false;
                    flag = true;

                }
               
                else if (e.ClickedItem.Name == "ColView")
                {
                    mymenu.Visible = false;
                    mymenu.Enabled = false;
                    SendKeys.Send("{ENTER}");
                    flag = true;

                }
            }*/
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frm_envios_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_envios_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_envios_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void frm_envios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_envios_Load(object sender, EventArgs e)
        {
            //dataGridView1.AllowUserToDeleteRows = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void cargaDatos()
        {
           
        }
        private void button4_Click(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           /* if (e.KeyData == Keys.Down)
            {
                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Focus();
                    if (dataGridView1.RowCount > 1)
                        this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.CurrentRow.Index + 1];
                }
            }*/
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
