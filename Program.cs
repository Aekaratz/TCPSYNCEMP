using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using SyncDataApp.data.Center;
using SyncDataApp.data.WolfApproveCore.SyncData;
using SyncDataApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;

namespace SyncDataApp
{
    class Program
    {

        private static readonly CenterTContext _wolfcenter = new CenterTContext();
        private static readonly WolfApproveCoreSyncDataContext db = new WolfApproveCoreSyncDataContext();
        //string EmployeeFile = ConfigurationManager.AppSettings["EmployeeFile"];


        public static void Main(string[] args )
        {

            try
            {
                string EmployeeFile = ConfigurationManager.AppSettings["EmployeeFile"];
                ReadCsvData(EmployeeFile);

            }
            catch (Exception e)
            {

                Logger.WriteLog($"ERROR:{e.Message} \n Detail:{e.InnerException.Message}");
                Console.WriteLine("ERROR!!");

            }

        }

        #region manage
        //static void ManageDivision(DivisionDto divisionDto)
        //{
        //    var data = db.Mstdivisions.FirstOrDefault(a => a.DivisionCode.Equals(divisionDto.DivisionCode));
        //    if (data == null)
        //    {
        //        db.Mstdivisions.Add(new Mstdivision()
        //        {
        //            DivisionCode = divisionDto.DivisionCode,
        //            NameEn = divisionDto.NameEn,
        //            NameTh = divisionDto.NameTh,
        //            IsActive = divisionDto.IsActive,
        //            AccountId = divisionDto.AccountId,
        //            CreatedBy = divisionDto.CreatedBy,
        //            CreatedDate = divisionDto.CreatedDate,
        //            ModifiedBy = divisionDto.ModifiedBy,
        //            ModifiedDate = divisionDto.ModifiedDate,

        //        });

        //    }
        //    else
        //    {
        //        data.DivisionCode = divisionDto.DivisionCode;
        //        data.NameEn = divisionDto.NameEn;
        //        data.NameTh = divisionDto.NameTh;
        //        data.IsActive = divisionDto.IsActive;
        //        data.AccountId = divisionDto.AccountId;
        //        data.CreatedBy = divisionDto.CreatedBy;
        //        data.CreatedDate = divisionDto.CreatedDate;
        //        data.ModifiedBy = data.ModifiedBy;
        //        data.ModifiedDate = divisionDto.ModifiedDate;
        //    }
        //    db.SaveChanges();
        //}
        //static void ManageDepartment(DepartMentDto departmentDto)
        //{
        //    var data = db.Mstdepartments.FirstOrDefault(a => a.DepartmentCode.Equals(departmentDto.DepartmentCode));
        //    //var paren = db.MSTDepartments.FirstOrDefault(a => a.DepartmentCode.Contains(departmentDto.ParenName));
        //    var division = db.Mstdivisions.FirstOrDefault(a => a.DivisionCode.Contains(departmentDto.ParenName));
        //    var parent = db.Mstdepartments.FirstOrDefault(a => a.DepartmentCode.Contains(division.DivisionCode));


        //    if (data == null)
        //    {
        //        db.Mstdepartments.Add(new Mstdepartment()
        //        {
        //            ParentId = parent.DepartmentId,
        //            DivisionId = division.DivisionId,
        //            DepartmentCode = departmentDto.DepartmentCode,
        //            NameTh = departmentDto.NameTh,
        //            NameEn = departmentDto.NameEn,
        //            CreatedDate = departmentDto.CreatedDate,
        //            CreatedBy = departmentDto.CreatedBy,
        //            ModifiedDate = departmentDto.ModifiedDate,
        //            ModifiedBy = departmentDto.ModifiedBy,
        //            IsActive = departmentDto.IsActive,
        //            AccountId = departmentDto.AccountId,
        //            CompanyCode = departmentDto.CompanyCode

        //        });

        //    }
        //    else if (division == null)
        //    {

        //        data.DivisionId = null;
        //        data.DepartmentCode = departmentDto.DepartmentCode;
        //        data.NameTh = departmentDto.NameTh;
        //        data.NameEn = departmentDto.NameEn;
        //        data.CreatedDate = data.CreatedDate;
        //        data.CreatedBy = departmentDto.CreatedBy;
        //        data.ModifiedDate = departmentDto.ModifiedDate;
        //        data.ModifiedBy = departmentDto.ModifiedBy;
        //        data.IsActive = departmentDto.IsActive;
        //        data.AccountId = departmentDto.AccountId;
        //        data.CompanyCode = departmentDto.CompanyCode;
        //    }


