namespace dcPrevent
{
    partial class App
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.cbFilterDC = new System.Windows.Forms.CheckBox();
            this.btnHideToTray = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // cbFilterDC
            // 
            this.cbFilterDC.AutoSize = true;
            this.cbFilterDC.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterDC.Location = new System.Drawing.Point(13, 13);
            this.cbFilterDC.Name = "cbFilterDC";
            this.cbFilterDC.Size = new System.Drawing.Size(125, 17);
            this.cbFilterDC.TabIndex = 0;
            this.cbFilterDC.Text = "Filter Double Clicks";
            this.cbFilterDC.UseVisualStyleBackColor = true;
            // 
            // btnHideToTray
            // 
            this.btnHideToTray.Location = new System.Drawing.Point(204, 9);
            this.btnHideToTray.Name = "btnHideToTray";
            this.btnHideToTray.Size = new System.Drawing.Size(75, 23);
            this.btnHideToTray.TabIndex = 1;
            this.btnHideToTray.Text = "Hide to Tray";
            this.btnHideToTray.UseVisualStyleBackColor = true;
            this.btnHideToTray.Click += new System.EventHandler(this.btnHideToTray_Click);
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(13, 37);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(266, 238);
            this.lstLog.TabIndex = 2;
            // 
            // niTray
            // 
            this.niTray.BalloonTipText = "Application minimized.";
            this.niTray.BalloonTipTitle = "dcPrevent";
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Text = "dcPrevent";
            this.niTray.Visible = true;
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 287);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.btnHideToTray);
            this.Controls.Add(this.cbFilterDC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "App";
            this.Text = "dcPrevent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbFilterDC;
        private System.Windows.Forms.Button btnHideToTray;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.NotifyIcon niTray;
    }
}

