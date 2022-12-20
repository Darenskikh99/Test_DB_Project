using System;

namespace Test.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public int StatusId { get; set; }

        public int DepartmentId { get; set; }

        public int PostId { get; set; }
    }
}