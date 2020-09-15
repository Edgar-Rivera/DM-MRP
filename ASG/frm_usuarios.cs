﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using System.IO;
using MySql.Data.MySqlClient;
/// <summary>
/// Programador: Edgar RIvera
/// Qwerty Labs 2019
/// Contiene los controles necesario para la consulta, creacion y eliminacion de un usuario con privilegios dentro del sistema
/// cada uno de los campos de privilegios estan declarados tipo BOOL se compone de un total de 32
/// </summary>


namespace ASG
{
    public partial class frm_usuarios : Form
    {
        string nameUs;
        string rolUs;
        string usuario;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool flag;
        string pathFile;
        string currenRol;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        bool[] privilegios;
        public frm_usuarios(string nameUser, string rolUser, string user, bool[] privilegio)
        {
            InitializeComponent();
            this.privilegios = privilegio;
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            cargaRoles();
            cargaDatos();
            stripMenu();
            flag = false;
            label19.Text = label19.Text + "" + nameUs;
            if(privilegios[12] != true)
            {
                tabControl1.TabPages.Remove(this.tabPage2);
            }
            if (privilegios[13] != true)
            {
                tabControl1.TabPages.Remove(this.tabPage3);
                button3.Enabled = false;
                mymenu.Items[1].Enabled = false;
                mymenu.Items[2].Enabled = false;
                mymenu.Items[3].Enabled = false;
            }

        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Editar Usuario");
            mymenu.Items[1].Name = "ColEdit";
            mymenu.Items.Add("Eliminar Usuario");
            mymenu.Items[2].Name = "ColDelete";
            mymenu.Items.Add("Activar / Desactivar Usuario");
            mymenu.Items[3].Name = "ColView";
        }
        private void my_menu_Itemclicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (flag == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Visible = false;
                    flag = true;
                }
                else if (e.ClickedItem.Name == "ColEdit")
                {
                    tabControl1.SelectedTab = tabPage3;
                    textBox14.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    getData(textBox14.Text.Trim());
                    mymenu.Visible = false;
                    flag = true;

                }
                else if (e.ClickedItem.Name == "ColDelete")
                {
                    mymenu.Visible = false;
                    mymenu.Enabled = false;
                    flag = true;
                    button3.PerformClick();
                }
                else if (e.ClickedItem.Name == "ColView")
                {
                    mymenu.Visible = false;
                    mymenu.Enabled = false;
                    flag = true;
                    if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "ACTIVO")
                    {
                        desactivaUsuario(this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    } else
                    {
                        activaUsuario(this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    }
                    
              
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" | textBox14.Text != "" | textBox6.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
                this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frm_usuarios_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_usuarios_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_usuarios_MouseUp(object sender, MouseEventArgs e)
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

        private void frm_usuarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if(textBox2.Text != "" | textBox14.Text != "" | textBox6.Text != "")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button2.PerformClick();
                } else if (tabControl1.SelectedTab == tabPage3)
                {
                    button10.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.B))
            {
                if (tabControl1.SelectedTab == tabPage3)
                {
                    button8.PerformClick();
                }
              
            }
        }
        internal class getCredencials
        {
            public bool getSesion { get; set; }
        }
        private Boolean emailValidate(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿DESEA TOMAR FOTO DESDE LA CAMARA WEB?", "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var forma = new frm_webcam();
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    pictureBox2.Image = forma.currenProfile.getProfile;
                    pathFile = forma.currentPath.getPath;
                    pathFile = pathFile.Replace("\\", "\\\\");
                }
            }
            else
            {
                try
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    opf.Filter = "Elija Imagen(*.jpg;*.png)|*.jpg;*.png";
                    opf.Title = "Seleccione Imagen de Usuario";
                    if (opf.ShowDialog() == DialogResult.OK)
                    {
                        string picPath = opf.FileName.ToString();
                        pathFile = picPath;
                        pictureBox2.ImageLocation = picPath;
                        pathFile = pathFile.Replace("\\", "\\\\");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido" + ex.ToString());
                }
            }
        }
        internal class imageUser
        {
            public Image getProfile { get; set; }
            public string getPath { get; set; }
        }
        internal class User
        {
            public string getUser { get; set; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿DESEA TOMAR FOTO DESDE LA CAMARA WEB?", "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var forma = new frm_webcam();
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    pictureBox5.Image = forma.currenProfile.getProfile;
                    pathFile = forma.currentPath.getPath;
                    pathFile = pathFile.Replace("\\", "\\\\");
                }
            }
            else
            {
                try
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    opf.Filter = "Elija Imagen(*.jpg;*.png)|*.jpg;*.png";
                    if (opf.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox5.Image = Image.FromFile(opf.FileName);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido" + ex.ToString());
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (!emailValidate(textBox4.Text))
                {
                    textBox4.BackColor = Color.OrangeRed;
                    textBox4.ForeColor = Color.White;
                }
                else
                {
                    textBox4.BackColor = Color.White;
                    textBox4.ForeColor = Color.Black;
                }
            }
            
        }

        private void frm_usuarios_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox6.Visible = false;
            textBox8.UseSystemPasswordChar = true;
            dataGridView1.AllowUserToDeleteRows = false;
            //textBox9.UseSystemPasswordChar = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT ID_USUARIO FROM USUARIO WHERE ID_USUARIO = '{0}';", textBox2.Text.Trim());
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        //pictureBox1.Image = ASG.Properties.Resources.delete__2_;
                    }
                    else
                    {
                        pictureBox1.Visible = true;
                        //pictureBox1.Image = ASG.Properties.Resources.checked2;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }
        private void cargaRoles()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_ROL FROM ROL WHERE ID_ROL <> 'ROOT' AND ID_ROL <> 'DATA BASE ADMIN';");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox4.Items.Add(reader.GetString(0));
                    comboBox1.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader.GetString(0));
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                }
                else
                {
                    comboBox4.Items.Add("NO EXISTEN ROLES");
                    comboBox4.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaDatos()
        {
            dataGridView1.Rows.Clear();
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE ID_ROL <> 'ROOT';");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1) , reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    while (reader.Read())
                    {

                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        //styleDV(this.dataGridView1);
                    }
                    setEstado(4);
                }
                else
                {
                    MessageBox.Show("NO EXISTEN USUARIOS!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
      
        private void setEstado(int celda)
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[celda].Value.ToString() == "1")
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "ACTIVO";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "INACTIVO";
                    dataGridView1.Rows[i].Cells[celda].Style.ForeColor = Color.White;
                    dataGridView1.Rows[i].Cells[celda].Style.BackColor = Color.FromArgb(207, 136, 65);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    mymenu.Show(dataGridView1, new Point(e.X, e.Y));
                    mymenu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_Itemclicked);
                    mymenu.Enabled = true;
                    flag = false;
                }
            }
        }
        private void datoGuardado()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox4.Items.Clear();
            comboBox1.Items.Clear();
            comboBox3.SelectedIndex = -1;
            textBox8.Text = "";
            checkBox35.Checked = false;
            pictureBox2.Image = null;
            timerActions();
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            cargaRoles();
            pathFile = "";
        }
        private void guardaPrivilegios(string id_usuario, string rol_usuario)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO PRIVILEGIOS_USUARIO VALUES ('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},TRUE);", id_usuario, rol_usuario,Convert.ToBoolean(checkBox9.Checked), Convert.ToBoolean(checkBox7.Checked), Convert.ToBoolean(checkBox8.Checked),Convert.ToBoolean(checkBox6.Checked), Convert.ToBoolean(checkBox1.Checked), Convert.ToBoolean(checkBox2.Checked), Convert.ToBoolean(checkBox3.Checked), Convert.ToBoolean(checkBox4.Checked), Convert.ToBoolean(checkBox5.Checked), Convert.ToBoolean(checkBox10.Checked), Convert.ToBoolean(checkBox14.Checked), Convert.ToBoolean(checkBox13.Checked), Convert.ToBoolean(checkBox12.Checked),Convert.ToBoolean(checkBox11.Checked), Convert.ToBoolean(checkBox15.Checked), Convert.ToBoolean(checkBox16.Checked), Convert.ToBoolean(checkBox17.Checked), Convert.ToBoolean(checkBox37.Checked), Convert.ToBoolean(checkBox36.Checked), Convert.ToBoolean(checkBox39.Checked), Convert.ToBoolean(checkBox40.Checked), Convert.ToBoolean(checkBox41.Checked), Convert.ToBoolean(checkBox42.Checked), Convert.ToBoolean(checkBox43.Checked), Convert.ToBoolean(checkBox44.Checked), Convert.ToBoolean(checkBox45.Checked), Convert.ToBoolean(checkBox46.Checked), Convert.ToBoolean(checkBox47.Checked), Convert.ToBoolean(checkBox48.Checked), Convert.ToBoolean(checkBox49.Checked));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    datoGuardado();
                    stateCheckbox(false);
                    cargaDatos();
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE GUARDAR PRIVILEGIOS DEL USUARIO!", "GESTION USUAIROS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void asginaConfiguracion(string usuarioRegistrado)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {

                string sql = sql = string.Format("CALL ACTUALIZA_CONFIGURACION('{0}');", usuarioRegistrado);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    //timerActions();
                    MessageBox.Show("Se han añadido configuraciones por defecto al usuario, puede cambiar las configuraciones en los ajustes del sistema!", "Nuevo Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //adressUser.ingresaBitacora(nomb, usuario, "ACTUALIZACION DE CONFIGURACION", "SYSTEM CONFIG");
                }
            }
            catch
          (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & comboBox4.Text != "" & comboBox3.Text != "" & textBox8.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT ID_USUARIO FROM USUARIO WHERE ID_USUARIO = '{0}';", textBox2.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        conexion.Close();
                        //insertBitacora();
                        conexion = ASG_DB.connectionResult();                     
                        sql = string.Format("CALL INGRESA_USUARIO ('{0}','{1}','{2}','{3}','{4}',{5},'{6}')", textBox2.Text.Trim(), comboBox4.Text.Trim(), textBox8.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), comboBox3.SelectedIndex, null);
                        cmd = new OdbcCommand(sql, conexion);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            asginaConfiguracion(textBox2.Text);
                            if(pictureBox2.Image != null)
                            {
                                imagenUsuario(textBox2.Text, pictureBox2);
                            }
                            guardaPrivilegios(textBox2.Text.Trim(), comboBox4.Text.Trim());
                        }
                        else
                        {
                            MessageBox.Show("IMPOSIBLE GUARDAR USUARIO!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    } else
                    {
                        MessageBox.Show("IMPOSIBLE GUARDAR USUARIO, EL USUARIO YA EXISTE!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } else
            {
                MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }        
        }
        private void imagenUsuario(string codigo, PictureBox pb)
        {

            try
            {
                string connectionString = "datasource=192.168.1.10;port=3306;username=root;password=1234;database=santo_tomas;Allow User Variables=True;";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                databaseConnection.Open();
                byte[] imagen = Convertir_Imagen_Bytes(pb.Image);
                MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET imageUser = @avatar WHERE id_usuario = '" + codigo + "'", databaseConnection);
                MySqlParameter parImagen = new MySqlParameter();
                parImagen.ParameterName = "avatar";
                parImagen.MySqlDbType = MySqlDbType.MediumBlob;
                parImagen.Size = 16000000;
                parImagen.Value = imagen;
                cmd.Parameters.Add(parImagen);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    //MessageBox.Show("DATOS GUARDADOS DE LA IMAGEN DEL USUARIO SUPUESTAMENTE");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static byte[] Convertir_Imagen_Bytes(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        private void getPrivilegios(string id_usuario, string id_rol)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM PRIVILEGIOS_USUARIO WHERE USUARIO_ID_USUARIO = '{0}' AND ROL_ID_ROL = '{1}';", id_usuario, id_rol);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    checkBox34.Checked = reader.GetBoolean(2);
                    checkBox32.Checked = reader.GetBoolean(3); 
                    checkBox33.Checked = reader.GetBoolean(4);
                    checkBox31.Checked = reader.GetBoolean(5);
                    checkBox30.Checked = reader.GetBoolean(6);
                    checkBox29.Checked = reader.GetBoolean(7);
                    checkBox28.Checked = reader.GetBoolean(8);
                    checkBox27.Checked = reader.GetBoolean(9);
                    checkBox26.Checked = reader.GetBoolean(10);
                    checkBox25.Checked = reader.GetBoolean(11);
                    checkBox24.Checked = reader.GetBoolean(12);
                    checkBox23.Checked = reader.GetBoolean(13);
                    checkBox22.Checked = reader.GetBoolean(14);
                    checkBox21.Checked = reader.GetBoolean(15);
                    checkBox20.Checked = reader.GetBoolean(16);
                    checkBox19.Checked = reader.GetBoolean(17);
                    checkBox18.Checked = reader.GetBoolean(18);
                    checkBox38.Checked = reader.GetBoolean(19);
                    checkBox61.Checked = reader.GetBoolean(20);
                    checkBox60.Checked = reader.GetBoolean(21);
                    checkBox59.Checked = reader.GetBoolean(22);
                    checkBox58.Checked = reader.GetBoolean(23);
                    checkBox57.Checked = reader.GetBoolean(24);
                    checkBox56.Checked = reader.GetBoolean(25);
                    checkBox55.Checked = reader.GetBoolean(26);
                    checkBox54.Checked = reader.GetBoolean(27);
                    checkBox53.Checked = reader.GetBoolean(28);
                    checkBox52.Checked = reader.GetBoolean(29);
                    checkBox51.Checked = reader.GetBoolean(30);
                    checkBox50.Checked = reader.GetBoolean(31);
                } else
                {
                    MessageBox.Show("NO SE ENCONTRO");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void getData(string usuario)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT id_usuario, id_rol, nombre_usuario,email,estado_usuario FROM USUARIO WHERE ID_USUARIO = '{0}';", usuario);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {              
                    textBox6.Text = reader.GetString(0);
                    textBox10.Text = reader.GetString(1);
                    textBox5.Text = reader.GetString(2);
                    textBox7.Text = reader.GetString(3);
                    comboBox2.SelectedIndex = reader.GetInt32(4);
                    verimagen(pictureBox5, textBox6.Text);
                    currenRol = comboBox1.Text;
                    getPrivilegios(textBox6.Text.Trim(), textBox10.Text.Trim());
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("NO EXISTEN EL USUARIO!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox14.Text = "";
                    textBox14.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        

        private void timerActions()
        {
            frm_done frm = new frm_done();
            frm.ShowDialog();
            timer1.Interval = 1500;
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox35.Checked == true)
            {
                textBox8.UseSystemPasswordChar =false;
            } else
            {
                textBox8.UseSystemPasswordChar = true;
            }
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                getData(textBox14.Text);
            }
        }

        
        private bool createFolder()
        {
            string carpeta = Application.StartupPath + @"\IMAGE_USERS";
            try
            {
                if (Directory.Exists(carpeta))
                {
                    return true;
                } else
                {
                    Directory.CreateDirectory(carpeta);
                    return true;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (createFolder())
            {
                MessageBox.Show("YA EXISTE CARPETA");
;           } else
            {
                MessageBox.Show("NO EXISTE");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA BORRAR EL USUARIO?" + "\n" + "  " + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - " + this.dataGridView1.CurrentRow.Cells[2].Value.ToString(), "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE USUARIO SET ESTADO_TUPLA = FALSE WHERE ID_USUARIO = '{0}';", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            timerActions();
                            //insertBitacoradel();
                            cargaDatos();
                        }
                        else
                        {
                            MessageBox.Show("NO SE ENCONTRO EL USUARIO!", "GESTION USAURIOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    conexion.Close();
                }
                else
                {
                    button4.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("INGRESE UN NIT VALIDO!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
        }
        private void desactivaUsuario(string id, string nombre)
        {
            if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                DialogResult result;                
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA DESACTIVAR EL USUARIO?" + "\n" + "  " + id + " - " + nombre, "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE USUARIO SET ESTADO_USUARIO = FALSE WHERE ID_USUARIO = '{0}';", id);
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            timerActions();
                            //insertBitacoradel();
                            cargaDatos();
                        }
                        else
                        {
                            MessageBox.Show("NO SE ENCONTRO EL USUARIO!", "GESTION USAURIOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    conexion.Close();
                }
                else
                {
                    button4.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("INGRESE UN NIT VALIDO!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
        }
        private void activaUsuario(string id, string nombre)
        {
            if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ACTIVAR EL USUARIO?" + "\n" + "  " + id + " - " + nombre, "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE USUARIO SET ESTADO_USUARIO = TRUE WHERE ID_USUARIO = '{0}';", id);
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            timerActions();
                            //insertBitacoradel();
                            cargaDatos();
                        }
                        else
                        {
                            MessageBox.Show("NO SE ENCONTRO EL USUARIO!", "GESTION USAURIOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    conexion.Close();
                }
                else
                {
                    button4.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("INGRESE UN NIT VALIDO!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
        }
        private void setDescripcion(int celda)
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[celda].Style.ForeColor = Color.White;
                dataGridView1.Rows[i].Cells[celda].Style.BackColor = Color.FromArgb(207, 136, 65);
            }
        }
        private void cargaSeleccion(int numero)
        {
            dataGridView1.Rows.Clear();
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE ESTADO_USUARIO = {0};",numero);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    while (reader.Read())
                    {

                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        //styleDV(this.dataGridView1);
                    }
                    setEstado(4);
                    setDescripcion(4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "INACTIVOS")
            {
                cargaSeleccion(0);
            }
            else if (comboBox5.Text == "ACTIVOS")
            {
                cargaSeleccion(1);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            if (textBox1.Text != "") {
                dataGridView1.Rows.Clear();
                try
                {
                    string sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE ID_USUARIO LIKE '%{0}%' AND ID_ROL <> 'ROOT';", textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        while (reader.Read())
                        {

                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                            //styleDV(this.dataGridView1);
                        }
                        setEstado(4);
                        setDescripcion(0);
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE ID_ROL LIKE '%{0}%' AND ID_ROL <> 'ROOT';", textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                            while (reader.Read())
                            {

                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                //styleDV(this.dataGridView1);
                            }
                            setEstado(4);
                            setDescripcion(1);
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE NOMBRE_USUARIO LIKE '%{0}%' AND ID_ROL <> 'ROOT';", textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                while (reader.Read())
                                {

                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                    //styleDV(this.dataGridView1);
                                }
                                setEstado(4);
                                setDescripcion(2);
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE EMAIL LIKE '%{0}%' AND ID_ROL <> 'ROOT';", textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                    while (reader.Read())
                                    {

                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                        //styleDV(this.dataGridView1);
                                    }
                                    setEstado(4);
                                    setDescripcion(3);
                                }
                                else
                                {
                                    sql = string.Format("SELECT * FROM VISTA_USUARIOS WHERE LAST_LOGGIN LIKE '%{0}%' AND ID_ROL <> 'ROOT';", textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                        while (reader.Read())
                                        {

                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                                            //styleDV(this.dataGridView1);
                                        }
                                        setEstado(4);
                                        setDescripcion(5);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } else
            {
                cargaDatos();
            }
            conexion.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void stateCheckbox(bool estado)
        {
            checkBox9.Checked = estado;
            checkBox7.Checked = estado;
            checkBox8.Checked = estado;
            checkBox6.Checked = estado;
            checkBox1.Checked = estado;
            checkBox2.Checked = estado;
            checkBox3.Checked = estado;
            checkBox4.Checked = estado;
            checkBox5.Checked = estado;
            checkBox10.Checked = estado;
            checkBox14.Checked = estado;
            checkBox13.Checked = estado;
            checkBox12.Checked = estado;
            checkBox11.Checked = estado;
            checkBox15.Checked = estado;
            checkBox16.Checked = estado;
            checkBox17.Checked = estado;
            checkBox37.Checked = estado;
            /// <summary>
            /// SE DESACRIVAN LSO CONTROLES DE EL CHECKBOX POR QUE HA SELECCIONADO QUE NO DESEA VER LOS REPORTES
            /// </summary>
            /// 
            checkBox36.Checked = estado;
            checkBox39.Checked = estado;
            checkBox40.Checked = estado;
            checkBox41.Checked = estado;
            checkBox42.Checked = estado;
            checkBox43.Checked = estado;
            checkBox44.Checked = estado;
            checkBox45.Checked = estado;
            checkBox46.Checked = estado;
            checkBox47.Checked = estado;
            checkBox48.Checked = estado;
            checkBox49.Checked = estado;
        }
        private void stateCheckboxEdit(bool estado)
        {
            checkBox34.Checked = estado;
            checkBox32.Checked = estado;
            checkBox33.Checked = estado;
            checkBox31.Checked = estado;
            checkBox30.Checked = estado;
            checkBox29.Checked = estado;
            checkBox28.Checked = estado;
            checkBox27.Checked = estado;
            checkBox26.Checked = estado;
            checkBox25.Checked = estado;
            checkBox24.Checked = estado;
            checkBox23.Checked = estado;
            checkBox22.Checked = estado;
            checkBox21.Checked = estado;
            checkBox20.Checked = estado;
            checkBox19.Checked = estado;
            checkBox18.Checked = estado;
            checkBox38.Checked = estado;
            checkBox61.Checked = estado;
            checkBox60.Checked = estado;
            checkBox59.Checked = estado;
            checkBox58.Checked = estado;
            checkBox57.Checked = estado;
            checkBox56.Checked = estado;
            checkBox55.Checked = estado;
            checkBox54.Checked = estado;
            checkBox53.Checked = estado;
            checkBox52.Checked = estado;
            checkBox51.Checked = estado;
            checkBox50.Checked = estado;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox4.Text == "ADMINISTRADOR") | (comboBox4.Text == "DATA BASE ADMIN"))
            {
                stateCheckbox(true);
            } else if ((comboBox4.Text == "CAJERO"))
            {
                checkBox9.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox6.Checked = true;
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox13.Checked = false;
                checkBox12.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
                checkBox17.Checked = false;
            }
            else if ((comboBox4.Text == "VENDEDOR"))
            {
                checkBox9.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = true;
                checkBox6.Checked = true;
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox13.Checked = false;
                checkBox12.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
                checkBox17.Checked = false;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox8.Focus();
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.BackColor == Color.OrangeRed)
            {
                textBox4.BackColor = Color.White;
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                comboBox4.Focus();
            }
            else if (e.KeyData == Keys.Space)
            {
                if (textBox4.Text == "")
                {
                    textBox4.Text = "noaplica@noaplica.com";
                    comboBox4.Focus();
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
                    pb.Image = null;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());

            }

            connection.Close();
        }
      
        private void button8_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaUsuario();
            forma.ShowDialog();
            if (forma.DialogResult == DialogResult.OK)
            {
                textBox14.Text = forma.currentUser.getUser;
                if (textBox14.Text != "")
                {
                    getData(textBox14.Text);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" | textBox3.Text != "" | textBox4.Text != "" | comboBox4.Text != "" | textBox8.Text != "" | comboBox3.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox8.Text = "";
                    stateCheckbox(false);
                    comboBox3.SelectedIndex = -1;
                    comboBox4.Items.Clear();
                    pictureBox2.Image = null;
                    cargaRoles();
                }
            }

        }
        private void privilegiosDefecto()
        {
            if ((comboBox1.Text == "ADMINISTRADOR") | (comboBox1.Text == "DATA BASE ADMIN"))
            {
                stateCheckboxEdit(true);
            }
            else if ((comboBox1.Text == "CAJERO"))
            {
                checkBox34.Checked = false;
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox31.Checked = true;
                checkBox30.Checked = false;
                checkBox29.Checked = true;
                checkBox28.Checked = true;
                checkBox27.Checked = true;
                checkBox26.Checked = true;
                checkBox25.Checked = false;
                checkBox24.Checked = false;
                checkBox23.Checked = false;
                checkBox22.Checked = false;
                checkBox21.Checked = false;
                checkBox20.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;


            }
            else if ((comboBox1.Text == "VENDEDOR"))
            {
                checkBox34.Checked = false;
                checkBox32.Checked = false;
                checkBox33.Checked = true;
                checkBox31.Checked = true;
                checkBox30.Checked = false;
                checkBox29.Checked = true;
                checkBox28.Checked = true;
                checkBox27.Checked = true;
                checkBox26.Checked = true;
                checkBox25.Checked = false;
                checkBox24.Checked = false;
                checkBox23.Checked = false;
                checkBox22.Checked = false;
                checkBox21.Checked = false;
                checkBox20.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿DESEA ASIGNAR PRIVILEGIOS POR DEFECTO?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    privilegiosDefecto();
                }
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox7.Focus();
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                comboBox1.Focus();
            } else if (e.KeyData == Keys.Space)
            {
                if (textBox7.Text == "")
                {
                    textBox7.Text = "noaplica@noaplica.com";
                    comboBox1.Focus();
                }
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" | textBox5.Text != "" | textBox7.Text != "" | comboBox1.Text != "" | comboBox2.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
                    //textBox9.Text = "";
                    textBox14.Text =  "";
                    textBox10.Text = "";
                    stateCheckboxEdit(false);
                    comboBox2.SelectedIndex = -1;
                    comboBox1.Items.Clear();
                    pictureBox5.Image = null;
                    cargaRoles();
                }
            }

        }
        private void edicionGuardada()
        {
            textBox6.Text = "";
            textBox5.Text = "";
            //textBox9.Text = "";
            textBox7.Text = "";
            textBox14.Text = "";
            textBox10.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            stateCheckboxEdit(false);
            timerActions();
            pictureBox5.Image = null;
        }
        private void actualizaPrivilegios()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string temp;
                if (comboBox1.Text != "")
                {
                    temp = comboBox1.Text;
                }
                else
                {
                    temp = textBox10.Text;
                }
                string sql = string.Format("CALL UPDATE_PRIVILEGIOS ('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32});", textBox6.Text.Trim(), temp, temp, Convert.ToBoolean(checkBox34.Checked), Convert.ToBoolean(checkBox32.Checked), Convert.ToBoolean(checkBox33.Checked), Convert.ToBoolean(checkBox31.Checked), Convert.ToBoolean(checkBox30.Checked), Convert.ToBoolean(checkBox29.Checked), Convert.ToBoolean(checkBox28.Checked), Convert.ToBoolean(checkBox27.Checked), Convert.ToBoolean(checkBox26.Checked), Convert.ToBoolean(checkBox25.Checked), Convert.ToBoolean(checkBox24.Checked), Convert.ToBoolean(checkBox23.Checked), Convert.ToBoolean(checkBox22.Checked), Convert.ToBoolean(checkBox21.Checked), Convert.ToBoolean(checkBox20.Checked), Convert.ToBoolean(checkBox19.Checked), Convert.ToBoolean(checkBox18.Checked), Convert.ToBoolean(checkBox38.Checked), Convert.ToBoolean(checkBox61.Checked), Convert.ToBoolean(checkBox60.Checked),Convert.ToBoolean(checkBox59.Checked), Convert.ToBoolean(checkBox58.Checked), Convert.ToBoolean(checkBox57.Checked), Convert.ToBoolean(checkBox56.Checked), Convert.ToBoolean(checkBox55.Checked), Convert.ToBoolean(checkBox54.Checked), Convert.ToBoolean(checkBox53.Checked), Convert.ToBoolean(checkBox52.Checked), Convert.ToBoolean(checkBox51.Checked), Convert.ToBoolean(checkBox50.Checked));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        edicionGuardada();
                        cargaDatos();
                    }
                }
                else
                {
                    edicionGuardada();
                    cargaDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void updatePrivilegios()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string temp;
                if (comboBox1.Text != "")
                {
                    temp = comboBox1.Text;
                }
                else
                {
                    temp = textBox10.Text;
                }
                string sql = string.Format("SELECT USUARIO_ID_USUARIO, ROL_ID_ROL FROM PRIVILEGIOS_USUARIO WHERE USUARIO_ID_USUARIO = '{0}' AND ROL_ID_ROL = '{1}'", textBox6.Text.Trim(), temp);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   // MessageBox.Show("IR A ACTUALIZARR NAD AMAS");
                    actualizaPrivilegios();
                } else
                {
                    //MessageBox.Show("SE CREARAN");
                    guardaPrivilegiosEdicion(textBox6.Text.Trim(), temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void updateUser()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string temp;
                if(pathFile == "")
                {
                    pathFile = "  ";
                }
                if(comboBox1.Text != "")
                {
                    temp = comboBox1.Text;
                } else
                {
                    temp = textBox10.Text;
                }
                string sql = string.Format("CALL ACTUALIZA_USUARIO ('{0}','{1}','{2}','{3}',{4},'{5}');",textBox6.Text.Trim(),textBox5.Text.Trim(),textBox7.Text.Trim(),temp,comboBox2.SelectedIndex, null);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                { 
                    if(pictureBox5.Image != null)
                    {
                        imagenUsuario(textBox6.Text, pictureBox5);
                    }
                    updatePrivilegios();
                }
                else
                {
                    if (pictureBox5.Image != null)
                    {
                        imagenUsuario(textBox6.Text, pictureBox5);
                    }
                    updatePrivilegios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void guardaPrivilegiosEdicion(string id_usuario, string rol_usuario)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO PRIVILEGIOS_USUARIO VALUES ('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}TRUE);", id_usuario, rol_usuario, Convert.ToBoolean(checkBox34.Checked), Convert.ToBoolean(checkBox32.Checked), Convert.ToBoolean(checkBox33.Checked), Convert.ToBoolean(checkBox31.Checked), Convert.ToBoolean(checkBox30.Checked), Convert.ToBoolean(checkBox29.Checked), Convert.ToBoolean(checkBox28.Checked), Convert.ToBoolean(checkBox27.Checked), Convert.ToBoolean(checkBox26.Checked), Convert.ToBoolean(checkBox25.Checked), Convert.ToBoolean(checkBox24.Checked), Convert.ToBoolean(checkBox23.Checked), Convert.ToBoolean(checkBox22.Checked), Convert.ToBoolean(checkBox21.Checked), Convert.ToBoolean(checkBox20.Checked), Convert.ToBoolean(checkBox19.Checked), Convert.ToBoolean(checkBox18.Checked), Convert.ToBoolean(checkBox38.Checked), Convert.ToBoolean(checkBox61.Checked), Convert.ToBoolean(checkBox60.Checked), Convert.ToBoolean(checkBox59.Checked), Convert.ToBoolean(checkBox58.Checked), Convert.ToBoolean(checkBox57.Checked), Convert.ToBoolean(checkBox56.Checked), Convert.ToBoolean(checkBox55.Checked), Convert.ToBoolean(checkBox54.Checked), Convert.ToBoolean(checkBox53.Checked), Convert.ToBoolean(checkBox52.Checked), Convert.ToBoolean(checkBox51.Checked), Convert.ToBoolean(checkBox50.Checked));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    edicionGuardada();
                    cargaDatos();
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE GUARDAR PRIVILEGIOS DEL USUARIO!", "GESTION USUAIROS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" & textBox5.Text != "" & textBox7.Text != ""  & comboBox2.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("LOS NUEVOS CAMBIOS Y PRIVILEGIOS SE APLICARAN AL USUARIO, DESEA CONTINUAR?", "GESTION USUARIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string currentUser = textBox6.Text;
                    updateUser();
                    if (usuario == currentUser)
                    {
                        notifyIcon1.ShowBalloonTip(2500);
                    }

                }
            }
            else
            {
                MessageBox.Show("DEBE COMPLETAR LOS DATOS!", "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var forma = new frm_userDetails(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                forma.ShowDialog();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.BackColor == Color.OrangeRed)
            {
                textBox7.BackColor = Color.White;
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                if (!emailValidate(textBox7.Text))
                {
                    textBox7.BackColor = Color.OrangeRed;
                    textBox7.ForeColor = Color.White;
                }
                else
                {
                    textBox7.BackColor = Color.White;
                    textBox7.ForeColor = Color.Black;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Focus();
                    if (dataGridView1.RowCount > 1)
                        this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.CurrentRow.Index + 1];
                }
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if(textBox6.Text != "")
            {
                var forma = new frm_cambiarContrasenia(textBox6.Text.Trim());
                if(forma.ShowDialog() == DialogResult.OK)
                {
                    if (usuario == textBox6.Text.Trim())
                    {
                        notifyIcon1.ShowBalloonTip(2500);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                checkBox36.Enabled = true;
                checkBox39.Enabled = true;
                checkBox40.Enabled = true;
                checkBox41.Enabled = true;
                checkBox42.Enabled = true;
                checkBox43.Enabled = true;
                checkBox44.Enabled = true;
                checkBox45.Enabled = true;
                checkBox46.Enabled = true;
                checkBox47.Enabled = true;
                checkBox48.Enabled = true;
                checkBox49.Enabled = true;
            } else
            {
                checkBox36.Enabled = false;
                checkBox39.Enabled = false;
                checkBox40.Enabled = false;
                checkBox41.Enabled = false;
                checkBox42.Enabled = false;
                checkBox43.Enabled = false;
                checkBox44.Enabled = false;
                checkBox45.Enabled = false;
                checkBox46.Enabled = false;
                checkBox47.Enabled = false;
                checkBox48.Enabled = false;
                checkBox49.Enabled = false;
                /// <sumary>
                /// SE DESABILITAN LOS CONTROLES DE ESTADO DEL CHECKBOX PO RQUE SE HA SELECCIONADO QUE NO VA A VER REPROTES
                /// </sumary>
                /// 
                checkBox36.Checked = false;
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;

            }
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox30.Checked == true)
            {
                checkBox61.Enabled = true;
                checkBox60.Enabled = true;
                checkBox59.Enabled = true;
                checkBox58.Enabled = true;
                checkBox57.Enabled = true;
                checkBox56.Enabled = true;
                checkBox55.Enabled = true;
                checkBox54.Enabled = true;
                checkBox53.Enabled = true;
                checkBox52.Enabled = true;
                checkBox51.Enabled = true;
                checkBox50.Enabled = true;
            } else
            {
                checkBox61.Enabled = false;
                checkBox60.Enabled = false;
                checkBox59.Enabled = false;
                checkBox58.Enabled = false;
                checkBox57.Enabled = false;
                checkBox56.Enabled = false;
                checkBox55.Enabled = false;
                checkBox54.Enabled = false;
                checkBox53.Enabled = false;
                checkBox52.Enabled = false;
                checkBox51.Enabled = false;
                checkBox50.Enabled = false;
                // PONER EN FALSO PARA NO TENER NADA DE ACCESO
                checkBox61.Checked = false;
                checkBox60.Checked = false;
                checkBox59.Checked = false;
                checkBox58.Checked = false;
                checkBox57.Checked = false;
                checkBox56.Checked = false;
                checkBox55.Checked = false;
                checkBox54.Checked = false;
                checkBox53.Checked = false;
                checkBox52.Checked = false;
                checkBox51.Checked = false;
                checkBox50.Checked = false;
            }
        }

        private void button11_Click_2(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                var forma = new frm_cambiarContrasenia(textBox6.Text.Trim());
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    if (usuario == textBox6.Text.Trim())
                    {
                        notifyIcon1.ShowBalloonTip(2500);
                    }
                }
            }
        }
       
        private void button6_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿DESEA TOMAR FOTO DESDE LA CAMARA WEB?", "GESTION USUARIOS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var forma = new frm_webcam();
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    pictureBox5.Image = forma.currenProfile.getProfile;
                    pathFile = forma.currentPath.getPath;
                    pathFile = pathFile.Replace("\\", "\\\\");
                }
            }
            else if(result == System.Windows.Forms.DialogResult.No)
            {
                try
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    opf.Filter = "Elija Imagen(*.jpg;*.png)|*.jpg;*.png";
                    if (opf.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox5.Image = Image.FromFile(opf.FileName);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido" + ex.ToString());
                }
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿DESEA TOMAR FOTO DESDE LA CAMARA WEB?", "GESTION USUARIOS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var forma = new frm_webcam();
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    pictureBox2.Image = forma.currenProfile.getProfile;
                    pathFile = forma.currentPath.getPath;
                    pathFile = pathFile.Replace("\\", "\\\\");
                }
            }
            else if(result == System.Windows.Forms.DialogResult.No)
            {
                try
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    opf.Filter = "Elija Imagen(*.jpg;*.png)|*.jpg;*.png";
                    opf.Title = "Seleccione Imagen de Usuario";
                    if (opf.ShowDialog() == DialogResult.OK)
                    {
                        string picPath = opf.FileName.ToString();
                        pathFile = picPath;
                        pictureBox2.ImageLocation = picPath;
                        pathFile = pathFile.Replace("\\", "\\\\");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido" + ex.ToString());
                }
            }
        }

        private void checkBox35_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox35.Checked == true)
            {
                textBox8.UseSystemPasswordChar = false;
            }
            else
            {
                textBox8.UseSystemPasswordChar = true;
            }
        }
        private void fullScreen_Click(object sender, EventArgs e)
        {
            ((Form)sender).Close();
        }
        private void FullScreenImage(Image imageToShow)
        {
            Form fullScreenForm = new Form()
            {
                WindowState = FormWindowState.Maximized,
                Text = "Vista Previa Imagen",
                FormBorderStyle = FormBorderStyle.Sizable,
                BackgroundImage = imageToShow,
                BackgroundImageLayout = ImageLayout.Zoom
                
            };

            fullScreenForm.Click += fullScreen_Click;

            fullScreenForm.ShowDialog();
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
            {
                FullScreenImage(((PictureBox)sender).Image);
            }
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            if (pictureBox5.Image != null)
            {
                FullScreenImage(((PictureBox)sender).Image);
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                DialogResult result;
                result = MessageBox.Show("¿Esta seguro que desea eliminar la imagen del usuario?", "Gestion Usuarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    pictureBox2.Image = null;
                }
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (pictureBox5.Image != null)
            {
                DialogResult result;
                result = MessageBox.Show("¿Esta seguro que desea eliminar la imagen del usuario?", "Gestion Usuarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    pictureBox5.Image = null;
                }
            }
        }

        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}