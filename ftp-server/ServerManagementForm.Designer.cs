
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
            this.SuspendLayout();
            // 
            // workerAmountLbl
            // 
            this.workerAmountLbl.AutoSize = true;
            this.workerAmountLbl.BackColor = System.Drawing.Color.Transparent;
            this.workerAmountLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workerAmountLbl.Location = new System.Drawing.Point(12, 82);
            this.workerAmountLbl.Name = "workerAmountLbl";
            this.workerAmountLbl.Size = new System.Drawing.Size(434, 42);
            this.workerAmountLbl.TabIndex = 0;
            this.workerAmountLbl.Text = "Worker Threads Amount:";
            // 
            // workerAmountTxtBox
            // 
            this.workerAmountTxtBox.Location = new System.Drawing.Point(461, 94);
            this.workerAmountTxtBox.Name = "workerAmountTxtBox";
            this.workerAmountTxtBox.Size = new System.Drawing.Size(100, 31);
            this.workerAmountTxtBox.TabIndex = 1;
            // 
            // workersAmountEditBtn
            // 
            this.workersAmountEditBtn.Location = new System.Drawing.Point(567, 82);
            this.workersAmountEditBtn.Name = "workersAmountEditBtn";
            this.workersAmountEditBtn.Size = new System.Drawing.Size(103, 51);
            this.workersAmountEditBtn.TabIndex = 2;
            this.workersAmountEditBtn.Text = "Edit";
            this.workersAmountEditBtn.UseVisualStyleBackColor = true;
            this.workersAmountEditBtn.Click += new System.EventHandler(this.workersAmountEditBtn_Click);
            // 
            // ServerManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 954);
            this.Controls.Add(this.workersAmountEditBtn);
            this.Controls.Add(this.workerAmountTxtBox);
            this.Controls.Add(this.workerAmountLbl);
            this.Name = "ServerManagementForm";
            this.Text = "ServerManagementForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label workerAmountLbl;
        private System.Windows.Forms.TextBox workerAmountTxtBox;
        private System.Windows.Forms.Button workersAmountEditBtn;
    }
}