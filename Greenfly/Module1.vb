Module Module1

    Dim Seniles As Decimal() = {0, 0, 0}
    Dim Adults As Decimal() = {0, 2, 1}
    Dim Juveniles As Decimal() = {0, 1, 1}

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
        Dim disease As Decimal = Console.ReadLine()
        Console.WriteLine("How many Generations?")
        generations = Console.ReadLine()
        start(generations, disease)

    End Sub
    Function start(generations As Integer, disease As Decimal)

        For i = 1 To generations
            Juveniles(0) *= Juveniles(2)
            Adults(0) *= Adults(2)
            Seniles(0) *= Seniles(2)

            Dim _Adults As Decimal = Adults(0)
            Seniles(0) += Adults(0) * Adults(2)
            Adults(0) = Juveniles(0)
            Juveniles(0) = _Adults * Adults(1)
            Console.Write(Juveniles(0) & " ")
            Console.Write(Adults(0) & " ")
            Console.Write(Seniles(0) & " ")
            Dim total As Integer = Juveniles(0) + Adults(0) + Seniles(0)
            If total > 100 a Then

            End If
            Console.Write(total)
            Console.WriteLine()
        Next
        Console.ReadLine()
        Return True
    End Function

End Module
