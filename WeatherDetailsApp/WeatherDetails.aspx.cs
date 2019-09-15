using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherDetailsApp
{
    public partial class _Default : Page
    {
        static string url = "https://api.openweathermap.org/data/2.5/weather?q=";
        static string apiKey = "1dcc04bb13db410f874b2e3b1409bb60";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Resetting all the fields
            lblHeader.Visible = true;
            lblCity.Visible = true;
            txtCity.Visible = true;
            btnSearch.Visible = true;
            btnBack.Visible = false;
            ltTable.Visible = false;
            lblError.Visible = false;
            #endregion
        }

        /// <summary>
        /// This method is used to show the weather details of the provided City.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text;
            if (!String.IsNullOrEmpty(city))
            {
                #region Hide and show form fields 
                lblCity.Visible = false;
                lblHeader.Text = "Weather details of : " + city;
                txtCity.Visible = false;
                btnSearch.Visible = false;
                btnBack.Visible = true;
                ltTable.Visible = true;
                lblError.Visible = false;
                #endregion
                string response = GetWeatherDetails(city);
                if (!String.IsNullOrEmpty(response))
                    ltTable.Text = response;
                else
                {
                    lblError.Visible = true;
                    lblHeader.Text = "Please Enter the details below to know the weather";
                    txtCity.Visible = true;
                    lblCity.Visible = true;
                    btnSearch.Visible = true;
                    ltTable.Text = "";
                    ltTable.Visible = false;
                }
            }
            else
            {
                lblError.Visible = true;
            }
        }

        /// <summary>
        /// This method is used to call the weather API and get response
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public ResponseWeather GetWeatherAPIResponse(String city)
        {
            String unitType = "metric"; //To fetch the Temperature in celcius unit
            HttpWebRequest apiRequest = WebRequest.Create(url + city + "&units=" + unitType + "&appid=" + apiKey) as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);
            return rootObject;
        }

        /// <summary>
        /// This method is used to populate the weather details of provided city in table
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string GetWeatherDetails(string city)
        {
            try
            { 
                    #region call API and get response
                    ResponseWeather rootObject = GetWeatherAPIResponse(city);
                    #endregion

                    #region Add dynamic table to show output data
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table><tr><th>Weather Description</th></tr>");
                    sb.Append("<tr><td>City:</td><td>" +
                    rootObject.name + "</td></tr>");
                    sb.Append("<tr><td>Country:</td><td>" +
                    rootObject.sys.country + "</td></tr>");
                    sb.Append("<tr><td>Wind:</td><td>" +
                    rootObject.wind.speed + " Km/h</td></tr>");
                    sb.Append("<tr><td>Current Temperature:</td><td>" +
                    rootObject.main.temp + " °C / " + ConvertCelciusToFahrenheit(rootObject.main.temp) + " F </td></tr>");
                    sb.Append("<tr><td>Minimum Temperature:</td><td>" +
                    rootObject.main.temp_min + " °C / " + ConvertCelciusToFahrenheit(rootObject.main.temp_min) + " F </td></tr>");
                    sb.Append("<tr><td>Maximum Temperature:</td><td>" +
                    rootObject.main.temp_max + " °C / " + ConvertCelciusToFahrenheit(rootObject.main.temp_max) + " F </td></tr>");
                    sb.Append("<tr><td>Pressure:</td><td>" +
                    rootObject.main.pressure + " hPa</td></tr>");
                    sb.Append("<tr><td>Humidity:</td><td>" +
                    rootObject.main.humidity + "%</td></tr>");
                    sb.Append("<tr><td>Sunrise:</td><td>" +
                    UnixTimeStampToDateTime(rootObject.sys.sunrise) + "</td></tr>");
                    sb.Append("<tr><td>Sunset:</td><td>" +
                    UnixTimeStampToDateTime(rootObject.sys.sunset) + "</td></tr>");
                    sb.Append("<tr><td>Weather:</td><td>" +
                    rootObject.weather[0].description + "</td></tr>");
                    sb.Append("</table>");
                    #endregion

                    return sb.ToString();

            }
            catch (Exception e)
            {
                //lblError.Visible = true;
                return "";
            }
        }

        /// <summary>
        /// This method is used to convert the temperature unit from celcius to Fahrenheit
        /// </summary>
        /// <param name="celsius"></param>
        /// <returns></returns>
        public static double ConvertCelciusToFahrenheit(double celsius)
        {
            double fahrenheit;

            fahrenheit = (celsius * 9) / 5 + 32;
            return fahrenheit;
        }

        /// <summary>
        /// This method is used to convert the timestamp into local time
        /// </summary>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        /// <summary>
        /// This method is used to reset all the fields and display the home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            lblCity.Visible = true;
            txtCity.Visible = true;
            txtCity.Text = "";
            btnSearch.Visible = true;
            btnBack.Visible = false;
            lblHeader.Text = "Please Enter the details below to know the weather";
            ltTable.Visible = false;
            ltTable.Text = "";
        }
    }
}