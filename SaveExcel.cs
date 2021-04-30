using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GenFormDBDll
{
    class SaveExcel
    {
        public void SaveEx(DataGridView dataGridView)
        {

            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                Excel.Application excelapp = new Excel.Application();
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {



                    for (int i = 0; i < dataGridView.RowCount + 1; i++)
                    {
                        for (int j = 1; j < dataGridView.ColumnCount + 1; j++)
                        {
                            if (i == 0)
                            {
                                worksheet.Rows[i + 1].Columns[j] = dataGridView.Columns[j - 1].HeaderText;
                            }
                            else
                                worksheet.Rows[i + 1].Columns[j] = dataGridView.Rows[i - 1].Cells[j - 1].Value;
                        }
                    }

                    excelapp.AlertBeforeOverwriting = false;
                    workbook.SaveAs(saveFileDialog1.FileName);
                    excelapp.Quit();



                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


        }
    }
}
