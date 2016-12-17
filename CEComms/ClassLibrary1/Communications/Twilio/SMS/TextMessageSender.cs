using CEComms.Communications.Twilio.User;
using CommentEverythingCryptography.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;

namespace Stockiment.Communications.Twilio.SMS {
    public class TextMessageSender {
        public int MaxSend { get; set; }
        public int CharLimit { get; set; }

        public TextMessageSender() {
            MaxSend = 200;
            CharLimit = 4000;
        }

        IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);

        private bool _limitWarningSent = false;

        private bool SMSLimitReached() {
            Usage twilioUsage = new Usage();
            bool limitReached = (twilioUsage.GetSMSCountToday() >= MaxSend);

            if (!limitReached) {
                _limitWarningSent = false;
            }

            // --- Send SMS limit warning if daily limit reached
            if (limitReached && !_limitWarningSent) {
                TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));
                Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(UserProfile.ADMIN_NUMBER_CIPHER), "===========================\n SMS daily maximum reached\n===========================");
                _limitWarningSent = true;
            }

            return limitReached;
        }

        /// <summary>
        /// Checks CharLimit and MaxSend.
        /// If limits not reached, sends SMS to list of recipients defined for the registered User.
        /// If limits reached, sends SMS warning to admin once a day for this instance of TextMessageSender.
        /// </summary>
        /// <param name="msg"></param>
        public void SendText(string msg) {
            // Find your Account Sid and Auth Token at twilio.com/user/account
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));

            if (msg.Length <= CharLimit) {
                foreach (string recipientCipher in UserProfile.RecipientListCiphers) {
                    if (!SMSLimitReached()) {
                        Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(recipientCipher), msg);
                    }
                }
            } else {
                Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(UserProfile.ADMIN_NUMBER_CIPHER), "===========================\n SMS length over character limit\n===========================");
            }
        }
    }
}
