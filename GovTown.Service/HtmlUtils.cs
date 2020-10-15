using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;

namespace GovTown.Services
{
    public class HtmlUtils
    {
        public static string MessageBox(string str, int goBack = -1, bool refresh = false)
        {
            string t = "";
            t = $"<script>alert('{str}');history.go({goBack});</script>";
            if (goBack == -1 && refresh)
            {
                t = $"<script>alert('{str}');location.href=document.referrer;</script>";
            }
            return t;
        }

        public static string MessageBox(string str, string url)
        {
            string t= $"<script>alert('{str}');location.href='{url}';</script>";
            return t;
        }
        public static void SetTicket(string name, string roleName, int expireMin)
        {
            DateTime expireTime = DateTime.Now.AddMinutes(expireMin);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now,
                    expireTime, false, roleName);
            string authTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, authTicket);
            cookies.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(cookies);
        }

        public static void SetTicket(string name, string roleName)
        {
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now,
            //        expireTime, false, roleName);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now, false, roleName);
            string authTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, authTicket);
            HttpContext.Current.Response.Cookies.Add(cookies);
        }

        /// <summary>
        /// 数字转大写汉字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str5 = "";  //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 = "";    //数字的汉语读法
            string ch2 = "";    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            var str4 = ((long)(num * 100)).ToString();
            j = str4.Length;      //找出最高位
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                var str3 = str4.Substring(i, 1);    //从原num值中取出的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5.Replace("元", "").Replace("整", "");
        }

        public static readonly string[] WeekDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };


        /// <summary>
        /// 移除所有html标签
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string inputStr)
        {
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            return regex.Replace(inputStr, "");
        }

        public static string GetClientIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        /// <summary>
        /// 判断是否整数
        /// </summary>
        /// <param name="str">输入值</param>
        /// <returns>布尔型</returns>
        public static bool IsNumeric(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex reg = new Regex("^([0-9]{1,})$");
                Match number = reg.Match(str);
                return number.Success;
            }
            return false;
        }

        /// <summary>
        /// 获取指定长度字符串
        /// </summary>
        /// <param name="stringInput">输入字符串</param>
        /// <param name="stringLength">字符串长度</param>
        /// <param name="adddot">是否加入...</param>
        /// <returns>截取后字符串</returns>
        public static string GetString(string stringInput, int stringLength, bool adddot = false)
        {
            if (stringInput == " " || stringInput == null)
                return " ";
            string tempTitle = " ";
            if (stringInput.Length > stringLength)
                if (adddot)
                    tempTitle = stringInput.Substring(0, stringLength) + "...";
                else
                    tempTitle = stringInput.Substring(0, stringLength);
            else if (stringInput.Length == stringLength)
                tempTitle = stringInput;
            else
            {
                for (int i = 0; i < stringLength - stringInput.Length; i++)
                {
                    tempTitle = stringInput;
                }
            }
            return tempTitle;
        }

        public static string ConvertBoolToCharator(bool str)
        {
            return str ? "是" : "否";
        }

        /// <summary>
        /// HMTL转码
        /// </summary>
        /// <param name="inputStr">需要转码的字符串</param>
        /// <returns>转码后的字符串</returns>
        public static string HtmlEncode(string inputStr)
        {
            if (!string.IsNullOrEmpty(inputStr))
            {
                inputStr = HttpContext.Current.Server.HtmlEncode(inputStr);
                //inputStr = inputStr.Replace("<", "&lt;").Replace(">", "&gt;");
                //inputStr = inputStr.Replace(" ", "&nbsp;");
                inputStr = inputStr.Replace("\n\r", "<br />");
                inputStr = inputStr.Replace("\r\n", "<br />");
                inputStr = inputStr.Replace("\n", "<br />");
                inputStr = inputStr.Replace("\r", "<br />");
                return inputStr;
            }
            return "";
        }

        public static string HtmlDecode(string inputStr)
        {
            if (!string.IsNullOrEmpty(inputStr))
            {
                inputStr = HttpContext.Current.Server.HtmlDecode(inputStr);
                inputStr = inputStr.Replace("<br>", "<br />").Replace("<br />", "\n");
                return inputStr;
            }
            return "";
        }

        /// <summary>
        /// AES算法
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static class AES
        {
            //默认密钥向量 
            private static readonly byte[] AesKeys = { 0xB4, 0xD3, 0xBE, 0x06, 0xE8, 0xF0, 0xCD, 0xB3, 0x8C, 0xEA, 0xEE, 0xFA, 0x9C, 0x12, 0xDA, 0xA3 };

            // ReSharper disable once InconsistentNaming
            private static readonly byte[] _AesKeys = { 0xA3, 0xBB, 0x4E, 0xE9, 0xBC, 0xCF, 0x04, 0x48, 0xDA, 0xB4, 0x22, 0x47, 0x36, 0xE1, 0xFF, 0x3E, 0x9E, 0x93, 0x86, 0x77, 0xE9, 0xA9, 0x9D, 0x7E, 0x45, 0x91, 0x5D, 0xEA, 0xF6, 0x22, 0xE9, 0x7D, 0x38, 0x8C, 0xC9, 0xA3, 0xAC, 0xAE, 0x91, 0x22, 0xE9, 0x7D, 0x38, 0x8C, 0xC9, 0xA3, 0xAC, 0xAE, 0xB4, 0x22, 0x47, 0x36, 0xE1, 0xFF, 0xBB, 0x4E, 0xE9, 0xBC, 0xCF, 0x04, 0x48, 0xDA, 0xB4, 0x22 };

            //默认密匙
            //private const string _AESKey = "thisisakeyofhttp://www.jindou.indesignbyzhangwei20150110";

            private static string CreateAesKey()
            {
                string str = Convert.ToBase64String(_AesKeys).Substring(0, 32);
                return str;
            }

            private static readonly string AesKey = CreateAesKey();

            /// <summary>  
            /// AES加密算法  
            /// </summary>  
            /// <param name="encryptString">明文字符串</param>
            /// <param name="encryptKey">密钥</param>
            /// <returns>返回加密后的密文字节数组</returns>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static string Encrypt(string encryptString, string encryptKey = "")
            {
                encryptKey = encryptKey == "" ? AesKey : encryptKey;
                //int keyLength = encryptKey.Length;
                //System.Diagnostics.Debug.Assert(keyLength % 16 == 0, "密匙错误");
                //if (keyLength % 16 != 0) return encryptString;
                //return _AESKeys.Length.ToString() + "<br>" + Convert.ToBase64String(_AESKeys).Length.ToString() + "<br>" + encryptKey.Length;
                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey);
                rijndaelProvider.IV = AesKeys;
                ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

                byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
                byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Convert.ToBase64String(encryptedData);
            }

            /// <summary>  
            /// AES解密  
            /// </summary>  
            /// <param name="decryptString">密文字节数组</param>  
            /// <param name="decryptKey">密钥</param>  
            /// <returns>返回解密后的字符串</returns>  
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static string Decrypt(string decryptString, string decryptKey = "")
            {
                decryptKey = decryptKey == "" ? AesKey : decryptKey;
                int keyLength = decryptKey.Length;
                Debug.Assert(keyLength % 16 == 0, "密匙错误");
                if (keyLength % 16 != 0) return decryptString;
                try
                {
                    RijndaelManaged rijndaelProvider = new RijndaelManaged();
                    rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                    rijndaelProvider.IV = AesKeys;
                    ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                    byte[] inputData = Convert.FromBase64String(decryptString);
                    byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                    return Encoding.UTF8.GetString(decryptedData);
                }
                catch
                {
                    return decryptString;
                }
            }
        }

        /// <summary>
        /// 生成guid
        /// </summary>
        /// <returns>新的guid</returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        /// 加密后的guid
        /// </summary>
        /// <returns></returns>
        public static string GetEncryptGuid()
        {
            var str = GetGuid();
            return "wei" + str + "zhi";
        }

        /// <summary>
        /// 读取cookies
        /// </summary>
        /// <param name="cookiesName">cookies名称</param>
        /// <returns></returns>
        public static string ReadCookies(string cookiesName)
        {
            string temp = string.Empty;
            if (HttpContext.Current.Request.Cookies[cookiesName] != null)
            {
                temp = HttpContext.Current.Request.Cookies[cookiesName].Value;
                temp = AES.Decrypt(temp);
            }
            return temp;
        }

        /// <summary>
        /// 解密cookies
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <returns></returns>
        public static string ReadEncryptCookies(string cookiesName)
        {
            var cookies = ReadCookies(cookiesName);
            if (!string.IsNullOrEmpty(cookies))
            {
                cookies = cookies.Substring(3);
                cookies = cookies.Substring(0, cookies.Length - 3);
            }
            else cookies = "";
            return cookies;
        }

        /// <summary>
        /// 设置加密后的COOKIES
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <param name="cookiesValue"></param>
        /// <param name="expiress"></param>
        public static void SetEncryptCookies(string cookiesName, string cookiesValue, int expiress = 1440)
        {
            cookiesValue = "wei" + cookiesValue + "zhi";
            SetCookies(cookiesName, cookiesValue, expiress);
        }

        /// <summary>
        /// 设置加密后的COOKIES
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <param name="cookiesValue"></param>
        /// <param name="expireTime"></param>
        public static void SetEncryptCookies(string cookiesName, string cookiesValue, DateTime expireTime)
        {
            cookiesValue = "wei" + cookiesValue + "zhi";
            SetCookies(cookiesName, cookiesValue, expireTime);
        }

        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <param name="cookiesValue">cookies值</param>
        /// <param name="expires">超时时间(分钟)</param>
        public static void SetCookies(string cookiesName, string cookiesValue, int expires)
        {
            var httpCookie = HttpContext.Current.Response.Cookies[cookiesName] ?? new HttpCookie(cookiesName);
            httpCookie.Value = AES.Encrypt(cookiesValue);
            httpCookie.Expires = DateTime.Now.AddMinutes(expires);
            //HttpContext.Current.Response.Cookies[cookiesName].Value = AES.Encrypt(cookiesValue);
            //HttpContext.Current.Response.Cookies[cookiesName].Expires = DateTime.Now.AddMinutes(expires);
        }

        public static void SetCookies(string cookiesName, string cookiesValue)
        {
            var httpCookie = HttpContext.Current.Response.Cookies[cookiesName] ?? new HttpCookie(cookiesName);
            httpCookie.Value = AES.Encrypt(cookiesValue);
            //HttpContext.Current.Response.Cookies[cookiesName].Value = AES.Encrypt(cookiesValue);
            //HttpContext.Current.Response.Cookies[cookiesName].Expires = DateTime.Now.AddMinutes(expires);
        }

        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <param name="cookiesValue">cookies值</param>
        /// <param name="expires">超时时间(分钟)</param>
        public static void SetCookies(string cookiesName, string cookiesValue, DateTime expires)
        {
            var httpCookie = HttpContext.Current.Response.Cookies[cookiesName] ?? new HttpCookie(cookiesName);
            httpCookie.Value = AES.Encrypt(cookiesValue);
            httpCookie.Expires = expires;
            //HttpContext.Current.Response.Cookies[cookiesName].Value = AES.Encrypt(cookiesValue);
            //HttpContext.Current.Response.Cookies[cookiesName].Expires = DateTime.Now.AddMinutes(expires);
        }

        /// <summary>
        /// 清除Cookies
        /// </summary>
        /// <param name="cookiesName"></param>
        public static void ClearCookies(string cookiesName)
        {
            var httpCookie = HttpContext.Current.Response.Cookies[cookiesName];
            if (httpCookie != null)
                httpCookie.Value = null;
        }

        /// <summary>
        /// 将指定cookies设为过期
        /// </summary>
        /// <param name="cookiesName">cookies名称</param>
        public static void RemoveCookies(string cookiesName)
        {
            var httpCookie = HttpContext.Current.Response.Cookies[cookiesName];
            if (httpCookie != null)
                httpCookie.Value = AES.Encrypt("过期");
            var cookie = HttpContext.Current.Response.Cookies[cookiesName];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// 创建指定长度的随机码
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string CreateVerificationText(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < length; i++) { verification[i] = dictionary[random.Next(dictionary.Length - 1)]; }
            return new string(verification);
        }

        /// <summary>
        /// 随机生成一个长度不限的字符串
        /// </summary>
        /// <param name="minlength"></param>
        /// <param name="maxlength"></param>
        /// <returns></returns>
        public static string CreateVerificationText(int minlength, int maxlength)
        {
            var b = Guid.NewGuid().GetHashCode();
            Random rdm = new Random(b);
            int i = rdm.Next(minlength, maxlength);
            return CreateVerificationText(i);
        }

        public static int GetRandomNumberFormArray(int[] obj)
        {
            Random random = new Random();
            return obj[random.Next((obj.Length))];
        }

        /// <summary>
        /// 创建指定长度随机数字
        /// </summary>
        /// <param name="length">随机码长度</param>
        /// <returns></returns>
        public static string CreateRandomNum(int length)
        {
            char[] rdmNum = new char[length];
            char[] dictionary = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < length; i++)
            {
                rdmNum[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(rdmNum);
        }

        /// <summary>
        /// 创建指定长度随机字母加数字
        /// </summary>
        /// <param name="length">随机码长度</param>
        /// <returns></returns>
        public static string CreateNumberVerificationText(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random random = new Random();
            for (int i = 0; i < length; i++) { verification[i] = dictionary[random.Next(dictionary.Length - 1)]; }
            return new string(verification);
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="verificationText"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Pen(Color.Black);
            Font font = new Font("Arial", 12);
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            SizeF totalSizeF = g.MeasureString(verificationText, font);
            //PointF startPointF = new PointF((width - totalSizeF.Width) / 2, (height - totalSizeF.Height) / 2);
            PointF startPointF = new PointF(0, 0);
            //随机数产生器
            Random random = new Random();
            g.Clear(Color.White);
            foreach (char t in verificationText)
            {
                Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                g.DrawString(t.ToString(), font, brush, startPointF);
                var curCharSizeF = g.MeasureString(t.ToString(), font);
                startPointF.X += curCharSizeF.Width;
            }
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 產生圖形驗證碼。
        /// </summary>
        /// <param name="Code">傳出驗證碼。</param>
        /// <param name="CodeLength">驗證碼字元數。</param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(out String Code, int CodeLength, int Width, int Height, int FontSize)
        {
            String sCode = String.Empty;
            //顏色列表，用於驗證碼、噪線、噪點
            Color[] oColors ={ 
            Color.Black,
             Color.Red,
             Color.Blue,
             Color.Green,
             Color.Brown,
             Color.DarkBlue
            };
            Color[] bColors ={
                                 Color.Azure,
                                 Color.LightCoral,
                                 Color.LightPink,
                                 Color.LightSeaGreen
                            };
            //字體列表，用於驗證碼
            string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //驗證碼的字元集，去掉了一些容易混淆的字元
            char[] oCharacter = { '2', '3', '4', '5', '6', '8', '9', '0', '1' };
            Random oRnd = new Random();
            Bitmap oBmp = null;
            Graphics oGraphics = null;
            int N1 = 0;
            //System.Drawing.Point oPoint1 = default(System.Drawing.Point);
            //System.Drawing.Point oPoint2 = default(System.Drawing.Point);
            string sFontName = null;
            Font oFont = null;
            Color oColor = default(Color);
            Color bColor = default(Color);

            //生成驗證碼字串
            for (N1 = 0; N1 <= CodeLength - 1; N1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }

            oBmp = new Bitmap(Width, Height);
            oGraphics = Graphics.FromImage(oBmp);
            //填充背景色
            bColor = bColors[oRnd.Next(bColors.Length)];
            oGraphics.Clear(bColor);
            try
            {
                //for (N1 = 0; N1 <= 4; N1++)
                //{
                //    //畫噪線
                //    oPoint1.X = oRnd.Next(Width);
                //    oPoint1.Y = oRnd.Next(Height);
                //    oPoint2.X = oRnd.Next(Width);
                //    oPoint2.Y = oRnd.Next(Height);
                //    oColor = oColors[oRnd.Next(oColors.Length)];
                //    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                //}

                float spaceWith = 0, dotX = 0, dotY = 0;
                if (CodeLength != 0)
                {
                    spaceWith = (Width - FontSize * CodeLength - 10) / CodeLength;
                }

                for (N1 = 0; N1 <= sCode.Length - 1; N1++)
                {
                    //畫驗證碼字串
                    sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                    oFont = new Font(sFontName, FontSize, FontStyle.Italic);
                    oColor = oColors[oRnd.Next(oColors.Length)];

                    dotY = (Height - oFont.Height) / 2 + 2;//中心下移2像素
                    dotX = Convert.ToSingle(N1) * FontSize + (N1 + 1) * spaceWith;

                    oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
                }

                for (int i = 0; i < 30; i++)
                {
                    //畫噪點
                    int x = oRnd.Next(oBmp.Width);
                    int y = oRnd.Next(oBmp.Height);
                    Color clr = oColors[oRnd.Next(oColors.Length)];
                    oBmp.SetPixel(x, y, clr);
                }
                Code = sCode;
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                oBmp.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                oGraphics.Dispose();
            }
        }

        public static string AlertBox(string msg, int goHistroy = -1)
        {
            string str = "<script>alert('" + msg + "'); history.go(" + goHistroy + ");</script>";
            return str;
        }

        public static string Week(DateTime str)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(str.DayOfWeek)];
            return week;
        }

        public static string ShortWeek(DateTime str)
        {
            return Week(str).Replace("星期", "");
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }


        /// <summary>
        /// 检查上传文件扩展名
        /// </summary>
        /// <param name="fileName">被检查的文件名</param>
        /// <returns>布尔型</returns>
        public static bool CheckFileExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension != null)
                fileExtension = fileExtension.ToLower();
            else
                return false;
            string[] restrictExtension = { ".gif", ".jpg", ".png" };
            return restrictExtension.Any(t => fileExtension == t);
        }

        /// <summary>
        /// unicode转汉字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnicodeToGB(string text)
        {
            System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m2 in mc)
                {
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes));
                }
            }
            else
            {

            }
            return text;
        }
    }
}
