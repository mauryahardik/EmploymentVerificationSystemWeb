using System.ComponentModel.DataAnnotations;

namespace EmploymentVerificationSystemWeb.Models.Request
{
    public class EmploymentVerificationRequest
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Verification code is required.")]
        [StringLength(20, ErrorMessage = "Verification code cannot exceed 20 characters.")]
        public string VerificationCode { get; set; }
    }
}
