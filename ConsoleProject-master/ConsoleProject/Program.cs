using ConsoleProject.Interfaces;
using ConsoleProject.Models;
using ConsoleProject.Services;
using System;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IHumanResourceManager humanResourceManager = new HumanResourceManager();
            do
            {
                Console.WriteLine("Emeliyyat etmek istediyiniz sahenin nomresini daxil edin");
                Console.WriteLine("1 Departmentle bagli emeliyyat");
                Console.WriteLine("2 Iscilerle bagli emeliyyat");
                string EmeliyyatStr = Console.ReadLine();
                int EmeliyyatNum;
                while (!int.TryParse(EmeliyyatStr, out EmeliyyatNum) || EmeliyyatNum < 1 || EmeliyyatNum > 2)
                {
                    Console.WriteLine("Duzgun emeliyyat nomresi daxil edin");
                    EmeliyyatStr = Console.ReadLine();
                }
                switch (EmeliyyatNum)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("Departmentle bagli etmek istediyiniz emeliyyatin nomresini daxil edin.");
                            Console.WriteLine("1 Departameantlerin siyahisini gostermek");
                            Console.WriteLine("2 Yeni Departamenet yaratmaq");
                            Console.WriteLine("3 Departmanetde deyisiklik etmek");
                            string DepartmentStr = Console.ReadLine();
                            int DepartmentNum;
                            while (!int.TryParse(DepartmentStr, out DepartmentNum) || DepartmentNum < 1 || DepartmentNum > 3)
                            {
                                Console.WriteLine("Departmentle bagli duzgun emeliyyat nomresi daxil edin");
                                DepartmentStr = Console.ReadLine();
                            }
                            switch (DepartmentNum)
                            {
                                case 1:
                                    Console.Clear();
                                    GetDepartments(humanResourceManager);
                                    break;
                                case 2:
                                    Console.Clear();
                                    AddDepartment(ref humanResourceManager);
                                    break;
                                case 3:
                                    Console.Clear();
                                    EditDepartments(ref humanResourceManager);
                                    break;
                            }
                        } while (true);
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine("Iscilerle bagli etmek istediyiniz emeliyyatin nomresini daxil edin.");
                            Console.WriteLine("1 Iscilerin siyahisini gostermek");
                            Console.WriteLine("2 Departamentdeki iscilerin siyahisini gostermrek");
                            Console.WriteLine("3 Isci elave etmek");
                            Console.WriteLine("4 Isci uzerinde deyisiklik etmek");
                            Console.WriteLine("5 Departamentden isci silinmesi");
                            string WorkerStr = Console.ReadLine();
                            int WorkerNum;
                            while (!int.TryParse(WorkerStr, out WorkerNum) || WorkerNum < 1 || WorkerNum > 5)
                            {
                                Console.WriteLine("Isci prosesi ile bagli duzgun emeliyyat nomresi daxil edin.");
                                WorkerStr = Console.ReadLine();
                            }
                            switch (WorkerNum)
                            {
                                case 1:

                                    Console.Clear();
                                    GetEmployee(humanResourceManager);
                                    break;
                                case 2:
                                    Console.Clear();
                                    GetEmployeebyDepartmentName(humanResourceManager);
                                    break;
                                case 3:
                                    Console.Clear();
                                    AddEmployee(ref humanResourceManager);
                                    break;
                                case 4:
                                    EditEmployee(ref humanResourceManager);
                                    break;
                                case 5:
                                    Console.Clear();
                                    DeleteEmployee(ref humanResourceManager);
                                    break;
                            }
                        } while (true);
                        break;
                }
            } while (true);
        }

        private static void AddDepartment(ref IHumanResourceManager humanResourceManager)
        {
            string departmentname;
            int workersNum;
            int salaryNum;
            Console.WriteLine("Yaratmaq istediyiniz departamentin adini daxil edin:");
            Console.Write("=> ");
            departmentname = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(departmentname) || departmentname.Length < 2)
            {
                Console.WriteLine("Departament adini duzgun daxil edin.");
                departmentname = Console.ReadLine();
            }

            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name.ToUpper() == departmentname.ToUpper())
                {
                    Console.Clear();
                    Console.WriteLine("Daxil etdiyiniz adda departament artiq movcuddur.");
                    return;
                }
            }
            Console.WriteLine("Departamentde maximum var ola bilecek isci sayini daxil edin:");

            Console.Write("=> ");
            string workers = Console.ReadLine();
            while (!int.TryParse(workers, out workersNum) || workersNum < 1)
            {
                Console.WriteLine("Isci sayini duzgun daxil edin.");
                workers = Console.ReadLine();
                Console.Write("=> ");
            }
            while (true)
            {
                Console.WriteLine("Departamentde butun iscilere verilecek ayliq maas limitini daxil edin:");
                Console.Write("=> ");
                string salarylimit = Console.ReadLine();
                while (!int.TryParse(salarylimit, out salaryNum) || salaryNum < 250)
                {
                    Console.WriteLine("Meblegi duzgun daxil edin.");
                    salarylimit = Console.ReadLine();
                    Console.Write("=> ");
                }

                if (salaryNum / workersNum < 250)
                {
                    Console.WriteLine("Maas limitini duzgun teyin edin:");
                    continue;
                }
                else
                {
                    break;
                }
            }


            Console.Clear();
            Console.WriteLine("Departament yaradildi.");

            humanResourceManager.AddDepartment(departmentname, workersNum, salaryNum);
        }

        static void GetDepartments(IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine($"Depatmentin adi: {department.Name}; Isci limiti: {department.Workerlimit}; Maas limiti: {department.Salarylimit}");
            }

        }
        static void AddEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Iscinin ad soyadini daxil edin");
            Console.Write("=> ");
            string FullName = Console.ReadLine();

            Console.WriteLine("Iscini elave etmek istediyiniz departamentin adini siyahidan secib  daxil edin");
            Console.WriteLine(" ");
            GetDepartments(humanResourceManager);
            string Departmentname = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(Departmentname))
            {
                Console.WriteLine("Departament adini duzgun daxil edin");
            }
            Console.WriteLine("iscinin vezifesini daxil edin");
            string PositionStr = Console.ReadLine();
            while (PositionStr.Length < 2)
            {
                Console.WriteLine("iscinin vezifesi en az 2 simvoldan ibaret olmalidir");
                PositionStr = Console.ReadLine();
            }
            Console.WriteLine("Iscinin maasini daxil edin");
            string SalaryStr = Console.ReadLine();
            int SalaryNum;
            while (!int.TryParse(SalaryStr, out SalaryNum) || SalaryNum < 250)
            {
                Console.WriteLine("Minimum emek haqqi 250 olmalidir");
                SalaryStr = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine("Qeyd etdiyiniz departmente isci elave edildi");
            humanResourceManager.AddEmployee(FullName, PositionStr, SalaryNum, Departmentname);

        }
        static void GetEmployee(IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        Console.WriteLine($"Ad Soyadi: {employee.FullName}; vezifesi: {employee.Position}; Maasi: {employee.Salary}; Departmentin adi: {employee.DepartmentName}; nomresi: {employee.No}");
                    }
                }
            }

        }
        static void GetEmployeebyDepartmentName(IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Hansi departmentdeki iscilerin siyahisini gostermek istediyinizi daxil edin");
            GetDepartments(humanResourceManager);
            string GetEmployeebyDepartmentNameStr = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(GetEmployeebyDepartmentNameStr) || GetEmployeebyDepartmentNameStr.Length < 2)
            {
                Console.WriteLine("Departament adini duzgun daxil edin");
            }
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name.ToLower() == GetEmployeebyDepartmentNameStr.ToLower())
                {
                    foreach (Employee item in department.Employees)
                    {
                        Console.WriteLine($"{item.FullName} {item.DepartmentName} {item.Salary} { item.No}");
                    }
                    break;
                }
            }

        }
        static void DeleteEmployee(ref IHumanResourceManager humanResourceManager)
        {

            Console.WriteLine("Hansi departamentden isci silmek isteyirsiniz");
            string deleteEmployeeInDepartment = Console.ReadLine();

            foreach (Department departments in humanResourceManager.Departments)
            {
                if (departments.Name.ToUpper() != deleteEmployeeInDepartment.ToUpper())
                {
                    continue;

                }
                else
                {
                    Console.WriteLine("Silmek istediyiniz iscinin nomresini daxil et");
                    string deleteEmployeeBynumber = Console.ReadLine();
                    for (int i = 0; i < departments.Employees.Length; i++)
                    {
                        if (!(deleteEmployeeBynumber.ToUpper() == departments.Employees[i].No.ToUpper()))
                        {

                            Console.WriteLine("bu nomreli isci movcud deyil");
                        }
                        else
                        {
                           
                            Console.WriteLine("Qeyd etdiyiniz isci siyahidan silindi");

                        }
                    }
                }
            }
        }
        static void EditEmployee(ref IHumanResourceManager humanResourceManager)
        {
            string fullname = null;
            string NewPositionStr = null;
            string newSalary = null;
            int newSalaryNum = 0;
            foreach (Department department in humanResourceManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee == null)
                    {
                        Console.WriteLine("Hec bir isci movcud deil. Emeliyyati yerine yetirmek ucun en azi 1 isci ola biler");
                    }
                }
                break;
            }
            Console.WriteLine("Deyisiklik etmek istediyiniz iscinin nomresini daxil edin:");
            Console.Write("=> ");
            string workerNo = Console.ReadLine();
            while (String.IsNullOrEmpty(workerNo))
            {
                Console.WriteLine("duzgun isci nomresi daxil edin");
            }
            foreach (Department department in humanResourceManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (workerNo==employee.No)
                    {
                        Console.WriteLine("Deyisiklik etmek istediyiniz iscinin tam adini daxil edin:");
                        Console.Write("=> ");
                        fullname = Console.ReadLine();
                        while (String.IsNullOrWhiteSpace(fullname))
                        {
                            Console.WriteLine("Duzgun ad daxil edin:");
                        }
                        if (employee != null)
                        {
                            if ((employee.No.ToLower() == workerNo.ToLower()) && (employee.FullName.ToLower() == fullname.ToLower()))
                            {
                                Console.Clear();

                                Console.WriteLine("Isci uzerinde etmek istediyiniz emeliyyatin qarsisindaki nomreni daxil edin:\n");
                                Console.WriteLine("1 - Iscinin aldigi maasda duzelis etmek");
                                Console.WriteLine("2 - Iscinin vezifesinde duzelis etmek");
                                Console.Write("=> ");
                                string select = Console.ReadLine();
                                int selectNum;
                                while (!int.TryParse(select, out selectNum))
                                {
                                    Console.WriteLine("Duzgun secim daxil edin.");
                                }
                                switch (selectNum)
                                {
                                    case 1:
                                        Console.WriteLine("Iscinin yeni maasini daxil edin:");
                                        Console.Write("=> ");
                                        newSalary = Console.ReadLine();
                                        while (!(int.TryParse(newSalary, out newSalaryNum)) || newSalaryNum < 250)
                                        {
                                            Console.WriteLine("Duzgun maas araligi daxil edin.");
                                            newSalary = Console.ReadLine();
                                        }
                                        while (newSalaryNum > department.Salarylimit)
                                        {
                                            Console.WriteLine("Maas limitini asa bilmezsiniz");
                                            newSalary = Console.ReadLine();
                                            continue;
                                        }
                                        Console.WriteLine("Maasda duzelis olundu.");
                                        break;
                                    case 2:
                                        Console.WriteLine("Iscinin yeni vezifesini daxil edin:");
                                        Console.Write("=> ");
                                        NewPositionStr = Console.ReadLine();
                                        while (String.IsNullOrWhiteSpace(NewPositionStr) || NewPositionStr.Length < 2)
                                        {
                                            Console.WriteLine("Duzgun vezife daxil edin.");
                                            NewPositionStr = Console.ReadLine();
                                        }
                                        Console.WriteLine("Vezifede duzelis olundu.");
                                        break;
                                }
                            }
                        }

                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("Bu nomreye sahib isci yoxdur");
                            Console.WriteLine("Deyisiklik etmek istediyiniz iscinin nomresini daxil edin:");
                            Console.Write("=> ");
                            workerNo = Console.ReadLine();
                        }
                         
                    }
                   
                }
               
            }
            humanResourceManager.EditEmployee(workerNo, fullname, NewPositionStr, newSalaryNum);
        }
        static void EditDepartments(ref IHumanResourceManager humanResourceManager)
        {
            string NewNameDepartment = null;
            Console.WriteLine("Deyisiklik etmek istediyiniz departamentin adini daxil edin: ");
            Console.Write("=> ");
            string NameDepartment = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(NameDepartment))
            {
                Console.WriteLine("Duzgun department adi daxil edin:");
                NameDepartment = Console.ReadLine();

            }
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name.ToUpper() == NameDepartment.ToUpper())
                {
                    Console.WriteLine("Yeni department adini daxil edin");
                    NewNameDepartment = Console.ReadLine();
                    Console.Write("=> ");

                    while (String.IsNullOrWhiteSpace(NewNameDepartment) || NewNameDepartment.Length < 2)
                    {
                        Console.WriteLine("Duzgun ad daxil edin:");
                        NewNameDepartment = Console.ReadLine();
                    }
                    if (NewNameDepartment == department.Name)
                    {
                        Console.WriteLine("Ferqli ad daxil edin");
                        break;
                    }
                    foreach (Department department2 in humanResourceManager.Departments)
                    {
                        bool check = false;
                        if (department2.Name.ToUpper() == NewNameDepartment.ToUpper())
                        {
                            Console.WriteLine("Bu adda department artiq movcuddur");
                            check = true;

                        }
                        if (check = false)
                        {
                            Console.WriteLine("Departament adi deyisdirildi.");
                            humanResourceManager.EditDepartments(NameDepartment, NewNameDepartment);

                        }
                    }

                }
                else
                {
                    Console.WriteLine("Deyisiklik etmek istediyiniz department movcud deil.Duzgun ad daxil edin");
                    NameDepartment = Console.ReadLine();
                    break;
                }
               

            }
            
        }
    }
}