namespace TwitterChecker
{
	public class Data
	{
		public string Email { get; set; }

		public string Pass { get; set; }

		public Data(string E, string P)
		{
			Email = E;
			Pass = P;
		}
	}
}
