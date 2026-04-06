namespace BuildSPED
{
    partial class principal
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
            exibir = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)exibir).BeginInit();
            SuspendLayout();
            // 
            // exibir
            // 
            exibir.AllowExternalDrop = true;
            exibir.CreationProperties = null;
            exibir.DefaultBackgroundColor = Color.White;
            exibir.Dock = DockStyle.Fill;
            exibir.Location = new Point(0, 0);
            exibir.Name = "exibir";
            exibir.Size = new Size(1050, 448);
            exibir.TabIndex = 0;
            exibir.ZoomFactor = 1D;
            // 
            // principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 448);
            Controls.Add(exibir);
            Name = "principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BuildSPED";
            WindowState = FormWindowState.Maximized;
            Load += principal_Load;
            ((System.ComponentModel.ISupportInitialize)exibir).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Microsoft.Web.WebView2.WinForms.WebView2 exibir;
    }
}