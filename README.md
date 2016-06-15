Application Logger
==================

Application Logger is a small standalone Windows application that logs the applications the current user is running at any time, adding them to a log file every time the user changes focus.

I created this because I wanted to have a log of all the applications I've been using, and what file they had open, for statistical purposes. While there are some applications and online services that already do this for you (e.g. RescueTime), no other service had the level of flexibility and raw access to data I wanted so I decided to write this myself.

The result is an application that writes data to a raw, tab-separated log file.

Usage
-----

Download [ApplicationLogger.exe](https://github.com/zeh/app-application-logger/tree/master/deploy). Copy it somewhere, and then run it. It will add itself to the system tray, and start logging the applications you run.

By default, it will also run at Windows startup. You can disable this behavior by right-clicking on its tray icon and deselecting that option.

After it is ran for the first time, it will also write a config file to its folder. This file has several options that dictate how the application runs; open the file for more information.

By default, it polls the active application every 0.5 seconds, and assumes the user to be away if no user input has been performed for 10 minutes.


Log format
----------

The log file contains these fields, separated by a tab:

 * Time where an event was recorded
 * Event type
   * `app::focus`: an application gained focus.
   * `status::idle`: the user is now idle. Time is the time of the last known interaction.
   * `status::stop`: logging has stopped, either because the application has quit, the user has stopped logging, or because the day has ended.
   * `status::end-of-day`: an app focus event has occurred in a different day and a different log file, so this will be the last event in that specific log file; the app focus event will be added to the next file.
 * Machine name
 * Process name
 * Full process path
 * Main process window title (if possible)

These are some examples of log lines:

	2014-12-11T12:55:12.3908117-05:00	app::focus	ZMACHINE	TweetDeck	C:\Program Files (x86)\Twitter\TweetDeck\TweetDeck.exe	TweetDeck
	2014-12-11T13:00:39.6875381-05:00	app::focus	ZMACHINE	chrome	C:\Program Files (x86)\Google\Chrome\Application\chrome.exe	Showtime Anytime app comes to Xbox One | Polygon - Google Chrome
	2014-12-11T13:29:55.1320650-05:00	app::focus	ZMACHINE	firefox	C:\Program Files (x86)\Mozilla Firefox\firefox.exe	C# snippets - Google Docs - Mozilla Firefox
	2014-12-11T13:33:14.0059504-05:00	app::focus	ZMACHINE	notepad++	C:\Program Files (x86)\Notepad++\notepad++.exe	D:\apps\logger\ApplicationLogger.cfg - Notepad++ [Administrator]
	2014-12-11T13:37:15.4130887-05:00	app::focus	ZMACHINE	explorer	C:\Windows\explorer.exe	src
	2014-12-11T11:37:23.3319560-05:00	app::focus	ZMACHINE	WDExpress	C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\WDExpress.exe	ApplicationLogger - Microsoft Visual Studio Express 2013 for Windows Desktop (Administrator)
	2014-12-11T13:44:53.9509379-05:00	app::focus	ZMACHINE	studio64	C:\Users\zeh.fernando\AppData\Local\Android\android-studio-0.8.14\bin\studio64.exe	android - [D:\work\QWERTY_APPS2852\dev\android-app] - [app] - ...\app\src\main\java\com\querty\obfuscated\ApplicationConstants.java - Android Studio 1.0
	2014-12-11T12:24:14.1160028-05:00	status::idle			
	2014-12-11T13:49:01.4716875-05:00	status::stop			


TODO
----

 * Maybe create a parser (pre-visualizer) with better internal architecture. Recognize applications, and from there parse "language", "project name", etc. Like what WakaTime does.
 * Finish visualizer UI. Use [other JS library](http://www.jsgraphs.com/) for better charts.
 * Better UI for the app window
 * Allow opening current log dir in context menu
 * Create analyzer
 * ignore private windows? http://stackoverflow.com/questions/14132142/using-c-sharp-to-close-google-chrome-incognito-windows-only
 * log URLs in case of browsers? http://stackoverflow.com/questions/18897070/getting-the-current-tabs-url-from-google-chrome-using-c-sharp, http://stackoverflow.com/questions/5317642/retrieve-current-url-from-c-sharp-windows-forms-application


Other info
----------

The project is a Visual Studio 2013 project. It's a single form project, so it should be fairly simple to change, customize and recompile.

The application is freeware. The source code is licensed under the [MIT](LICENSE.MD) license.
