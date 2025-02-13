using Twilio;

namespace GenerateOTPApi.Service
{
    public class OtpService
    {
        private readonly IConfiguration _configuration;
        private static Dictionary<string, (string Otp, DateTime Timestamp)> otpStore = new Dictionary<string, (string, DateTime)>();

        public OtpService(IConfiguration configuration)
        {
            _configuration = configuration;;
        }

        public string GenerateOtp(string userId)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            (string otp, DateTime UtcNow) value = (otp, DateTime.UtcNow);
            otpStore[userId] = value;
            return otp;
        }

        public bool ValidateOtp(string userId, string userOTP)
        {
            if (otpStore.ContainsKey(userId))
            {
                var (storedOtp, timestamp) = otpStore[userId];
                if (storedOtp == userOTP && (DateTime.UtcNow - timestamp).TotalSeconds <= 30)
                {
                    otpStore.Remove(userId);
                    return true;
                }
            }

            return false;
        }
    }
}
