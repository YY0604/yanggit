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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TestWebService
{
    public partial class testjson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void ButtonReg_Click(object sender, EventArgs e)
        {
            #region json测试代码
            //string connectionstring = @" Data Source = 192.168.2.24\sql2012; Initial Catalog = Exd2SYJZ; User ID = sa; Password =sa.; Charset = UTF8;";
            //string commandtext = "select FOrderBillNo,FBarCode,FItemName from InvExpXiaoShouChuKuDetail";
            //SqlDataAdapter da = new SqlDataAdapter(connectionstring,commandtext);
            //DataSet ds = new DataSet();
            //DataTable dt = ds.Tables[0];
            //da.Fill(ds,"customer");
            //DataSet ds = new DataSet();
            //DataTable auto = new DataTable("User");
            //添加列
            //auto.Columns.Add("ID");//添加ID列
            //auto.Columns.Add("Name");//添加Name列
            //auto.Columns.Add("Sex");
            //for (int i = 1; i <= 10; i++)
            //{
            //    auto.Rows.Add(new object[] { i, "Boss", "a" });//初始化
            //}
            //ds.Tables.Add(auto);
            //DataTable copy_Table = ds.Tables[0];//将dataset集合中第一个表复制到copy_Table
            //for (int i = 0; i < copy_Table.Rows.Count; i++)
            //{
            //    this.label_Result.Text += copy_Table.Rows[i][1].ToString() + "<br/>";
            //    //Response.Write("<script>alert(" + copy_Table.Rows[i][0] + ");</script>");
            //}

            //StringBuilder buffer = new StringBuilder();
            //foreach (DataColumn dc in copy_Table.Columns)
            //{
            //    buffer.AppendLine(String.Format("{0,15}", dc.ColumnName));
            //    buffer.AppendLine(dc.ColumnName);//输出列名，AppendLine自带的换行符输出到web界面上不会换行，只在控制台应用程序中才会显示。
            //}
            //this.label_Result.Text += buffer;

            //foreach (DataRow dr in copy_Table.Rows)
            //{
            //    this.label_Result.Text += "<br/>";
            //    foreach (DataColumn dc in copy_Table.Columns)
            //    {
            //        this.label_Result.Text += dr[dc] + " ";//通过Foreach循环遍历DataTable的行和列
            //    }
            //}
            //string p;
            //JArray data = new JArray();
            //string[] n1 = { "oid", "c01", "c02" };
            //DataTable dt = ds.Tables[0];
            //int m = dt.Rows.Count;
            //int n = dt.Columns.Count;
            //JToken[] v4 = new JToken[n];
            //for (int i = 0; i < m; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //    {
            //        v4[j] = dt.Rows[i][j].ToString();
            //    }
            //    data.Add(addData(n1, v4));
            //}


            //JToken[] v1 = { "ab", "dfs1", 12 };
            //JToken[] v2 = { "cd", "dfs2", 13 };
            //data.Add(addData(n1, v1));
            //data.Add(addData(n1, v2));
            //p = data.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");

            //data.Add(addData(n1, v1));
            //data.Add(addData(n1, v2));
            //data.Add(addData(n1, v3));
            //p = data.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            //this.label_Result.Text += p;
            #endregion

            string uname = "test01";
            string pswd = "111111";
            CookieContainer cookie = new CookieContainer();
            this.label_Result.Text += login(cookie, "http://120.55.161.147/orange/webservice/appservice", uname, pswd);
            this.label_Result.Text += sendProduct(cookie, "http://120.55.161.147/orange/webservice/appservice");
            this.label_Result.Text += sendPackingInfo(cookie, "http://120.55.161.147/orange/webservice/appservice");
            //string x = @"{""success"":true}";
            //this.label_Result.Text += x;
            //if (x.Contains( @"""success"":false"))
            //{
            //    this.label_Result.Text += "失败";
            //}
            //else
            //{
            //    this.label_Result.Text += "成功";
            //}

            FileStream fs = new FileStream(@"E:\MyTest.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.Position = fs.Length;//设定书写的开始位置为文件的末尾
            fs.Write(Encoding.UTF8.GetBytes(this.label_Result.Text), 0, Encoding.UTF8.GetByteCount(this.label_Result.Text));//将待写入内容追加到文件末尾
            StreamWriter sw = new StreamWriter(fs);//根据上面创建的文件流创建写数据流
            sw.BaseStream.Seek(0, SeekOrigin.End);//设置写数据流的起始位置为文件流的末尾
            sw.WriteLine(DateTime.Now);
            sw.Flush();//清空缓冲区内容，并把缓冲区内容写入基础流
            sw.Close();//关闭写数据流  

         }

        private JObject addData(string[] name, JToken[] value)
        {
            JObject jb = new JObject();
            for (int i = 0; i < name.Length; i++)
            {
                jb.Add(name[i], value[i]);
            }
            return jb;
        }

        private string HttpPost(CookieContainer cookie, string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //request.ServicePoint.Expect100Continue = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            byte[] buf = Encoding.GetEncoding("utf-8").GetBytes(postDataStr);
            myRequestStream.Write(buf, 0, buf.Length);
            //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            //myStreamWriter.Write(postDataStr);
            // myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Cookies = cookie.GetCookies(response.ResponseUri);//获取包含与特定 URI 关联的 Cookie 实例的 CookieCollection。
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
        private JObject addParam(string name, JToken value)
        {
            JObject jb = new JObject();
            jb.Add("name", name);
            jb.Add("value", value);
            return jb;
        }
        private string login(CookieContainer cookie, string url, string uname, string pswd)
        {
            string p;
            JArray param = new JArray();
            param.Add(addParam("uname", uname));
            param.Add(addParam("pswd", pswd));
            p = param.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            p = "action=servicelogin&params=" + UrlEncode(p);
            return HttpPost(cookie, url, p);
        }
        //推送销售出库信息
        private string sendProduct(CookieContainer cookie, string url)
        {
            string p;
            JArray param = new JArray();

            param.Add(addParam("oid", 1003853)); // (订单号)
            param.Add(addParam("fid", 600000001));//固定值(厂商ID)
            param.Add(addParam("proid", 700000001)); //固定值(流程ID)
            param.Add(addParam("status", 1)); //固定值(流程编码)
            param.Add(addParam("odid", 1001));//固定值(流程状态) 注意当前值和 包装接口发送时不一样
            param.Add(addParam("dataid", 0)); //固定值(数据编号) 注意当前值和 包装接口发送时不一样

            JArray data = new JArray();
            string[] n1 = { "c01", "c02" };
            JToken[] v1 = { "dfs1", 12 };
            JToken[] v2 = { "dfs2", 13 };
            data.Add(addData(n1, v1));
            data.Add(addData(n1, v2));
            p = data.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            param.Add(addParam("data", p));

            p = param.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            p = "action=commitflow&params=" + UrlEncode(p);
            return HttpPost(cookie, url, p);
        }
        //推送发货信息
        private string sendPackingInfo(CookieContainer cookie, string url)
        {
            string p;
            JArray param = new JArray();

            param.Add(addParam("fid", 600000001));//固定值(厂商ID)
            param.Add(addParam("proid", 700000001)); //固定值(流程ID)
            param.Add(addParam("oid", 1003853)); // (订单号)
            param.Add(addParam("odid", 1001)); //固定值(流程编码)
            param.Add(addParam("status", 2));//固定值(流程状态) 注意当前值和包装接口发送时不一样
            param.Add(addParam("dataid", 1)); //固定值(数据编号) 注意当前值和包装接口发送时不一样
            param.Add(addParam("prtype", 1)); //固定值发送包装信息时需要
            param.Add(addParam("fsaddress", 12345)); //发货地址
            param.Add(addParam("logistics", 123446)); //物流信息


            JArray data = new JArray();
            string[] n1 = { "c01", "c02" };//数据列集合
            JToken[] v1 = { "dfs", "kkkk" };
            JToken[] v2 = { "xdf", "yrc" };
          
            data.Add(addData(n1, v1));
            data.Add(addData(n1, v2));
            p = data.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            param.Add(addParam("data", p));

            p = param.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "");
            p = "action=commitflow&params=" + UrlEncode(p);
            return HttpPost(cookie, url, p);
        }
    }
}