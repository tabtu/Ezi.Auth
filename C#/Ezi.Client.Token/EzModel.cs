using System.Runtime.Serialization;

namespace Ezi.Client.Token
{
    public class AccessTokenPkg
    {
        public string? access_token;
        public string? expires_in;
    }

    [DataContract]
    public class EzConfig
    {
        [DataMember]
        public string? appid;
        [DataMember]
        public string? timestamp;
        [DataMember]
        public string? signature;
    }
}

