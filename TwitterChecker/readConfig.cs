using System;
using System.IO;
using Newtonsoft.Json;

namespace TwitterChecker
{
	internal class readConfig
	{
		public static int threads { get; set; }

		public static string comboFile { get; set; }

		public static string proxyFile { get; set; }

		public static string usernamesFile { get; set; }

		public static bool failsPrint { get; set; }

		public static bool print2FA { get; set; }

		public static bool color { get; set; }

		public static bool saveFail { get; set; }

		public static bool useProxy { get; set; }

		public static bool useOG { get; set; }

		public static string proxyType { get; set; }

		public static string comboSplit { get; set; }

		public static string webhookurl { get; set; }

		public static bool usewebhook { get; set; }

		public static bool usehits2 { get; set; }

		public static bool useaged2 { get; set; }

		public static bool usetranslators2 { get; set; }

		public static bool useverifieds2 { get; set; }

		public static bool useogusernames2 { get; set; }

		public static bool usegif2 { get; set; }

		public static bool use4l2 { get; set; }

		public static bool use50kmore2 { get; set; }

		public static bool use30kto50k2 { get; set; }

		public static bool use20kto30k2 { get; set; }

		public static bool use10kto20k2 { get; set; }

		public static bool use1kto10k2 { get; set; }

		public static bool usehits { get; set; }

		public static bool useaged { get; set; }

		public static bool usetranslators { get; set; }

		public static bool useverifieds { get; set; }

		public static bool useogusernames { get; set; }

		public static bool usegif { get; set; }

		public static bool use4l { get; set; }

		public static bool use50kmore { get; set; }

		public static bool use30kto50k { get; set; }

		public static bool use20kto30k { get; set; }

		public static bool use10kto20k { get; set; }

		public static bool use1kto10k { get; set; }

		public static void read()
		{
			try
			{
				string value = File.ReadAllText("configuration.json");
				RootConfig rootConfig = JsonConvert.DeserializeObject<RootConfig>(value);
				threads = rootConfig.Settings.threads;
				comboFile = rootConfig.Settings.comboFile;
				proxyFile = rootConfig.Proxys.proxyFile;
				usernamesFile = rootConfig.OGUsernames.usernamesFile;
				saveFail = rootConfig.Settings.saveFails;
				useProxy = rootConfig.Proxys.use;
				useOG = rootConfig.OGUsernames.use;
				proxyType = rootConfig.Proxys.type;
				comboSplit = rootConfig.Settings.comboSplit;
				usewebhook = rootConfig.Webhook.use;
				webhookurl = rootConfig.Webhook.url;
				usehits2 = rootConfig.Webhook.WebhookSettings.With2FA.send_Hits;
				useaged2 = rootConfig.Webhook.WebhookSettings.With2FA.send_Aged;
				usetranslators2 = rootConfig.Webhook.WebhookSettings.With2FA.send_Translators;
				useverifieds2 = rootConfig.Webhook.WebhookSettings.With2FA.send_Verifieds;
				useogusernames2 = rootConfig.Webhook.WebhookSettings.With2FA.send_OGUserrnames;
				usegif2 = rootConfig.Webhook.WebhookSettings.With2FA.send_GIFProfile;
				use4l2 = rootConfig.Webhook.WebhookSettings.With2FA.send_1to4Letter;
				use50kmore2 = rootConfig.Webhook.WebhookSettings.With2FA.send_50Kmore;
				use30kto50k2 = rootConfig.Webhook.WebhookSettings.With2FA.send_30Kto50K;
				use20kto30k2 = rootConfig.Webhook.WebhookSettings.With2FA.send_20Kto30K;
				use10kto20k2 = rootConfig.Webhook.WebhookSettings.With2FA.send_10Kto20K;
				use1kto10k2 = rootConfig.Webhook.WebhookSettings.With2FA.send_1Kto10K;
				usehits = rootConfig.Webhook.WebhookSettings.No2FA.send_Hits;
				useaged = rootConfig.Webhook.WebhookSettings.No2FA.send_Aged;
				usetranslators = rootConfig.Webhook.WebhookSettings.No2FA.send_Translators;
				useverifieds = rootConfig.Webhook.WebhookSettings.No2FA.send_Verifieds;
				useogusernames = rootConfig.Webhook.WebhookSettings.No2FA.send_OGUserrnames;
				usegif = rootConfig.Webhook.WebhookSettings.No2FA.send_GIFProfile;
				use4l = rootConfig.Webhook.WebhookSettings.No2FA.send_1to4Letter;
				use50kmore = rootConfig.Webhook.WebhookSettings.No2FA.send_50Kmore;
				use30kto50k = rootConfig.Webhook.WebhookSettings.No2FA.send_30Kto50K;
				use20kto30k = rootConfig.Webhook.WebhookSettings.No2FA.send_20Kto30K;
				use10kto20k = rootConfig.Webhook.WebhookSettings.No2FA.send_10Kto20K;
				use1kto10k = rootConfig.Webhook.WebhookSettings.No2FA.send_1Kto10K;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
