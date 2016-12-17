using CommentEverythingCryptography.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace CEComms.Communications.Twilio.User {
    public class Usage {
        public double GetUsageThisMonth() {
            IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));

            UsageTrigger trigger = twilio.CreateUsageTrigger("sms", "1000", "http://www.example.com/");
            UsageResult totalPrice = twilio.ListUsage("totalprice", "ThisMonth");
            UsageRecord record = totalPrice.UsageRecords[0];
            return record.Usage;
        }
    }
}
