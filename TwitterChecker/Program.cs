using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using Colorful;
using Newtonsoft.Json;

namespace TwitterChecker
{
	internal class Program
	{
		public static HttpClient client = new HttpClient();

		public int StatusCode { get; set; }

		private static void Main(string[] args)
		{
			bool flag = false;
			Colorful.Console.Title = "HAZLA Twitter Checker";
			string[] array = new string[41]
			{
				"codecracker", "x32dbg", "x64dbg", "ollydbg", "ida", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer",
				"httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb", "kgdb",
				"mdb", "processhacker", "scylla_x86", "scylla_x64", "scylla", "idau64", "idau", "idaq", "idaq64", "idaw",
				"idaw64", "idag", "idag64", "ida64", "ida", "ImportREC", "IMMUNITYDEBUGGER", "MegaDumper", "CodeBrowser", "reshacker",
				"cheat engine"
			};
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				if (process == Process.GetCurrentProcess())
				{
					continue;
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (process.ProcessName.ToLower().Contains(array[j]))
					{
						flag = true;
					}
					if (process.MainWindowTitle.ToLower().Contains(array[j]))
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				Environment.Exit(0);
				return;
			}
			string value = "1.7";
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if (!File.Exists(folderPath + "autz.txt"))
			{
				Colorful.Console.Write(" License: ");
				string text = System.Console.ReadLine();
				string serverAuthUrl = "https://api.aksgfkookpko1pjganoagkpp54125kgjalgjt4okt.cf/login/";
				string productName = "Hazla";
				Authorize authorize = new Authorize();
				authorize.ServerAuthUrl = serverAuthUrl;
				authorize.ProductName = productName;
				if (!authorize.Auth(text))
				{
					System.Console.WriteLine("Auth fail!");
					System.Console.WriteLine("Reason: " + authorize.ResponseStatus);
				}
				else
				{
					File.WriteAllText(folderPath + "autz.txt", text);
					using (WebClient webClient = new WebClient())
					{
						if (!webClient.DownloadString("https://api.aksgfkookpko1pjganoagkpp54125kgjalgjt4okt.cf/version.html").Contains(value))
						{
							Colorful.Console.Write("New update! Updating...");
							Thread.Sleep(1000);
							Process.Start("Updater.exe");
							if (File.Exists("configuration.json"))
							{
								File.Delete("configuration.json");
							}
							Environment.Exit(0);
						}
					}
					Colorful.Console.Clear();
					Maingui();
					Mainpage();
				}
				System.Console.ReadLine();
				return;
			}
			string key = File.ReadAllText(folderPath + "autz.txt");
			string serverAuthUrl2 = "https://api.aksgfkookpko1pjganoagkpp54125kgjalgjt4okt.cf/login/";
			string productName2 = "Hazla";
			Authorize authorize2 = new Authorize();
			authorize2.ServerAuthUrl = serverAuthUrl2;
			authorize2.ProductName = productName2;
			if (!authorize2.Auth(key))
			{
				System.Console.WriteLine("Auth fail!");
				System.Console.WriteLine("Reason: " + authorize2.ResponseStatus);
			}
			else
			{
				using (WebClient webClient2 = new WebClient())
				{
					if (!webClient2.DownloadString("https://api.aksgfkookpko1pjganoagkpp54125kgjalgjt4okt.cf/version.html").Contains(value))
					{
						Colorful.Console.Write("New update! Updating...");
						Thread.Sleep(1000);
						Process.Start("Updater.exe");
						if (File.Exists("configuration.json"))
						{
							File.Delete("configuration.json");
						}
						Environment.Exit(0);
					}
				}
				Colorful.Console.Clear();
				Maingui();
				Mainpage();
			}
			System.Console.ReadLine();
		}

		public static void Maingui()
		{
			Colorful.Console.WriteLine("  _   _   ___   ______ _       ___  ", Color.Aqua);
			Colorful.Console.WriteLine(" | | | | / _ \\ |___  /| |     / _ \\ ", Color.Aqua);
			Colorful.Console.WriteLine(" | |_| |/ /_\\ \\   / / | |    / /_\\ \\", Color.Aqua);
			Colorful.Console.WriteLine(" |  _  ||  _  |  / /  | |    |  _  |", Color.Aqua);
			Colorful.Console.WriteLine(" | | | || | | |./ /___| |____| | | |", Color.Aqua);
			Colorful.Console.WriteLine(" \\_| |_/\\_| |_/\\_____/\\_____/\\_| |_/", Color.Aqua);
		}

