using ApplicationLogger.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ApplicationLogger {

	public partial class MainForm : Form {

		// Constants
		private const string SETTINGS_FIELD_PATH_TEMPLATE = "PathTemplate";

		// Properties
		private Timer timerCheck;
		private ContextMenu contextMenu;
		private MenuItem menuItemOpen;
		private MenuItem menuItemStartStop;
		private MenuItem menuItemExit;
		private bool isClosing;
		private bool isStarted;
		private string lastUserProcessId;
		private List<string> queuedMessages;


		// ================================================================================================================
		// CONSTRUCTOR ----------------------------------------------------------------------------------------------------

		public MainForm() {
			InitializeComponent();
		}


		// ================================================================================================================
		// EVENT INTERFACE ------------------------------------------------------------------------------------------------

		private void onFormLoad(object sender, EventArgs e) {
			// Just loaded everything

			// Initialize
			isClosing = false;
			isStarted = false;
			queuedMessages = new List<string>();

			// Create context menu for the tray icon
			createContextMenu();

			// Initialize notification icon
			notifyIcon.Icon = ApplicationLogger.Properties.Resources.trayIcon;
			notifyIcon.ContextMenu = contextMenu;

			// Initialize UI
			if (getSavedPathTemplate() == null || getSavedPathTemplate() == "") setSavedPathTemplate("logs/[[year]]_[[month]].log");
			textPathTemplate.Text = getSavedPathTemplate();

			// Finally, start
			start();
		}

		private void onFormClosing(object sender, FormClosingEventArgs e) {
			// Form is attempting to close
			if (!isClosing) {
				// User initiated, just minimize instead
				e.Cancel = true;
				Hide();
			} else {
				// Actually closing
			}
		}

		private void onTimer(object sender, EventArgs e) {
			// Timer tick: check for the current application
			var process = getCurrentUserProcess();

			bool isUserIdle = Win32.GetIdleTime() > 15l * 60l * 1000l; // 15 min
			bool isProcessValid = true;

			// Create a unique id
			string newUserProcessId;
			if (isUserIdle) {
				// User idle
				newUserProcessId = "User idle";
				isProcessValid = false;
				updateText("User Idle");
			} else if (process == null) {
				// Unknown process
				//newUserProcessId = "?";
				isProcessValid = false;

				return;
			} else {
				// Normal process
				newUserProcessId = process.ProcessName + "_" + process.MainWindowTitle;
			}

			if (lastUserProcessId != newUserProcessId) {
				// New process, do everything

				// Update dialog
				updateText(process);

				// Update textfile
				var now = DateTime.Now;
				string fileName = getSavedPathTemplate().Replace("[[month]]", now.ToString("MM")).Replace("[[day]]", now.ToString("dd")).Replace("[[year]]", now.ToString("yyyy"));
				string lineToWrite = "";
				lineToWrite += now.ToString();
				lineToWrite += "\t";
				lineToWrite += isProcessValid ? (process.MainModule.FileName + "\t" + process.ProcessName + "\t" + process.MainWindowTitle) : newUserProcessId;
				lineToWrite += "\r\n";
				try {
					System.IO.File.AppendAllText(fileName, lineToWrite);
				} catch (Exception exception) {
				}

				lastUserProcessId = newUserProcessId;
			}

		}

		private void onResize(object sender, EventArgs e) {
			// Resized window
			//notifyIcon.BalloonTipTitle = "Minimize to Tray App";
			//notifyIcon.BalloonTipText = "You have successfully minimized your form.";

			if (FormWindowState.Minimized == this.WindowState) {
				//notifyIcon.ShowBalloonTip(500);
				this.Hide();    
			}
		}

		private void onMenuItemOpenClicked(object Sender, EventArgs e) {
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void onMenuItemStartStopClicked(object Sender, EventArgs e) {
			exit();
		}

		private void onMenuItemExitClicked(object Sender, EventArgs e) {
			exit();
		}

		private void onDoubleClickNotificationIcon(object sender, MouseEventArgs e) {
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void onClickSave(object sender, EventArgs e) {
			// Save options
			setSavedPathTemplate(textPathTemplate.Text);
		}


		// ================================================================================================================
		// INTERNAL INTERFACE ---------------------------------------------------------------------------------------------

		private void createContextMenu() {
			// Initialize context menu
			contextMenu = new ContextMenu();

			// Initialize menu items
			menuItemOpen = new MenuItem();
			menuItemOpen.Index = 0;
			menuItemOpen.Text = "&Open";
			menuItemOpen.Click += new EventHandler(onMenuItemOpenClicked);

			menuItemStartStop = new MenuItem();
			menuItemStartStop.Index = 0;
			menuItemStartStop.Text = "";
			menuItemStartStop.Click += new EventHandler(onMenuItemStartStopClicked);

			menuItemExit = new MenuItem();
			menuItemExit.Index = 1;
			menuItemExit.Text = "E&xit";
			menuItemExit.Click += new EventHandler(onMenuItemExitClicked);

			contextMenu.MenuItems.AddRange(new MenuItem[] {menuItemOpen, menuItemStartStop, menuItemExit});

			updateContextMenu();
		}

		private void updateContextMenu() {
			if (menuItemStartStop != null) {
				if (isStarted) {
					menuItemStartStop.Text = "&Stop";
				} else {
					menuItemStartStop.Text = "&Start";
				}
			}
		}

		private void start() {
			if (!isStarted) {
				// Initialize timer
				timerCheck = new Timer();
				timerCheck.Tick += new EventHandler(onTimer);
				timerCheck.Interval = 500; // in miliseconds
				timerCheck.Start();

				isStarted = true;

				updateContextMenu();
			}
		}

		private void stop() {
			if (isStarted) {
				timerCheck.Stop();
				timerCheck.Dispose();
				timerCheck = null;

				isStarted = false;

				updateContextMenu();
			}
		}

		private void updateText(Process process) {
			// Update dialog text with the current application's data

			if (process != null) {
				updateText("Name: " + process.ProcessName + ", " + process.MainWindowTitle);
			} else {
				updateText("?");
			}
		}

		private void updateText(string text) {
			labelApplication.Text = text;
		}

		private void exit() {
			stop();
			isClosing = true;
			Close();
		}

		private Process getCurrentUserProcess() {
			// Find the process that's currently on top
			var procs = new List<Process>();

            var processListSnapshot = Process.GetProcesses();
            foreach (var process in processListSnapshot) {
                if (process.Id <= 4) { continue; } // system processes
				if (process.MainWindowHandle == Win32.GetForegroundWindow()) return process;
            }

			// Nothing found!
			return null;
		}

		private string getSavedPathTemplate() {
			return Settings.Default[SETTINGS_FIELD_PATH_TEMPLATE] as string;
		}

		private void setSavedPathTemplate(string pathTemplate) {
			Settings.Default[SETTINGS_FIELD_PATH_TEMPLATE] = pathTemplate;
			Settings.Default.Save();
		}


		// ================================================================================================================
		// INTERNAL CLASSES -----------------------------------------------------------------------------------------------

		internal struct LASTINPUTINFO {
			public uint cbSize;
			public uint dwTime;
		}

		public class Win32 {

			// System calls
			[DllImport("User32.dll")]
			private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

			[DllImport("Kernel32.dll")]
			private static extern uint GetLastError();

			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern bool IsWindowVisible(IntPtr hWnd);

			[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
			public static extern IntPtr GetForegroundWindow();

			public static uint GetIdleTime() {
				LASTINPUTINFO lastInPut = new LASTINPUTINFO();
				lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
				GetLastInputInfo(ref lastInPut);

				return ((uint)Environment.TickCount - lastInPut.dwTime);
			}

			public static long GetTickCount() {
				return Environment.TickCount;
			}

			public static long GetLastInputTime() {
				LASTINPUTINFO lastInPut = new LASTINPUTINFO();
				lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
				if (!GetLastInputInfo(ref lastInPut)) {
					throw new Exception(GetLastError().ToString());
				}

				return lastInPut.dwTime;
			}
		}

	}
}
