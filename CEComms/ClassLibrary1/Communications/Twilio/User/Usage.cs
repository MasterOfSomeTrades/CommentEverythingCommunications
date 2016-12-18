using CommentEverythingCryptography.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace CEComms.Communications.Twilio.User {
    public class Usage {
        public decimal GetUsageThisMonth() {
            IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));

            UsageResult totalPrice = twilio.ListUsage("totalprice", "ThisMonth");
            UsageRecord record = totalPrice.UsageRecords[0];
            return (decimal) record.Usage;
        }

        public int GetSMSCountToday() {
            IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));

            UsageResult totalSent = twilio.ListUsage("sms", "Today");
            UsageRecord record = totalSent.UsageRecords[0];
            return (int) Math.Round(record.Usage);
        }
    }
}
