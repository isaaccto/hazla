using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Colorful;

namespace TwitterChecker
{
	internal class Uploader
	{
		public static List<string> accountList = new List<string>();

		public static void Combo()
		{
			try
			{
				StreamReader streamReader = new StreamReader(readConfig.comboFile);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					string[] array = text.Split(':');
					if (array.Length == 2)
					{
						try
						{
							Helper.ComboQueue.Enqueue(new Data(array[0], array[1]));
							accountList.Add(text);
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception ex)
			{
				Colorful.Console.WriteLine("\n" + ex.Message, Color.Red);
				Colorful.Console.ReadLine();
				Process.GetCurrentProcess().Kill();
			}
		}

		public static void Proxies()
		{
			if (!readConfig.useProxy)
			{
				return;
			}
			try
			{
				StreamReader streamReader = new StreamReader(readConfig.proxyFile);
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					Helper.ProxyList.Add(item);
				}
			}
			catch (Exception ex)
			{
				Colorful.Console.WriteLine("\n" + ex.Message, Color.Red);
				Colorful.Console.ReadLine();
				Process.GetCurrentProcess().Kill();
			}
		}

		public static void OGNicks()
		{
			if (!readConfig.useOG)
			{
				return;
			}
			try
			{
				StreamReader streamReader = new StreamReader(readConfig.usernamesFile);
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					Helper.ogNick.Add(item);
				}
			}
			catch (Exception ex)
			{
				Colorful.Console.WriteLine("\n" + ex.Message, Color.Red);
				Colorful.Console.ReadLine();
				Process.GetCurrentProcess().Kill();
			}
		}
	}
}
