namespace WindowsFormsApp1
{
    partial class Form
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
            this.BtnAnalizar = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtbox = new System.Windows.Forms.TextBox();
            this.txtbox_errores = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAnalizar
            // 
            this.BtnAnalizar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAnalizar.Location = new System.Drawing.Point(657, 15);
            this.BtnAnalizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAnalizar.MaximumSize = new System.Drawing.Size(100, 58);
            this.BtnAnalizar.MinimumSize = new System.Drawing.Size(100, 58);
            this.BtnAnalizar.Name = "BtnAnalizar";
            this.BtnAnalizar.Size = new System.Drawing.Size(100, 58);
            this.BtnAnalizar.TabIndex = 2;
            this.BtnAnalizar.Text = "Analizar";
            this.BtnAnalizar.UseVisualStyleBackColor = true;
            this.BtnAnalizar.Click += new System.EventHandler(this.BtnAnalizar_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(17, 559);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(1385, 265);
            this.dgv.TabIndex = 3;
            // 
            // txtbox
            // 
            this.txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox.Location = new System.Drawing.Point(17, 16);
            this.txtbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox.Multiline = true;
            this.txtbox.Name = "txtbox";
            this.txtbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbox.Size = new System.Drawing.Size(631, 534);
            this.txtbox.TabIndex = 4;
            // 
            // txtbox_errores
            // 
            this.txtbox_errores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbox_errores.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox_errores.Location = new System.Drawing.Point(765, 15);
            this.txtbox_errores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_errores.Multiline = true;
            this.txtbox_errores.Name = "txtbox_errores";
            this.txtbox_errores.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbox_errores.Size = new System.Drawing.Size(631, 534);
            this.txtbox_errores.TabIndex = 5;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1419, 838);
            this.Controls.Add(this.txtbox_errores);
            this.Controls.Add(this.txtbox);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.BtnAnalizar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1815, 934);
            this.MinimumSize = new System.Drawing.Size(1434, 875);
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnAnalizar;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtbox;
        private System.Windows.Forms.TextBox txtbox_errores;
    }
}

