using System.Collections.Generic;

namespace TwitterChecker
{
	public class Description
	{
		public List<object> hashtags { get; set; }

		public List<object> symbols { get; set; }

		public List<object> urls { get; set; }

		public List<object> user_mentions { get; set; }
	}
}
