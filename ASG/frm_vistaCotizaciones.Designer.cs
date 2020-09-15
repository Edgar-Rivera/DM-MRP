namespace ASG
{
    partial class frm_vistaCotizaciones
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_vistaCotizaciones));
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.button26 = new System.Windows.Forms.Button();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ASG.Properties.Resources.substract__2_;
            this.pictureBox4.Location = new System.Drawing.Point(1027, 20);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(14, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 42;
            this.pictureBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox4, "Minimizar Pantalla");
            this.pictureBox4.Click += new System.EventHandler(this.PictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ASG.Properties.Resources.cancel__3_;
            this.pictureBox3.Location = new System.Drawing.Point(1053, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(14, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 41;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Salir");
            this.pictureBox3.Click += new System.EventHandler(this.PictureBox3_Click);
            this.pictureBox3.MouseLeave += new System.EventHandler(this.PictureBox3_MouseLeave);
            this.pictureBox3.MouseHover += new System.EventHandler(this.PictureBox3_MouseHover);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ASG.Properties.Resources.boss__1_;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(38, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(427, 50);
            this.label4.TabIndex = 40;
            this.label4.Text = "Cotizaciones a Clientes Pendientes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownHeight = 110;
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.DropDownWidth = 85;
            this.comboBox9.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.IntegralHeight = false;
            this.comboBox9.Location = new System.Drawing.Point(252, 484);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(232, 23);
            this.comboBox9.TabIndex = 201;
            this.toolTip1.SetToolTip(this.comboBox9, "Sucursal Actual");
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.ComboBox9_SelectedIndexChanged);
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.Transparent;
            this.button26.FlatAppearance.BorderSize = 0;
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button26.ForeColor = System.Drawing.Color.White;
            this.button26.Image = global::ASG.Properties.Resources.funnel;
            this.button26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button26.Location = new System.Drawing.Point(34, 480);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(212, 31);
            this.button26.TabIndex = 200;
            this.button26.Text = "COTIZACIONES DE SUCURSAL:";
            this.button26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.button26, "Sucursal Actual");
            this.button26.UseVisualStyleBackColor = false;
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            this.dataGridView7.AllowUserToDeleteRows = false;
            this.dataGridView7.AllowUserToResizeColumns = false;
            this.dataGridView7.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView7.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView7.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView7.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView7.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView7.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView7.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn57,
            this.Column27,
            this.Column28});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(82)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView7.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView7.EnableHeadersVisualStyles = false;
            this.dataGridView7.Location = new System.Drawing.Point(23, 96);
            this.dataGridView7.MultiSelect = false;
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView7.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView7.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView7.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView7.Size = new System.Drawing.Size(1047, 361);
            this.dataGridView7.TabIndex = 78;
            this.dataGridView7.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView7_CellMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.HeaderText = "NUMERO COTIZACION";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.HeaderText = "FECHA";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.ReadOnly = true;
            this.dataGridViewTextBoxColumn49.Width = 120;
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.HeaderText = "CLIENTE";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.ReadOnly = true;
            this.dataGridViewTextBoxColumn50.Width = 175;
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.HeaderText = "TERMINOS";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.ReadOnly = true;
            this.dataGridViewTextBoxColumn51.Width = 175;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.HeaderText = "VENCIMIENTO";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.ReadOnly = true;
            this.dataGridViewTextBoxColumn52.Width = 125;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.HeaderText = "SUCURSAL";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.ReadOnly = true;
            this.dataGridViewTextBoxColumn53.Width = 120;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.HeaderText = "TOTAL FACTURA";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.HeaderText = "VENDEDOR";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            this.dataGridViewTextBoxColumn55.Width = 115;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.HeaderText = "SUBTOTAL";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.ReadOnly = true;
            this.dataGridViewTextBoxColumn56.Visible = false;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.HeaderText = "DESCUENTO";
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            this.dataGridViewTextBoxColumn57.ReadOnly = true;
            this.dataGridViewTextBoxColumn57.Visible = false;
            // 
            // Column27
            // 
            this.Column27.HeaderText = "NIT";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            this.Column27.Visible = false;
            // 
            // Column28
            // 
            this.Column28.HeaderText = "DIRECCION";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(79, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 21);
            this.label1.TabIndex = 257;
            this.label1.Text = "Cotizaciones realizadas a clientes pendientes a facturar";
            // 
            // frm_vistaCotizaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1091, 533);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox9);
            this.Controls.Add(this.button26);
            this.Controls.Add(this.dataGridView7);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm_vistaCotizaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotizaciones Pendientes";
            this.Load += new System.EventHandler(this.frm_vistaCotizaciones_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_vistaCotizaciones_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_vistaCotizaciones_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_vistaCotizaciones_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_vistaCotizaciones_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dataGridView7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Label label1;
    }
}