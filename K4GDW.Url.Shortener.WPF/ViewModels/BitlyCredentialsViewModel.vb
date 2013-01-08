Imports Caliburn.Micro
Imports System.ComponentModel.Composition
Imports K4GDW.Infrastructure.Logging


<Export(GetType(BitlyCredentialsViewModel))>
Public Class BitlyCredentialsViewModel
	Inherits PropertyChangedBase

	Private ReadOnly _logger As ILogger
	Private ReadOnly _config As AppConfiguration

	<ImportingConstructor()>
	Public Sub New(logger As ILogger, config As AppConfiguration)
		_logger = logger
		_config = config
	End Sub

	Private _BitLyApiKey As String

	''' <summary>
	''' Gets or sets the BitLyApiKey property and raises the PropertyChanged event.
	''' </summary>
	''' <remarks>
	''' <para>
	''' This template works with the Caliburn.Micro MVVM framework.
	''' </para>
	''' </remarks>
	Public Property BitLyApiKey As String
		Get
			Return _BitLyApiKey
		End Get
		Set(ByVal value As String)
			If Not _BitLyApiKey = value Then
				_BitLyApiKey = value
				NotifyOfPropertyChange(Function() BitLyApiKey)
			End If
		End Set
	End Property

	Private _BitLyApiLogin As String

	''' <summary>
	''' Gets or sets the BitLyApiLogin property and raises the PropertyChanged event.
	''' </summary>
	''' <remarks>
	''' <para>
	''' This template works with the Caliburn.Micro MVVM framework.
	''' </para>
	''' </remarks>
	Public Property BitLyApiLogin As String
		Get
			Return _BitLyApiLogin
		End Get
		Set(ByVal value As String)
			If Not _BitLyApiLogin = value Then
				_BitLyApiLogin = value
				NotifyOfPropertyChange(Function() BitLyApiLogin)
			End If
		End Set
	End Property

	Public ReadOnly Property CanSaveBitLyCredentials As Boolean
		Get
			Return BitLyCredsNotEmpty
		End Get
	End Property

	Private Function BitLyCredsNotEmpty() As Boolean
		If Not String.IsNullOrEmpty(_BitLyApiLogin) AndAlso Not String.IsNullOrEmpty(_BitLyApiKey) Then
			Return True
		End If
		Return False
	End Function
End Class