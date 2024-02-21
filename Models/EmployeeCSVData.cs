using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDataApp.Models
{
    class EmployeeCSVData
    {
        public string WorkId { get; set; }
        public string UserEmail { get; set; }
        public string FullNameTh { get; set; }
        public string FullNameEn { get; set; }
        public string LoginEmail { get; set; }
        public bool Status { get; set; }
        public string PositionNameEn { get; set; }
        public string PosEn { get; set; }
        public string PosLvsEn { get; set; }
        public string DepCode { get; set; }
        public string PositionJobInformation { get; set; }
        public string ReportTo { get; set; }
        public string DefaultEn { get; set; }
        public string PhoneNumber { get; set; }
    }
}