        //    else if (parent == null)
        //    {

        //        data.ParentId = null;
        //        data.DivisionId = division.DivisionId;
        //        data.DepartmentCode = departmentDto.DepartmentCode;
        //        data.NameTh = departmentDto.NameTh;
        //        data.NameEn = departmentDto.NameEn;
        //        data.CreatedDate = data.CreatedDate;
        //        data.CreatedBy = departmentDto.CreatedBy;
        //        data.ModifiedDate = departmentDto.ModifiedDate;
        //        data.ModifiedBy = departmentDto.ModifiedBy;
        //        data.IsActive = departmentDto.IsActive;
        //        data.AccountId = departmentDto.AccountId;
        //        data.CompanyCode = departmentDto.CompanyCode;
        //    }

        //    else
        //    {
        //        data.ParentId = parent.DepartmentId;
        //        data.DivisionId = division.DivisionId;
        //        data.DepartmentCode = departmentDto.DepartmentCode;
        //        data.NameTh = departmentDto.NameTh;
        //        data.NameEn = departmentDto.NameEn;
        //        data.CreatedDate = data.CreatedDate;
        //        data.CreatedBy = departmentDto.CreatedBy;
        //        data.ModifiedDate = departmentDto.ModifiedDate;
        //        data.ModifiedBy = departmentDto.ModifiedBy;
        //        data.IsActive = departmentDto.IsActive;
        //        data.AccountId = departmentDto.AccountId;
        //        data.CompanyCode = departmentDto.CompanyCode;

        //    }
        //    db.SaveChanges();
        //}

        //static void ManagePositionLavel(MstpositionLevel positionLevel)
        //{
        //    var data = db.MstpositionLevels.FirstOrDefault(a => a.NameEn.Equals(positionLevel.NameEn) && a.NameTh.Equals(positionLevel.NameTh));
        //    if (data == null)
        //    {
        //        db.MstpositionLevels.Add(positionLevel);
        //    }
        //    else
        //    {
        //        data.NameEn = positionLevel.NameEn;
        //        data.NameTh = positionLevel.NameTh;
        //        data.PositionLevel = positionLevel.PositionLevel;
        //        data.IsActive = positionLevel.IsActive;
        //        data.CreatedDate = positionLevel.CreatedDate;
        //        data.CreatedBy = positionLevel.CreatedBy;
        //        data.ModifiedDate = positionLevel.ModifiedDate;
        //        data.ModifiedBy = positionLevel.ModifiedBy;
        //        data.AccountId = positionLevel.AccountId;
        //    }
        //    db.SaveChanges();

        //}




        #endregion endmanage

