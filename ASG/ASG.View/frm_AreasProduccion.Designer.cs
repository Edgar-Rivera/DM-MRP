using System.Security.Cryptography;

namespace ASG.ASG.View
{
    partial class frm_AreasProduccion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public void setDetallesArea(string id, string nombre, string descripcion, string idEncargado, string encargado)
        {
            txtIdArea.Text = id;
            txtNArea.Text = nombre;
            txtDArea.Text = descripcion;
            TxtEncargadoA.Text = encargado;
            TxtIdEnca.Text = idEncargado;

        }

        public void setEncargado(string id, string nombre)
        {
            txtEncargado.Text = nombre;
            txtIdEncargado.Text = id;
        }

        public void setEncargado2(string id, string nombre)
        {
            TxtEncargadoA.Text = nombre;
            TxtIdEnca.Text = id;
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_area = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIdEncargado = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnBuscarEncargado = new System.Windows.Forms.Button();
            this.txtEncargado = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescripcionArea = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNombreArea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtIdEnca = new System.Windows.Forms.TextBox();
            this.txtIdArea = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.TxtEncargadoA = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnBuscarAreas = new System.Windows.Forms.Button();
            this.txtDArea = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNArea = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_usuario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_area)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(20, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 513);
            this.tabControl1.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ImageIndex = 2;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1037, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CONSULTAR / ELIMINAR";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button17);
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 479);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Áreas Existentes";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.Image = global::ASG.Properties.Resources.research;
            this.button8.Location = new System.Drawing.Point(722, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(33, 31);
            this.button8.TabIndex = 196;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Transparent;
            this.button11.Image = global::ASG.Properties.Resources.delete__1_;
            this.button11.Location = new System.Drawing.Point(800, 19);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(32, 31);
            this.button11.TabIndex = 194;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.Image = global::ASG.Properties.Resources.refresh;
            this.button17.Location = new System.Drawing.Point(761, 19);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(33, 31);
            this.button17.TabIndex = 195;
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.BackColor = System.Drawing.Color.White;
            this.txtBusqueda.Location = new System.Drawing.Point(173, 25);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(538, 23);
            this.txtBusqueda.TabIndex = 72;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_area);
            this.groupBox6.Location = new System.Drawing.Point(1, 52);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1024, 424);
            this.groupBox6.TabIndex = 80;
            this.groupBox6.TabStop = false;
            // 
            // dgv_area
            // 
            this.dgv_area.AllowUserToAddRows = false;
            this.dgv_area.AllowUserToDeleteRows = false;
            this.dgv_area.AllowUserToResizeColumns = false;
            this.dgv_area.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_area.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_area.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_area.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv_area.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_area.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_area.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_area.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_area.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(82)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_area.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_area.EnableHeadersVisualStyles = false;
            this.dgv_area.Location = new System.Drawing.Point(75, 22);
            this.dgv_area.MultiSelect = false;
            this.dgv_area.Name = "dgv_area";
            this.dgv_area.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_area.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_area.RowHeadersVisible = false;
            this.dgv_area.RowHeadersWidth = 51;
            this.dgv_area.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_area.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_area.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_area.Size = new System.Drawing.Size(856, 379);
            this.dgv_area.TabIndex = 77;
            this.dgv_area.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_area_CellDoubleClick);
            this.dgv_area.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_area_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(73, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 73;
            this.label6.Text = "Buscar Área: ";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1037, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "INGRESAR ÁREA";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIdEncargado);
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1025, 479);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // txtIdEncargado
            // 
            this.txtIdEncargado.Location = new System.Drawing.Point(88, 434);
            this.txtIdEncargado.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdEncargado.Name = "txtIdEncargado";
            this.txtIdEncargado.Size = new System.Drawing.Size(76, 23);
            this.txtIdEncargado.TabIndex = 123;
            this.txtIdEncargado.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnBuscarEncargado);
            this.groupBox7.Controls.Add(this.txtEncargado);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.txtDescripcionArea);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.txtNombreArea);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox7.Location = new System.Drawing.Point(16, 102);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(992, 275);
            this.groupBox7.TabIndex = 122;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Información del Área";
            // 
            // btnBuscarEncargado
            // 
            this.btnBuscarEncargado.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarEncargado.FlatAppearance.BorderSize = 0;
            this.btnBuscarEncargado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarEncargado.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarEncargado.Image = global::ASG.Properties.Resources.research;
            this.btnBuscarEncargado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarEncargado.Location = new System.Drawing.Point(487, 82);
            this.btnBuscarEncargado.Name = "btnBuscarEncargado";
            this.btnBuscarEncargado.Size = new System.Drawing.Size(140, 33);
            this.btnBuscarEncargado.TabIndex = 299;
            this.btnBuscarEncargado.Text = "Buscar Encargado";
            this.btnBuscarEncargado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarEncargado.UseVisualStyleBackColor = false;
            this.btnBuscarEncargado.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtEncargado
            // 
            this.txtEncargado.Location = new System.Drawing.Point(166, 87);
            this.txtEncargado.Margin = new System.Windows.Forms.Padding(2);
            this.txtEncargado.Name = "txtEncargado";
            this.txtEncargado.ReadOnly = true;
            this.txtEncargado.Size = new System.Drawing.Size(302, 25);
            this.txtEncargado.TabIndex = 125;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(26, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 17);
            this.label9.TabIndex = 124;
            this.label9.Text = "Encargado del área:";
            // 
            // txtDescripcionArea
            // 
            this.txtDescripcionArea.Location = new System.Drawing.Point(662, 167);
            this.txtDescripcionArea.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescripcionArea.Name = "txtDescripcionArea";
            this.txtDescripcionArea.Size = new System.Drawing.Size(302, 25);
            this.txtDescripcionArea.TabIndex = 123;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(509, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 17);
            this.label7.TabIndex = 122;
            this.label7.Text = "Descripción del área:";
            // 
            // txtNombreArea
            // 
            this.txtNombreArea.Location = new System.Drawing.Point(166, 167);
            this.txtNombreArea.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreArea.Name = "txtNombreArea";
            this.txtNombreArea.Size = new System.Drawing.Size(302, 25);
            this.txtNombreArea.TabIndex = 121;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(26, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 120;
            this.label2.Text = "Nombre del área:";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightGray;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::ASG.Properties.Resources.arrow;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(926, 441);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 34);
            this.button5.TabIndex = 104;
            this.button5.Text = "Cancelar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightGray;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::ASG.Properties.Resources.floppy_disk_;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(803, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 34);
            this.button2.TabIndex = 66;
            this.button2.Text = "Guardar Área";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label24.Location = new System.Drawing.Point(14, 20);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(323, 26);
            this.label24.TabIndex = 39;
            this.label24.Text = "Ingrese Nueva Área de Producción";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.ImageIndex = 0;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1037, 485);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "EDITAR ÁREA";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtIdEnca);
            this.groupBox3.Controls.Add(this.txtIdArea);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(7, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1025, 479);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // TxtIdEnca
            // 
            this.TxtIdEnca.Location = new System.Drawing.Point(164, 433);
            this.TxtIdEnca.Margin = new System.Windows.Forms.Padding(2);
            this.TxtIdEnca.Name = "TxtIdEnca";
            this.TxtIdEnca.Size = new System.Drawing.Size(76, 23);
            this.TxtIdEnca.TabIndex = 128;
            this.TxtIdEnca.Visible = false;
            // 
            // txtIdArea
            // 
            this.txtIdArea.Location = new System.Drawing.Point(59, 433);
            this.txtIdArea.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdArea.Name = "txtIdArea";
            this.txtIdArea.Size = new System.Drawing.Size(76, 23);
            this.txtIdArea.TabIndex = 127;
            this.txtIdArea.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.TxtEncargadoA);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.btnBuscarAreas);
            this.groupBox4.Controls.Add(this.txtDArea);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtNArea);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(10, 59);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(992, 275);
            this.groupBox4.TabIndex = 126;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información del Área";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button4.Image = global::ASG.Properties.Resources.research;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(665, 129);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 33);
            this.button4.TabIndex = 4;
            this.button4.Text = "Buscar Encargado";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // TxtEncargadoA
            // 
            this.TxtEncargadoA.Location = new System.Drawing.Point(314, 139);
            this.TxtEncargadoA.Margin = new System.Windows.Forms.Padding(2);
            this.TxtEncargadoA.Name = "TxtEncargadoA";
            this.TxtEncargadoA.ReadOnly = true;
            this.TxtEncargadoA.Size = new System.Drawing.Size(324, 25);
            this.TxtEncargadoA.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(174, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 17);
            this.label10.TabIndex = 299;
            this.label10.Text = "Encargado del área:";
            // 
            // btnBuscarAreas
            // 
            this.btnBuscarAreas.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarAreas.FlatAppearance.BorderSize = 0;
            this.btnBuscarAreas.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAreas.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarAreas.Image = global::ASG.Properties.Resources.research;
            this.btnBuscarAreas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarAreas.Location = new System.Drawing.Point(665, 62);
            this.btnBuscarAreas.Name = "btnBuscarAreas";
            this.btnBuscarAreas.Size = new System.Drawing.Size(185, 33);
            this.btnBuscarAreas.TabIndex = 2;
            this.btnBuscarAreas.Text = "Buscar Área de proudccion";
            this.btnBuscarAreas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarAreas.UseVisualStyleBackColor = false;
            this.btnBuscarAreas.Click += new System.EventHandler(this.btnBuscarMercaderia_Click);
            // 
            // txtDArea
            // 
            this.txtDArea.Location = new System.Drawing.Point(314, 202);
            this.txtDArea.Margin = new System.Windows.Forms.Padding(2);
            this.txtDArea.Name = "txtDArea";
            this.txtDArea.Size = new System.Drawing.Size(324, 25);
            this.txtDArea.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(167, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 17);
            this.label3.TabIndex = 122;
            this.label3.Text = "Descripción del área:";
            // 
            // txtNArea
            // 
            this.txtNArea.Location = new System.Drawing.Point(314, 67);
            this.txtNArea.Margin = new System.Windows.Forms.Padding(2);
            this.txtNArea.Name = "txtNArea";
            this.txtNArea.Size = new System.Drawing.Size(324, 25);
            this.txtNArea.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(188, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 17);
            this.label5.TabIndex = 120;
            this.label5.Text = "Nombre del área:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ASG.Properties.Resources.arrow;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(928, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 34);
            this.button1.TabIndex = 125;
            this.button1.Text = "Cancelar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGray;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::ASG.Properties.Resources.floppy_disk_;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(805, 433);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 34);
            this.button3.TabIndex = 6;
            this.button3.Text = "Guardar Área";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(16, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(226, 26);
            this.label8.TabIndex = 123;
            this.label8.Text = "Editar Área de Producción";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(101)))), ((int)(((byte)(36)))));
            this.panel2.Location = new System.Drawing.Point(18, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 3);
            this.panel2.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_usuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 591);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 28);
            this.panel1.TabIndex = 33;
            // 
            // lbl_usuario
            // 
            this.lbl_usuario.BackColor = System.Drawing.Color.Transparent;
            this.lbl_usuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_usuario.ForeColor = System.Drawing.Color.White;
            this.lbl_usuario.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_usuario.Location = new System.Drawing.Point(891, 1);
            this.lbl_usuario.Name = "lbl_usuario";
            this.lbl_usuario.Size = new System.Drawing.Size(163, 18);
            this.lbl_usuario.TabIndex = 22;
            this.lbl_usuario.Text = "Usuario: ";
            this.lbl_usuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(18, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "Multivelas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ASG.Properties.Resources.substract__2_;
            this.pictureBox4.Location = new System.Drawing.Point(1019, 14);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(14, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 37;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ASG.Properties.Resources.cancel__3_;
            this.pictureBox3.Location = new System.Drawing.Point(1045, 14);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(14, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 36;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ASG.Properties.Resources.bullet_list;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(24, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 38);
            this.label4.TabIndex = 35;
            this.label4.Text = "Gestión Áreas de Producción";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_AreasProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(47)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1084, 619);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_AreasProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_AreasProduccion";
            this.Load += new System.EventHandler(this.frm_AreasProduccion_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_area)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_area;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNombreArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_usuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtDescripcionArea;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNArea;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBuscarAreas;
        private System.Windows.Forms.Button btnBuscarEncargado;
        private System.Windows.Forms.TextBox txtEncargado;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIdEncargado;
        private System.Windows.Forms.TextBox TxtEncargadoA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox TxtIdEnca;
        private System.Windows.Forms.TextBox txtIdArea;
    }
}