using HexonetAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexonetAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            HexonetAPI.Connection connection = new HexonetAPI.Connection("https://coreapi.1api.net/api/call.cgi", "1234", "login", "password");

            Dictionary<string, string> command = new Dictionary<string, string>();
            command.Add("COMMAND", "CheckDomain");
            command.Add("DOMAIN", "microsoft.com");
            Response response = connection.Request(command);
            List<string> lst = response.AsList();

            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            Console.WriteLine(response.Code);
            Console.WriteLine(response.Description);

            Console.ReadLine();
        }
    }
}
