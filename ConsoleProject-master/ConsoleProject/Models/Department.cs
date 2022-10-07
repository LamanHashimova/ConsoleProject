﻿using ConsoleProject.Interfaces;
using ConsoleProject.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleProject.Models
{
    class Department
    {
        private string _name;
        public string Name 
        {
            get => _name;
            set
            {
                while (value.Length <2)
                {
                    Console.WriteLine("Duzgun department adi daxil edin. Minimum 2 herfden ibaret olmalidir");
                }
                _name = value;
            } 
        }
        private int _workerlimit;
        public int Workerlimit 
        {
            get => _workerlimit;
            set
            {
                while (!(value>=1))
                {
                    Console.WriteLine("Duzgun isci limiti daxil edin.Minimum 1 ola biler.");
                }
                _workerlimit = value;
            } 
        }
        private int _salarylimit;
        public int Salarylimit
        {
            get => _salarylimit;
            set
            {
                while (!(value>=250))
                {
                    Console.WriteLine("Duzgun maas limiti daxil edin. Minimum 250 ola biler.");
                }
                _salarylimit = value;
            }
        }

        public Employee[] Employees;
        public Department(string name, int workerlimit, int salarylimit)
        {
            _name = name;
            _workerlimit = workerlimit;
            _salarylimit = salarylimit;

            Employees = new Employee[0];

        }
        public int CalcSalaryAvarage()
        {
            int count = 0;
            int sum = 0;
            int avarage = 0;
            foreach (Employee employee in Employees )
            {
                    sum += employee.Salary;
                    count++;

            }avarage = sum / count;
            return avarage;
        }
        public override string ToString()
        {
            return $"Department adi: {Name}\nMax Isci sayi limiti: {_workerlimit} nefer\nIscilerin maas limiti: Max {_workerlimit} AZN\nIscilerin maas ortalamasi: {CalcSalaryAvarage()} AZN";
        }

    }
    
}
