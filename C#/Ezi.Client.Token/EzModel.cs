using System.Runtime.Serialization;

namespace Ezi.Client.Token
{
    public class AccessToken
    {
        public string ezAppId;
        public string accessToken;
        public DateTime expiry;

        public AccessToken()
        {
            ezAppId = "";
            accessToken = "";
            expiry = DateTime.Now;
        }
    }

    [DataContract]
    public class EzConfig
    {
        [DataMember]
        public string appid;
        [DataMember]
        public string timestamp;
        [DataMember]
        public string signature;

        public EzConfig()
        {
            appid = "";
            timestamp = "";
            signature = "";
        }
    }
}