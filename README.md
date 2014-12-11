app-application-logger
======================

A small standalone Windows application to log the applications one is using.

I created this because I wanted to have a log of all the applications I've been using, and what file they had open.

The result is a raw, tab-separated log file. This can be used for usage statistics; in my case, I wanted to know which programming platforms I've been working on over time, to generate better statistics for my own language usage charts.

Usage
-----

Run Application Logger. Write the name of the file you want it to save to in the field provided. Let it run in the background; it will poll the system every 500ms for the current running application, and write it to the log file if it's different.

It assumes the user is away if no input is performed after 15 minutes.

Other info
----------

The project is a Visual Studio 2013 project. It's a single form project, so it should be fairly simple to change, customize and recompile.

This project is licensed under the [WTFPL](http://en.wikipedia.org/wiki/WTFPL) license.
