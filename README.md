#K4GDW Url Shortener

Thank you for trying out the K4GDW URL Shortener.  I sincerely hope you find it useful.

--  
vy73 - K4GDW  
bjohns@greendragonweb.com

##Installation:

To install, simply unzip the contents of this zip file into a folder of your choice, then create a shortcut to it on your desktop or wherever else you want it.

To use it, simply paste the long URL you wish shortened into the text box and click the "Get Shortened Url" button.  The app currently supports bit.ly and tinyurl.com and defaults to bit.ly.  If you wish to use one of the other services, just tick the appropriate radio button.

The shortened URL will be automatically copied into your windows clipboard.

##Future Plans:

* 5.x -	Create a Windows 8 Store version that will run on Windows RT, Windows Phone 7.x/8, and Windows 8 Pro and be installed from the app store.

* 4.2.x - Change where the xml configuration file is stored so that it gets along with Windows security not liking to have editable files under Program Files.  This will be especially important once I create an installer for the app and create a Windows Store version.  Add the ability to let the user enter their bit.ly account credentials which will be stored encrypted into the xml file.  I also plan to include a WiX installer and create a TeamCity project.

##Change History:
* 4.2.0 * - 
* 4.1.1 - I found a bug where the Is.Gd service didn't work unless the url was urlencoded.  Modified the is.gd library to make sure the url was properly encoded.

* 4.1.0 - The app will remember the last shortening service used, which is stored in an xml file, which is, in turn, stored in the same directory as the application executable.

* 4.0.2 - Convert README.txt to README.md for nicer formating when I end up moving the project to github and move it out to the solution root folder so github will pick it up.  Edit the build script so that it knows where to find it.

* 4.0.1	- Fixed a bug that caused "http://" to be prepended to the long url while the user is typing.  Now, it is only prepended when the user clicks the Get Shortened Url button.

* 4.0.0 - Upgraded to Visual Studio 2012 and .Net Framework 4.5.  Changed the individual buttons for the different shortening engines to radio buttons.  Set the default shortening engine to bit.ly as it seems to be the most reliable.  Is.gd will generate slightly shorter urls but it doesn't always respond to requests.

* 3.0.0	- Upgraded project to Visual Studio 2010 and .Net 4.0 framework.

* 2.0.0	- Added support for the is.gd service.

* 1.2.0 - This is a minor change that should preempt further bugs in the URL validation.  Instead of trying to fix an invalid URL, it now leaves the buttons disabled until a valid URL is entered.

* 1.1.1	- Fixed a bug in the URL validation function that caused some URLs to get http:// inserted when it shouldn't.

* 1.1.0 - Added URL validation to make sure the URL is valid before calling the bit.ly or tinyurl.com api.

* 1.0.0	- Initial release.

\* = Current Version
