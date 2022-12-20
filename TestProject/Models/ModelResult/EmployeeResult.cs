using System;

namespace Test.Models
{
    public class EmployeeResult
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public string Status { get; set; }

        public string Department { get; set; }

        public string Post { get; set; }
    }
}