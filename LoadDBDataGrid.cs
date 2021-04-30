using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenFormDBDll
{
    class LoadDBDataGrid : DBConnectMsSql
    {
        protected SqlCommandBuilder sqlCommandBuilder { get; set; }
        protected static DataSet dataSet = null;
        private readonly DataGridView datagridView;
        private readonly string NameTable;
        protected SqlDataAdapter SqlDataAdapter = null;
        public int CountTableInDB { get; set; }


        public LoadDBDataGrid(string NameTable, DataGridView datagridView)
        {
            this.datagridView = datagridView;
            this.NameTable = NameTable;

        }

        public void save()
        {
            SqlConnection connection = new SqlConnection(DBConnectMsSql.connectionString);
            connection.Open();
            //фиксируем измененные данные
            SqlDataAdapter.Update(dataSet.Tables[0]);
            connection.Close();
            Reload();
        }

        public void Reload()
        {
            SqlConnection connection = new SqlConnection(DBConnectMsSql.connectionString);
            try
            {


                connection.Open();
                dataSet.Clear();
                dataSet.Tables[NameTable].Clear();
                SqlDataAdapter.Fill(dataSet, NameTable);
                datagridView.DataSource = dataSet.Tables[NameTable];


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
        public async Task AsyncLoadDB(DataGridView datagridView)
        {
            await Task.Run(() => LoadDB());


        }
        public int countdtb()
        {




            return dataSet.Tables.Count;
        }
        public void LoadDB()
        {
            SqlConnection connection = new SqlConnection(DBConnectMsSql.connectionString);

            //try
            //{

            string commantext = "SELECT * FROM " + "[" + NameTable + "] ";

            connection.Open();

            SqlCommand mySqlCommand = new SqlCommand(commantext, connection);

            SqlDataAdapter = new SqlDataAdapter(mySqlCommand);
            sqlCommandBuilder = new SqlCommandBuilder(SqlDataAdapter);
            sqlCommandBuilder.GetDeleteCommand();

            dataSet = new DataSet();

            SqlDataAdapter.Fill(dataSet, NameTable);




            // обновление datagridview
            datagridView.DataSource = dataSet.Tables[NameTable];



            for (int i = 0; i < datagridView.RowCount; i++)
            {
                if (i != 0)
                {
                    datagridView.Rows[i - 1].ReadOnly = true;
                }
            }



            //}
            //catch (Exception exp)
            //{

            //    MessageBox.Show(exp.Message);
            //}
            //finally
            //{

            //    connection.Close();

            //}
        }
        public void del()
        {
            try
            {
                if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ind = datagridView.SelectedCells[0].RowIndex;
                    datagridView.Rows.RemoveAt(ind);
                    dataSet.Tables[NameTable].Rows[ind].Delete();
                    SqlDataAdapter.Update(dataSet, NameTable);
                    Reload();
                }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString());
            }
        }
        private void del_row(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                /* if (e.ColumnIndex == datagridView.ColumnCount - 1)
                 {
                     string task = datagridView.Rows[e.RowIndex].Cells[datagridView.ColumnCount - 1].Value.ToString();


                         if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                         {
                             int rowIndex = e.RowIndex;
                             datagridView.Rows.RemoveAt(rowIndex);

                             dataSet.Tables[NameTable].Rows[rowIndex].Delete();
                             SqlDataAdapter.Update(dataSet, NameTable);

                         }





                 }*/

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());

            }
            finally
            {
                Reload();
            }

        }

        private List<string> ListTableName()
        {
            List<string> list = new List<string>();


            connection.Open();

            SqlCommand com = new SqlCommand("SHOW TABLES", connection);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                CountTableInDB++;
                //   comboBox1.Items.Add(reader[0].ToString());
                list.Add(reader[0].ToString());
            }

            connection.Close();
            return list;

        }



    }
}
