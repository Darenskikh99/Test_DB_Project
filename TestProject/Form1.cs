using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Test.Models;
using Test.Database;

namespace TestProject
{
    public partial class Form1 : Form
    {
        private readonly DatabaseManager _databaseManager;

        public Form1()
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager("Data Source=DESKTOP-QTGMEVU;Initial Catalog=test_DB;Integrated Security=True");
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("Last Name", "Фамилия");
            dataGridView1.Columns.Add("Second Name", "Отчество");
            dataGridView1.Columns.Add("First Name", "Имя");
            dataGridView1.Columns.Add("Date_Employ", "Дата приема на работу");
            dataGridView1.Columns.Add("Date_Uneploy", "Дата увольнения");
            dataGridView1.Columns.Add("Status", "Статус");
            dataGridView1.Columns.Add("Department", "Отдел");
            dataGridView1.Columns.Add("Post", "Должность");
        }

        private void ReadSingleRow(DataGridView dwg, IDataRecord record)
        {
            var employee = new EmployeeResult
            {
                Id = record.GetInt32(0),
                FirstName = record.GetString(1),
                MiddleName = record.GetString(2),
                LastName = record.GetString(3),
                HireDate = !record.IsDBNull(4) ? record.GetDateTime(4) : (DateTime?)null,
                LeaveDate = !record.IsDBNull(5) ? record.GetDateTime(5) : (DateTime?)null,
                Status = record.GetString(6),
                Department = record.GetString(7),
                Post = record.GetString(8)
            };

            dwg.Rows.Add(new object[]
            {
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.MiddleName,
                employee.HireDate,
                employee.LeaveDate,
                employee.Status,
                employee.Department,
                employee.Post
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
        }

        private void ComboBox1_TextUpdate(object sender, EventArgs e)
        {
            string sqlExpression = "orderBy" + comboBox1.Text;

            dataGridView1.Rows.Clear();
            _databaseManager.OpenConection();

            using (var command = new SqlCommand(sqlExpression, _databaseManager.OpenConection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }
                }
                reader.Close();

                dataGridView1.Refresh();
            }
        }

        private void ComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            string sqlExpression = "filtrationBy" + comboBox2.Text;
            if(textBox1.Text == null)
            {
                textBox2.Text = "1";
            }

            dataGridView1.Rows.Clear();
            _databaseManager.OpenConection();

            using (var command = new SqlCommand(sqlExpression, _databaseManager.OpenConection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParametr = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = textBox2.Text
                };
                command.Parameters.Add(nameParametr);


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }
                }
                reader.Close();

                dataGridView1.Refresh();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string sqlExpression = "filtrationByLastName";

            dataGridView1.Rows.Clear();
            _databaseManager.OpenConection();

            using (var command = new SqlCommand(sqlExpression, _databaseManager.OpenConection()))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParametr = new SqlParameter
                {
                    ParameterName = "@subStringName",
                    Value = textBox1.Text
                };
                command.Parameters.Add(nameParametr);


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }
                }
                reader.Close();

                dataGridView1.Refresh();
            }
        }
    }
}
