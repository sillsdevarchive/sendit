namespace SendIt
{
	public class FileChoice
	{
		public string Path { get; private set; }

		public FileChoice(string path)
		{
			Path = path;
		}

		public override string ToString()
		{
			return System.IO.Path.GetFileNameWithoutExtension(Path);
		}
	}
}