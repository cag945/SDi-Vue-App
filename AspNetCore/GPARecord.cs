using System;

namespace AspNetCore
{
    public class GPARecord
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public DateTime DateOfBirth { get; set; }
        public decimal GPA { get; set; }
    }
}