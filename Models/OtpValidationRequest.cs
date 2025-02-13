namespace GenerateOTPApi.Models
{
    public class OtpValidationRequest
    {
        public string userId { get; set; }
        public string UserOtp { get; set; }
    }
}
