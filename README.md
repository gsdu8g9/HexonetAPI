HexonetAPI
==========

C# API for Hexonet.com Domain reseller 


Implementation:

    HexonetAPI.Connection connection = new HexonetAPI.Connection("https://coreapi.1api.net/api/call.cgi", "1234", "login", "password");
  
    Dictionary<string, string> command = new Dictionary<string, string>();
    command.Add("COMMAND", "CheckDomain");
    command.Add("DOMAIN", "microsoft.com");
    Response response = connection.Request(command);
    List<string> lst = response.AsList();
