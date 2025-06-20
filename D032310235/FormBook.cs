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
    public partial class FormBook : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DITP2123\\LabTest2\\D032310235\\D032310235\\AdmiralBookstoreDatabase.mdf;Integrated Security=True");

        public FormBook()
        {
            InitializeComponent();
        }

        private void bookBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bookBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.admiralBookDataSet);

        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'admiralBookDataSet.Book' table. You can move, or remove it, as needed.
            this.bookTableAdapter.Fill(this.admiralBookDataSet.Book);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string formattedDate = dateTimePicker.Value.ToString("yyyy-MM-dd");

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
UPDATE Book
SET Tittle = @Tittle,
    Publisher = @Publisher,
    PublishDate = @PublishDate
WHERE [ISBN-13] = @ISBN";
            cmd.Parameters.AddWithValue("@ISBN", textBoxISBN.Text.Trim());
            cmd.Parameters.AddWithValue("@Tittle", textBoxTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@Publisher", textBoxPublisher.Text.Trim());
            cmd.Parameters.AddWithValue("@PublishDate", formattedDate);

            int rows = cmd.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show(rows > 0 ? "Book updated successfully." : "Update failed. ISBN not found.");
        }
    }
}
