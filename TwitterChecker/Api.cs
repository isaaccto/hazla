using System.Linq;
using System.Management;
using System.Security;
using System.Text;
using Leaf.xNet;

namespace TwitterChecker
{
	[SecuritySafeCritical]
	internal class Api
	{
		internal string ResponseStatus { get; private set; }

		protected string PostData(Authorize var)
		{
			return "hwid=" + HWID() + "&license=" + var.LicenseKey + "&type=" + var.ProductName;
		}

		protected string HWID()
		{
			byte[] bytes = new byte[46]
			{
				83, 69, 76, 69, 67, 84, 32, 83, 101, 114,
				105, 97, 108, 78, 117, 109, 98, 101, 114, 32,
				70, 82, 79, 77, 32, 87, 105, 110, 51, 50,
				95, 79, 112, 101, 114, 97, 116, 105, 110, 103,
				83, 121, 115, 116, 101, 109
			};
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(Encoding.ASCII.GetString(bytes));
			ManagementObject managementObject = managementObjectSearcher.Get().Cast<ManagementObject>().First();
			return managementObject["SerialNumber"].ToString();
		}

		internal bool Check(Authorize Provider)
		{
			try
			{
				using HttpRequest httpRequest = new HttpRequest();
				httpRequest.ConnectTimeout = 7500;
				httpRequest.ReadWriteTimeout = 7500;
				HttpResponse httpResponse = httpRequest.Post(Provider.ServerAuthUrl, PostData(Provider), "application/x-www-form-urlencoded");
				if (httpResponse.Address.ToString() != Provider.ServerAuthUrl)
				{
					ResponseStatus = "Incorrect Request!";
					return false;
				}
				if (httpResponse.ToString() == "-1")
				{
					ResponseStatus = "Incorrect License!";
					return false;
				}
				if (httpResponse.ToString() == "0")
				{
					ResponseStatus = "Your License is inactive!";
					return false;
				}
				if (httpResponse.ToString() == "1")
				{
					ResponseStatus = "License is binded to another PC!";
					return false;
				}
				if (httpResponse.ToString() == "2")
				{
					ResponseStatus = "License binding Error!";
					return false;
				}
				if (httpResponse.ToString() == "3")
				{
					ResponseStatus = "Unknown Server Error!";
					return false;
				}
				if (httpResponse.ToString() == "69")
				{
					ResponseStatus = "";
					Dead.GoKill();
					return false;
				}
				if (!long.TryParse(httpResponse.ToString(), out var _))
				{
					ResponseStatus = "Unexpected Server Response!";
					return false;
				}
				ResponseStatus = "License OK";
				return true;
			}
			catch
			{
				ResponseStatus = "License Checking Error!";
				return false;
			}
		}
	}
}
