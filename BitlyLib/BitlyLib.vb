Imports System.Web
Imports System.Xml.Linq
Imports System.Threading.Tasks
Imports System.Net
Imports System.IO

''' <summary>
''' This class handles talking to the bit.ly longUrl shortening service.
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
	''' Gets the shortened url from Bit.ly asynchronously
	''' </summary>
	''' <param name="longUrl">The long longUrl.</param>
	''' <returns>Task{System.String}.</returns>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:18 PM
	''' </para>
	''' </remarks>
	Public Shared Async Function ShortenUrlAsync(ByVal longUrl As String) As Task(Of String)
		Try
			Dim url = String.Format("http://api.bit.ly/shorten?format=xml&version=2.0.1&longUrl={0}&login={1}&apiKey={2}",
			                        HttpUtility.UrlEncode(longUrl),
			                        login,
			                        apiKey)
			Dim req As WebRequest = WebRequest.Create(url)
			Using rsp As WebResponse = Await req.GetResponseAsync()
				Dim xdoc As XDocument
				Using rdr As New StreamReader(rsp.GetResponseStream())
					xdoc = XDocument.Parse(rdr.ReadToEnd())
				End Using
				Dim x = (From xd In xdoc.Descendants("nodeKeyVal")
					    Select New BitlyResults(xd.Descendants("hash").Value,
					                            xd.Descendants("shortUrl").Value))
				Return x.FirstOrDefault.ShortUrl
			End Using

		Catch ex As WebException
			Throw New WebException("There was an error communicating with the bit.ly service.", ex)
		Catch ex As Exception
			Throw New ApplicationException("There was an error getting the shortened longUrl.", ex)
		End Try

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
