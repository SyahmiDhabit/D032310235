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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D032310235
{
    public partial class FormAuthor : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DITP2123\\LabTest2\\D032310235\\D032310235\\AdmiralBookstoreDatabase.mdf;Integrated Security=True");


        public FormAuthor()
        {
            InitializeComponent();
        }

        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.authorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.admiralBookDataSet);

        }

        private void FormAuthor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'admiralBookDataSet.Author' table. You can move, or remove it, as needed.
            this.authorTableAdapter.Fill(this.admiralBookDataSet.Author);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [Author] (AuthorID, Name, BirthYear) values ('" + textBoxAuthorID.Text + "' , '" + textBoxName.Text + "' , '" + textBoxBirthYear.Text + "')";
            cmd.ExecuteNonQuery();
            connection.Close();
            textBoxAuthorID.Text = "";
            textBoxName.Text = "";
            textBoxBirthYear.Text = "";
            MessageBox.Show("Data inserted successfully");
        }

        private void btnFormBook_Click(object sender, EventArgs e)
        {
            this.Validate();
            FormBook formbook = new FormBook();
            this.Hide(); 
            formbook.ShowDialog(); 
            this.Show(); 
            this.Close();
        }

        private void btnFormStock_Click(object sender, EventArgs e)
        {
            this.Validate();
            FormStock formstock = new FormStock();
            this.Hide(); 
            formstock.ShowDialog();
            this.Show();
            this.Close();
        }
    }
}
