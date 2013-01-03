Thank you for trying out the K4GDW URL Shortener.

To install, simply unzip the contents of this zip file into a folder of your choice, then create a shortcut to it on your desktop or wherever else you want it.

To use it, simply paste the long URL you wish shortened into the text box and click the "Get Shortened Url" button.  The app currently supports bit.ly and tinyurl.com and defaults to bit.ly.  If you wish to use one of the other services, just tick the appropriate radio button.

The shortened URL will be automatically copied into your windows clipboard.

Future Plans:

5.x		-	Create a Windows 8 Store version that will run on Windows RT, Windows Phone 7.x/8, and Windows 8 Pro and be installed from the app store.

4.1.x	-	Provide a way for the user to change the default shortening engine.

Versions:

* 4.0.1	-	Fixed a bug that caused "http://" to be prepended to the long url while the user is typing.  Now, it is only prepended when the user clicks the Get Shortened Url button.

4.0.0	-	Upgraded to Visual Studio 2012 and .Net Framework 4.5.
			Changed the individual buttons for the different shortening engines to radio buttons.
			Set the default shortening engine to bit.ly as it seems to be the most reliable.
			Is.gd will generate slightly shorter urls but it doesn't always respond to requests.

3.0.0	-	Upgraded project to Visual Studio 2010 and .Net 4.0 framework.
			
2.0.0	-	Added support for the is.gd service.
				
1.2.0 	-	This is a minor change that should preempt further bugs in the URL validation.  Instead of trying to fix an invalid URL, it now leaves the buttons disabled until a valid URL is entered.

1.1.1	-	Fixed a bug in the URL validation function that caused some URLs to get http:// inserted when it shouldn't.

1.1.0 	-	Added URL validation to make sure the URL is valid before calling the bit.ly or tinyurl.com api.

1.0.0	-	Initial release.

* = Current Version