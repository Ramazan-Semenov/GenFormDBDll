using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GenFormDBDll
{
    class Search
    {
        public void combosearch(DataGridView DataGridViewReload, ComboBox comboBox1)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < DataGridViewReload.ColumnCount; i++)
                {
                    list.Add(DataGridViewReload.Columns[i].Name);
                }
                comboBox1.DataSource = list;
            }
            catch (Exception exp)
            { MessageBox.Show(exp.Message); }

        }
        public void search(TextBox textBox, DataGridView dataGrid, string nametable, string nameCollumn)
        {
            SqlConnection connection = new SqlConnection(DBConnectMsSql.connectionString);
            try
            {
                string cmdCL = "SELECT * FROM [" + nametable + "] WHERE [" + nameCollumn + "] like N'%" + textBox.Text + "%' ";

                connection.Open();

                SqlCommand cmd = new SqlCommand(cmdCL, connection);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGrid.DataSource = dt;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                connection.Close();

            }

        }
    }
}
