using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ApplicationLogger {
	static class Program {

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Check if it's already running
			if (Process.GetProcessesByName("ApplicationLogger").Length > 1 && !System.Diagnostics.Debugger.IsAttached) {
				// Already running!
				Console.WriteLine("Application already running, will exit");
				Application.Exit();
			} else {
				Application.Run(new MainForm());
			}
		}
	}
}
