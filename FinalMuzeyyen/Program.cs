using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalMuzeyyen
{
    internal class Program
    {
        private static List<worker> workersList = new List<worker>();
        static void Main(string[] args)
        {
            
            worker worker2 = new worker("Muzeyyen", "HR", 4500);
            worker worker3 = new worker("John", "IT", 4000);
            worker worker4 = new worker("Jesica", "HR", 6500);
            worker worker5 = new worker("Maria", "HR", 2000);
            worker worker6 = new worker("Miles", "HR", 3500);
            worker worker7 = new worker("Kataryna", "IT", 4000);
            workersList.Add(worker2);
            workersList.Add(worker3);
            workersList.Add(worker4);
            workersList.Add(worker5);
            workersList.Add(worker6);
            workersList.Add(worker7);

            DisplayAllWorkers();
            DisplayHRWorkers();

            while (true)
            {
                PrintMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddWorker();
                        break;
                    case "2":
                        DisplayAllWorkers();
                        break;
                    case "3":
                        DisplayHRWorkers();
                        break;
                    case "4":
                        DisplayAverageSalary();
                        break;
                    case "5":
                        IncreaseSalaryHRDepartment();
                        break;
                    case "6":
                        FireWorkerFromDepartment();
                        break;
                    case "7":
                        ChangeDepartment();
                        break;
                    case "8":
                        AddManager();
                        break;
                    case "9":
                        DisplayManagers();
                        break;
                    case "10":
                        FireManager();
                        break;
                    case "11":
                        SaveHRWorkersToCsv();
                        break;
                    case"12":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine("\nPlease Select:");
            Console.WriteLine("1. Add Worker");
            Console.WriteLine("2. Print All Workers List");
            Console.WriteLine("3. Print HR Workers List");
            Console.WriteLine("4. Display Average Salary");
            Console.WriteLine("5. Increase Salary in HR Department");
            Console.WriteLine("6. Fire Someone from Your Department");
            Console.WriteLine("7. Change Department of a Worker");
            Console.WriteLine("8. Add Manager");
            Console.WriteLine("9. Print Manager List");
            Console.WriteLine("10. Fire Manager");
            Console.WriteLine("11. Save HR Workers to CSV File");
            Console.WriteLine("12. Exit");
        }

        public static void IncreaseSalaryHRDepartment()
        {
            double percentage=20;
            while (percentage < 0 || percentage > 100)
            {
                Console.WriteLine("Invalid input. Please enter a valid percentage between 0 and 100.");
                Console.Write("Percentage increase: ");
            }

            foreach (var worker in workersList)
            {
                if (worker.Department == "HR")
                {
                    worker.GetRise(percentage);
                }
            }

            Console.WriteLine($"Salary increased by {percentage}% for HR Department.");
        }

        public static void DisplayAverageSalary()
        { 

            double hrTotalSalary = workersList.Where(w => w.Department == "HR").Sum(w => w.Salary);
            int hrWorkerCount = workersList.Count(w => w.Department == "HR");
            double hrAverageSalary = hrTotalSalary / hrWorkerCount;

            Console.WriteLine($"Average Salary of HR Workers: {hrAverageSalary}");
        }

        public static void DisplayAllWorkers()
        {
            Console.WriteLine("\nAll Worker List:");
            for (int i = 0; i < workersList.Count; i++)
            {
                var worker = workersList[i];
                if (worker is Manager manager)
                {
                    Console.WriteLine($"[{i + 1}] Name: {manager.Name}, Department: {manager.Department}, Salary: {manager.Salary}, Bonus: {manager.Bonus}");
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] Name: {worker.Name}, Department: {worker.Department}, Salary: {worker.Salary}");
                }
            }
        }

        public static void DisplayHRWorkers()
        {
            Console.WriteLine("\nHR Worker List:");
            int count = 0;
            for (int i = 0; i < workersList.Count; i++)
            {
                var worker = workersList[i];
                if (worker.Department == "HR")
                {
                    count++;
                    Console.WriteLine($"[{count}] Name: {worker.Name}, Department: {worker.Department}, Salary: {worker.Salary}");
                }
            }
        }

        public static void AddWorker()
        {
            Console.WriteLine("\nEnter worker details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Salary: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            workersList.Add(new worker(name, "HR" , salary));
            Console.WriteLine("Worker added successfully.");
        }

        public static void ChangeDepartment()
        {
            Console.WriteLine("\nSelect the number of the worker whose department you want to change:");
            DisplayHRWorkers();

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > workersList.Count)
            {
                Console.WriteLine("Invalid input. Please enter a valid number of a worker from HR department.");
                Console.WriteLine("Select the number of the worker whose department you want to change:");
            }

            // HR departmanındaki çalışanları filtreleyelim
            var hrWorkers = workersList.Where(w => w.Department == "HR").ToList();

            // Seçilen indekse göre doğru çalışanı belirleyelim
            var workerToChange = hrWorkers[index - 1];

            Console.WriteLine($"Enter new department for {workerToChange.Name}:");
            string newDepartment = Console.ReadLine();

            // Yeni departmanı ayarlayalım
            workerToChange.ChangeDep(newDepartment);
            Console.WriteLine($"{workerToChange.Name}'s department has been changed to {workerToChange.Department}.");

            Console.WriteLine("\nUpdated Worker List:");
            DisplayAllWorkers();
        }

        public static void FireWorkerFromDepartment()
        {
            Console.WriteLine("\nSelect the number of the worker you want to fire from your department:");
            DisplayHRWorkers();

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > workersList.Count(w => w.Department == "HR"))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of a worker from HR department.");
                Console.WriteLine("Select the number of the worker you want to fire from your department:");
            }

            // HR departmanındaki çalışanları filtreleyelim
            var hrWorkers = workersList.Where(w => w.Department == "HR").ToList();

            // Seçilen indekse göre doğru çalışanı belirleyelim
            var workerToFire = hrWorkers[index - 1];

            workerToFire.BeFired(workersList);
            Console.WriteLine($"{workerToFire.Name} has been fired from HR department.");

            Console.WriteLine("\nUpdated Worker List:");
            DisplayHRWorkers();
        }


        public static void AddManager()
        {
            Console.WriteLine("\nEnter manager details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            

            Console.Write("Salary: ");
            double salary;
            while (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid salary.");
                Console.Write("Salary: ");
            }

            Console.Write("Bonus: ");
            double bonus;
            while (!double.TryParse(Console.ReadLine(), out bonus) || bonus < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid bonus.");
                Console.Write("Bonus: ");
            }

            Manager manager = new Manager(name, "HR", salary, bonus);
            workersList.Add(manager);
            Console.WriteLine("Manager added successfully.");

            Console.WriteLine("\nUpdated Manager List:");
            DisplayManagers();
        }
        public static void DisplayManagers()
        {
            Console.WriteLine("\nManager List:");
            int count = 0;
            foreach (var worker in workersList)
            {
                if (worker is Manager)
                {
                    count++;
                    Console.WriteLine($"[{count}] Name: {worker.Name}, Department: {worker.Department}, Salary: {worker.Salary}" + ", Bonus: " + ((Manager)worker).Bonus);
                }
            }
        }
        public static void FireManager()
        {
            Console.WriteLine("\nSelect the number of the manager you want to fire:");

            DisplayManagers(); // Yöneticileri listeleme

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > workersList.Count(w => w is Manager))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of a manager.");
                Console.WriteLine("Select the number of the manager you want to fire:");
            }

            var managerToFire = workersList.Where(w => w is Manager).ToList()[index - 1];

            // Manager sınıfındaki BeFired metodu çağrılacak
            managerToFire.BeFired(workersList);

            Console.WriteLine("\nUpdated Worker List:");
            DisplayManagers(); // Güncellenmiş çalışan listesini gösterme
        }

        public static void SaveHRWorkersToCsv()
        {
            var hrWorkers = workersList.Where(w => w.Department == "HR").ToList();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",", // CSV dosyasının ayraç karakteri
            };

            using (var writer = new StreamWriter("hr_workers.csv"))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(hrWorkers);
            }

            Console.WriteLine("HR workers have been saved to hr_workers.csv");
        }

    }
}
