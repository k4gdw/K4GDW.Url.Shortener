Public Class AppConfiguration
	Inherits Westwind.Utilities.Configuration.AppConfiguration

	Public Sub New()
		_DefaultShortener = Shortener.IsGd
	End Sub


	Private _DefaultShortener As Shortener

	Public Property DefaultShortener As Shortener
		Get
			Return _DefaultShortener
		End Get
		Set(value As Shortener)
			_DefaultShortener = value
		End Set
	End Property

	Private _BitLyKey As String

	Public Property BitLyKey As String
		Get
			Return _BitLyKey
		End Get
		Set(ByVal value As String)
			_BitLyKey = value
		End Set
	End Property

	Private _BitLyLogin As String

	Public Property BitLyLogin As String
		Get
			Return _BitLyLogin
		End Get
		Set(ByVal value As String)
			_BitLyLogin = value
		End Set
	End Property
	
End Class

Public Enum Shortener As Integer
	BitLy = 1
	IsGd = 2
	TinyUrl = 3
End Enum