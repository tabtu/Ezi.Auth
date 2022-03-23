using Ezi.Client.Token;

namespace Ezi.Client.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            string AppId = "my@email.com";
            string AppSecret = "abcdefghijklmn";
            Token.Generator token = new Token.Generator();


            var req = token.requestAccessToken(AppId, AppSecret, "100001", "192.168.1.1").GetAwaiter().GetResult();
            Console.WriteLine(req.accessToken);
            Console.WriteLine("END");
        }
    }
}
