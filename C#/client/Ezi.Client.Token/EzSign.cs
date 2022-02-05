using System.Text;
using System.Security.Cryptography;

namespace Ezi.Client.Token
{
	public class EzSign
	{
        private static string token = "";

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);  // Dictionary sorting
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = SHA1(tmpStr, Encoding.Default);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SHA1 Encrypt
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="encode">Encode</param>
        /// <returns>SHA1 encrypted string</returns>
        static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);

                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1 Encrypt Error: " + ex.Message);
            }
        }
    }
}

