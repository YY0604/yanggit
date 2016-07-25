using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.Net;
using System.Web.Services;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using Microsoft.CSharp;

 
 namespace TestWebService
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonReg_Click(object sender, EventArgs e)
        {
            #region 返回一个字符串
            //string url = "http://www.webservicex.net/globalweather.asmx";
            //string[] args = new string[1];
            //args[0] = this.textBox_CityName.Text;
            //args[1] = "China";
            //object result = WebServiceHelper.InvokeWebService(url, "GetCitiesByCountry", args);
            //string a = result.ToString();
            //string[] strWeatherInfo = a.Split(',');

            //StringBuilder str = new StringBuilder("");
            //str.AppendLine("您查看天气信息如下:");
            //foreach (string info in strWeatherInfo)
            //{
            //    str.AppendLine(info + "<br/>");
            //}
            //this.label_Result.Text = result.ToString();
            #endregion

            #region 获得国内手机号码归属地数据库信息
            //string url = "http://ws.webxml.com.cn/WebServices/MobileCodeWS.asmx";
            ////string[] args = null;
            ////args[0] = this.textBox_CityName.Text;
            ////string[] s = new string[23];
            //string[]  result =(string[]) WebServiceHelper.InvokeWebService(url, "getDatabaseInfo", null);
            ////string a = result.ToString();
            ////string[] strWeatherInfo = a.Split(',');

            //StringBuilder str = new StringBuilder("");
            //str.AppendLine("您查看天气信息如下:");
            //foreach (string info in result)
            //{
            //    str.AppendLine(info + "<br/>");
            //}
            //this.label_Result.Text = str.ToString();
            #endregion

            #region 根据城市或地区名称查询获得未来三天内天气情况、现在的天气实况、天气和生活指数
            string url = "http://www.webxml.com.cn/WebServices/WeatherWebService.asmx";
            string[] args = new string[1];
            args[0] = this.textBox_CityName.Text;
            string[] result = (string[])WebServiceHelper.InvokeWebService(url, "getWeatherbyCityName", args);

            StringBuilder str = new StringBuilder("");
            str.AppendLine("您查看天气信息如下:");
            foreach (string info in result)
            {
                str.AppendLine(info + "<br/>");
            }
            this.label_Result.Text = str.ToString();
            #endregion
        }
       
    }
}