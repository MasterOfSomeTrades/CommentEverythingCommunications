using CEComms.Communications.Twilio.User;
using CommentEverythingCryptography.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twilio;

namespace Stockiment.Communications.Twilio.SMS {
    public class TextMessageSender {
        public TextMessageSender() {
            MaxSend = 20;
            CharLimit = 4000;
        }

        public int MaxSend { get; set; }
        public int CharLimit { get; set; }
        private int _numberSent = 0;

        public void SendText(string msg) {
            IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);

            // Find your Account Sid and Auth Token at twilio.com/user/account
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt(UserProfile.ACCOUNT_SID_CIPHER), decryptor.Decrypt(UserProfile.AUTH_TOKEN_CIPHER));

            if (_numberSent < MaxSend) {
                if (msg.Length <= CharLimit) {
                    foreach (string recipientCipher in UserProfile.RecipientListCiphers) {
                        if (_numberSent < MaxSend) {
                            Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(recipientCipher), msg);
                            _numberSent++;
                        }
                    }
                } else {
                    Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(UserProfile.ADMIN_NUMBER_CIPHER), "====\nSMS length over character limit/n====");
                    _numberSent++;
                }
            }

            if (_numberSent == MaxSend) {
                Message message = twilio.SendMessage(decryptor.Decrypt(UserProfile.PHONE_NUMBER_CIPHER), decryptor.Decrypt(UserProfile.ADMIN_NUMBER_CIPHER), "====\nSMS alerts maxed out for time period/n====");
                _numberSent++;
            }
        }

        // TODO: Counter reset as function of this class
        // TODO: Count message segments in counter (e.g. using message length)
        public void ResetCounter() {
            _numberSent = 0;
        }
    }
}
