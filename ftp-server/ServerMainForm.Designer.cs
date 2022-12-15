
namespace ftp_server
{
    partial class ServerMainForm
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
            this.ipAddrTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serverRunnerBtn = new System.Windows.Forms.Button();
            this.runningLoopLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipAddrTextBox
            // 
            this.ipAddrTextBox.Location = new System.Drawing.Point(197, 196);
            this.ipAddrTextBox.Name = "ipAddrTextBox";
            this.ipAddrTextBox.Size = new System.Drawing.Size(227, 31);
            this.ipAddrTextBox.TabIndex = 0;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(197, 319);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(227, 31);
            this.portTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP-Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 37);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // serverRunnerBtn
            // 
            this.serverRunnerBtn.Location = new System.Drawing.Point(197, 429);
            this.serverRunnerBtn.Name = "serverRunnerBtn";
            this.serverRunnerBtn.Size = new System.Drawing.Size(227, 67);
            this.serverRunnerBtn.TabIndex = 4;
            this.serverRunnerBtn.Text = "Run";
            this.serverRunnerBtn.UseVisualStyleBackColor = true;
            this.serverRunnerBtn.Click += new System.EventHandler(this.serverRunnerBtn_Click);
            // 
            // runningLoopLbl
            // 
            this.runningLoopLbl.AutoSize = true;
            this.runningLoopLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runningLoopLbl.Location = new System.Drawing.Point(235, 99);
            this.runningLoopLbl.Name = "runningLoopLbl";
            this.runningLoopLbl.Size = new System.Drawing.Size(288, 55);
            this.runningLoopLbl.TabIndex = 5;
            this.runningLoopLbl.Text = "RUNNING...";
            this.runningLoopLbl.Visible = false;
            // 
            // ServerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 628);
            this.Controls.Add(this.runningLoopLbl);
            this.Controls.Add(this.serverRunnerBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.ipAddrTextBox);
            this.KeyPreview = true;
            this.Name = "ServerMainForm";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterPressed);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.controlAddedEvent);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.controlRemovedEvent);
        }

        

        #endregion

        private System.Windows.Forms.TextBox ipAddrTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button serverRunnerBtn;
        private System.Windows.Forms.Label runningLoopLbl;
    }
}

