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

	public class AdaptItCommandLineDriver : IFileProcessor
	{
		public string Process(string path)
		{
			var process = new System.Diagnostics.Process();
			process.StartInfo.FileName = "AdaptIt_ForSendIt.exe";
			string adaptationsFolderPath = Path.GetDirectoryName(Path.GetDirectoryName(path));
			//painfully, AI requires that we give a partial path, starting with its magic folder.
			adaptationsFolderPath = Path.GetFileName(adaptationsFolderPath);//this strips off all but the leaf directory

			//@"C:\Users\John\Documents\Adapt It Work\Palaung-B to Rulai adaptations";

			//painfully, we don't get to actually specificy the file name, AI does that, so here we must match what it does
			string outputPath=Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(path)+".txt");
			process.StartInfo.Arguments = "export \"" + adaptationsFolderPath + "\" \"" + Path.GetFileName(path) + "\" \"" + Path.GetTempPath().TrimEnd(new []{'\\'}) + "\"";
			process.Start();
			process.WaitForExit();
			return outputPath;
		}
	}
}