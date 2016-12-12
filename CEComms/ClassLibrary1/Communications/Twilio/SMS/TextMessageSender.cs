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
            TwilioRestClient twilio = new TwilioRestClient(decryptor.Decrypt("PKPBJJDXite9Ccsm1Skfj2J3zq9c0v7cD8fzljHVNZYvKemFKEhyihaNgP/swDvE"), decryptor.Decrypt("pAP9/hFxQ4ruGvZl5naAihLsOyBcU/kzVtyZnZK7GlAJkDfyh5h49c2vKj/NiUsw"));

            if (_numberSent < MaxSend) {
                if (msg.Length <= CharLimit) {
                    Message message = twilio.SendMessage(decryptor.Decrypt("TSFhK1EaNhtPPJIx4UjYBA=="), decryptor.Decrypt("7eYiH6SvvlNxcvdmgX4+Yg=="), msg);
                } else {
                    Message message = twilio.SendMessage(decryptor.Decrypt("TSFhK1EaNhtPPJIx4UjYBA=="), decryptor.Decrypt("7eYiH6SvvlNxcvdmgX4+Yg=="), "====\nSMS length over character limit/n====");
                }
            } else if (_numberSent == MaxSend) {
                Message message = twilio.SendMessage(decryptor.Decrypt("TSFhK1EaNhtPPJIx4UjYBA=="), decryptor.Decrypt("7eYiH6SvvlNxcvdmgX4+Yg=="), "====\nSMS alerts maxed out for time period/n====");
            }
            _numberSent++;
        }

        // TODO: Counter reset as function of this class
        // TODO: Count message segments in counter (e.g. using message length)
        public void ResetCounter() {
            _numberSent = 0;
        }
    }
}