        public static List<EmployeeCSVData> ReadCsvData(string EmployeeFile)
        {
            List<EmployeeCSVData> employees = new List<EmployeeCSVData>();
            try
            {
                Logger.DeleteLog();
                Console.WriteLine("Waiting ........... ");
                Logger.WriteLog("Start");
                int CounterError = 0;
                using (TextFieldParser csvParser = new TextFieldParser(EmployeeFile))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    // Skip the row with the column names
                    csvParser.ReadLine();
                    
                    while (!csvParser.EndOfData)
                    {
                        // Processing row

                        string[] fields = csvParser.ReadFields();

                        string[] keywordActive = { "true", "1", "Active" };
                        string[] keywordNotActive = { "false", "0", "InActive", "", " " };

                        bool statusActive = false;
                        if (keywordActive.Contains(fields[5]))
                        {
                            statusActive = true;
                        }
                        else if (keywordNotActive.Contains(fields[5]))
                        {
                            statusActive = false;
                        }

                        if (fields.Any(field => string.IsNullOrWhiteSpace(field)))
                        {
                            for (int i = 0; i < fields.Length; i++)
                            {
                                if (string.IsNullOrWhiteSpace(fields[i]))
                                {
                                    fields[i] = "Null";
                                }
                            }

                            Logger.WriteLog("***************************************************************************************************************************\n");
                            Logger.WriteLog($"ERRORLISTEMPLOYEE(ข้อมูลใน csv ไม่ครบถ้วน)\n");
                            Logger.WriteLog($"WORK_ID:{fields[0]}\t UserEmail:{fields[1]}\t FULL_NAME_TH: {fields[2]}\t  FULL_NAME_EN:{fields[3]}\t LOGIN_EMAIL:{fields[4]}\t STATUS:{fields[5]}\t PositionNameEN::{fields[6]}\t PosEN::{fields[7]}\t PosLvsEN::{fields[8]}\t DepCode:{fields[9]}\t Position / ตำแหน่ง-Job Informationข้อมูลงาน:{fields[10]}\t REPORT_TO::{fields[11]}\t Default EN:{fields[12]}\t Phone Number / หมายเลขโทรศัพท์-Phone Information / ข้อมูลโทรศัพท์:{fields[13]}\t");
                            CounterError ++ ;
                            continue;
                        }

                        if (fields[0].Length > 10)
                        {
                            Logger.WriteLog("***************************************************************************************************************************\n");
                            Logger.WriteLog($"WORK_ID String >10 (ตัวอักษร WORK_ID มากกว่า10ตัวอักษร)\n");
                            Logger.WriteLog($"WORK_ID:{fields[0]}\t UserEmail:{fields[1]}\t FULL_NAME_TH: {fields[2]}\t  FULL_NAME_EN:{fields[3]}\t LOGIN_EMAIL:{fields[4]}\t STATUS:{fields[5]}\t PositionNameEN::{fields[6]}\t PosEN::{fields[7]}\t PosLvsEN::{fields[8]}\t DepCode:{fields[9]}\t Position / ตำแหน่ง-Job Informationข้อมูลงาน:{fields[10]}\t REPORT_TO::{fields[11]}\t Default EN:{fields[12]}\t Phone Number / หมายเลขโทรศัพท์-Phone Information / ข้อมูลโทรศัพท์:{fields[13]}\t");
                            CounterError ++ ;   
                            continue;
                        }

                        var LocalAccout = "";
                        if (fields[0].Contains(fields[1]))
                        {
                            LocalAccout = fields[0];
                        }

                        ManageAccount(new Wolfaccount()
                        {

                            Username = LocalAccout,
                            IsActive = statusActive,

                        });

                        ManagePosition(new PositionLavelDto()
                        {
                            NameEn = fields[6],
                            NameTh = fields[7],
                            PositionLvsName = fields[8],
                            IsActive = statusActive,

                        });


                        EmployeeCSVData emp = new EmployeeCSVData
                        {

                            WorkId = fields[0],
                            UserEmail = fields[1],
                            FullNameTh = fields[2],
                            FullNameEn = fields[3],
                            LoginEmail = fields[4],
                            Status = statusActive,
                            PositionNameEn = fields[6],
                            PosEn = fields[7],
                            PosLvsEn = fields[8],
                            DepCode = fields[9],
                            PositionJobInformation = fields[10],
                            ReportTo = fields[11],
                            DefaultEn = fields[12],
                            PhoneNumber = fields[13]


                        };

                        employees.Add(emp);

                        Console.WriteLine($"ReadData => Work ID: {emp.WorkId}, Username: {emp.UserEmail}");

                    }

                }
                ManageEmployee(employees , CounterError);

                return employees;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }

        }
        static void ManagePosition(PositionLavelDto positionDto)
        {
            for (int i = 1; i <= 2; i++)
            {
                var data = db.Mstpositions.FirstOrDefault(a => a.NameEn.Equals(positionDto.NameEn));
                var positionLvs = db.MstpositionLevels.FirstOrDefault(a => a.NameEn.Equals(positionDto.PositionLvsName));
                #region
                //if (data == null)
                //{
                //    db.MSTPositionLevels.Add(new MSTPositionLevel()
                //    {
                //        NameEn = positionDto.NameEn,
                //        NameTh = positionDto.NameTh,
                //        CreatedDate = DateTime.Today,
                //        CreatedBy = "System",
                //        ModifiedDate = DateTime.Today,
                //        ModifiedBy = "System",
                //        AccountId = 1,
                //    });

                //}

                //else

                //{
                //    data.NameEn = positionDto.NameEn;
                //    data.NameTh = positionDto.NameTh;
                //    data.CreatedDate = DateTime.Today;
                //    data.CreatedBy = "System";
                //    data.ModifiedDate = DateTime.Today;
                //    data.ModifiedBy = "System";
                //    data.AccountId = 1;
                //}
                //db.SaveChanges();
                #endregion

                if (data == null)
                {

                    db.Mstpositions.Add(new Mstposition()
                    {

                        NameEn = positionDto.NameEn,
                        NameTh = positionDto.NameTh,
                        PositionLevelId = null,
                        IsActive = positionDto.IsActive,
                        CreatedDate = DateTime.Today,
                        CreatedBy = "System",
                        ModifiedDate = DateTime.Today,
                        ModifiedBy = "System",
                        AccountId = 1,
                        CompanyCode = positionDto.CompanyCode,

                    });

                }
                else if (positionLvs == null)
                {

                    data.NameEn = positionDto.NameEn;
                    data.NameTh = positionDto.NameTh;
                    data.PositionLevelId = null;
                    data.IsActive = positionDto.IsActive;
                    data.CreatedDate = DateTime.Today;
                    data.CreatedBy = "System";
                    data.ModifiedDate = DateTime.Today;
                    data.ModifiedBy = "System";
                    data.AccountId = 1;
                    data.CompanyCode = positionDto.CompanyCode;

                }
                else
                {
                    data.NameEn = positionDto.NameEn;
                    data.NameTh = positionDto.NameTh;
                    data.PositionLevelId = positionLvs.PositionLevelId;
                    data.IsActive = positionDto.IsActive;
                    data.CreatedDate = data.CreatedDate;
                    data.CreatedBy = "System";
                    data.ModifiedDate = DateTime.Today;
                    data.ModifiedBy = "System";
                    data.AccountId = 1;
                    data.CompanyCode = positionDto.CompanyCode;
                }
                db.SaveChanges();
            }

        }
        static void ManageEmployee(List<EmployeeCSVData> employees, int CounterError)
        {
            int roundCounter = 0;
            int departMentnull = 0;
            int positionName = 0;
            foreach (var employeeDto in employees)
            {
                Mstemployee data = db.Mstemployees.FirstOrDefault(a => a.EmployeeCode.Equals(employeeDto.WorkId));
                var position = db.Mstpositions.FirstOrDefault(p => p.NameEn.Equals(employeeDto.PositionNameEn));
                var departmentcode = db.Mstdepartments.FirstOrDefault(d => d.DepartmentCode.Contains(employeeDto.DepCode));
                var reportto = db.Mstemployees.FirstOrDefault(a => a.EmployeeCode.Contains(employeeDto.ReportTo));
                if (data == null)
                {

                    db.Mstemployees.Add(new Mstemployee
                    {
                        DivisionId = departmentcode != null ? departmentcode.DivisionId : null,
                        EmployeeCode = employeeDto.WorkId,
                        Username = employeeDto.UserEmail,
                        NameTh = employeeDto.FullNameTh,
                        NameEn = employeeDto.FullNameEn,
                        Email = employeeDto.LoginEmail,
                        IsActive = employeeDto.Status,
                        ReportToEmpCode = employeeDto.ReportTo,
                        PositionId = position != null ? position.PositionId : null,
                        DepartmentId = departmentcode != null ? departmentcode.DepartmentId : null,
                        Adtitle = employeeDto.PhoneNumber,
                        CreatedDate = DateTime.Today,
                        CreatedBy = "System",
                        ModifiedDate = DateTime.Today,
                        ModifiedBy = "System",
                        AccountId = 1,
                        Lang = "EN"
                    });
                    if (departmentcode == null)
                    {
                        Logger.WriteLog("***************************************************************************************************************************\n");
                        Logger.WriteLog($"Update");
                        Logger.WriteLog($"This department code does not exist in the database.\n");
                        Logger.WriteLog($"DepCode:{employeeDto.DepCode}");
                        departMentnull++;
                    }
                    else if (position == null)
                    {

                        Logger.WriteLog("***************************************************************************************************************************\n");
                        Logger.WriteLog($"Update");
                        Logger.WriteLog($"This position Name does not exist in the database.\n");
                        Logger.WriteLog($"Position Name:{employeeDto.PositionNameEn}");
                        positionName++;
                    }
                }
                else
                {

                    data.DivisionId = departmentcode != null ? departmentcode.DivisionId : null;
                    data.EmployeeCode = employeeDto.WorkId;
                    data.Username = employeeDto.UserEmail;
                    data.NameTh = employeeDto.FullNameTh;
                    data.NameEn = employeeDto.FullNameEn;
                    data.Email = employeeDto.LoginEmail;
                    data.IsActive = employeeDto.Status;
                    // data.ReportToEmpCode = reportto.EmployeeId.ToString();
                    data.ReportToEmpCode = employeeDto.ReportTo;
                    data.PositionId = position != null ? position.PositionId : null;
                    data.DepartmentId = departmentcode != null ? departmentcode.DepartmentId : null;
                    data.Adtitle = employeeDto.PhoneNumber;
                    data.CreatedDate = data.CreatedDate;
                    data.CreatedBy = "System";
                    data.ModifiedDate = DateTime.Today;
                    data.ModifiedBy = "System";
                    data.AccountId = 1;
                    data.Lang = "EN";
                    data.SignPicPath = data.SignPicPath;

                    if (departmentcode == null)
                    {
                        Logger.WriteLog("***************************************************************************************************************************\n");
                        Logger.WriteLog($"Update");
                        Logger.WriteLog($"This department code does not exist in the database.\n");
                        Logger.WriteLog($"DepCode:{employeeDto.DepCode}");
                        departMentnull++;
                    }
                    else if (position == null)
                    {
                        Logger.WriteLog("***************************************************************************************************************************\n");
                        Logger.WriteLog($"Update");
                        Logger.WriteLog($"This position Name does not exist in the database.\n");
                        Logger.WriteLog($"Position Name:{employeeDto.PositionNameEn}");
                        positionName++;
                    }

                }
                db.SaveChanges();
                roundCounter++;
               
                Console.WriteLine($"Update Completed round: {roundCounter} \t EmployeeName:{employeeDto.FullNameEn}");

            }
           
            Console.WriteLine($"SyncEmp Fail Check data in CSV :{CounterError} row");
            Console.WriteLine($"This department code does not exist in the database. :{departMentnull} row");
            Console.WriteLine($"This position Name does not exist in the database. :{positionName} row");
            Console.WriteLine($"SyncEmp Success :{roundCounter} row");

            Logger.WriteLog($"SyncEmp Fail  Check data in CSV :{CounterError} row");
            Logger.WriteLog($"This department code does not exist in the database. :{departMentnull} row");
            Logger.WriteLog($"This position Name does not exist in the database. :{positionName} row");
            Logger.WriteLog($"SyncEmp Success :{roundCounter} row");
            SentEmail.SentToEmail();
           
        }
        static void ManageAccount(Wolfaccount wolfAccount)
        {
            string _ContactCode = ConfigurationManager.AppSettings["ContactCode"];
            string _CreatedBy = ConfigurationManager.AppSettings["CreatedBy"];
            string _ModifiedBy = ConfigurationManager.AppSettings["ModifiedBy"];
            string _Password = ConfigurationManager.AppSettings["Password"];
            string _GuidVerify = ConfigurationManager.AppSettings["GuidVerify"];
            var data = _wolfcenter.Wolfaccounts.FirstOrDefault(a => a.Username.Equals(wolfAccount.Username));

            if (data == null)
            {
                _wolfcenter.Wolfaccounts.Add(new Wolfaccount()
                {
                    ContactCode = _ContactCode,
                    Username = wolfAccount.Username,
                    IsVerify = true,
                    IsActive = wolfAccount.IsActive,
                    CreatedBy = _CreatedBy,
                    CreatedDate = DateTime.Today,
                    ModifiedBy = _ModifiedBy,
                    ModifiedDate = DateTime.Today,
                    Password = _Password,
                    GuidVerify = _GuidVerify

                });
            }
            else
            {
                data.ContactCode = _ContactCode;
                data.Username = wolfAccount.Username;
                data.Password = _Password;
                data.GuidVerify = _GuidVerify;
                data.IsVerify = true;
                data.IsActive = true;
                data.CreatedBy = _CreatedBy;
                data.CreatedDate = data.CreatedDate;
                data.ModifiedBy = _ModifiedBy;
                data.ModifiedDate = DateTime.Today;
            }
            _wolfcenter.SaveChanges();

        }

    }


}