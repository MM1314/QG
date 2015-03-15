using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using WX.Framework;
using System.Text;
using WX.Demo.WebClasses;

namespace WebDemo
{
    public partial class SendMessage : System.Web.UI.Page
    {
        private wxmessage GetWxMessage()
        {
            wxmessage wx = new wxmessage();
            StreamReader str = new StreamReader(Request.InputStream, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
            wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
            wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
            MyLog.Log("MsgType:" + wx.MsgType);
            if (wx.MsgType.Trim() == "event")
            {
                wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                MyLog.Log(wx.EventName);
                if (wx.EventName.ToUpper() == "LOCATION")
                {
                    wx.Latitude = xml.SelectSingleNode("xml").SelectSingleNode("Latitude").InnerText;
                    wx.Longitude = xml.SelectSingleNode("xml").SelectSingleNode("Longitude").InnerText;
                    wx.Precision = xml.SelectSingleNode("xml").SelectSingleNode("Precision").InnerText;
                }
                else
                {
                    wx.EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
                }
            }
            if (wx.MsgType.Trim() == "text")
            {
                wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
            }
            if (wx.MsgType.Trim() == "location")
            {
                wx.Location_X = xml.SelectSingleNode("xml").SelectSingleNode("Location_X").InnerText;
                wx.Location_Y = xml.SelectSingleNode("xml").SelectSingleNode("Location_Y").InnerText;
                wx.Scale = xml.SelectSingleNode("xml").SelectSingleNode("Scale").InnerText;
                wx.Label = xml.SelectSingleNode("xml").SelectSingleNode("Label").InnerText;

            }

            if (wx.MsgType.Trim() == "voice")
            {
                wx.Recognition = xml.SelectSingleNode("xml").SelectSingleNode("Recognition").InnerText;
            }

            return wx;
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    wxmessage wx = GetWxMessage();

        //    string locationInfo = "您的位置是经度：" + wx.Latitude +
        //        "，维度是：" + wx.Longitude + "，地理经度为：" + wx.Precision;
        //    Response.Write(locationInfo);
        //    //string res = "";


        //    //if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "subscribe")
        //    //{
        //    //    string content = "";
        //    //    if (!wx.EventKey.Contains("qrscene_"))
        //    //    {
        //    //        content = "/:rose欢迎北京永杰友信科技有限公司/:rose\n直接回复“你好”";
        //    //        res = sendTextMessage(wx, content);
        //    //    }
        //    //    else
        //    //    {
        //    //        content = "二维码参数：\n" + wx.EventKey.Replace("qrscene_", "");
        //    //        res = sendTextMessage(wx, content);
        //    //    }
        //    //}

        //    //else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.ToLower() == "scan")
        //    //{
        //    //    string str = "二维码参数：\n" + wx.EventKey;
        //    //    res = sendTextMessage(wx, str);
        //    //}
        //    //else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "CLICK")
        //    //{
        //    //    if (wx.EventKey == "HELLO")
        //    //        res = sendTextMessage(wx, "你好,欢迎使用北京永杰友信科技有限公司公共微信平台!");
        //    //}
        //    //else if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "LOCATION")
        //    //{
        //    //    res = sendTextMessage(wx, "您的位置是经度：" + wx.Latitude + "，维度是：" + wx.Longitude + "，地理经度为：" + wx.Precision);
        //    //}
        //    //else
        //    //{
        //    //    if (wx.MsgType == "text" && wx.Content == "你好")
        //    //    {
        //    //        res = sendTextMessage(wx, "你好,欢迎使用北京永杰友信科技有限公司公共微信平台!");
        //    //    }
        //    //    else if (wx.MsgType == "voice")
        //    //    {
        //    //        res = sendTextMessage(wx, wx.Recognition);
        //    //    }
        //    //    else if (wx.MsgType == "location")
        //    //    {
        //    //        res = sendTextMessage(wx, "您发送的位置是:" + wx.Label + ";纬度是:" + wx.Location_X + ";经度是:" + wx.Location_Y + ";缩放比例为:" + wx.Scale);
        //    //    }
        //    //    else
        //    //    {
        //    //        res = sendTextMessage(wx, "你好,未能识别消息!");
        //    //    }
        //    //}

        //    //Response.Write(res);
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            string MyOpenID = Request.QueryString["wxcode"];// "oJhpEuJ8bUnfTR9r6MyH0Whw_iC4";
            string MyContent = "有新业务.【车辆调度部】";

            if (string.IsNullOrEmpty(MyOpenID))
            {
                Response.Write("微信号为空！");
                return;
            }

            string res = "";
            string Access_Token = ApiAccessTokenManager.Instance.GetCurrentToken();

            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
            string postData = "{\"touser\":\"" + MyOpenID + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + MyContent + "\"}}";
            res = GetPage(posturl, postData);

            Response.Write(res);           
        }



        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Response.Write(err);
                return string.Empty;
            }
        }

        ///// <summary>
        ///// 根据当前日期 判断Access_Token 是否超期  如果超期返回新的Access_Token   否则返回之前的Access_Token
        ///// </summary>
        ///// <param name="datetime"></param>
        ///// <returns></returns>
        //public static string IsExistAccess_Token()
        //{

        //    string Token = string.Empty;
        //    DateTime YouXRQ;
        //    // 读取XML文件中的数据，并显示出来 ，注意文件路径
        //    string filepath = HttpContext.Current.Server.MapPath("XMLFile.xml");

        //    StreamReader str = new StreamReader(filepath, System.Text.Encoding.UTF8);
        //    XmlDocument xml = new XmlDocument();
        //    xml.Load(str);
        //    str.Close();
        //    str.Dispose();
        //    Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
        //    YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);

        //    if (DateTime.Now > YouXRQ)
        //    {
        //        DateTime _youxrq = DateTime.Now;
        //        Access_token mode = GetAccess_token();
        //        xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.access_token;
        //        _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
        //        xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
        //        xml.Save(filepath);
        //        Token = mode.access_token;
        //    }
        //    return Token;
        //}



        //public static Access_token GetAccess_token()
        //{
        //    string appid = "wx043225275885dafd";
        //    string secret = "cb4425b24ab79ef875029cf0bf326ae9";
        //    string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
        //    Access_token mode = new Access_token();

        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);

        //    req.Method = "GET";
        //    using (WebResponse wr = req.GetResponse())
        //    {
        //        HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

        //        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

        //        string content = reader.ReadToEnd();
        //        //Response.Write(content);
        //        //在这里对Access_token 赋值
        //        Access_token token = new Access_token();
        //        token = JsonHelper.ParseFromJson<Access_token>(content);
        //        mode.access_token = token.access_token;
        //        mode.expires_in = token.expires_in;
        //    }
        //    return mode;
        //}

    }
}

