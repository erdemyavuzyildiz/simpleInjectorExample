namespace WebApplication10
{
	public class test1 : Itest1
	{
		public string variable1 { get; set; }

		public static test1 instance()
		{
			return new test1() { variable1 = "77" };
		}
	}
}