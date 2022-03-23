using System;
using System.Security.Cryptography;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Ezi.Client.Token
{

    public class Generator
    {
        private static string AppId = "";
        private static string AppSecret = "";
        private static string AppSite = "";

        private static string AppBinding = "";

        //private static int expiry_mins = 15;
        //public static string ECHO = "PRODUCTION";

        private static readonly HttpClient client = new HttpClient();

        public static EzConfig createEzConfig(string pathUrl)
        {
            EzConfig ez = new EzConfig();
            ez.appid = AppId;
            ez.timestamp = createTimeStamp(DateTime.Now).ToString();
            string rawstring = "timestamp=" + ez.timestamp + "&url=" + pathUrl + "";
            ez.signature = SHA1_Hash(rawstring);
            return ez;
        }

        private static int createTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }

        private static string createNonceStr()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        private static string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }


        public string GetToken()
        {
            AccessToken actk = FileTool.GetLastAccessToken();
            if (actk != null && DateTime.Now < actk.expiry)
            {
                return actk.accessToken;
            }
            else
            {
                AccessToken atp = requestAccessToken(AppId, AppSecret, AppSite, AppBinding).GetAwaiter().GetResult();
                if (atp != null)
                {
                    FileTool.UpdateToken(AppId, atp.accessToken, atp.expiry);
                    return atp.accessToken;
                }
                else
                {
                    return "ERROR";
                }
            }
        }

        public async Task<AccessToken> requestAccessToken(string appid, string secret, string siteid, string binding)
        {
            try
            {
                string postData = "\"{'usr':'" + appid + "','pwd':'" + secret + "','st':'" + siteid + "','hd':'" + binding + "'}\"";
                var data = new StringContent(postData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.yjezimoc.com/token/login", data);
                AccessToken token = new AccessToken();
                token.ezAppId = appid;
                token.accessToken = response.Content.ReadAsStringAsync().Result;
                token.expiry = DateTime.Now;
                return token;
            }
            catch { return new AccessToken(); }
        }
    }
}

