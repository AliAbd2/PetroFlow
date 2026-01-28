namespace PetroFlow_PresentationLayer
{
    partial class frmDataReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataReader));
            dgvIPRData = new DataGridView();
            dgvcBottomHolePressure = new DataGridViewTextBoxColumn();
            dgvcFlowRate = new DataGridViewTextBoxColumn();
            btnReadData = new Button();
            btnClose = new Button();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)dgvIPRData).BeginInit();
            SuspendLayout();
            // 
            // dgvIPRData
            // 
            dgvIPRData.BackgroundColor = SystemColors.WindowFrame;
            dgvIPRData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIPRData.Columns.AddRange(new DataGridViewColumn[] { dgvcBottomHolePressure, dgvcFlowRate });
            dgvIPRData.GridColor = SystemColors.InactiveCaptionText;
            dgvIPRData.Location = new Point(12, 141);
            dgvIPRData.Name = "dgvIPRData";
            dgvIPRData.RightToLeft = RightToLeft.No;
            dgvIPRData.Size = new Size(587, 424);
            dgvIPRData.TabIndex = 0;
            // 
            // dgvcBottomHolePressure
            // 
            dgvcBottomHolePressure.HeaderText = "Bottom Hole Pressure";
            dgvcBottomHolePressure.MinimumWidth = 10;
            dgvcBottomHolePressure.Name = "dgvcBottomHolePressure";
            dgvcBottomHolePressure.Width = 150;
            // 
            // dgvcFlowRate
            // 
            dgvcFlowRate.HeaderText = "Flow Rate";
            dgvcFlowRate.Name = "dgvcFlowRate";
            // 
            // btnReadData
            // 
            btnReadData.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReadData.Location = new Point(473, 100);
            btnReadData.Name = "btnReadData";
            btnReadData.Size = new Size(126, 35);
            btnReadData.TabIndex = 1;
            btnReadData.Text = "Read Data";
            btnReadData.UseVisualStyleBackColor = true;
            btnReadData.Click += btnReadData_Click;
            // 
            // btnClose
            // 
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.Location = new Point(523, 571);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(76, 39);
            btnClose.TabIndex = 2;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(197, 9);
            label1.Name = "label1";
            label1.Size = new Size(203, 45);
            label1.TabIndex = 3;
            label1.Text = "Data Reader";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "ofdDataReader";
            // 
            // frmDataReader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(613, 622);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(btnReadData);
            Controls.Add(dgvIPRData);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmDataReader";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Data Reader";
            Load += frmDataReader_Load;
            ((System.ComponentModel.ISupportInitialize)dgvIPRData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridViewTextBoxColumn dgvcBottomHolePressure;
        private DataGridViewTextBoxColumn dgvcFlowRate;
        private Button btnReadData;
        private Button btnClose;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        public DataGridView dgvIPRData;
    }
}