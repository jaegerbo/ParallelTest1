Imports System.Threading

Module LongRunner

   Public Function Test(Anzahl As Integer,
                        cancelationtoken As Threading.CancellationToken,
                        Progress As IProgress(Of Integer)) As Integer
      Dim i As Integer
      Try
         For i = 0 To Anzahl - 1
            Threading.Thread.Sleep(1000)

            ' prüfen, ob abgebrochen werden soll
            If cancelationtoken.IsCancellationRequested Then Exit For

            ' Fortschritt dokumentieren
            If Progress IsNot Nothing Then
               Progress.Report((i + 1) * 20)
            End If

         Next
      Catch ex As Exception
         Stop
      End Try
      Return i
   End Function
   Public Async Sub TestAsync(Anzahl As Integer,
                               cancelationtoken As Threading.CancellationToken,
                               Progress As IProgress(Of Integer),
                               mainControl As Control,
                               Optional waittext As String = Nothing)

      Dim sc As SynchronizationContext = SynchronizationContext.Current

      ' waitControl einrichten
      Dim waitControl As Label = clsAsync.waitControlEinrichten(mainControl, waittext)

      Dim i As Integer
      i = Await Task.Run(Function()
                            Return Test(Anzahl, cancelationtoken, Progress)
                         End Function)

      ' waitControl wieder entfernen, und das mainControl mit den neuen Werten versehen
      sc.Post(New SendOrPostCallback(Sub()
                                        clsAsync.waitControlEntfernen(mainControl, waitControl)
                                        mainControl.Text = i.ToString
                                     End Sub), Nothing)
   End Sub


   Public Function Test6(Parameterliste As Hashtable) As clsAufgabenErgebnis
      Dim Ergebnis As New clsAufgabenErgebnis
      Try
         Debug.WriteLine($"Test6 gestartet um {Now.ToLongTimeString}")

         ' Anzahl ermitteln
         Dim Anzahl As Integer = 0
         If Parameterliste.ContainsKey("Anzahl") Then
            Anzahl = CInt(Parameterliste.Item("Anzahl"))
         End If

         ' Cancelationtoken ermitteln
         Dim cancelationtokenSource As Threading.CancellationTokenSource = Nothing
         If Parameterliste.ContainsKey("cancelationtokenSource") Then
            cancelationtokenSource = CType(Parameterliste.Item("cancelationtokenSource"), Threading.CancellationTokenSource)
         End If

         ' Aufgabe durchführen
         Dim i As Integer
         For i = 0 To Anzahl - 1
            Threading.Thread.Sleep(1000)

            ' prüfen, ob abgebrochen werden soll
            If cancelationtokenSource.IsCancellationRequested Then
               Debug.WriteLine($"Test6 abgebrochen um {Now.ToLongTimeString} mit i = {i.ToString }")
               Ergebnis.abgebrochen = True
               Exit For
            End If

            Application.DoEvents()
         Next

         ' Erfolgreich beenden
         Ergebnis.OK = True
         Ergebnis.WertObjekt = i

         Debug.WriteLine($"Test6 beendet um {Now.ToLongTimeString} mit i = {i.ToString }")
      Catch ex As Exception
         Stop
         Ergebnis.Fehler = ex.Message
      End Try
      Return Ergebnis
   End Function

End Module
