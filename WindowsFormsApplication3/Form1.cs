using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public SqlConnection sqlConnection = new SqlConnection("Data Source=Akshant;Initial Catalog=shashank;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "select distinct [Given Name] from master";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            textBox3.AutoCompleteCustomSource = MyCollection;
            reader.Close();
            sqlConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x, y, z;
            string a, b;
            a = textBox1.Text;
            b = textBox2.Text;

            x = int.Parse(a);
            y = int.Parse(b);
          
            if (x < 1944 || x > 2013)
            {
                MessageBox.Show(" year between 1944 and 2013", "Error");
            }

            else
            {

                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    MessageBox.Show("please select male , female or both", "Error");
                }


                else
                {
                    
                    if (checkBox1.Checked == true && checkBox2.Checked == true)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM  master where year ='" + textBox1.Text + "'  ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();


                        if (y > z)
                        {
                            MessageBox.Show("please enter valid number   ", "Error");
                        }
                        else
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText = "select top " + textBox2.Text + " [Given Name],Amount  from master where year='" + textBox1.Text + "' order by CAST(Amount as int) desc; ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }

                    else if (checkBox1.Checked)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM female_cy" + textBox1.Text + "_top ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();


                        if (y > z)
                        {
                            MessageBox.Show("please enter valid number in 'TOP' space  ", "Error");
                        }
                        else
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText = "SELECT top " + textBox2.Text + " [Given Name],Amount  FROM male_cy" + textBox1.Text + "_top ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    else if (checkBox2.Checked)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM male_cy" + textBox1.Text + "_top ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();


                        if (y > z)
                        {
                            MessageBox.Show("please enter valid number in 'TOP' space  ", "Error");
                        }
                        else
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText = "SELECT top " + textBox2.Text + " [Given Name],Amount  FROM female_cy" + textBox1.Text + "_top ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            chart1.Series[0].Points.Clear();


            int x, y;

            string a;
            a = textBox4.Text;
            x = int.Parse(a);
            if (x < 1944 || x > 2013)
            {
                MessageBox.Show("please enter year between 1944 and 2013", "Error");
            }
            else
            {
                if (checkBox3.Checked == false && checkBox4.Checked == false)
                {
                    MessageBox.Show("please select male or female", "Error");
                }
                else
                {
                    if (checkBox3.Checked == true && checkBox4.Checked == true)
                    {
                        MessageBox.Show("please choose one", "Error");
                    }
                    else if (checkBox3.Checked)
                    {
                        for (y = x; y <= 2013; y++)
                        {
                            int q, p;
                            string l, n;
                            q = y;
                            l = y.ToString();
                            SqlCommand sqlComman = new SqlCommand();
                            sqlComman.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlComman.CommandText = "SELECT amount FROM male_cy" + l + "_top where [Given Name] = '" + textBox3.Text + "' ";

                            sqlComman.ExecuteNonQuery();

                            object d = sqlComman.ExecuteScalar();
                            if (d == null || d == DBNull.Value)
                            {
                                p = 0;
                            }
                            else
                            {


                                n = sqlComman.ExecuteScalar().ToString();
                                p = int.Parse(n);

                            }
                            sqlConnection.Close();
                            chart1.Series["Series1"].Points.AddXY(q, p);
                            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                        }
                    }

                    else if (checkBox4.Checked)
                    {
                        for (y = x; y <= 2013; y++)
                        {
                            int q, p;
                            string l, n;
                            q = y;
                            l = y.ToString();
                            SqlCommand sqlComman = new SqlCommand();
                            sqlComman.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlComman.CommandText = "SELECT amount FROM female_cy" + l + "_top where [Given Name] = '" + textBox3.Text + "' ";

                            sqlComman.ExecuteNonQuery();

                            object d = sqlComman.ExecuteScalar();
                            if (d == null || d == DBNull.Value)
                            {
                                p = 0;
                            }
                            else
                            {


                                n = sqlComman.ExecuteScalar().ToString();
                                p = int.Parse(n);

                            }
                            sqlConnection.Close();
                            chart1.Series["Series1"].Points.AddXY(q, p);
                            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                        }
                    }


                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
