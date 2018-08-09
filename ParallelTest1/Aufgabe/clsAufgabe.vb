Public Class clsAufgabe

   Private _tokenSource As New Threading.CancellationTokenSource

   Public Enum enumAufgabenstatus
      erledigt = 0
      inArbeit = 1
      abbrechen = 2
      abgebrochen = 3
   End Enum

   Public Sub New(Aufgabenname As String, Aufgabeparameter As Hashtable)
      _Name = Aufgabenname
      _Parameterliste = Aufgabeparameter

      ' der Parameterliste immer ein CancelationToken anhängen, damit die Aufgabe abgebrochen werden kann
      _Parameterliste.Add("cancelationtokenSource", _tokenSource)
   End Sub

   Private _Name As String
   Public Property Name() As String
      Get
         Return _Name
      End Get
      Set(ByVal value As String)
         _Name = value
      End Set
   End Property

   Private _Parameterliste As Hashtable
   Public Property Parameterliste() As Hashtable
      Get
         Return _Parameterliste
      End Get
      Set(ByVal value As Hashtable)
         _Parameterliste = value
      End Set
   End Property

   Private _Prozess As Task
   Public Property Prozess() As Task
      Get
         Return _Prozess
      End Get
      Set(value As Task)
         _Prozess = value
      End Set
   End Property

   Private _Status As enumAufgabenstatus = enumAufgabenstatus.inArbeit
   Public Property Status() As enumAufgabenstatus
      Get
         Return _Status
      End Get
      Set(ByVal value As enumAufgabenstatus)
         If value = enumAufgabenstatus.abbrechen Then
            _tokenSource.Cancel()
         End If
         _Status = value
      End Set
   End Property

   'Public ReadOnly Property tokenSource() As Threading.CancellationTokenSource
   '   Get
   '      Return _tokenSource
   '   End Get
   'End Property

End Class
