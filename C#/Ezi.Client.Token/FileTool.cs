using System;
using System.IO;
using System.Text;

namespace Ezi.Client.Token
{
	public class FileTool
	{
        private static string TAG = "#;#";
        //private static double expireTime = 7000;  // seconds
        private static string TokenFile = "ezToken.txt";

        public static void UpdateToken(string appid, string token, DateTime expiry)
        {
            //token = token == null ? "" : token;
            //DateTime expiry = DateTime.Now.AddSeconds(expireTime);
            string content = appid + TAG + token + TAG + expiry.ToString();
            SaveFile(TokenFile, content);
            return;
        }

        public static AccessToken GetLastAccessToken()
        {
            try
            {
                string content = ReadFile(TokenFile);
                string[] paras = content.Split(TAG);
                AccessToken atdb = new AccessToken();
                atdb.ezAppId = paras[0];
                atdb.accessToken = paras[1];
                atdb.expiry = DateTime.Parse(paras[2]);
                return atdb;
            }
            catch { return new AccessToken(); }
        }

        private static string ReadFile(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                int n = fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FileNotFoundException e) { return e.Message; }
        }

        private static string SaveFile(string savedName, string content)
        {
            try
            {
                FileStream fs = new FileStream(savedName, FileMode.Create, FileAccess.Write);
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                fs.Write(byteArray, 0, byteArray.Length);
                fs.Close();
                return content;
            }
            catch (FileNotFoundException e) { return e.Message; }
        }
    }
}

