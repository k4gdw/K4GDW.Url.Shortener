Imports System.Web
Imports System.Xml.Linq

''' <summary>
''' This class handles talking to the bit.ly URL shortening service.
''' </summary>
''' <remarks>
''' <para>
''' Created By:  Bryan Johns<br />
''' On:  3/18/2010 at 3:20 PM
''' </para>
''' </remarks>
''' 
Public Class BitlyApi

	Private Const apiKey As String = "R_59bbcc38b6cbca42297a62957de321b8 "
	Private Const login As String = "k4gdw"

	''' <summary>
	''' Shortens the URL.
	''' </summary>
	''' <param name="longUrl">The long URL.</param>
	''' <returns></returns>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:18 PM
	''' </para>
	''' </remarks>
	Public Shared Function ShortenUrl(ByVal longUrl As String) As BitlyResults
		Dim url = String.Format("http://api.bit.ly/shorten?format=xml&version=2.0.1&longUrl={0}&login={1}&apiKey={2}", HttpUtility.UrlEncode(longUrl), login, apiKey)
		Dim resultXml As XDocument
		resultXml = XDocument.Load(url)
		Dim x = (From result In resultXml.Descendants("nodeKeyVal") _
		  Select New BitlyResults(result.Descendants("hash").Value.ToString, result.Descendants("shortUrl").Value.ToString))
        Return x.FirstOrDefault
    End Function

End Class

''' <summary>
''' A class to encapsulate the data returned by the Bit.ly service.
''' </summary>
''' <remarks>
''' <para>
''' Created By:  Bryan Johns<br />
''' On:  3/18/2010 at 3:19 PM
''' </para>
''' </remarks>
Public Class BitlyResults

	''' <summary>
	''' Initializes a new instance of the <see cref="BitlyResults" /> class.
	''' </summary>
	''' <param name="hash">The hash.</param>
	''' <param name="url">The URL.</param>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:19 PM
	''' </para>
	''' </remarks>
	Public Sub New(ByVal hash As String, ByVal url As String)
		_UserHash = hash
		_ShortUrl = url
	End Sub

	Private _UserHash As String
	Private _ShortUrl As String

	''' <summary>
	''' Gets or sets the user hash.
	''' </summary>
	''' <value>The user hash.</value>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:19 PM
	''' </para>
	''' </remarks>
	Public Property UserHash() As String
		Get
			Return _UserHash
		End Get
		Set(ByVal value As String)
			_UserHash = value
		End Set
	End Property

	''' <summary>
	''' Gets or sets the short URL.
	''' </summary>
	''' <value>The short URL.</value>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:20 PM
	''' </para>
	''' </remarks>
	Public Property ShortUrl() As String
		Get
			Return _ShortUrl
		End Get
		Set(ByVal value As String)
			_ShortUrl = value
		End Set
	End Property

End Class
