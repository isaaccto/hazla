using System;
using System.Security;

namespace TwitterChecker
{
	[SecuritySafeCritical]
	public class Authorize
	{
		public string ServerAuthUrl { internal get; set; }

		public string ProductName { internal get; set; }

		public string ResponseStatus { get; protected set; }

		internal string LicenseKey { get; private set; }

		public bool Auth(string key)
		{
			if (!Key.Validation(key))
			{
				ResponseStatus = "Incorrect License Key!";
				return false;
			}
			if (!Internet.Connection())
			{
				ResponseStatus = "Internet Connection is Required!";
				return false;
			}
			Dead.Check();
			LicenseKey = key;
			Api api = new Api();
			bool result = api.Check(this);
			ResponseStatus = api.ResponseStatus;
			api = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			return result;
		}
	}
}
