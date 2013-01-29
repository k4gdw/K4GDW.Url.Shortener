Imports System.Globalization
Imports System.Windows.Markup


Class Application
	' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
	' can be handled in this file.

	Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
		FrameworkElement.LanguageProperty.OverrideMetadata(GetType(FrameworkElement),
		                                                   New FrameworkPropertyMetadata(
			                                                   XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)))
	End Sub
End Class
