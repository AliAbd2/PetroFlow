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
            components = new System.ComponentModel.Container();
            pnlPlot = new Panel();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            btnFomatPlot = new Button();
            pnlFormatPlot = new Panel();
            panel2 = new Panel();
            vScrollBar1 = new VScrollBar();
            panel3 = new Panel();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            txtBubblePointPressure = new TextBox();
            label2 = new Label();
            txtReservoirPressure = new TextBox();
            label1 = new Label();
            grpTestData = new GroupBox();
            txtTestFlowrate = new TextBox();
            textBox1 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtTestFlowingPressure = new TextBox();
            label3 = new Label();
            cmsNodalPlot = new ContextMenuStrip(components);
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            fromatPlotToolStripMenuItem = new ToolStripMenuItem();
            pnlPlot.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            grpTestData.SuspendLayout();
            cmsNodalPlot.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPlot
            // 
            pnlPlot.Controls.Add(formsPlot1);
            pnlPlot.Controls.Add(btnFomatPlot);
            pnlPlot.Controls.Add(pnlFormatPlot);
            pnlPlot.Dock = DockStyle.Right;
            pnlPlot.Location = new Point(684, 0);
            pnlPlot.Name = "pnlPlot";
            pnlPlot.Size = new Size(700, 661);
            pnlPlot.TabIndex = 0;
            // 
            // formsPlot1
            // 
            formsPlot1.BorderStyle = BorderStyle.FixedSingle;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 0);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(410, 661);
            formsPlot1.TabIndex = 2;
            // 
            // btnFomatPlot
            // 
            btnFomatPlot.Dock = DockStyle.Right;
            btnFomatPlot.Location = new Point(410, 0);
            btnFomatPlot.Name = "btnFomatPlot";
            btnFomatPlot.Size = new Size(5, 661);
            btnFomatPlot.TabIndex = 1;
            btnFomatPlot.UseVisualStyleBackColor = true;
            btnFomatPlot.Click += button1_Click;
            btnFomatPlot.MouseEnter += button1_MouseEnter;
            btnFomatPlot.MouseLeave += btnFomatPlot_MouseLeave;
            // 
            // pnlFormatPlot
            // 
            pnlFormatPlot.BackColor = SystemColors.ActiveCaptionText;
            pnlFormatPlot.BorderStyle = BorderStyle.FixedSingle;
            pnlFormatPlot.Dock = DockStyle.Right;
            pnlFormatPlot.Location = new Point(415, 0);
            pnlFormatPlot.Name = "pnlFormatPlot";
            pnlFormatPlot.Size = new Size(285, 661);
            pnlFormatPlot.TabIndex = 0;
            pnlFormatPlot.Visible = false;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(vScrollBar1);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(grpTestData);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(684, 661);
            panel2.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Location = new Point(663, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(19, 607);
            vScrollBar1.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 607);
            panel3.Name = "panel3";
            panel3.Size = new Size(682, 52);
            panel3.TabIndex = 12;
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(337, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(307, 121);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "IPR Method Selection";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBubblePointPressure);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtReservoirPressure);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(307, 121);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Reservoir Data";
            // 
            // txtBubblePointPressure
            // 
            txtBubblePointPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBubblePointPressure.Location = new Point(154, 61);
            txtBubblePointPressure.Name = "txtBubblePointPressure";
            txtBubblePointPressure.Size = new Size(100, 25);
            txtBubblePointPressure.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 65);
            label2.Name = "label2";
            label2.Size = new Size(138, 17);
            label2.TabIndex = 6;
            label2.Text = "Bubble Point Pressure:";
            // 
            // txtReservoirPressure
            // 
            txtReservoirPressure.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReservoirPressure.Location = new Point(154, 29);
            txtReservoirPressure.Name = "txtReservoirPressure";
            txtReservoirPressure.Size = new Size(100, 25);
            txtReservoirPressure.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 33);
            label1.Name = "label1";
            label1.Size = new Size(120, 17);
            label1.TabIndex = 4;
            label1.Text = "Reservoir Pressure:";
            // 
            // grpTestData
            // 
            grpTestData.Controls.Add(txtTestFlowrate);
            grpTestData.Controls.Add(textBox1);
            grpTestData.Controls.Add(label4);
            grpTestData.Controls.Add(label5);
            grpTestData.Controls.Add(txtTestFlowingPressure);
            grpTestData.Controls.Add(label3);
            grpTestData.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpTestData.Location = new Point(12, 139);
            grpTestData.Name = "grpTestData";
            grpTestData.Size = new Size(632, 121);
            grpTestData.TabIndex = 4;
            grpTestData.TabStop = false;
            grpTestData.Text = "Test Data";
            // 
            // txtTestFlowrate
            // 
            txtTestFlowrate.Font = new Font("Segoe UI", 9.75F);
            txtTestFlowrate.Location = new Point(149, 66);
            txtTestFlowrate.Name = "txtTestFlowrate";
            txtTestFlowrate.Size = new Size(100, 25);
            txtTestFlowrate.TabIndex = 6;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 9.75F);
            textBox1.Location = new Point(378, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 25);
            textBox1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F);
            label4.Location = new Point(7, 70);
            label4.Name = "label4";
            label4.Size = new Size(87, 17);
            label4.TabIndex = 5;
            label4.Text = "Test Flowrate:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F);
            label5.Location = new Point(278, 36);
            label5.Name = "label5";
            label5.Size = new Size(94, 17);
            label5.TabIndex = 6;
            label5.Text = "Flow Efficiency:";
            // 
            // txtTestFlowingPressure
            // 
            txtTestFlowingPressure.Font = new Font("Segoe UI", 9.75F);
            txtTestFlowingPressure.Location = new Point(149, 32);
            txtTestFlowingPressure.Name = "txtTestFlowingPressure";
            txtTestFlowingPressure.Size = new Size(100, 25);
            txtTestFlowingPressure.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.Location = new Point(7, 36);
            label3.Name = "label3";
            label3.Size = new Size(136, 17);
            label3.TabIndex = 0;
            label3.Text = "Test Flowing Pressure:";
            // 
            // cmsNodalPlot
            // 
            cmsNodalPlot.Items.AddRange(new ToolStripItem[] { saveAsToolStripMenuItem, toolStripSeparator1, fromatPlotToolStripMenuItem });
            cmsNodalPlot.Name = "cmsNodalPlot";
            cmsNodalPlot.Size = new Size(137, 54);
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(136, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(133, 6);
            // 
            // fromatPlotToolStripMenuItem
            // 
            fromatPlotToolStripMenuItem.Name = "fromatPlotToolStripMenuItem";
            fromatPlotToolStripMenuItem.Size = new Size(136, 22);
            fromatPlotToolStripMenuItem.Text = "Fromat Plot";
            // 
            // frmNodalAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1384, 661);
            Controls.Add(panel2);
            Controls.Add(pnlPlot);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmNodalAnalysis";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nodal Analysis";
            Load += frmNodalAnalysis_Load;
            pnlPlot.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            grpTestData.ResumeLayout(false);
            grpTestData.PerformLayout();
            cmsNodalPlot.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPlot;
        private Panel panel2;
        private TextBox textBox1;
        private Label label5;
        private GroupBox grpTestData;
        private TextBox txtTestFlowrate;
        private Label label4;
        private TextBox txtTestFlowingPressure;
        private Label label3;
        private GroupBox groupBox1;
        private TextBox txtBubblePointPressure;
        private Label label2;
        private TextBox txtReservoirPressure;
        private Label label1;
        private GroupBox groupBox2;
        private Panel panel3;
        private VScrollBar vScrollBar1;
        private ContextMenuStrip cmsNodalPlot;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem fromatPlotToolStripMenuItem;
        private Button btnFomatPlot;
        private Panel pnlFormatPlot;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}