		public static void Mainpage()
		{
			Colorful.Console.Title = "HAZLA Twitter Checker";
			Colorful.Console.Write("\n[", Color.DarkGray);
			Colorful.Console.Write("1", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" START");
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("2", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" SETTINGS");
			string text = Colorful.Console.ReadLine();
			if (text == "2")
			{
				Settings();
				return;
			}
			Stats.Startgui = true;
			Actualstart();
		}

		private static void Settings()
		{
			Colorful.Console.Title = "HAZLA Twitter Checker | Settings";
			Colorful.Console.Clear();
			Maingui();
			Colorful.Console.Write("\n[", Color.DarkGray);
			Colorful.Console.Write("1", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" GUI SETTINGS [SOON]");
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("2", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" CONFIG SETTINGS");
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("3", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" RETURN HOME");
			string text = Colorful.Console.ReadLine();
			if (text == "1")
			{
				Colorful.Console.Clear();
				Maingui();
				Settings();
			}
			if (text == "2")
			{
				Colorful.Console.Clear();
				Maingui();
				Modifyconfig();
			}
			if (text == "3")
			{
				Colorful.Console.Clear();
				Maingui();
				Mainpage();
			}
		}

		private static void Modifyconfig()
		{
			Colorful.Console.Clear();
			Maingui();
			Config();
			Colorful.Console.Clear();
			Settings();
		}

