﻿using CEComms.Communications.Indicators;
using CEComms.Communications.SendGrid.User;
using CommentEverythingCryptography.Encryption;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockiment.Communications.SendGrid.CEEmail {
    public class EmailSender {
        public EmailSender() {
            MaxSend = 20;
            CharLimit = 1000000;
        }

        public int MaxSend { get; set; }
        public int CharLimit { get; set; }
        private int _numberSent = 0;
        public SendFlag SendFlag = new SendFlag();
        public UserProfile User = new UserProfile();

        public void SendEmail(string subject, string msg) {
            if (SendFlag.ShouldSend()) {
                IEncryptionProvider decryptor = EncryptionProviderFactory.CreateInstance(EncryptionProviderFactory.CryptographyMethod.AES);

                SendGridAPIClient sg = new SendGridAPIClient(decryptor.Decrypt(User.APIKeyCipher));
                Email from = new Email(decryptor.Decrypt(User.FromEmailCipher));
                Email admin = new Email(decryptor.Decrypt(User.AdminEmailCipher));

                foreach (string addrCipher in User.RecipientListCiphers) {
                    Email to = new Email(decryptor.Decrypt(addrCipher));

                    if (_numberSent < MaxSend) {
                        if (msg.Length <= CharLimit) {
                            Content content = new Content("text/plain", msg);
                            Mail mail = new Mail(from, subject, to, content);
                            sg.client.mail.send.post(requestBody: mail.Get());
                        } else {
                            subject = "Undeliverable Mail";
                            Content content = new Content("text/plain", "Email not delivered to " + decryptor.Decrypt(addrCipher) + ". Email message length greater than maximum number of characters of " + CharLimit.ToString());
                            Mail mail = new Mail(from, subject, admin, content);
                            sg.client.mail.send.post(requestBody: mail.Get());
                        }
                    } else if (_numberSent == MaxSend) {
                        subject = "Undeliverable Mail";
                        Content content = new Content("text/plain", "Email not delivered to " + decryptor.Decrypt(addrCipher) + ". Number of email messages sent in specified time period has reached the limit of " + MaxSend.ToString());
                        Mail mail = new Mail(from, subject, admin, content);
                        sg.client.mail.send.post(requestBody: mail.Get());
                    }
                    _numberSent++;
                }
            }
        }

        // TODO: Counter reset as function of this class
        // TODO: Count message segments in counter (e.g. using message length)
        public void ResetCounter() {
            _numberSent = 0;
        }
    }
}
