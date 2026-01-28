using PetroFlow_BusinessLayer.Production.NodalAnalysis.InFlowPreformance.IPRData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PetroFlow_PresentationLayer
{
    public partial class frmDataReader : Form
    {

        private string _FilePath;

        public List<clsInFlowDataRow> Data = new List<clsInFlowDataRow>();
        public frmDataReader()
        {
            InitializeComponent();

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv";
            openFileDialog1.Title = "Select Data File";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (dgvIPRData.RowCount > 0)
            {

                foreach (DataGridViewRow Row in dgvIPRData.Rows)
                {


                    if (double.TryParse(Row.Cells[0].Value?.ToString(), out double buttomHolePressure) &&
                                            double.TryParse(Row.Cells[1].Value?.ToString(), out double flowRate))
                    {

                        clsInFlowDataRow dataRow = new clsInFlowDataRow(buttomHolePressure,
                            flowRate);

                        Data.Add(dataRow);

                    }



                }

            }


            this.Close();
        }

        private void frmDataReader_Load(object sender, EventArgs e)
        {

        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            _FilePath = openFileDialog1.FileName;

            if (string.IsNullOrEmpty(_FilePath))
            {

                MessageBox.Show(
                    "An error occurred while loading the data file. Please try again.",
                    "Data Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

            }

        }

    }
}
