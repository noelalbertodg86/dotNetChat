namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.retroceder = new System.Windows.Forms.ToolStripButton();
            this.avanzar = new System.Windows.Forms.ToolStripButton();
            this.refrescar = new System.Windows.Forms.ToolStripButton();
            this.homo = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.ir = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retroceder,
            this.avanzar,
            this.refrescar,
            this.homo,
            this.toolStripTextBox1,
            this.ir,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // retroceder
            // 
            this.retroceder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.retroceder.Image = ((System.Drawing.Image)(resources.GetObject("retroceder.Image")));
            this.retroceder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.retroceder.Name = "retroceder";
            this.retroceder.Size = new System.Drawing.Size(23, 22);
            this.retroceder.Text = "Back";
            this.retroceder.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // avanzar
            // 
            this.avanzar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.avanzar.Image = ((System.Drawing.Image)(resources.GetObject("avanzar.Image")));
            this.avanzar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.avanzar.Name = "avanzar";
            this.avanzar.Size = new System.Drawing.Size(23, 22);
            this.avanzar.Text = "Forward";
            this.avanzar.Click += new System.EventHandler(this.avanzar_Click);
            // 
            // refrescar
            // 
            this.refrescar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refrescar.Image = ((System.Drawing.Image)(resources.GetObject("refrescar.Image")));
            this.refrescar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refrescar.Name = "refrescar";
            this.refrescar.Size = new System.Drawing.Size(23, 22);
            this.refrescar.Text = "Refresh";
            this.refrescar.Click += new System.EventHandler(this.refrescar_Click);
            // 
            // homo
            // 
            this.homo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homo.Image = ((System.Drawing.Image)(resources.GetObject("homo.Image")));
            this.homo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homo.Name = "homo";
            this.homo.Size = new System.Drawing.Size(23, 22);
            this.homo.Text = "Home";
            this.homo.Click += new System.EventHandler(this.homo_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(400, 25);
            // 
            // ir
            // 
            this.ir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ir.Image = ((System.Drawing.Image)(resources.GetObject("ir.Image")));
            this.ir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ir.Name = "ir";
            this.ir.Size = new System.Drawing.Size(23, 22);
            this.ir.Text = "Go";
            this.ir.Click += new System.EventHandler(this.ir_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(784, 536);
            this.webBrowser1.TabIndex = 1;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Financial Chat";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Chat Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton retroceder;
        private System.Windows.Forms.ToolStripButton avanzar;
        private System.Windows.Forms.ToolStripButton refrescar;
        private System.Windows.Forms.ToolStripButton homo;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton ir;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

