Imports System.IO
Module Module1

    Dim Seniles As Decimal() = {0, 0, 0}
    Dim Adults As Decimal() = {0, 2, 1}
    Dim Juveniles As Decimal() = {0, 1, 1}
    Dim Disease As Boolean
    Dim DiseaseTrigerStart As Decimal = 100
    Dim DiseaseTrigerEnd As Decimal = 50
    Dim total As Integer

    Sub Main()
        Dim generations As Decimal
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
        Console.WriteLine("Enter the disease rate")
        Console.WriteLine("How many Generations?")
        generations = Console.ReadLine()
        start(generations)

    End Sub
    Function start(generations As Integer)
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
                Dim diseaseFactor As Decimal = CInt(Math.Floor((0.5 - 0.2 + 1) * Rnd())) + 0.2
                Juveniles(0) *= diseaseFactor
                Seniles(0) *= diseaseFactor
            End If
            Dim _Adults As Decimal = Adults(0)
            Seniles(0) += Adults(0) * Adults(2)
            Adults(0) = Juveniles(0)
            Juveniles(0) = _Adults * Adults(1)
            Console.Write(Math.Round(Juveniles(0)) & " ")
            Console.Write(Math.Round(Adults(0)) & " ")
            Console.Write(Math.Round(Seniles(0)) & " ")
            total = Juveniles(0) + Adults(0) + Seniles(0)
            Console.Write(Math.Round(total))
            Console.WriteLine()
            Values(i - 1) = New Decimal(3) {}
            Values(i - 1)(0) = Math.Round(Juveniles(0))
            Values(i - 1)(1) = Math.Round(Adults(0))
            Values(i - 1)(2) = Math.Round(Seniles(0))
            Values(i - 1)(3) = Math.Round(total)

        Next
        Console.WriteLine("Enter Name of File to be Saved: ")
        Dim savename As String = Console.ReadLine()
        If File.Exists(savename & ".csv") Then
            Console.WriteLine("File already exists, Would you like to overwrite it? y/n")
            Dim temp As String = Console.ReadLine()
            If temp = "n" Then
                Return True
            End If
        End If
        Dim csv As String = Nothing
        For i = 1 To generations

            csv = csv & String.Join(",", Values(i - 1)) & Environment.NewLine

            Console.WriteLine(csv)


        Next
        My.Computer.FileSystem.WriteAllText(savename & ".csv", csv, False)

        Return True
    End Function

End Module