		private static void Guisettings()
		{
			Colorful.Console.Title = "HAZLA Twitter Checker | GUI Settings";
			Colorful.Console.Write("\n[", Color.DarkGray);
			Colorful.Console.Write("1", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" COLOR SETTINGS");
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("2", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" SOUND SETTINGS");
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("3", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.WriteLine(" RETURN HOME");
			string text = Colorful.Console.ReadLine();
			if (text == "1")
			{
				Colorful.Console.Write("bogotac");
				Thread.Sleep(10000);
			}
			if (text == "2")
			{
				Colorful.Console.Write("2 bogotac");
				Thread.Sleep(10000);
			}
			if (text == "3")
			{
				Colorful.Console.Clear();
				Maingui();
				Mainpage();
			}
		}

		private static void proverasavea()
		{
			ConsoleKey key = System.Console.ReadKey(intercept: true).Key;
			if (key == ConsoleKey.Q)
			{
				File.WriteAllLines("combo_unchecked.txt", provera().Skip(Stats.DRUGICHECKED).ToArray());
				Environment.Exit(0);
			}
		}

		private static void Startgui()
		{
			while (true)
			{
				bool flag = false;
				string[] array = new string[41]
				{
					"codecracker", "x32dbg", "x64dbg", "ollydbg", "ida", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer",
					"httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb", "kgdb",
					"mdb", "processhacker", "scylla_x86", "scylla_x64", "scylla", "idau64", "idau", "idaq", "idaq64", "idaw",
					"idaw64", "idag", "idag64", "ida64", "ida", "ImportREC", "IMMUNITYDEBUGGER", "MegaDumper", "CodeBrowser", "reshacker",
					"cheat engine"
				};
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					if (process == Process.GetCurrentProcess())
					{
						continue;
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (process.ProcessName.ToLower().Contains(array[j]))
						{
							flag = true;
						}
						if (process.MainWindowTitle.ToLower().Contains(array[j]))
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					Environment.Exit(0);
				}
				Colorful.Console.Clear();
				Maingui();
				Stats.calculateFails();
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write(">", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Stats", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Checked:", Color.LightGray);
				Colorful.Console.WriteLine($"         {Stats.DRUGICHECKED} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Retries:", Color.LightGray);
				Colorful.Console.WriteLine($"         {Stats.NORMALRETRIES} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Fails:", Color.LightGray);
				Colorful.Console.WriteLine($"           {Stats.FAILSGLOBAL} ", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write(">", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" NO-2FA Accounts", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Hits:", Color.LightGray);
				Colorful.Console.WriteLine($"            {Stats.NORMALHITS} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Translators:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.NORMALTRANSLATOR} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Verifieds:", Color.LightGray);
				Colorful.Console.WriteLine($"       {Stats.NORMALVERIFIEDS} ", Color.Aqua);
				if (readConfig.useOG)
				{
					Colorful.Console.Write(" [", Color.DarkGray);
					Colorful.Console.Write("+", Color.Aqua);
					Colorful.Console.Write("]", Color.DarkGray);
					Colorful.Console.Write(" OG Usernames:", Color.LightGray);
					Colorful.Console.WriteLine($"    {Stats.NORMALOGUSERNAME} ", Color.Aqua);
				}
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" GIF Profile:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.NORMALGIF} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 1-4 Letters:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.NORMALLETTERNICK} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 50,000+:", Color.LightGray);
				Colorful.Console.WriteLine($"         {Stats.PREKOPEDESET} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 30,000 - 50,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DOPEDESET} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 20,000 - 30,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DOTRIDESET} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 10,000 - 20,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DODVADESET} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 1,000  - 10,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DODESET} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Aged:", Color.LightGray);
				Colorful.Console.WriteLine($"            {Stats.GOD2006} ", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write(">", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 2FA Accounts", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Hits:", Color.LightGray);
				Colorful.Console.WriteLine($"            {Stats.HITS2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Translators:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.TRANSLATOR2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Verifieds:", Color.LightGray);
				Colorful.Console.WriteLine($"       {Stats.VERIFIEDS2FA} ", Color.Aqua);
				if (readConfig.useOG)
				{
					Colorful.Console.Write(" [", Color.DarkGray);
					Colorful.Console.Write("+", Color.Aqua);
					Colorful.Console.Write("]", Color.DarkGray);
					Colorful.Console.Write(" OG Usernames:", Color.LightGray);
					Colorful.Console.WriteLine($"    {Stats.OGUSERNAME2FA} ", Color.Aqua);
				}
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" GIF Profile:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.GIF2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 1-4 Letters:", Color.LightGray);
				Colorful.Console.WriteLine($"     {Stats.LETTERNICK2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 50,000+:", Color.LightGray);
				Colorful.Console.WriteLine($"         {Stats.PREKOPEDESET2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 30,000 - 50,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DOPEDESET2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 20,000 - 30,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DOTRIDESET2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 10,000 - 20,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DODVADESET2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" 1,000  - 10,000:", Color.LightGray);
				Colorful.Console.WriteLine($" {Stats.DODESET2FA} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Aged:", Color.LightGray);
				Colorful.Console.WriteLine($"            {Stats.GOD20062} ", Color.Aqua);
				Thread.Sleep(1000);
			}
		}

		private static void StartguiVM()
		{
			while (true)
			{
				bool flag = false;
				string[] array = new string[41]
				{
					"codecracker", "x32dbg", "x64dbg", "ollydbg", "ida", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer",
					"httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb", "kgdb",
					"mdb", "processhacker", "scylla_x86", "scylla_x64", "scylla", "idau64", "idau", "idaq", "idaq64", "idaw",
					"idaw64", "idag", "idag64", "ida64", "ida", "ImportREC", "IMMUNITYDEBUGGER", "MegaDumper", "CodeBrowser", "reshacker",
					"cheat engine"
				};
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					if (process == Process.GetCurrentProcess())
					{
						continue;
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (process.ProcessName.ToLower().Contains(array[j]))
						{
							flag = true;
						}
						if (process.MainWindowTitle.ToLower().Contains(array[j]))
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					Environment.Exit(0);
				}
				Colorful.Console.Clear();
				Maingui();
				StatsVM.calculateFails();
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write(">", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Stats", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Checked:", Color.LightGray);
				Colorful.Console.WriteLine($"         {StatsVM.DRUGICHECKED} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Retries:", Color.LightGray);
				Colorful.Console.WriteLine($"         {StatsVM.NORMALRETRIES} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Fails:", Color.LightGray);
				Colorful.Console.WriteLine($"           {StatsVM.FAILSGLOBAL} ", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write(">", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Valid Mail Check", Color.Aqua);
				Colorful.Console.Write("\n [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Valid:", Color.LightGray);
				Colorful.Console.WriteLine($"           {StatsVM.NORMALHITS} ", Color.Aqua);
				Colorful.Console.Write(" [", Color.DarkGray);
				Colorful.Console.Write("+", Color.Aqua);
				Colorful.Console.Write("]", Color.DarkGray);
				Colorful.Console.Write(" Invalid:", Color.LightGray);
				Colorful.Console.WriteLine($"         {StatsVM.FAILSGLOBAL} ", Color.Aqua);
				Thread.Sleep(1000);
			}
		}

		private static void Config()
		{
			Colorful.Console.Clear();
			Maingui();
			Colorful.Console.Title = "HAZLA Twitter Checker | Modify config";
			Colorful.Console.Write("\n[", Color.DarkGray);
			Colorful.Console.Write("1", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.Write(" PROXY TYPE (HTTP/SOCKS4/SOCKS5): ");
			string text = Colorful.Console.ReadLine();
			if (!(text.ToLower() == "http") && !(text.ToLower() == "https") && !(text.ToLower() == "socks4") && !(text.ToLower() == "socks5"))
			{
				Colorful.Console.WriteLine(" Invalid proxy type!", Color.Red);
				Colorful.Console.ReadLine();
				Environment.Exit(0);
			}
			Colorful.Console.Write("[", Color.DarkGray);
			Colorful.Console.Write("2", Color.Aqua);
			Colorful.Console.Write("]", Color.DarkGray);
			Colorful.Console.Write(" THREADS: ");
			int num;
			try
			{
				num = int.Parse(Colorful.Console.ReadLine());
			}
			catch
			{
				num = 50;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary6 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary8 = new Dictionary<string, object>();
			dictionary2["threads"] = num;
			dictionary2["comboFile"] = "combo.txt";
			dictionary2["comboSplit"] = "|";
			dictionary2["saveFails"] = false;
			dictionary3["use"] = true;
			dictionary3["type"] = text;
			dictionary3["proxyFile"] = "proxies.txt";
			dictionary4["use"] = true;
			dictionary4["usernamesFile"] = "ogusernames.txt";
			dictionary["Settings"] = dictionary2;
			dictionary["Proxys"] = dictionary3;
			dictionary["OGUsernames"] = dictionary4;
			dictionary["Webhook"] = dictionary5;
			dictionary5["use"] = false;
			dictionary5["url"] = "yoururl";
			dictionary5["WebhookSettings"] = dictionary6;
			dictionary6["With2FA"] = dictionary7;
			dictionary7["send_Hits"] = false;
			dictionary7["send_Aged"] = true;
			dictionary7["send_Translators"] = true;
			dictionary7["send_Verifieds"] = true;
			dictionary7["send_OGUserrnames"] = true;
			dictionary7["send_GIFProfile"] = true;
			dictionary7["send_1to4Letter"] = true;
			dictionary7["send_50Kmore"] = true;
			dictionary7["send_30Kto50K"] = true;
			dictionary7["send_20Kto30K"] = true;
			dictionary7["send_10Kto20K"] = true;
			dictionary7["send_1Kto10K"] = true;
			dictionary6["No2FA"] = dictionary8;
			dictionary8["send_Hits"] = false;
			dictionary8["send_Aged"] = true;
			dictionary8["send_Translators"] = true;
			dictionary8["send_Verifieds"] = true;
			dictionary8["send_OGUserrnames"] = true;
			dictionary8["send_GIFProfile"] = true;
			dictionary8["send_1to4Letter"] = true;
			dictionary8["send_50Kmore"] = true;
			dictionary8["send_30Kto50K"] = true;
			dictionary8["send_20Kto30K"] = true;
			dictionary8["send_10Kto20K"] = true;
			dictionary8["send_1Kto10K"] = true;
			File.WriteAllText("configuration.json", JsonConvert.SerializeObject(dictionary, Formatting.Indented));
		}

		private static string[] provera()
		{
			string comboFile = readConfig.comboFile;
			return File.ReadAllLines(comboFile);
		}

		private static void Actualstart()
		{
			if (!File.Exists("configuration.json"))
			{
				Config();
			}
			readConfig.read();
			Uploader.Combo();
			Uploader.Proxies();
			Uploader.OGNicks();
			Stats.titleChange = true;
			Stats.Listener();
			new Thread(Startgui).Start();
			for (int i = 0; i < readConfig.threads; i++)
			{
				Thread thread = new Thread(Multithreading);
				thread.IsBackground = true;
				thread.Start();
			}
			while (Helper.ComboQueue.Count != 0)
			{
			}
			Colorful.Console.ReadKey();
			Colorful.Console.ReadLine();
		}

		private static void VMStart()
		{
			if (!File.Exists("configuration.json"))
			{
				Config();
			}
			readConfig.read();
			Uploader.Combo();
			Uploader.Proxies();
			Uploader.OGNicks();
			StatsVM.titleChange = true;
			StatsVM.Listener();
			new Thread(StartguiVM).Start();
			for (int i = 0; i < readConfig.threads; i++)
			{
				Thread thread = new Thread(MultithreadingVM);
				thread.IsBackground = true;
				thread.Start();
			}
			while (Helper.ComboQueue.Count != 0)
			{
			}
			Colorful.Console.ReadKey();
			Colorful.Console.ReadLine();
		}

		public static void Multithreading()
		{
			while (Helper.ComboQueue.Count != 0)
			{
				try
				{
					Data data = Helper.ComboQueue.Dequeue();
					bool flag = data != null;
					if (Stats.DRUGICHECKED < Uploader.accountList.Count)
					{
						Request.Start(data);
					}
				}
				catch
				{
				}
			}
		}

		public static void MultithreadingVM()
		{
			while (Helper.ComboQueue.Count != 0)
			{
				try
				{
					Data data = Helper.ComboQueue.Dequeue();
					bool flag = data != null;
					if (StatsVM.DRUGICHECKED < Uploader.accountList.Count)
					{
						TwitterVM.Start(data);
					}
				}
				catch
				{
				}
			}
		}
	}
}
