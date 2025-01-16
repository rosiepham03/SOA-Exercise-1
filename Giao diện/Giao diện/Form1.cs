using BTRef;
using System.Data;

namespace Giaodien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);

                // Gọi GetAllCountries từ Web Service
                var response = await client.GetAllCountriesAsync();

                var countries = response.Body.GetAllCountriesResult;

                if (countries != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ConvertToDataTable(countries.ToArray());
                }
                else
                {
                    MessageBox.Show("No countries found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);

                string countryCode = textBox1.Text.Trim();

                var response = await client.GetCountryByCodeAsync(countryCode);

                string countryName = response.Body.GetCountryByCodeResult; // Lấy tên quốc gia

                if (!string.IsNullOrEmpty(countryName))
                {
                    // Tạo DataTable từ kết quả
                    var resultTable = new DataTable();
                    resultTable.Columns.Add("Country Code", typeof(string));
                    resultTable.Columns.Add("Country Name", typeof(string));

                    resultTable.Rows.Add(countryCode, countryName);

                    // Gán DataTable cho DataGridView
                    dataGridView1.DataSource = resultTable;
                }
                else
                {
                    // Không tìm thấy quốc gia
                    MessageBox.Show("No country found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Xóa dữ liệu cũ trong DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);

                string cityName = textBox2.Text.Trim();

                var response = await client.GetCityByNameAsync(cityName);

                var cities = response.Body.GetCityByNameResult;

                if (cities != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ConvertToDataTable(cities.ToArray());
                }
                else
                {
                    MessageBox.Show("No cities found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new WebService1SoapClient(WebService1SoapClient.EndpointConfiguration.WebService1Soap);

                string countryCode = textBox3.Text.Trim();

                var response = await client.GetCitiesByCountryAsync(countryCode);

                var cities = response.Body.GetCitiesByCountryResult;

                if (cities != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ConvertToDataTable(cities.ToArray());
                }
                else
                {
                    MessageBox.Show("No cities found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private System.Data.DataTable ConvertToDataTable(string[] data)
        {
            var table = new System.Data.DataTable();
            table.Columns.Add("Name");

            foreach (var item in data)
            {
                table.Rows.Add(item);
            }

            return table;
        }
    }
}
