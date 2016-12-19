Imports System.IO
Module Module1

    Dim Seniles As Decimal() = {0, 0, 0}
    Dim Adults As Decimal() = {0, 2, 1}
    Dim Juveniles As Decimal() = {0, 1, 1}
    Dim Disease As Boolean
    Dim DiseaseTrigerStart As Decimal = 200
    Dim DiseaseTrigerEnd As Decimal = 100
    Dim total As Decimal
    Dim generations As Decimal


    Sub Main()
        Console.WriteLine("Greenfly Population Simulator")
        Console.WriteLine("~~~~~MENU~~~~~")
        Console.WriteLine("")
        Console.WriteLine("1: Set the starting values ( Do this before running the simulation )")
        Console.WriteLine("2: Print the starting values")
        Console.WriteLine("3: Run the simulation")
        Console.WriteLine("4: Exit the program")
        Dim temp As String = Console.ReadLine()
        If temp = 1 Then
            Setvalues()
            Main()
        ElseIf temp = 2 Then
            Printvalues()
            Main()
        ElseIf temp = 3 Then
            If IsNothing(Seniles(0)) Then
                Console.WriteLine("Error: No values were found, Try setting the values at the menu.")
                Main()
            Else
                start(generations)
                Main()
            End If
        ElseIf temp = 4 Then
            Environment.Exit(0.0)
        End If

    End Sub
    Function Printvalues()
        Console.clear
        Console.WriteLine("Amount of Juviniles: " & Juveniles(0))
        Console.WriteLine("Amount of Adults: " & Adults(0))
        Console.WriteLine("Amount of Seniles: " & Seniles(0))
        Console.WriteLine("Adult Birthrate: " & Adults(1))
        Console.WriteLine("Juvinile Survival Rate: " & Juveniles(2))
        Console.WriteLine("Adult Survival Rate: " & Adults(2))
        Console.WriteLine("Senile Survival Rate: " & Seniles(2))
        Console.WriteLine("Total Generations:" & generations)

        Return True
    End Function
    Function Setvalues()
        Console.Clear()
        Console.WriteLine("Enter the starting population of the Juviniles")
        Juveniles(0) = Console.ReadLine()
        Console.WriteLine("Enter the starting population of the Adults")
        Adults(0) = Console.ReadLine()
        Console.WriteLine("Enter the starting population of the Seniles")
        Seniles(0) = Console.ReadLine()
        Console.WriteLine("Enter the Adult birthrate")
        Adults(1) = Console.ReadLine()
        Console.WriteLine("Enter the Juvinile survival rate")
        Juveniles(2) = Console.ReadLine()
        Console.WriteLine("Enter the Adults survival rate")
        Adults(2) = Console.ReadLine()
        Console.WriteLine("Enter the Senile survival rate")
        Seniles(2) = Console.ReadLine()
        Console.WriteLine("How many Generations?")
        generations = Console.ReadLine()
        Return True
    End Function
    Public Function start(generations As Integer)
        Randomize()
        Dim Values(generations - 1)() As Decimal
        For i = 1 To generations
            If total >= DiseaseTrigerStart And Disease = False Then
                Disease = True
            End If
            If total <= DiseaseTrigerEnd And Disease = True Then
                Disease = False
            End If
            Juveniles(0) *= Juveniles(2)
            Adults(0) *= Adults(2)
            Seniles(0) *= Seniles(2)
            If Disease Then
                Dim diseaseFactor As Decimal = (CInt(Math.Floor((5 - 2 + 1) * Rnd())) + 2) / 10
                Console.WriteLine(diseaseFactor)
                diseaseFactor = (1 - diseaseFactor)
                Juveniles(0) *= diseaseFactor
                Seniles(0) *= diseaseFactor
            End If
            Dim _Adults As Decimal = Adults(0)
            Seniles(0) += Adults(0) * Adults(2)
            Adults(0) = Juveniles(0)
            Juveniles(0) = _Adults * Adults(1)
            Seniles(0) = Math.Round(Seniles(0))
            Adults(0) = Math.Round(Adults(0))
            Juveniles(0) = Math.Round(Juveniles(0))
            Console.Write(Math.Round(Juveniles(0)) & " ")
            Console.Write(Math.Round(Adults(0)) & " ")
            Console.Write(Math.Round(Seniles(0)) & " ")
            Try
                total = Juveniles(0) + Adults(0) + Seniles(0)

            Catch OverflowException As Exception
                Dim temp3 As Boolean = False
                Dim savename2 As String = Nothing
                Console.WriteLine("Error: Total value too large to continue, Would you like to save the file? y/n")
                While temp3 = False
                    Dim temp As String = Console.ReadLine()
                    If temp = "y" Then
                        temp3 = True
                        Dim temp4 As Boolean = False

                        While temp4 = False

                            Console.WriteLine("Enter File Name:")
                            savename2 = Console.ReadLine()
                            savename2 = (savename2 & ".csv")
                            If File.Exists(savename2) Then
                                Console.WriteLine("File already Exists. Overwrite y/n?")
                                Dim temp5 As String = Console.ReadLine()
                                If temp5 = "y" Then
                                    temp4 = True
                                    Dim csv2 As String = Nothing
                                    csv2 = csv2 & "Juviniles, Adults, Seniles, Total" & Environment.NewLine
                                    For k = 1 To generations - 1
                                        csv2 = csv2 & String.Join(",", Values(k - 1)) & Environment.NewLine
                                    Next
                                    My.Computer.FileSystem.WriteAllText(savename2, csv2, False)
                                ElseIf temp5 = "n" Then
                                    temp4 = False
                                End If
                            Else
                                Dim csv1 As String = Nothing
                                csv1 = csv1 & "Juviniles, Adults, Seniles, Total" & Environment.NewLine
                                For k = 1 To generations - 1
                                    csv1 = csv1 & String.Join(",", Values(k - 1)) & Environment.NewLine
                                Next
                                My.Computer.FileSystem.WriteAllText(savename2, csv1, False)
                                temp4 = True
                            End If
                        End While
                    ElseIf temp = "n" Then
                        temp3 = True
                        Environment.Exit(0.1)
                    End If
                End While
            End Try
            Console.Write(Math.Round(total))
            Console.WriteLine()
            Values(i - 1) = New Decimal(3) {}
            Values(i - 1)(0) = Math.Round(Juveniles(0))
            Values(i - 1)(1) = Math.Round(Adults(0))
            Values(i - 1)(2) = Math.Round(Seniles(0))
            Values(i - 1)(3) = Math.Round(total)

        Next

        Dim savename As String = Nothing
        Dim temp2 As Boolean = False
        While temp2 = False

            Console.WriteLine("Enter File Name:")
            savename = Console.ReadLine()
            savename = (savename & ".csv")
            If File.Exists(savename) Then
                Console.WriteLine("File already Exists. Overwrite y/n?")
                Dim temp As String = Console.ReadLine()
                If temp = "y" Then
                    temp2 = True
                ElseIf temp = "n" Then
                    temp2 = False
                End If
            Else
                temp2 = True
            End If


        End While
        Dim csv As String = Nothing
        csv = csv & "Juviniles, Adults, Seniles, Total" & Environment.NewLine
        For i = 1 To generations
            csv = csv & String.Join(",", Values(i - 1)) & Environment.NewLine
        Next
        My.Computer.FileSystem.WriteAllText(savename, csv, False)

        Return True
    End Function

End Module
