using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "maxmum of name length is 50")]
        [MinLength(5, ErrorMessage = "minmum of name length is 50")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age must between 22 and 60")]
        public int? Age { get; set; }

        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public decimal Salaey { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HairDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

    }
}
