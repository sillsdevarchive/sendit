using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SendIt
{
	public class ChooseTextViewModel
	{
		private readonly IChoiceProvider _choiceProvider;

		public ChooseTextViewModel(IChoiceProvider choiceProvider)
		{
			_choiceProvider = choiceProvider;
		}

		public IEnumerable<FileChoice> GetPathChoices()
		{
			return _choiceProvider.GetChoices();
		}

		public FileChoice SelectedPath { get; set; }

		public bool CanSend
		{
			get { return SelectedPath != null;}
		}

		public void SendPath()
		{

		}
	}


}
