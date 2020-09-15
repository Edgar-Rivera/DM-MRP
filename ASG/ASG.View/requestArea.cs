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
    public partial class requestArea : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string areaS;
        bool estadoS;
        public requestArea(string orden, string area, string codigo, string desc, bool estado)
        {
            InitializeComponent();
            areaS = area;
            label3.Text = orden;
            textBox3.Text = codigo;
            textBox2.Text = desc;
            textBox1.Focus();
            estadoS = estado;
            // verdadero es ingreso 1  | falso es retiro
            if (estado)
            {
                label6.Text = "Se solicita por:";
            } else
            {
                label6.Text = "Se perdio por:";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR? LOS CAMBIOS NO SE REALIZARAN", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void requestArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (textBox1.Text != "")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR? LOS CAMBIOS NO SE REALIZARAN", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void requestArea_MouseDown(object sender, MouseEventArgs e)
        {

            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;

        }

        private void requestArea_MouseMove(object sender, MouseEventArgs e)
        {

            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void requestArea_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void requestArea_Load(object sender, EventArgs e)
        {
            
        }
        private bool pedidoCantidades(string area, string ordenes, string merca, string cantidad)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("update proceso_produccion_has_materia_prima set cantidad_agregada = cantidad_agregada + {0} where id_proceso_produccion = {1} and id_orden_produccion = {2} and id_materia_prima = '{3}';", cantidad, area, ordenes,merca );
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private bool retiroCantidades(string area, string ordenes, string merca, string cantidad)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("update proceso_produccion_has_materia_prima set cantidad_perdida = cantidad_perdida + {0} where id_proceso_produccion = {1} and id_orden_produccion = {2} and id_materia_prima = '{3}';", cantidad, area, ordenes, merca);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (estadoS)
                {
                    if (pedidoCantidades(areaS, label3.Text, textBox3.Text,textBox1.Text))
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        DialogResult = DialogResult.OK;
                    } else
                    {
                        MessageBox.Show("La cantidad no se ha ingresado con exito!");
                    }
                } else
                {
                    if (retiroCantidades(areaS, label3.Text, textBox3.Text, textBox1.Text))
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("La cantidad no se ha ingresado con exito!");
                    }
                }
            }else
            {
                MessageBox.Show("No ha ingresado una cantidad valida!");
            }
        }
    }
}
