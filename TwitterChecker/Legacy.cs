using System.Collections.Generic;

namespace TwitterChecker
{
	public class Legacy
	{
		public List<object> advertiser_account_service_levels { get; set; }

		public string advertiser_account_type { get; set; }

		public string analytics_type { get; set; }

		public string created_at { get; set; }

		public string description { get; set; }

		public Entities entities { get; set; }

		public int fast_followers_count { get; set; }

		public int favourites_count { get; set; }

		public int followers_count { get; set; }

		public int friends_count { get; set; }

		public bool geo_enabled { get; set; }

		public bool has_custom_timelines { get; set; }

		public bool has_extended_profile { get; set; }

		public string id_str { get; set; }

		public bool is_translator { get; set; }

		public string location { get; set; }

		public int media_count { get; set; }

		public string name { get; set; }

		public int normal_followers_count { get; set; }

		public bool nsfw_user { get; set; }

		public List<object> pinned_tweet_ids_str { get; set; }

		public string profile_background_color { get; set; }

		public ProfileImageExtensions profile_image_extensions { get; set; }

		public string profile_image_url_https { get; set; }

		public string profile_interstitial_type { get; set; }

		public string profile_link_color { get; set; }

		public bool @protected { get; set; }

		public string screen_name { get; set; }

		public int statuses_count { get; set; }

		public string translator_type { get; set; }

		public bool verified { get; set; }
	}
}
