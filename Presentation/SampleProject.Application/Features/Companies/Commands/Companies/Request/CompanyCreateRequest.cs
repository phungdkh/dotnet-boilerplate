using System.ComponentModel.DataAnnotations;
using MediatR;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Commands.Companies.Request
{
    public class CompanyCreateRequest : IRequest<ResponseModel>
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string LogoUrl { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(25)]
        public string PhoneNumber { get; set; }

        [MaxLength(125)]
        public string Email { get; set; }

        public string Description { get; set; }
    }
}
