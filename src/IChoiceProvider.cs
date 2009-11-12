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
		public IEnumerable<FileChoice> GetChoices()
		{
			foreach (var path in Directory.GetFiles(@"C:\Users\John\Documents\Adapt It Work\Palaung-B to Rulai adaptations\Adaptations"))
			{
				yield return new FileChoice(path);
			}
		}
	}
}