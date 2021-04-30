using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace GenFormDBDll
{
    public partial class GenFormdll : MaterialForm
    {
        private static string Connstring { get; set; }// "server=localhost;user=root;database=сеть_ресторанов;password=root";//{ get; set; }//@"Server=MYSQL5031.site4now.net;Database=db_a6a4ef_sharp65;Uid=a6a4ef_sharp65;Pwd=qwerty123";//  { get; set; }
        LoadDBDataGrid LoadDBDataGrid;
        static Button[] _buttons;
        static int g = 0;
        List<string> list = new List<string>();
        bool on_off_edit = false;


        static string tablename;
        /// <summary>
        /// Конструктор поумолчанию
        /// </summary>
        public GenFormdll()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            Start(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + "\\Database1.mdf" + ";Integrated Security=True");
        }

        /// <summary>
        /// Метод для запуска компонента
        /// </summary>
        /// <param name="MssqlConnection">Строка подключения MsSql Server </param>
        public void Start(string MssqlConnection)
        {

            Connstring = MssqlConnection;
            SqlConnection mySqlConnection = new SqlConnection(MssqlConnection);
            mySqlConnection.Open();

            SqlCommand com = new SqlCommand(@"SELECT Name
 FROM dbo.sysobjects
  WHERE(xtype = 'U')", mySqlConnection);

            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {

                g++;
                list.Add(reader[0].ToString());
            }

            mySqlConnection.Close();
            mySqlConnection.Dispose();
            GenButton();
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            button3.BackColor = Color.Red;






        }
        private void sub(ref bool i)
        {
            i = !i;
            if (i)
            {
                button3.BackColor = Color.Green;


                dataGridView1.ReadOnly = false;


                // MessageBox.Show("Положение 1");
            }
            else
            {
                button3.BackColor = Color.Red;
                //  MessageBox.Show("Положение 2");
            }

        }
        /// <summary>
        /// Метод для загрузки имен столбцов в Combobox
        /// </summary>
        /// <param name="text">Имя таблицы</param>
        void ds(string text)
        {
            dataGridView1.Refresh();
            dataGridView1.Update();

            DBConnectMsSql.connectionString = Connstring;//@"server=localhost;user=root;database=сеть_ресторанов;password=root";// Connstring;
            LoadDBDataGrid = new LoadDBDataGrid(text, dataGridView1);
            LoadDBDataGrid.LoadDB();

            Search search = new Search();
            search.combosearch(dataGridView1, comboBox1);


        }
        /// <summary>
        /// Метод для генерации кнопок
        /// </summary>
        void GenButton()
        {



            _buttons = new Button[g];
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i] = new Button();
                _buttons[i].Location = new Point(0, _buttons[i].Height * i);
                _buttons[i].Text = string.Format(list[i]);
                _buttons[i].Tag = i;

                _buttons[i].BackColor = System.Drawing.SystemColors.ActiveCaption;
                _buttons[i].Click += new EventHandler(my_show_db_clicl);
                _buttons[i].Width = panel1.Width - 20;
                panel1.Controls.Add(_buttons[i]);


            }
            g = 0;
        }
        /// <summary>
        /// Событие обработки выбраной кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void my_show_db_clicl(object sender, EventArgs e)
        {
            string name = (sender as Button).Text;
            tablename = name;
            ds(name);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search search = new Search();
            search.search(textBox1, dataGridView1, tablename, comboBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sub(ref on_off_edit);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDBDataGrid.del();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveExcel saveExcel = new SaveExcel();
            saveExcel.SaveEx(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDBDataGrid.save();
        }
    }
}
