namespace Compiladorcito
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.boton_compilar = new System.Windows.Forms.Button();
            this.Pila = new System.Windows.Forms.RichTextBox();
            this.Codigo1 = new System.Windows.Forms.RichTextBox();
            this.Pila_C = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Codigo2 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.a = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.a)).BeginInit();
            this.SuspendLayout();
            // 
            // boton_compilar
            // 
            this.boton_compilar.Location = new System.Drawing.Point(694, 365);
            this.boton_compilar.Name = "boton_compilar";
            this.boton_compilar.Size = new System.Drawing.Size(79, 39);
            this.boton_compilar.TabIndex = 0;
            this.boton_compilar.Text = "Generar Codigo";
            this.boton_compilar.UseVisualStyleBackColor = true;
            this.boton_compilar.Click += new System.EventHandler(this.boton_compilar_Click);
            // 
            // Pila
            // 
            this.Pila.Location = new System.Drawing.Point(22, 26);
            this.Pila.Name = "Pila";
            this.Pila.Size = new System.Drawing.Size(270, 319);
            this.Pila.TabIndex = 1;
            this.Pila.Text = "";
            // 
            // Codigo1
            // 
            this.Codigo1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Codigo1.Location = new System.Drawing.Point(7, 9);
            this.Codigo1.Name = "Codigo1";
            this.Codigo1.Size = new System.Drawing.Size(182, 210);
            this.Codigo1.TabIndex = 2;
            this.Codigo1.Text = "class ProgPpal1     \n     {\n       void Main()\n         int i; \n         { \n     " +
                "      i = 1; \n           write(i,3);\n\n         } /* Fin Main */   \n}  /* Fin cla" +
                "ss ProgPpal1    */";
            // 
            // Pila_C
            // 
            this.Pila_C.Location = new System.Drawing.Point(20, 22);
            this.Pila_C.Name = "Pila_C";
            this.Pila_C.Size = new System.Drawing.Size(271, 322);
            this.Pila_C.TabIndex = 4;
            this.Pila_C.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(616, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "Paso a Paso";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(205, 264);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Codigo1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ejemplo1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Codigo2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(197, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ejemplo2";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // Codigo2
            // 
            this.Codigo2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Codigo2.Location = new System.Drawing.Point(7, 9);
            this.Codigo2.Name = "Codigo2";
            this.Codigo2.Size = new System.Drawing.Size(182, 210);
            this.Codigo2.TabIndex = 3;
            this.Codigo2.Text = "class ProgPpal2     \n     {\n       void Main()\n         float x; \n         { \n   " +
                "       x= 3,1415; \n           write(x,3);\n\n         } /* Fin Main */   \n}  /* Fi" +
                "n class ProgPpal2\n    */";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Pila);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(223, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 351);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CIL Z#";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.a);
            this.groupBox2.Controls.Add(this.Pila_C);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(547, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 350);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CIL C#";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(779, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 38);
            this.button2.TabIndex = 12;
            this.button2.Text = "Ver Estructura";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // a
            // 
            this.a.Location = new System.Drawing.Point(12, 22);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(290, 322);
            this.a.TabIndex = 13;
            this.a.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 410);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.boton_compilar);
            this.Name = "Form1";
            this.Text = "Generador de Codigo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.a)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button boton_compilar;
        private System.Windows.Forms.RichTextBox Pila;
        private System.Windows.Forms.RichTextBox Codigo1;
        private System.Windows.Forms.RichTextBox Pila_C;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox Codigo2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox a;
    }
}

