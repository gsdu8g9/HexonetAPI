HexonetAPI
==========

C# API for (Hexonet.com)[http://hexonet.com] domain reseller, with this api you can create your own domain registrar. This api is created for integrate with a (http://www.websitepanel.net/)[http://www.websitepanel.net/] module for customers can register and manage domains inside panel


#### How to implement:
Implement is super easy. First make a connection to Hexonet API with you private credentials, this will run on server side and is secure. After that call command see (https://www.hexonet.net/software/reseller-api)[https://www.hexonet.net/software/reseller-api] for more informations about api commands accepted.

After all you can return commnd result inside a List<string> here you can parse like you want.


	HexonetAPI.Connection connection = new HexonetAPI.Connection(new User("1234", "login", "password"));

	Command command = new Command();
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