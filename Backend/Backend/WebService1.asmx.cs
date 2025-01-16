using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Backend
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WorldDbConnection"].ConnectionString;

        [WebMethod]
        public List<string> GetAllCountries()
        {
            var countries = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Name FROM Country", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(reader.GetString(0));
                }
            }
            return countries;
        }

        [WebMethod]
        public List<string> GetCitiesByCountry(string countryCode)
        {
            var cities = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Name FROM City WHERE CountryCode = @CountryCode", connection);
                command.Parameters.AddWithValue("@CountryCode", countryCode);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(reader.GetString(0));
                }
            }
            return cities;
        }

        [WebMethod]
        public string GetCountryByCode(string countryCode)
        {
            string countryName = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Name FROM Country WHERE Code = @CountryCode", connection);
                command.Parameters.AddWithValue("@CountryCode", countryCode);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    countryName = reader.GetString(0);
                }
            }
            return countryName;
        }

        [WebMethod]
        public List<string> GetCityByName(string cityName)
        {
            var cities = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Name FROM City WHERE Name LIKE @CityName", connection);
                command.Parameters.AddWithValue("@CityName", "%" + cityName + "%"); // Tìm kiếm theo tên gần đúng
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(reader.GetString(0)); // Lấy tên thành phố
                }
            }
            return cities;
        }
    }
}
