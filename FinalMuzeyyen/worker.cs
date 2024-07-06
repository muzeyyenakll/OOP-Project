using System.Collections.Generic;

public class worker
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public string Department { get; set; }

    public worker(string name, string department, double salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    }

    public void GetRise(double percentage)
    {
        double raise = (Salary * percentage) / 100;
        Salary += raise;
    }

    public void ChangeDep(string newDepartment)
    {
        Department = newDepartment;
    }

    public virtual void BeFired(List<worker> workersList)
    {
        workersList.Remove(this);
    }
}
