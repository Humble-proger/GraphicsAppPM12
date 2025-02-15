using System;

public interface IPerson
{
    DateTime DateOfBirth { get; }
    int GetAge();
}

public class Person : IPerson
{
    public DateTime DateOfBirth { get; }

    public Person(DateTime dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public int GetAge()
    {
        return DateTime.Now.Year - DateOfBirth.Year;
    }
}

class Program
{
    static void Main()
    {
        Person person = new Person(new DateTime(1991, 12, 31));
        Console.WriteLine($"Age: {person.GetAge()} ans");
    }
    
}