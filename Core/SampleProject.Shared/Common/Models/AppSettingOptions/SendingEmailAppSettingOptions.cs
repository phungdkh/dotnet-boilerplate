namespace SampleProject.Shared.Common.Models.AppSettingOptions
{
    public class SendingEmailAppSettingOptions
    {
        public required string FromName { get; set; }

        public required string FromEmail { get; set; }

        public required string SendGridApiKey { get; set; }

    }
}
