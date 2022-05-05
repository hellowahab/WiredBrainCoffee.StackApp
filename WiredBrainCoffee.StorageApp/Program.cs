using System;
using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;

namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext());
            employeeRepository._itemAdded += EmployeeRepository_ItemAdded;

            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            GetEmployeeById(employeeRepository);
            WriteAllToConsole(employeeRepository);

            var organizationRepository = new ListRepository<Organization>();
            AddOrganizations(organizationRepository);
            WriteAllToConsole(organizationRepository);

            Console.ReadLine();

        }

        private static void EmployeeRepository_ItemAdded(object? sender, Employee e)
        {
            Console.WriteLine($"Employee added => {e.FirstName}");
        }
         
        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            var hussainManager = new Manager { FirstName = "Hussain" };
            var hussainManagerCopy = hussainManager.Copy();
            managerRepository.Add(hussainManager);

            if (hussainManagerCopy is not null)
            {
                hussainManagerCopy.FirstName += "_Copy";
                managerRepository.Add(hussainManagerCopy);
            }

            managerRepository.Add(new Manager {FirstName = "Wahab" });
            managerRepository.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with Id 2: {employee.FirstName}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Employee { FirstName = "Wahab" },
                new Employee { FirstName = "Hussain" },
                new Employee { FirstName = "Salar" }
            };
            employeeRepository.AddBatch(employees);
        }

        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {
                new Organization { Name = "Wahab" },
                new Organization { Name = "Hussain" },
                new Organization { Name = "Salar" }
            };
            organizationRepository.AddBatch(organizations);
        }

       
    }
}
