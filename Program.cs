using System;
using System.Threading;
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
			bool created = false;
			using (Mutex mutex = new Mutex(true, "ApplicationLoggerMutex", out created)) {
				if (!created) {
					// Already running
					Console.WriteLine("Application already running, will exit");
					Application.Exit();
				}
			}

			Application.Run(new MainForm());
		}
	}
}
