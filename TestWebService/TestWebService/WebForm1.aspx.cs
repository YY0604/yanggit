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
using System.Data.SqlClient;
using System.Data;

namespace TestWebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonReg_Click(object sender, EventArgs e)
        {
            DataTable auto = new DataTable();
            //添加列
            auto.Columns.Add("ID");//添加ID列
            auto.Columns.Add("Name");//添加Name列
            for (int i = 1; i <= 10; i++)
            {
                auto.Rows.Add(new object[] { i, "Boss" });//初始化
            }
            DataRow[] selectrow = auto.Select("ID>'" + 7 + "'");//读出特定的行
            foreach (DataRow dr in selectrow)
            {
                this.label_Result.Text += dr[0].ToString() + "    " + dr[1].ToString() + "<br/>";
            }
            DataTable copy_Table = auto.Copy();//创建DataTable的完全副本（复制表的结构和数据）
            for(int i=0;i<copy_Table.Rows.Count;i++)
            {
               this.label_Result.Text += copy_Table.Rows[i][1].ToString()+"<br/>";
               //Response.Write("<script>alert("+copy_Table.Rows[i][0]+");</script>");
            }

            StringBuilder      buffer=new StringBuilder();
            foreach(DataColumn dc in auto.Columns)
            {
                //buffer.AppendLine(String.Format("{0,15}",dc.ColumnName));
                buffer.AppendLine(dc.ColumnName);//输出列名，AppendLine自带的换行符输出到web界面上不会换行，只在控制台应用程序中才会显示。
            }
            //buffer.Append("/r/t");
            this.label_Result.Text += buffer+"<br/>";

            foreach(DataRow dr in auto.Rows)
            {
                this.label_Result.Text += "<br/>";
                foreach (DataColumn dc in auto.Columns)
                {
                    this.label_Result.Text += dr[dc];//通过Foreach循环遍历DataTable的行和列
                }
            }
            this.label_Result.Text += ConvertBetweenDataTableAndXML_AX(auto);

            //DataSet ds = new DataSet();
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.baidu.com");    //创建一个请求示例
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
            //Stream responseStream = response.GetResponseStream();
            //StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            //string html = streamReader.ReadToEnd();
            //this.label_Result.Text += html;
            //ds.Tables.AddRange();

        }
        //datatable转换为XML
        public static string ConvertBetweenDataTableAndXML_AX(DataTable dtNeedCoveret)
        {
            System.IO.TextWriter tw = new System.IO.StringWriter();
            //if TableName is empty, WriteXml() will throw Exception. 
            dtNeedCoveret.TableName = dtNeedCoveret.TableName.Length == 0 ? "Table_AX" : dtNeedCoveret.TableName;
            dtNeedCoveret.WriteXml(tw);
            dtNeedCoveret.WriteXmlSchema(tw);
            //writefile(tw);
            return tw.ToString();
        }

        public static void  writefile(TextWriter tw)
        {
            string path = @"E:\MyTest.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                    sw.WriteLine(tw.ToString());
                }
            }

            // Open the file to read from.
            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}

            //try
            //{
            //    string path2 = path + "temp";
            //    // Ensure that the target does not exist.
            //    File.Delete(path2);

            //    // Copy the file.
            //    File.Copy(path, path2);
            //    Console.WriteLine("{0} was copied to {1}.", path, path2);

            //    // Delete the newly created file.
            //    File.Delete(path2);
            //    Console.WriteLine("{0} was successfully deleted.", path2);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The process failed: {0}", e.ToString());
            //}
        }

        //public static void usehttp()
        //{
        //    string strResult = "";

        //    try
        //    {
        //        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://120.55.161.147/orange/webservice/appservice?action=servicelogin");

        //        myRequest.Method = "POST";

        //        myRequest.ContentType = "application/x-www-form-urlencoded";

        //        try
        //        {
        //            HttpWebResponse HttpWResp = (HttpWebResponse)myRequest.GetResponse();
        //            Stream myStream = HttpWResp.GetResponseStream();
        //            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
        //            StringBuilder strBuilder = new StringBuilder();
        //            while (-1 != sr.Peek())
        //            {
        //                strBuilder.Append(sr.ReadLine());
        //            }
        //            strResult = strBuilder.ToString();

        //        }
        //        catch (Exception exp)
        //        {
        //            strResult = "错误：" + exp.Message;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        strResult = "错误：" + exp.Message;
        //    }
        //}

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        //public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        //{
        //    if (string.IsNullOrEmpty(url))
        //    {
        //        throw new ArgumentNullException("url");
        //    }
        //    if (requestEncoding == null)
        //    {
        //        throw new ArgumentNullException("requestEncoding");
        //    }
        //    HttpWebRequest request = null;
        //    //如果是发送HTTPS请求  
        //    //if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //    //{
        //    //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //    //    request = WebRequest.Create(url) as HttpWebRequest;
        //    //    request.ProtocolVersion = HttpVersion.Version10;
        //    //}
        //    //else
        //    //{
        //        request = WebRequest.Create(url) as HttpWebRequest;
        //    //}
        //    request.Method = "POST";

        //    request.Headers.Add("X_REG_CODE", "288a633ccc1");
        //    request.Headers.Add("X_MACHINE_ID", "a306b7c51254cfc5e22c7ac0702cdf87");
        //    request.Headers.Add("X_REG_SECRET", "de308301cf381bd4a37a184854035475d4c64946");
        //    request.Headers.Add("X_STORE", "0001");
        //    request.Headers.Add("X_BAY", "0001-01");
        //    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.Headers.Add("Accept-Language", "zh-CN");
        //    request.Headers.Add("Accept-Encoding", "gzip, deflate");
        //    request.Accept = "*/*";

        //    if (!string.IsNullOrEmpty(userAgent))
        //    {
        //        request.UserAgent = userAgent;
        //    }
        //    else
        //    {
        //        request.UserAgent = DefaultUserAgent;
        //    }

        //    if (timeout.HasValue)
        //    {
        //        request.Timeout = timeout.Value;
        //    }
        //    // if (cookies != null)
        //    // {
        //    request.CookieContainer = new CookieContainer();
        //    // request.CookieContainer.Add(cookies);
        //    // }
        //    //如果需要POST数据  
        //    if (!(parameters == null || parameters.Count == 0))
        //    {
        //        StringBuilder buffer = new StringBuilder();
        //        int i = 0;
        //        foreach (string key in parameters.Keys)
        //        {
        //            if (i > 0)
        //            {
        //                buffer.AppendFormat("&{0}={1}", key, parameters[key]);
        //            }
        //            else
        //            {
        //                buffer.AppendFormat("{0}={1}", key, parameters[key]);
        //            }
        //            i++;
        //        }

        //        byte[] data = requestEncoding.GetBytes(buffer.ToString());
        //        using (Stream stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //    }
        //    HttpWebResponse res;
        //    try
        //    {
        //        res = (HttpWebResponse)request.GetResponse();
        //    }
        //    catch (WebException ex)
        //    {
        //        res = (HttpWebResponse)ex.Response;
        //    }

        //    return res;
        //}

        //public string PostToHttpService(string url, string jsonData, string userName, string password)
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.Credentials = new NetworkCredential(userName, password);
        //    request.Timeout = 180000;//两分钟
        //    request.ReadWriteTimeout = 180000;//两分钟
        //    request.KeepAlive = false;
        //    byte[] datas = Encoding.UTF8.GetBytes(jsonData);
        //    request.ContentLength = datas.Length;

        //    try
        //    {
        //        Stream requestStream = request.GetRequestStream();
        //        requestStream.Write(datas, 0, datas.Length);
        //        requestStream.Close();
        //    }
        //    catch (System.Net.ProtocolViolationException ex)
        //    {
        //        request.Abort();
        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        request.Abort();
        //    }
        //    catch (System.ObjectDisposedException ex)
        //    {
        //        request.Abort();
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        request.Abort();
        //    }
        //    catch (System.NotSupportedException ex)
        //    {
        //        request.Abort();
        //    }


        //    HttpWebResponse response = null;
        //    string responseDatas = string.Empty;
        //    try
        //    {
        //        response = (HttpWebResponse)request.GetResponse();
        //        Stream streamResponse = response.GetResponseStream();
        //        using (StreamReader sr = new StreamReader(streamResponse))
        //        {
        //            responseDatas = sr.ReadToEnd();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        request.Abort();
        //    }
        //    finally
        //    {
        //        if (response != null)
        //        {
        //            try
        //            {
        //                response.Close();
        //            }
        //            catch
        //            {
        //                request.Abort();
        //            }
        //        }
        //    }
        //    return responseDatas;
        //}
    }
}