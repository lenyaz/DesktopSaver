using System;
using System.Net;
using System.Web.Script.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

public class Program
{
	public static int Main(string[] args)
	{
		if (args.Length == 0)
		{
			System.Console.WriteLine("Please enter a numeric argument.");
			return 1;
		}
		/*
		using(var reader = new StreamReader("settings.ini"))
		{
			var consumer_key = reader.ReadLine();
			var access_token = reader.ReadLine();
			
			
		}
		*/
		
		string consumer_key = File.ReadLines("settings.ini").ElementAtOrDefault(0);
		string access_token = File.ReadLines("settings.ini").ElementAtOrDefault(1);
		consumer_key=consumer_key.Replace("consumer_key=", "");
		access_token=access_token.Replace("access_token=","");
		
		Console.WriteLine(consumer_key);
		Console.WriteLine(access_token);
		var auth_url = "https://getpocket.com/v3/add";
		var httpRequest = (HttpWebRequest)WebRequest.Create(auth_url);
		httpRequest.Method = "POST";
		//httpRequest.Headers["Authorization"] = "Bearer url";
		httpRequest.ContentType = "application/json";
		
		var url = args[0];
		
		var data = new JavaScriptSerializer().Serialize(new
		{
			url = url,
			consumer_key = consumer_key,
			access_token = access_token,
		});

		Console.WriteLine(data);
		
		using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
		{
			streamWriter.Write(data);
		}

		var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
	}

	Console.WriteLine(httpResponse.StatusCode);
	return 0;
}

}
