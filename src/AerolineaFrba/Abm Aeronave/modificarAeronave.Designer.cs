namespace AerolineaFrba.Abm_Aeronave
{
    partial class modificarAeronave
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
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textPisos = new System.Windows.Forms.TextBox();
            this.textButacas = new System.Windows.Forms.TextBox();
            this.textKg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textFabricante = new System.Windows.Forms.TextBox();
            this.textTS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textModelo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textMatricula = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(535, 211);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 29);
            this.button3.TabIndex = 51;
            this.button3.Text = "Crear nuevo modelo";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 211);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(517, 288);
            this.dataGridView1.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Modelos que pueden sustituir al actual:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 40);
            this.button2.TabIndex = 48;
            this.button2.Text = "Volver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 40);
            this.button1.TabIndex = 47;
            this.button1.Text = "Guardar cambios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textMatricula);
            this.groupBox1.Controls.Add(this.textPisos);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textButacas);
            this.groupBox1.Controls.Add(this.textKg);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textFabricante);
            this.groupBox1.Controls.Add(this.textTS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textModelo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 142);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Características actuales";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // textPisos
            // 
            this.textPisos.Location = new System.Drawing.Point(495, 53);
            this.textPisos.Name = "textPisos";
            this.textPisos.ReadOnly = true;
            this.textPisos.Size = new System.Drawing.Size(140, 20);
            this.textPisos.TabIndex = 66;
            this.textPisos.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textButacas
            // 
            this.textButacas.Location = new System.Drawing.Point(495, 86);
            this.textButacas.Name = "textButacas";
            this.textButacas.ReadOnly = true;
            this.textButacas.Size = new System.Drawing.Size(140, 20);
            this.textButacas.TabIndex = 65;
            this.textButacas.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // textKg
            // 
            this.textKg.Location = new System.Drawing.Point(495, 21);
            this.textKg.Name = "textKg";
            this.textKg.ReadOnly = true;
            this.textKg.Size = new System.Drawing.Size(140, 20);
            this.textKg.TabIndex = 64;
            this.textKg.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Cantidad de Butacas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Cantidad de Pisos:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Cantidad de Kgs para encomiendas:";
            // 
            // textFabricante
            // 
            this.textFabricante.Location = new System.Drawing.Point(94, 81);
            this.textFabricante.Name = "textFabricante";
            this.textFabricante.ReadOnly = true;
            this.textFabricante.Size = new System.Drawing.Size(205, 20);
            this.textFabricante.TabIndex = 60;
            this.textFabricante.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textTS
            // 
            this.textTS.Location = new System.Drawing.Point(94, 114);
            this.textTS.Name = "textTS";
            this.textTS.ReadOnly = true;
            this.textTS.Size = new System.Drawing.Size(205, 20);
            this.textTS.TabIndex = 59;
            this.textTS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Modelo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Tipo de servicio:";
            // 
            // textModelo
            // 
            this.textModelo.Location = new System.Drawing.Point(94, 50);
            this.textModelo.Name = "textModelo";
            this.textModelo.ReadOnly = true;
            this.textModelo.Size = new System.Drawing.Size(205, 20);
            this.textModelo.TabIndex = 54;
            this.textModelo.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Fabricante:";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(110, 165);
            this.maskedTextBox2.Mask = "AAA-000";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(205, 20);
            this.maskedTextBox2.TabIndex = 54;
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Nueva Matrícula:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 55;
            this.label9.Text = "Matrícula actual:";
            // 
            // textMatricula
            // 
            this.textMatricula.Location = new System.Drawing.Point(94, 24);
            this.textMatricula.Name = "textMatricula";
            this.textMatricula.ReadOnly = true;
            this.textMatricula.Size = new System.Drawing.Size(205, 20);
            this.textMatricula.TabIndex = 67;
            // 
            // modificarAeronave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 557);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "modificarAeronave";
            this.Text = "modificarAeronave";
            this.Load += new System.EventHandler(this.modificarAeronave_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textFabricante;
        private System.Windows.Forms.TextBox textTS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textModelo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textPisos;
        private System.Windows.Forms.TextBox textButacas;
        private System.Windows.Forms.TextBox textKg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textMatricula;
        private System.Windows.Forms.Label label9;

    }
}