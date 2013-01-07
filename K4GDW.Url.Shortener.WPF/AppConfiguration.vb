Public Class AppConfiguration
	Inherits Westwind.Utilities.Configuration.AppConfiguration

	Public Sub New()

	End Sub


	Private _DefaultShortener As Shortener = Shortener.BitLy

	Public Property DefaultShortener As Shortener
		Get
			Return _DefaultShortener
		End Get
		Set(value As Shortener)
			_DefaultShortener = value
		End Set
	End Property


End Class

Public Enum Shortener As Integer
	BitLy = 1
	IsGd = 2
	TinyUrl = 3
End Enum