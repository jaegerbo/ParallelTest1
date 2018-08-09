Public Class clsAufgabenErgebnis

   Private _Fehlerliste As New ArrayList
   Private _maximaleFehleranzahl As Integer = 10

   Public Sub New()
      _Uhr.Start()
   End Sub

   Private _Ok As Boolean = False
   Public Property OK() As Boolean
      Get
         Return _Ok
      End Get
      Set(ByVal value As Boolean)
         _Ok = value
      End Set
   End Property

   Private _abgebrochen As Boolean = False
   Public Property abgebrochen() As Boolean
      Get
         Return _abgebrochen
      End Get
      Set(ByVal value As Boolean)
         _abgebrochen = value
      End Set
   End Property

   Private _Wert As String = Nothing
   Public Property Wert() As String
      Get
         Return _Wert
      End Get
      Set(ByVal value As String)
         _Wert = value
      End Set
   End Property

   Private _WertDecimal As Decimal
   Public Property WertDecimal() As Decimal
      Get
         Return _WertDecimal
      End Get
      Set(ByVal value As Decimal)
         _WertDecimal = value
      End Set
   End Property

   Private _WertDouble As Double
   Public Property Wertdouble() As Double
      Get
         Return _WertDouble
      End Get
      Set(ByVal value As Double)
         _WertDouble = value
      End Set
   End Property

   Private _WertObjekt As Object
   Public Property WertObjekt() As Object
      Get
         Return _WertObjekt
      End Get
      Set(ByVal value As Object)
         _WertObjekt = value
      End Set
   End Property

   Public WriteOnly Property Except() As Exception
      Set(ByVal value As Exception)
         Fehler = value.Message & vbNewLine & value.StackTrace
      End Set
   End Property

   Private _Fehler As String = Nothing
   Public Property Fehler() As String
      Get
         Dim Text As String = Nothing
         For Each F As String In _Fehlerliste
            If Text Is Nothing Then
               Text = F
            Else
               Text &= vbNewLine & F
            End If
         Next
         Return Text
      End Get
      Set(ByVal value As String)
         If _Fehleranzahl < _maximaleFehleranzahl Then
            _Fehlerliste.Add(value)
            _Fehleranzahl += 1
         Else
            _Fehlerliste.Add("weitere Fehler vorhanden")
         End If
         _Ok = False
      End Set
   End Property

   Private _Fehleranzahl As Integer = 0
   Public ReadOnly Property Fehleranzahl() As Integer
      Get
         Return _Fehleranzahl
      End Get
   End Property

   Private _Bemerkung As String
   Public Property Bemerkung() As String
      Get
         Return _Bemerkung
      End Get
      Set(ByVal value As String)
         _Bemerkung = value
      End Set
   End Property

   Private _Eigenschaften As New Hashtable
   Public ReadOnly Property Eigenschaften() As Hashtable
      Get
         Return _Eigenschaften
      End Get
   End Property

   Private _Uhr As New Stopwatch
   Public Property Uhr() As Stopwatch
      Get
         Return _Uhr
      End Get
      Set(ByVal value As Stopwatch)
         _Uhr = value
      End Set
   End Property

   Private _Dauer As Long
   Public ReadOnly Property Dauer() As Long
      Get
         If _Uhr IsNot Nothing Then
            _Uhr.Stop()
            Return _Uhr.ElapsedMilliseconds
         End If
         Return _Dauer
      End Get
   End Property

   Public Overrides Function ToString() As String
      Dim Text As String = _Ok.ToString
      If Fehler IsNot Nothing Then
         Text &= "     " & Fehler
      End If
      Return Text
   End Function

End Class
