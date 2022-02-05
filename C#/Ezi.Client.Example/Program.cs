using Ezi.Client.Token;

namespace Ezi.Client.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            string AppId = "i@tabtu.cc";
            string AppSecret = "e807f1fcf82d132f9bb018ca6738a19f";
            Token.Token token = new Token.Token();


            var req = token.requestAccessToken(AppId, AppSecret, "188888").GetAwaiter().GetResult();
            Console.WriteLine(req.access_token);
            Console.WriteLine("END");
        }
    }
}
