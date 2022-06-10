namespace SampleProject.Application.Features.Me.Queries.Responses
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public string Avatar { get; set; }

        public Guid? CompanyId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Roles { get; set; }
    }
}
