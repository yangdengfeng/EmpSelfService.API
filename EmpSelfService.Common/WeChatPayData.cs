using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using LitJson;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace EmpSelfService.Common
{
    /// <summary>
    /// 微信支付协议接口数据类，所有的API接口通信都依赖这个数据结构，
    /// 在调用接口之前先填充各个字段的值，然后进行接口通信，
    /// 这样设计的好处是可扩展性强，用户可随意对协议进行更改而不用重新设计数据结构，
    /// 还可以随意组合出不同的协议数据包，不用为每个协议设计一个数据包结构
    /// </summary>
    public class WeChatPayData
    {
        public WeChatPayData()
        {

        }

        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws Exception
        **/
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                LogHelper.Error(this.GetType().ToString(), "WxPayData数据为空!");
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    LogHelper.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    LogHelper.Error(this.GetType().ToString(), "WxPayData字段数据类型错误!");
                    throw new Exception("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        
        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws Exception
        */
        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                LogHelper.Error(this.GetType().ToString(), "将空的xml串转换为WxPayData不合法!");
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
			
            try
            {
				//2015-06-29 错误是没有签名
				if(m_values["return_code"].ToString() != "SUCCESS")
				{
					return m_values;
				}
                //CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    LogHelper.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;
        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        */
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    LogHelper.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            LogHelper.Debug(this.GetType().ToString(), "Print in Web Page : " + str);
            return str;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSignRc4()
        {
            string str = ToUrl();
            string url = WeChatPayConfig.SIGN_URL;
            string rc4_sign = PostWebRequest(url, str, Encoding.UTF8);
            string de_sign = RC4(WeChatPayConfig.KEY, rc4_sign);
            return de_sign;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign()
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + WeChatPayConfig.KEY;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        /**
       * @生成签名，详见签名生成算法
       * @return 签名, sign字段不参加签名
       */
        public string MakeSignNew()
        {

            string tojson = ToJson();

            string postString = "user_tag=51e0c74e7c84f8c850968053d0953e65&sign=" + tojson;//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = "http://pay.bryzf.com/api/baotou/";//地址  
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  

            string[] aa = srcString.Replace("{", "").Replace("}", "").Replace("\"","").Split(',');


            return aa[0].Split(':')[1] + "|" + aa[1].Split(':')[1];
             

        }

        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }

        public static string RC4(string pwd, string data)
        {

            int[] key = new int[128];
            int[] box = new int[128];

            StringBuilder result = new StringBuilder();
            int pwd_length = pwd.Length;
            int data_length = data.Length;

            for (int i = 0; i < 128; i++)
            {
                key[i] = ord(pwd[i % pwd_length].ToString());
                box[i] = i;
            }

            for (int j = 0, i = 0; i < 128; i++)
            {
                j = (j + box[i] + key[i]) % 128;
                int tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }

            for (int a = 0, j = 0, i = 0; i < data_length; i++)
            {
                a = (a + 1) % 128;
                j = (j + box[a]) % 128;

                int tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;

                int k = box[((box[a] + box[j]) % 128)];

                result.Append(chr(ord(data[i].ToString()) ^ k));
            }
            return result.ToString();
        }

        public static int ord(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                //MessageBox.Show("ord：" + character);
                throw new Exception("Character is not valid.");
            }
        }

        public static string chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                //MessageBox.Show("chr：" + asciiCode);
                throw new Exception("ASCII Code is not valid.");
            }
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSignRc4()
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                //Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                //Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = MakeSignRc4();

            if (cal_sign == return_sign)
            {
                return true;
            }

            //Log.Error(this.GetType().ToString(), "WxPayData签名验证错误!");
            throw new Exception("WxPayData签名验证错误!");
        }


        public static Dictionary<string, string> getJson(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            Dictionary<string, string> jsonObject = (Dictionary<string, string>)ser.ReadObject(ms);
            return jsonObject;
        }



        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign()
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                LogHelper.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
               throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if(GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                LogHelper.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string ss = MakeSignNew();
            string cal_sign = "";
            if (ss.Split('|')[0] == "0")
            {
               cal_sign=ss.Split('|')[1];
            }
            else
            {
                throw new Exception("生成签名失败：" + ss.Split('|')[1]);

            }
            if (cal_sign == return_sign)
            {
                return true;
            }

            LogHelper.Error(this.GetType().ToString(), "WxPayData签名验证错误!");
            throw new Exception("WxPayData签名验证错误!");
        }

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }
    }
}