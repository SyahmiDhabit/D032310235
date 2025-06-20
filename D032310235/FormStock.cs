using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D032310235
{
    public partial class FormStock : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DITP2123\\LabTest2\\D032310235\\D032310235\\AdmiralBookstoreDatabase.mdf;Integrated Security=True");

        public FormStock()
        {
            InitializeComponent();
        }

        private void stockBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.stockBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.admiralBookDataSet);

        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'admiralBookDataSet.Stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.admiralBookDataSet.Stock);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string stockId = textBoxStockID.Text.Trim();

            if (string.IsNullOrEmpty(stockId))
            {
                MessageBox.Show("Please enter Stock ID to delete.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            { 
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Stock WHERE StockID = @StockID", connection))
                    {
                        cmd.Parameters.AddWithValue("@StockID", stockId);

                        int rows = cmd.ExecuteNonQuery();

                        MessageBox.Show(rows > 0 ? "Stock deleted successfully." : "Delete failed. Stock ID not found.", "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Refresh the DataGridView after deletion
                stockTableAdapter.Fill(admiralBookDataSet.Stock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
