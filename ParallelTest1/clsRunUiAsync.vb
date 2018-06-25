Imports System.Threading

Public Class clsRunUiAsync

   Public Shared Async Function Run(mainControl As Control, Aktion As Action, Optional showWaitControl As Boolean = True) As Task
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Infragistics.Win.Misc.UltraLabel = Nothing
         If showWaitControl = True Then
            waitControl = New Infragistics.Win.Misc.UltraLabel
         End If
         waitControlEinrichten(mainControl, waitControl)

         ' Task starten
         Await Task.Run(Aktion)
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Function
   Public Shared Async Function RunTest4(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken, Optional showWaitControl As Boolean = True) As Task
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Infragistics.Win.Misc.UltraLabel = Nothing
         If showWaitControl = True Then
            waitControl = New Infragistics.Win.Misc.UltraLabel
         End If
         waitControlEinrichten(mainControl, waitControl)

         ' Task starten
         Dim i As Integer = Await Task.Run(Function() Test4(Anzahl, cancelationtoken))
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Function
   Public Shared Async Function RunTest5(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken, Optional showWaitControl As Boolean = True) As Task
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Infragistics.Win.Misc.UltraLabel = Nothing
         If showWaitControl = True Then
            waitControl = New Infragistics.Win.Misc.UltraLabel
         End If
         waitControlEinrichten(mainControl, waitControl)

         ' Task starten
         Dim i As Integer = Await Task.Run(Function() Test5(Anzahl, waitControl, sc, cancelationtoken))
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Function

   Private Shared Sub waitControlEinrichten(mainControl As Control, waitControl As Infragistics.Win.Misc.UltraLabel)
      ' Zweck:    Wenn ein waitControl angezeigt werden soll, wird es hier erzeugt
      Try
         If waitControl IsNot Nothing Then
            mainControl.Visible = False
            With waitControl
               .Top = mainControl.Top
               .Left = mainControl.Left
               .Width = mainControl.Width
               .Height = mainControl.Height
               .Appearance.TextHAlign = Infragistics.Win.HAlign.Center
               .Appearance.TextVAlign = Infragistics.Win.VAlign.Middle
               .Appearance.FontData.SizeInPoints = CSng(.Width / 20)
               .Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True

               ' Ladetext anzeigen, wenn das waitControl groß genug dafür ist
               If .Appearance.FontData.SizeInPoints >= 6 Then
                  .Text = "wird geladen"
               End If

            End With
            mainControl.Parent.Controls.Add(waitControl)
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Private Shared Sub fertig(mainControl As Control, waitControl As Control)
      ' Zweck:    Wenn ein waitControl vorhanden war, wird es wieder entfernt
      Try
         If waitControl IsNot Nothing Then
            mainControl.Visible = True
            mainControl.Parent.Controls.Remove(waitControl)
            waitControl.Dispose()
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class
