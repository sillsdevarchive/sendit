using System;
using System.Collections.Generic;
using System.IO;

namespace SendIt
{
	public interface IChoiceProvider
	{
		IEnumerable<FileChoice> GetChoices();
	}

	public class TestChoiceProvider : IChoiceProvider
	{
		private readonly GetPathToAdaptationsFoder _pathToAdaptationsFolder;

		public delegate string GetPathToAdaptationsFoder();
		public TestChoiceProvider(GetPathToAdaptationsFoder pathToAdaptationsFolder)
		{
			_pathToAdaptationsFolder = pathToAdaptationsFolder;
		}

		public IEnumerable<FileChoice> GetChoices()
		{
			string folderPath = _pathToAdaptationsFolder();
			if (folderPath == null || !Directory.Exists(folderPath))
			{
				yield break;
			}
			foreach (var path in Directory.GetFiles(folderPath, "*.xml"))
			{
				if(!path.ToLower().Contains(".bak"))//e.g,, foo.BAK.XML
					yield return new FileChoice(path);
			}
		}
	}
}