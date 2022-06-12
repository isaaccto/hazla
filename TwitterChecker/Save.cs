using System;
using System.IO;
using System.Threading;

namespace TwitterChecker
{
	internal class Save
	{
		public static string mainDir = "results/" + DateTime.Now.ToString("hh.mm.ss - MMM dd, yyyy") + "/";

		public static void Writeline(string text, string file, string directory)
		{
			while (true)
			{
				try
				{
					Directory.CreateDirectory(directory);
					using (StreamWriter streamWriter = new StreamWriter(directory + "/" + file + ".txt", append: true))
					{
						streamWriter.WriteLine(text);
					}
					return;
				}
				catch
				{
					Thread.Sleep(100);
				}
			}
		}
	}
}
