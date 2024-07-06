using System.Collections.Generic;
using System;

public class Manager : worker
{
    public double Bonus { get; set; }

    public Manager(string name, string department, double salary, double bonus)
        : base(name, department, salary)
    {
        Bonus = bonus;
    }

    public double GetTotalSalary()
    {
        return Salary + Bonus;
    }

    public override void BeFired(List<worker> workersList)
    {
        Console.WriteLine($"Are you sure you want to fire {Name}? (YES/NO): ");
        string answer = Console.ReadLine().ToUpper();

        if (answer == "YES")
        {
            workersList.Remove(this);
            Console.WriteLine($"{Name} has been fired as a manager.");
        }
        else
        {
            Console.WriteLine($"{Name} was not fired.");
        }
    }

    public override string ToString()
    {
        return $"Name: {Name}, Department: {Department}, Salary: {Salary}, Bonus: {Bonus}";
    }
}
