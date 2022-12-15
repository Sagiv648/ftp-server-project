
namespace ftp_server
{
    partial class ServerManagementForm
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
            this.workerAmountLbl = new System.Windows.Forms.Label();
            this.workerAmountTxtBox = new System.Windows.Forms.TextBox();
            this.workersAmountEditBtn = new System.Windows.Forms.Button();
            this.workersLst = new System.Windows.Forms.ListBox();
            this.workersLstLbl = new System.Windows.Forms.Label();
            this.processinglastreqlbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // workerAmountLbl
            // 
            this.workerAmountLbl.AutoSize = true;
            this.workerAmountLbl.BackColor = System.Drawing.Color.Transparent;
            this.workerAmountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workerAmountLbl.Location = new System.Drawing.Point(6, 43);
            this.workerAmountLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.workerAmountLbl.Name = "workerAmountLbl";
            this.workerAmountLbl.Size = new System.Drawing.Size(222, 24);
            this.workerAmountLbl.TabIndex = 0;
            this.workerAmountLbl.Text = "Worker Threads Amount:";
            // 
            // workerAmountTxtBox
            // 
            this.workerAmountTxtBox.Location = new System.Drawing.Point(230, 49);
            this.workerAmountTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.workerAmountTxtBox.Name = "workerAmountTxtBox";
            this.workerAmountTxtBox.Size = new System.Drawing.Size(52, 20);
            this.workerAmountTxtBox.TabIndex = 1;
            // 
            // workersAmountEditBtn
            // 
            this.workersAmountEditBtn.Location = new System.Drawing.Point(284, 43);
            this.workersAmountEditBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.workersAmountEditBtn.Name = "workersAmountEditBtn";
            this.workersAmountEditBtn.Size = new System.Drawing.Size(52, 27);
            this.workersAmountEditBtn.TabIndex = 2;
            this.workersAmountEditBtn.Text = "Edit";
            this.workersAmountEditBtn.UseVisualStyleBackColor = true;
            this.workersAmountEditBtn.Click += new System.EventHandler(this.workersAmountEditBtn_Click);
            // 
            // workersLst
            // 
            this.workersLst.FormattingEnabled = true;
            this.workersLst.Location = new System.Drawing.Point(487, 116);
            this.workersLst.Name = "workersLst";
            this.workersLst.Size = new System.Drawing.Size(149, 277);
            this.workersLst.TabIndex = 3;
            // 
            // workersLstLbl
            // 
            this.workersLstLbl.AutoSize = true;
            this.workersLstLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workersLstLbl.Location = new System.Drawing.Point(492, 83);
            this.workersLstLbl.Name = "workersLstLbl";
            this.workersLstLbl.Size = new System.Drawing.Size(129, 20);
            this.workersLstLbl.TabIndex = 4;
            this.workersLstLbl.Text = "Current Workers:";
            // 
            // processinglastreqlbl
            // 
            this.processinglastreqlbl.AutoSize = true;
            this.processinglastreqlbl.BackColor = System.Drawing.Color.Transparent;
            this.processinglastreqlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processinglastreqlbl.Location = new System.Drawing.Point(355, 46);
            this.processinglastreqlbl.Name = "processinglastreqlbl";
            this.processinglastreqlbl.Size = new System.Drawing.Size(228, 24);
            this.processinglastreqlbl.TabIndex = 5;
            this.processinglastreqlbl.Text = "Processing last requests...";
            this.processinglastreqlbl.Visible = false;
            // 
            // ServerManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 496);
            this.Controls.Add(this.processinglastreqlbl);
            this.Controls.Add(this.workersLstLbl);
            this.Controls.Add(this.workersLst);
            this.Controls.Add(this.workersAmountEditBtn);
            this.Controls.Add(this.workerAmountTxtBox);
            this.Controls.Add(this.workerAmountLbl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ServerManagementForm";
            this.Text = "ServerManagementForm";
            this.Load += new System.EventHandler(this.ServerManagementForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label workerAmountLbl;
        private System.Windows.Forms.TextBox workerAmountTxtBox;
        private System.Windows.Forms.Button workersAmountEditBtn;
        private System.Windows.Forms.ListBox workersLst;
        private System.Windows.Forms.Label workersLstLbl;
        private System.Windows.Forms.Label processinglastreqlbl;
    }
}