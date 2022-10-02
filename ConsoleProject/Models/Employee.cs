using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleProject.Models
{
    class Employee
    {
        static int _no;
        public string No;
        public string FullName;
        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                while (!(value.Length >= 2))
                {
                    Console.WriteLine("Duzgun vezife daxil edin. Minimum 2 herfden ibaret ola biler");
                }
                _position = value;
            }
        }

        private int _salary;
        public int Salary 
        {
            get => _salary;
            set
            {
                while (!(value>=250))
                {
                    Console.WriteLine("Duzgun maas araligi daxil edin. Minimum 250 ola biler.");
                }
                _salary = value;
            }
        }
        public string DepartmentName;

        static Employee()
        {
            _no = 1000;
        }
        public Employee(string fullname, string position, int salary, string departmentname)
        {
            _no++;
            No = $"{departmentname.ToUpper().Substring(0, 2)}{_no}";
            FullName = fullname;
            _position = position;
            Salary = salary;
            DepartmentName = departmentname;
        }
    }
}
