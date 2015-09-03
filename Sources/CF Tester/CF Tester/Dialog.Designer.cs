namespace NotACompany.CF_Tester
{
    partial class Dialog
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
            this.InputLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.ExpectedLabel = new System.Windows.Forms.Label();
            this.OutputText = new System.Windows.Forms.Label();
            this.ExpectedText = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.InputText = new System.Windows.Forms.Label();
            this.Footer = new System.Windows.Forms.Panel();
            this.Left = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.Button();
            this.TestLabel = new System.Windows.Forms.Label();
            this.ToolPanel = new System.Windows.Forms.Panel();
            this.ShowNewlines = new System.Windows.Forms.CheckBox();
            this.ShowNewlinesTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ContentPanel.SuspendLayout();
            this.Footer.SuspendLayout();
            this.ToolPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputLabel.Location = new System.Drawing.Point(151, 12);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(82, 24);
            this.InputLabel.TabIndex = 0;
            this.InputLabel.Text = "Input:";
            this.InputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputLabel.Location = new System.Drawing.Point(527, 12);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(94, 24);
            this.OutputLabel.TabIndex = 1;
            this.OutputLabel.Text = "Output:";
            this.OutputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExpectedLabel
            // 
            this.ExpectedLabel.AutoSize = true;
            this.ExpectedLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpectedLabel.Location = new System.Drawing.Point(883, 12);
            this.ExpectedLabel.Name = "ExpectedLabel";
            this.ExpectedLabel.Size = new System.Drawing.Size(118, 24);
            this.ExpectedLabel.TabIndex = 2;
            this.ExpectedLabel.Text = "Expected:";
            this.ExpectedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OutputText
            // 
            this.OutputText.AutoSize = true;
            this.OutputText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.OutputText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OutputText.Location = new System.Drawing.Point(384, 56);
            this.OutputText.MinimumSize = new System.Drawing.Size(370, 460);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(370, 460);
            this.OutputText.TabIndex = 4;
            this.OutputText.Text = "jnitdmnom";
            // 
            // ExpectedText
            // 
            this.ExpectedText.AutoSize = true;
            this.ExpectedText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.ExpectedText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExpectedText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExpectedText.Location = new System.Drawing.Point(759, 56);
            this.ExpectedText.MinimumSize = new System.Drawing.Size(360, 460);
            this.ExpectedText.Name = "ExpectedText";
            this.ExpectedText.Size = new System.Drawing.Size(360, 460);
            this.ExpectedText.TabIndex = 5;
            this.ExpectedText.Text = "vgbiewruvewb";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(644, 18);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(112, 29);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContentPanel.Controls.Add(this.InputLabel);
            this.ContentPanel.Controls.Add(this.OutputLabel);
            this.ContentPanel.Controls.Add(this.ExpectedText);
            this.ContentPanel.Controls.Add(this.InputText);
            this.ContentPanel.Controls.Add(this.ExpectedLabel);
            this.ContentPanel.Controls.Add(this.OutputText);
            this.ContentPanel.Location = new System.Drawing.Point(147, 67);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(1138, 535);
            this.ContentPanel.TabIndex = 7;
            // 
            // InputText
            // 
            this.InputText.AutoSize = true;
            this.InputText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.InputText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InputText.Location = new System.Drawing.Point(18, 56);
            this.InputText.MinimumSize = new System.Drawing.Size(360, 460);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(360, 460);
            this.InputText.TabIndex = 3;
            this.InputText.Text = "hskibyhoi";
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.SystemColors.Control;
            this.Footer.Controls.Add(this.OKButton);
            this.Footer.Location = new System.Drawing.Point(-9, 621);
            this.Footer.Name = "Footer";
            this.Footer.Size = new System.Drawing.Size(1397, 67);
            this.Footer.TabIndex = 8;
            // 
            // Left
            // 
            this.Left.BackgroundImage = global::NotACompany.CF_Tester.Icons.Left;
            this.Left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Left.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Left.FlatAppearance.BorderSize = 0;
            this.Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Left.Location = new System.Drawing.Point(102, 313);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(39, 23);
            this.Left.TabIndex = 10;
            this.Left.UseVisualStyleBackColor = false;
            this.Left.Click += new System.EventHandler(this.Left_Click);
            // 
            // Right
            // 
            this.Right.BackgroundImage = global::NotACompany.CF_Tester.Icons.Right;
            this.Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Right.FlatAppearance.BorderSize = 0;
            this.Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Right.Location = new System.Drawing.Point(1291, 313);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(38, 23);
            this.Right.TabIndex = 9;
            this.Right.UseVisualStyleBackColor = false;
            this.Right.Click += new System.EventHandler(this.Right_Click);
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TestLabel.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel.Location = new System.Drawing.Point(654, 12);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(127, 34);
            this.TestLabel.TabIndex = 11;
            this.TestLabel.Text = "Test #1";
            this.TestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolPanel
            // 
            this.ToolPanel.AutoSize = true;
            this.ToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.ToolPanel.Controls.Add(this.ShowNewlines);
            this.ToolPanel.Location = new System.Drawing.Point(0, 0);
            this.ToolPanel.Name = "ToolPanel";
            this.ToolPanel.Size = new System.Drawing.Size(50, 622);
            this.ToolPanel.TabIndex = 12;
            // 
            // ShowNewlines
            // 
            this.ShowNewlines.Appearance = System.Windows.Forms.Appearance.Button;
            this.ShowNewlines.AutoSize = true;
            this.ShowNewlines.FlatAppearance.BorderSize = 0;
            this.ShowNewlines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowNewlines.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowNewlines.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowNewlines.Location = new System.Drawing.Point(3, 12);
            this.ShowNewlines.Name = "ShowNewlines";
            this.ShowNewlines.Size = new System.Drawing.Size(44, 34);
            this.ShowNewlines.TabIndex = 14;
            this.ShowNewlines.Text = "\\n";
            this.ShowNewlinesTooltip.SetToolTip(this.ShowNewlines, "Show/hide newline symbols");
            this.ShowNewlines.UseVisualStyleBackColor = true;
            this.ShowNewlines.CheckedChanged += new System.EventHandler(this.ShowNewlines_CheckedChanged);
            // 
            // ShowNewlinesTooltip
            // 
            this.ShowNewlinesTooltip.BackColor = System.Drawing.SystemColors.Menu;
            this.ShowNewlinesTooltip.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            // 
            // Dialog
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1378, 680);
            this.Controls.Add(this.ToolPanel);
            this.Controls.Add(this.TestLabel);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.ContentPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Codeforces Tester";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.Footer.ResumeLayout(false);
            this.ToolPanel.ResumeLayout(false);
            this.ToolPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Label ExpectedLabel;
        private System.Windows.Forms.Label OutputText;
        private System.Windows.Forms.Label ExpectedText;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Panel Footer;
        private System.Windows.Forms.Button Right;
        private System.Windows.Forms.Button Left;
        private System.Windows.Forms.Label TestLabel;
        private System.Windows.Forms.Label InputText;
        private System.Windows.Forms.Panel ToolPanel;
        private System.Windows.Forms.CheckBox ShowNewlines;
        private System.Windows.Forms.ToolTip ShowNewlinesTooltip;
    }
}