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
		private readonly string _pathToAdaptationsFolder;

		public TestChoiceProvider(string pathToAdaptationsFolder)
		{
			_pathToAdaptationsFolder = pathToAdaptationsFolder;
		}

		public IEnumerable<FileChoice> GetChoices()
		{
			foreach (var path in Directory.GetFiles(_pathToAdaptationsFolder))
			{
				yield return new FileChoice(path);
			}
		}
	}
}