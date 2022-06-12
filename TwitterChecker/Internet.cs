using System.Runtime.InteropServices;
using System.Security;

namespace TwitterChecker
{
	[SecuritySafeCritical]
	internal class Internet
	{
		[DllImport("wininet.dll")]
		protected static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

		internal static bool Connection()
		{
			int Description;
			return InternetGetConnectedState(out Description, 0);
		}
	}
}
