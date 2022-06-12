using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterChecker
{
	internal class Stats
	{
		public static bool titleChange;

		private static int previousCheckeds = 0;

		private static int[] CPMList = new int[60];

		public static int cpmI = 0;

		public static int CPM = 0;

		public static int HITS;

		public static int TOTALNIPROCENAT;

		public static int DRUGICHECKED;

		public static int FAILSGLOBAL;

		public static int NORMALFAILS;

		public static int NORMALRETRIES;

		public static int NORMALHITS;

		public static int NORMALTRANSLATOR;

		public static int NORMALVERIFIEDS;

		public static int CHECKED;

		public static int NORMALOGUSERNAME;

		public static int NORMALGIF;

		public static int NORMALLETTERNICK;

		public static int DODESET;

		public static int DODVADESET;

		public static int DOTRIDESET;

		public static int DOPEDESET;

		public static int PREKOPEDESET;

		public static int HITS2FA;

		public static int TRANSLATOR2FA;

		public static int VERIFIEDS2FA;

		public static int OGUSERNAME2FA;

		public static int GIF2FA;

		public static int LETTERNICK2FA;

		public static int DODESET2FA;

		public static int DODVADESET2FA;

		public static int DOTRIDESET2FA;

		public static int DOPEDESET2FA;

		public static int PREKOPEDESET2FA;

		public static int GOD20062;

		public static int GOD2006;

		public static bool Startgui { get; set; }

		public static void Listener()
		{
			Title();
		}

		public static void Title()
		{
			Task.Factory.StartNew(delegate
			{
				while (titleChange)
				{
					title();
					Thread.Sleep(1000);
				}
				title();
			});
		}

		public static void title()
		{
			Console.Title = $"HAZLA Twitter Checker | {DRUGICHECKED}/{Uploader.accountList.Count} | Checked – {TOTALNIPROCENAT}% | CPM – {CPM}";
			CalculateCPM();
			CalculateProcenat();
		}

		public static void CalculateCPM()
		{
			CPMList[cpmI % 60] = DRUGICHECKED - previousCheckeds;
			previousCheckeds = DRUGICHECKED;
			CPM = CPMList.Sum();
			cpmI++;
		}

		public static void calculateFails()
		{
			FAILSGLOBAL = DRUGICHECKED - (HITS2FA + NORMALHITS);
		}

		public static void CalculateProcenat()
		{
			TOTALNIPROCENAT = (int)(0.5f + 100f * (float)DRUGICHECKED / (float)Uploader.accountList.Count);
		}
	}
}
