using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SendIt.Properties;

namespace SendIt
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Settings.Default.SendItSettings.PathToAdaptationsFolder =   @"C:\Users\John\Documents\Adapt It Work\Palaung-B to Rulai adaptations\Adaptations";

			var model = new ChooseTextViewModel(new TestChoiceProvider(Settings.Default.SendItSettings.PathToAdaptationsFolder),
												new AdaptItCommandLineDriver());
			Application.Run(new Shell(new ChooseTextView(model)));
		}
	}
}
