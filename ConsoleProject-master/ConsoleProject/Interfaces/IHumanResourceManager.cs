using ConsoleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleProject.Interfaces
{
    interface IHumanResourceManager
    {
        Department[] Departments { get; }
        void AddDepartment(string name, int workerlimit, int salarylimit);
        void AddEmployee( string fullname, string position, int salary, string departmentname);

        Department[] GetDepartments();
        void EditDepartments(string namedepartment, string newnamedepartment);
        void RemoveEmployee(string departmentname,string no);
        void EditEmployee(string no, string fullname,string position, int salary);
   

    }
}
