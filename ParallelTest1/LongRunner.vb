Module LongRunner


   Public Sub Test1(DauerInSekunden As Integer)
      Try
         Threading.Thread.Sleep(DauerInSekunden * 1000)
         Application.DoEvents()
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Function Test3(Anzahl As Integer) As Integer
      Dim i As Integer
      Try
         Debug.WriteLine($"Test4 gestartet um {Now.ToLongTimeString}")
         For i = 0 To Anzahl - 1
            Threading.Thread.Sleep(1000)

            Application.DoEvents()
         Next
      Catch ex As Exception
         Stop
      End Try
      Debug.WriteLine($"Test4 beendet um {Now.ToLongTimeString} mit i = {i.ToString }")
      Return i
   End Function
   Public Function Test4(Anzahl As Integer, cancelationtoken As Threading.CancellationToken) As Integer
      Dim i As Integer
      Try
         Debug.WriteLine($"Test4 gestartet um {Now.ToLongTimeString}")
         For i = 0 To Anzahl - 1
            Threading.Thread.Sleep(1000)

            ' prüfen, ob abgebrochen werden soll
            If cancelationtoken.IsCancellationRequested Then
               Debug.WriteLine($"Test4 abgebrochen um {Now.ToLongTimeString} mit i = {i.ToString }")
               Exit For
            End If

            Application.DoEvents()
         Next
      Catch ex As Exception
         Stop
      End Try
      Debug.WriteLine($"Test4 beendet um {Now.ToLongTimeString} mit i = {i.ToString }")
      Return i
   End Function
   Public Function Test5(Anzahl As Integer, control As Control, sc As Threading.SynchronizationContext, cancelationtoken As Threading.CancellationToken) As Integer
      Dim i As Integer
      Try
         Dim timeNow As DateTime = DateTime.Now

         For i = 0 To Anzahl - 1

            ' prüfen, ob abgebrochen werden soll
            If cancelationtoken.IsCancellationRequested Then
               Exit For
            End If

            ' alle 50 Millisekunden den Bildschirm aktualisieren
            If (DateTime.Now - timeNow).Milliseconds >= 50 Then
               sc.Post(New Threading.SendOrPostCallback(Sub()
                                                           control.Text = i.ToString
                                                        End Sub), Nothing)
               timeNow = DateTime.Now
            End If

            Application.DoEvents()
         Next
      Catch ex As Exception
         Stop
      End Try
      Return i
   End Function
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
