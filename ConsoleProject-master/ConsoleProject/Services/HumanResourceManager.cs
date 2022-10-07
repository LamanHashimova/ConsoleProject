using ConsoleProject.Interfaces;
using ConsoleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleProject.Services
{
    class HumanResourceManager : IHumanResourceManager
    {
        public Department[] _departments;
        public Employee[] _employees;
        public Employee[] _no;
        public HumanResourceManager()
        {
            Department ComputerService = new Department("computerservice", 5, 350);
            Department Accounting = new Department("accounting", 19, 300);
            Department Manufacturing = new Department("manufacturing", 11, 450);
            Department Personnel = new Department("personnel", 18, 250);
            Department Administration = new Department("administration", 3, 800);

            _departments = new Department[5];

            _departments[0] = ComputerService;
            _departments[1] = Accounting;
            _departments[2] = Manufacturing;
            _departments[3] = Personnel;
            _departments[4] = Administration;

        }
        public Department[] Departments => _departments;

        public void AddDepartment(string name, int workerlimit, int salarylimit)
        {
            Department department = new Department(name, workerlimit, salarylimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;

        }

        public void AddEmployee(string fullname, string position, int salary, string departmentname)
        {
            Employee employee = new Employee(fullname,position,salary,departmentname);
            bool check = false;
            foreach (Department department in _departments)
            {
                if (departmentname.ToUpper()==department.Name.ToUpper())
                {
                    Array.Resize(ref department.Employees, department.Employees.Length + 1);
                    department.Employees[department.Employees.Length - 1] = employee;
                    check = true;
                    break;
                }

            }
            if (check==false)
            {
                Console.WriteLine("bu adda department yoxdur");
            }
        }


        public void EditDepartments(string namedepartment, string newnamedepartment)
        {
            foreach (Department department in _departments)
            {
                if (department.Name.ToLower() == namedepartment.ToLower())
                {
                    department.Name = newnamedepartment;
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            employee.DepartmentName = newnamedepartment;

                            employee.No = $"{newnamedepartment.ToUpper().Substring(0, 2)}{_no}";


                        }
                    }
                    break;
                }
            }
        }

        public void EditEmployee(string no, string fullname, string newposition, int newsalary)
        {

            foreach (Department department in _departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        if ((employee.No.ToUpper() == no.ToUpper()) && (employee.FullName.ToLower() == fullname.ToLower()))
                        {
                            if (newposition != null)
                            {
                                employee.Position = newposition;
                            }

                            if (newsalary != 0 && newsalary >= 250)
                            {
                                employee.Salary = newsalary;
                            }
                            break;
                        }
                    }
                }
            }
        }
        public Department[] GetDepartments()
        {
            return _departments;
            
        }

        public void RemoveEmployee(string departmentname, string no)
        {
            foreach (Department department in Departments)
            {
                for (int i = 0; i < department.Employees.Length; i++)
                {
                   if (department.Employees[i].No.ToUpper() == no.ToUpper())
                   {
                            department.Employees[i] = null;
                            break;
                   }
                }
            }
        }
    }
}
