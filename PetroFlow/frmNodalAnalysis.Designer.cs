namespace PetroFlow
{
    partial class frmNodalAnalysis
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            pltNodalAnalysis = new ScottPlot.WinForms.FormsPlot();
            panel2 = new Panel();
            button1 = new Button();
            grpTestData = new GroupBox();
            txtTestFlowrate = new TextBox();
            label4 = new Label();
            txtTestFlowingPressure = new TextBox();
            label3 = new Label();
            txtBubblePointPressure = new TextBox();
            label2 = new Label();
            txtReservoirPressure = new TextBox();
            label1 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            grpTestData.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pltNodalAnalysis);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(575, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(687, 555);
            panel1.TabIndex = 0;
            // 
            // pltNodalAnalysis
            // 
            pltNodalAnalysis.DisplayScale = 1F;
            pltNodalAnalysis.Dock = DockStyle.Left;
            pltNodalAnalysis.Location = new Point(0, 0);
            pltNodalAnalysis.Name = "pltNodalAnalysis";
            pltNodalAnalysis.Size = new Size(570, 555);
            pltNodalAnalysis.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(grpTestData);
            panel2.Controls.Add(txtBubblePointPressure);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txtReservoirPressure);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(575, 555);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(439, 504);
            button1.Name = "button1";
            button1.Size = new Size(130, 39);
            button1.TabIndex = 5;
            button1.Text = "Plot";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // grpTestData
            // 
            grpTestData.Controls.Add(txtTestFlowrate);
            grpTestData.Controls.Add(label4);
            grpTestData.Controls.Add(txtTestFlowingPressure);
            grpTestData.Controls.Add(label3);
            grpTestData.Location = new Point(12, 80);
            grpTestData.Name = "grpTestData";
            grpTestData.Size = new Size(538, 121);
            grpTestData.TabIndex = 4;
            grpTestData.TabStop = false;
            grpTestData.Text = "Test Data";
            // 
            // txtTestFlowrate
            // 
            txtTestFlowrate.Location = new Point(136, 66);
            txtTestFlowrate.Name = "txtTestFlowrate";
            txtTestFlowrate.Size = new Size(100, 23);
            txtTestFlowrate.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 74);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 5;
            label4.Text = "Test Flowrate:";
            // 
            // txtTestFlowingPressure
            // 
            txtTestFlowingPressure.Location = new Point(136, 33);
            txtTestFlowingPressure.Name = "txtTestFlowingPressure";
            txtTestFlowingPressure.Size = new Size(100, 23);
            txtTestFlowingPressure.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 36);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 0;
            label3.Text = "Test Flowing Pressure:";
            // 
            // txtBubblePointPressure
            // 
            txtBubblePointPressure.Location = new Point(143, 51);
            txtBubblePointPressure.Name = "txtBubblePointPressure";
            txtBubblePointPressure.Size = new Size(100, 23);
            txtBubblePointPressure.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 2;
            label2.Text = "Bubble Point Pressure:";
            // 
            // txtReservoirPressure
            // 
            txtReservoirPressure.Location = new Point(143, 19);
            txtReservoirPressure.Name = "txtReservoirPressure";
            txtReservoirPressure.Size = new Size(100, 23);
            txtReservoirPressure.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Reservoir Pressure:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 230);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 6;
            label5.Text = "Flow Efficiency:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(107, 226);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            // 
            // frmNodalAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 555);
            Controls.Add(panel2);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmNodalAnalysis";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nodal Analysis";
            Load += frmNodalAnalysis_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            grpTestData.ResumeLayout(false);
            grpTestData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private ScottPlot.WinForms.FormsPlot pltNodalAnalysis;
        private TextBox txtBubblePointPressure;
        private Label label2;
        private TextBox txtReservoirPressure;
        private Label label1;
        private GroupBox grpTestData;
        private TextBox txtTestFlowrate;
        private Label label4;
        private TextBox txtTestFlowingPressure;
        private Label label3;
        private Button button1;
        private Label label5;
        private TextBox textBox1;
    }
}
