using System.IO;
using System.Threading;

namespace SendIt
{
	public interface IFileProcessor
	{
		string Process(string path);
	}

	public class DummyFileProcessor : IFileProcessor
	{
		public string Process(string path)
		{
			Thread.Sleep(3000);
			string s = Path.GetTempFileName() + ".txt";
			File.WriteAllText(s, "hello there");
			return s;
		}
	}
}