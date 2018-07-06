Public Class clsAufgabenManager

   Public Delegate Function AufgabenDelegate(Parameterliste As Hashtable) As clsAufgabenErgebnis

   Private _Aufgabenliste As clsAufgabeListe

   Private Sub New()
      _Aufgabenliste = New clsAufgabeListe
   End Sub

   Public Shared Function getAufgabenManager() As clsAufgabenManager
      If __AufgabenManager Is Nothing Then
         __AufgabenManager = New clsAufgabenManager
      End If
      Return __AufgabenManager
   End Function

   Public Async Function starteAufgabe(aufzurufendeFunktion As AufgabenDelegate, Parameterliste As Hashtable) As Task(Of clsAufgabenErgebnis)
      Dim Ergebnis As clsAufgabenErgebnis = Nothing
      Try

         Dim Aufgabe As clsAufgabe
         Dim Name As String = aufzurufendeFunktion.Method.Name
         Debug.WriteLine($"Aufgabe {Name} gestartet. Aufgabenliste hat {_Aufgabenliste.Count.ToString} Elemente. TaskId = {Task.CurrentId}")

         ' prüfen, ob ein waitControl angezeigt werden soll
         Dim mainControl As Control = loadParameter("mainControl", Parameterliste)
         Dim waitControl As Control = Nothing
         If mainControl IsNot Nothing Then
            Dim waitControlText As String = loadParameter("waitControlText", Parameterliste)

            If waitControlText IsNot Nothing Then
               waitControl = waitControlEinrichten(Name, mainControl, waitControlText)
            End If
         End If

         ' prüfen, ob es schon eine Aufgabe mit diesem Namen gibt
         If _Aufgabenliste.ContainsKey(Name) Then
            ' bestehende Aufgabe abbrechen, bevor die Neue erzeugt wird

            Aufgabe = _Aufgabenliste.Item(Name)
            Aufgabe.Status = clsAufgabe.enumAufgabenstatus.abbrechen
            _Aufgabenliste.Remove(Name)
         End If

         ' neue Aufgabe erzeugen und der Aufgabenliste anfügen
         Aufgabe = New clsAufgabe(Name, Parameterliste)
         _Aufgabenliste.Add(Name, Aufgabe)

         ' und starten
         Debug.WriteLine($"Await für Aufgabe {Name} gestartet. TaskId = {Task.CurrentId}")
         Ergebnis = Await Task.FromResult(Of Object)(aufzurufendeFunktion.Invoke(Parameterliste))
         Debug.WriteLine($"Await für Aufgabe {Name} beendet. TaskId = {Task.CurrentId}")
         If Ergebnis.abgebrochen = False Then
            waitControlEntfernen(mainControl, waitControl)
            _Aufgabenliste.Remove(Name)
         End If

         Debug.WriteLine($"Aufgabe {Name} beendet. TaskId = {Task.CurrentId}")
      Catch ex As Exception
         Stop
      Finally
      End Try
      Return Ergebnis
   End Function

   Private Function loadParameter(Name As String, Parameterliste As Hashtable) As Object
      ' Zweck:    Aus der gegebenen Parameterliste den Parameterwert mit dem gegebenen Namen ermitteln
      Try
         If Parameterliste.ContainsKey(Name) Then
            Return Parameterliste.Item(Name)
         End If
      Catch ex As Exception
         Stop
      End Try
      Return Nothing
   End Function

   Public Function waitControlEinrichten(Name As String, mainControl As Control, waitControlText As String) As Control
      ' Zweck:    Wenn ein waitControl angezeigt werden soll, wird es hier erzeugt
      Dim waitControl As Label = Nothing
      Try
         If mainControl IsNot Nothing AndAlso waitControlText IsNot Nothing Then

            ' prüfen, ob es dieses waitcontrol schon gibt
            For Each C As Control In mainControl.Parent.Controls
               If C.Name = Name Then
                  ' wenn ja, wird es erst entfernt
                  mainControl.Invoke(Sub()
                                        mainControl.Parent.Controls.Remove(C)
                                        C.Dispose()
                                     End Sub)
                  Exit For
               End If
            Next

            ' mainControl ausblenden
            mainControl.Invoke(Sub()
                                  mainControl.Visible = False
                               End Sub)

            ' neues waitControl erzeugen
            waitControl = New Label
            With waitControl
               .Name = Name
               .Top = mainControl.Top
               .Left = mainControl.Left
               .Width = mainControl.Width
               .Height = mainControl.Height
               .TextAlign = ContentAlignment.MiddleCenter
               .Font = New Font("Arial", CSng(.Width / 20), FontStyle.Bold)

               ' Ladetext anzeigen, wenn das waitControl groß genug dafür ist
               If .Font.SizeInPoints >= 6 Then
                  .Text = waitControlText
               End If

            End With
            mainControl.Invoke(Sub()
                                  mainControl.Parent.Controls.Add(waitControl)
                               End Sub)
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
      Return waitControl
   End Function
   Public Sub waitControlEntfernen(mainControl As Control, waitControl As Control)
      ' Zweck:    Wenn ein waitControl vorhanden war, wird es wieder entfernt
      Try
         If mainControl IsNot Nothing Then
            mainControl.Invoke(Sub()
                                  mainControl.Visible = True
                               End Sub)
            If waitControl IsNot Nothing Then
               mainControl.Invoke(Sub()
                                     mainControl.Parent.Controls.Remove(waitControl)
                                     waitControl.Dispose()
                                  End Sub)
            End If
            Application.DoEvents()
         End If

      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class
