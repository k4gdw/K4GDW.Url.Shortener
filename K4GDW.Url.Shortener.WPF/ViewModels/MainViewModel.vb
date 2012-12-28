Imports Caliburn.Micro
Imports System.ComponentModel.Composition
Imports System.Text.RegularExpressions
Imports System.Net
Imports K4GDW.Infrastructure.Logging
Imports System.Threading

Namespace ViewModels

	<Export(GetType(MainViewModel))>
	Public Class MainViewModel
		Inherits PropertyChangedBase

		Private _LongUrl As String
		Private _ShortUrl As String
		Private _UseBitly As Boolean = True
		Private _UseIsgd As Boolean
		Private _UseTinyUrl As Boolean
		Private _Processing As Boolean = False

		Private ReadOnly _logger As ILogger

		<ImportingConstructor()>
		Public Sub New(logger As ILogger)
			_logger = logger
		End Sub

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
				Return _UseTinyUrl
			End Get
			Set(ByVal value As Boolean)
				If Not _UseTinyUrl = value Then
					_UseTinyUrl = value
					NotifyOfPropertyChange(Function() UseTinyUrl)
				End If
			End Set
		End Property

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
				Return _UseIsgd
			End Get
			Set(ByVal value As Boolean)
				If Not _UseIsgd = value Then
					_UseIsgd = value
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
				Return _UseBitly
			End Get
			Set(ByVal value As Boolean)
				If Not _UseBitly = value Then
					_UseBitly = value
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
				Return _LongUrl
			End Get
			Set(ByVal value As String)
				If Not value.StartsWith("http") Then
					value = String.Format("http://{0}", value)
				End If
				If Not _LongUrl = value Then
					_LongUrl = value
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
				Return _ShortUrl
			End Get
			Set(ByVal value As String)
				If Not _ShortUrl = value Then
					_ShortUrl = value
					For i As Int16 = 1 To 10
						Try
							Clipboard.SetText(value)
							_logger.LogInfo(String.Format("It took {0} tries to insert ""{1}"" into the clipboard.", i, value))
							Exit For
						Catch ex As Exception
							_logger.LogError(String.Format("The following exception occurred when Clipboard.SetText(""{0}"") was called.{1}{2}", value, Environment.NewLine, ex.GetFormattedException()), ex)
							Thread.Sleep(10)
						End Try
					Next
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
				Return _Processing
			End Get
			Private Set(ByVal value As Boolean)
				If Not _Processing = value Then
					_Processing = value
					NotifyOfPropertyChange(Function() Processing)
					NotifyOfPropertyChange(Function() CanShortenUrl)
				End If
			End Set
		End Property
		
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
				selectedService As String = "Bit.Ly"
			Try
				Processing = True
				If _UseBitly Then
					selectedService = "Bit.Ly"
					ShortUrl = Await BitlyApi.ShortenUrlAsync(url)
				ElseIf _UseIsgd Then
					selectedService = "Is.gd"
					ShortUrl = Await IsGd.ShortenUrl(url)
				ElseIf _UseTinyUrl Then
					selectedService = "TinyUrl"
					ShortUrl = Await TinyUrl.ShortenUrlAsync(url)
				Else
					Throw New ApplicationException(My.Resources.NoShortenerSelectedMessage)
				End If
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

	End Class

End Namespace
