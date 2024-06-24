using System.Security.Principal;

namespace Sss
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			string serviceName = "";
			string[] args = Environment.GetCommandLineArgs();
			if(args.Length > 1)
				{
				serviceName = args[1].Trim();
				}
			MainForm app = new MainForm(serviceName);
			if(!app.IsDisposed ) Application.Run(app);
		}


	}

	
}