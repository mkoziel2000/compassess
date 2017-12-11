namespace PFLApp.Models.PFL
{
    /// <summary>
    /// General Response for all PFL Apis
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Metadata section
        /// </summary>
        public ApiMetadata Meta { get; set; }
        /// <summary>
        /// Results section
        /// </summary>
        public ApiResults Results { get; set; }
    }
}