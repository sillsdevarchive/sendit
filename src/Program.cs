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

			//this is done as a delegate so the provider could updates if the settings are updated
			var model = new ChooseTextViewModel(new TestChoiceProvider(() => SendItSettings.Singleton.PathToAdaptationsFolder),
				new AdaptItCommandLineDriver());
			Application.Run(new Shell(new ChooseTextView(model)));
		}
	}
}
