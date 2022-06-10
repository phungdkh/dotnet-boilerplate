namespace SampleProject.Shared.Common.Models.AppSettingOptions
{
    public class ConnectionStringAppSettingOptions
    {
        public required string ApplicationInsights { get; set; }

        public required string AzureBlobStorage { get; set; }

        public required string AppDbConnection { get; set; }
    }
}
