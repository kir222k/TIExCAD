namespace TIExCAD
{
    partial class AdminAcadCmd
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
            this.txtNameApp = new System.Windows.Forms.TextBox();
            this.UnregisterApp = new System.Windows.Forms.Button();
            this.RegisterApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNameApp
            // 
            this.txtNameApp.Location = new System.Drawing.Point(12, 23);
            this.txtNameApp.Name = "txtNameApp";
            this.txtNameApp.Size = new System.Drawing.Size(147, 22);
            this.txtNameApp.TabIndex = 0;
            // 
            // UnregisterApp
            // 
            this.UnregisterApp.Location = new System.Drawing.Point(165, 51);
            this.UnregisterApp.Name = "UnregisterApp";
            this.UnregisterApp.Size = new System.Drawing.Size(113, 21);
            this.UnregisterApp.TabIndex = 1;
            this.UnregisterApp.Text = "Unregister App";
            this.UnregisterApp.UseVisualStyleBackColor = true;
            this.UnregisterApp.Click += new System.EventHandler(this.button1_Click);
            // 
            // RegisterApp
            // 
            this.RegisterApp.Location = new System.Drawing.Point(165, 24);
            this.RegisterApp.Name = "RegisterApp";
            this.RegisterApp.Size = new System.Drawing.Size(113, 21);
            this.RegisterApp.TabIndex = 2;
            this.RegisterApp.Text = "Register App";
            this.RegisterApp.UseVisualStyleBackColor = true;
            this.RegisterApp.Click += new System.EventHandler(this.RegisterApp_Click);
            // 
            // AdminAcadCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 111);
            this.Controls.Add(this.RegisterApp);
            this.Controls.Add(this.UnregisterApp);
            this.Controls.Add(this.txtNameApp);
            this.Name = "AdminAcadCmd";
            this.Text = "AdminAcadCmd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNameApp;
        private System.Windows.Forms.Button UnregisterApp;
        private System.Windows.Forms.Button RegisterApp;
    }
}