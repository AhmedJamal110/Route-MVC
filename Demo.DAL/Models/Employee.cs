using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }

        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public decimal Salaey { get; set; }
        public bool IsActive { get; set; }
        public string  Email { get; set; }
        public string  PhoneNumber { get; set; }

        public string  ImageName { get; set; }
        public DateTime HairDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;


        public int? departmentId { get; set; }
        public Department department { get; set; }
    }
}
