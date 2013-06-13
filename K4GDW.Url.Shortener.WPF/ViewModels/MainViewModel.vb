Imports Caliburn.Micro
Imports System.ComponentModel.Composition
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Dynamic
Imports K4GDW.Infrastructure.Logging
Imports K4GDW.Url.Shortener.Messages


Namespace ViewModels

	<Export(GetType(MainViewModel))>
	Public Class MainViewModel
		Inherits PropertyChangedBase
		Implements IHandle(Of PreferencesSavedEvent)
		
        Private _longUrl As String
        Private _shortUrl As String
        Private _useBitly As Boolean = True
        Private _useIsgd As Boolean
        Private _useTinyUrl As Boolean
        Private _processing As Boolean = False

		Private ReadOnly _logger As ILogger

		Private ReadOnly _config As AppConfiguration

		Private ReadOnly _windowManager As IWindowManager

		Private ReadOnly _events As IEventAggregator

		<ImportingConstructor()>
		Public Sub New(logger As ILogger, config As AppConfiguration, windowManager As IWindowManager, events As IEventAggregator)
			_logger = logger
			_config = config
			AppTitle = GetTitle()
			_windowManager = windowManager
			_events = events
			_events.Subscribe(Me)
			Select Case _config.DefaultShortener
				Case Shortener.BitLy
					UseBitly = True
					UseIsgd = False
					UseTinyUrl = False
				Case Shortener.IsGd
					UseBitly = False
					UseIsgd = True
					UseTinyUrl = False
				Case Shortener.TinyUrl
					UseBitly = False
					UseIsgd = False
					UseTinyUrl = True
			End Select
		End Sub

		''' <summary>
		''' Gets the title.
		''' </summary>
		''' <returns>System.String.</returns>
		Private Function GetTitle() As String
			Return String.Format("K4GDW Url Shortener - {0}", FileVersionInfo.GetVersionInfo("K4GDW.Url.Shortener.WPF.exe").FileVersion)
		End Function

        Private _appTitle As String

		''' <summary>
		''' Gets or sets the AppTitle property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property AppTitle As String
			Get
                Return _appTitle
			End Get
			Private Set(ByVal value As String)
				If Not _AppTitle = value Then
					_AppTitle = value
					NotifyOfPropertyChange(Function() AppTitle)
				End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets the UseTinyUrl property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property UseTinyUrl As Boolean
			Get
                Return _useTinyUrl
			End Get
			Set(ByVal value As Boolean)
                If Not _useTinyUrl = value Then
                    _useTinyUrl = value
                    If value Then
                        SaveDefaultShortener(Shortener.TinyUrl)
                    End If
                    NotifyOfPropertyChange(Function() UseTinyUrl)
                End If
			End Set
		End Property

		''' <summary>
		''' Saves the default shortener.
		''' </summary>
		''' <param name="shortener">The shortener.</param>
		Private Sub SaveDefaultShortener(shortener As Shortener)
			_config.DefaultShortener = shortener
			_config.Write()
		End Sub

		''' <summary>
		''' Gets or sets the UseIsgd property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property UseIsgd As Boolean
			Get
                Return _useIsgd
			End Get
			Set(ByVal value As Boolean)
                If Not _useIsgd = value Then
                    _useIsgd = value
                    If value Then
                        SaveDefaultShortener(Shortener.IsGd)
                    End If
                    NotifyOfPropertyChange(Function() UseIsgd)
                End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets the UseBitly property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property UseBitly As Boolean
			Get
                Return _useBitly
			End Get
			Set(ByVal value As Boolean)
                If Not _useBitly = value Then
                    _useBitly = value
                    If value Then
                        SaveDefaultShortener(Shortener.BitLy)
                    End If
                    NotifyOfPropertyChange(Function() UseBitly)
                End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets the LongUrl property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property LongUrl As String
			Get
                Return _longUrl
			End Get
			Set(ByVal value As String)
                If Not _longUrl = value Then
                    _longUrl = value
                    NotifyOfPropertyChange(Function() LongUrl)
                    NotifyOfPropertyChange(Function() CanShortenUrl)
                End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets the ShortUrl property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property ShortUrl As String
			Get
                Return _shortUrl
			End Get
			Set(ByVal value As String)
                If Not _shortUrl = value Then
                    _shortUrl = value
                    NotifyOfPropertyChange(Function() ShortUrl)
                End If
			End Set
		End Property

		''' <summary>
		''' Gets or sets the Processing property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property Processing As Boolean
			Get
                Return _processing
			End Get
			Private Set(ByVal value As Boolean)
                If Not _processing = value Then
                    _processing = value
                    NotifyOfPropertyChange(Function() Processing)
                    NotifyOfPropertyChange(Function() CanShortenUrl)
                End If
			End Set
		End Property

		''' <summary>
		''' Gets a value indicating whether this instance can use bitly.
		''' </summary>
		''' <value><c>true</c> if this instance can use bitly; otherwise, <c>false</c>.</value>
		''' <remarks></remarks>
		Public ReadOnly Property CanUseBitly As Boolean
			Get
				Return BitLyCredsNotEmpty()
			End Get
		End Property


		''' <summary>
		''' Bits the ly creds not empty.
		''' </summary>
		''' <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
		Private Function BitLyCredsNotEmpty() As Boolean
			If Not String.IsNullOrEmpty(_config.BitLyLogin) AndAlso Not String.IsNullOrEmpty(_config.BitLyKey) Then
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Gets a value indicating whether this instance can shorten longUrl.
		''' </summary>
		''' <value><c>true</c> if this instance can shorten longUrl; otherwise, <c>false</c>.</value>
		''' <remarks></remarks>
		Public ReadOnly Property CanShortenUrl As Boolean
			Get
				Return (LongUrlIsValid() And Not Processing)
			End Get
		End Property

		''' <summary>
		''' Determines whether the user entered longUrl is in a valid format.
		''' </summary>
		''' <returns><c>true</c> if longUrl is valid; otherwise, <c>false</c>.</returns>
		Private Function LongUrlIsValid() As Boolean
			If Not String.IsNullOrEmpty(LongUrl) Then
				Return Regex.IsMatch(LongUrl,
									 My.Resources.UrlRegex)
			End If
			Return False
		End Function

		''' <summary>
		''' Shortens the longUrl using the shortening service the user selected.
		''' </summary>
		''' <exception cref="System.ApplicationException"></exception>
		Public Async Sub ShortenUrl()
			Dim url As String = LongUrl,
				selectedService As String = "Bit.Ly",
				sUrl As String = String.Empty
			Try
				PrependHttp(url)
				Processing = True
                If _useBitly Then
                    selectedService = "Bit.Ly"
                    sUrl = Await BitlyApi.ShortenUrlAsync(url, _config.BitLyKey, _config.BitLyLogin)
                ElseIf _useIsgd Then
                    selectedService = "Is.gd"
                    sUrl = Await IsGd.ShortenUrlAsync(url)
                ElseIf _useTinyUrl Then
                    selectedService = "TinyUrl"
                    sUrl = Await TinyUrl.ShortenUrlAsync(url)
                Else
                    Throw New ApplicationException(My.Resources.NoShortenerSelectedMessage)
                End If
				ShortUrl = sUrl
				CopyUrlToClipboard(sUrl, 1)
			Catch ex As WebException
				_logger.LogError(ex)
				MsgBox(String.Format(My.Resources.ServiceConnectionError, selectedService), vbOKOnly Or vbCritical, My.Resources.AppName)
			Catch ex As Exception
				_logger.LogFatal(ex.GetFormattedException(), ex)
				MsgBox(ex.Message, vbOKOnly Or vbCritical, My.Resources.AppName)
			Finally
				Processing = False
			End Try
		End Sub

		''' <summary>
		''' Prepends HTTP if the user didn't enter it
		''' </summary>
		''' <param name="url">The URL.</param>
		Private Shared Sub PrependHttp(ByRef url As String)
			If Not url.StartsWith("http") Then
				url = String.Format("http://{0}", url)
			End If
		End Sub

		''' <summary>
		''' Copies the URL to clipboard.
		''' </summary>
		''' <param name="url">The URL.</param>
		''' <param name="maxTries">The max tries.</param>
		Public Sub CopyUrlToClipboard(url As String, Optional maxTries As Int16 = 3)
			Dim success As Boolean = False
			Dim tries As Int16 = 0
			While Not success AndAlso tries < maxTries
				Try
					tries += 1
					Clipboard.SetText(url)
					success = True
				Catch ex As Exception
					_logger.LogInfo(ex)
					success = False
				End Try
			End While
		End Sub

		''' <summary>
		''' Closes the app.
		''' </summary>
		Public Sub CloseApp()
			Application.Current.Shutdown()
		End Sub

		Public Sub Clear()
			ShortUrl = String.Empty
			LongUrl = String.Empty
		End Sub

		''' <summary>
		''' Shows the preferences screen.
		''' </summary>
		Public Sub ShowPreferences()
			Dim settings As Object = New ExpandoObject
			settings.WindowStartupLocation = WindowStartupLocation.CenterScreen

			_windowManager.ShowDialog(New PreferencesViewModel(_logger, _config, _events), Nothing, settings)
		End Sub

		''' <summary>
		''' Shows the about screen.
		''' </summary>
		Public Sub ShowAbout()
			Dim settings As Object = New ExpandoObject
			settings.WindowStartupLocation = WindowStartupLocation.CenterScreen

			_windowManager.ShowDialog(New AboutViewModel(_events), Nothing, settings)
		End Sub

		Public Sub Handle(message As PreferencesSavedEvent) Implements IHandle(Of PreferencesSavedEvent).Handle
			_config.Read()
			NotifyOfPropertyChange(Function() CanUseBitly)
		End Sub

	End Class

End Namespace
