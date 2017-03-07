using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEComms.Communications.Twilio.User {
    public class UserProfile {
        public const string ACCOUNT_SID_CIPHER = "PKPBJJDXite9Ccsm1Skfj2J3zq9c0v7cD8fzljHVNZYvKemFKEhyihaNgP/swDvE";
        public const string AUTH_TOKEN_CIPHER = "pAP9/hFxQ4ruGvZl5naAihLsOyBcU/kzVtyZnZK7GlAJkDfyh5h49c2vKj/NiUsw";

        /// <summary>
        /// Phone number from which messages are sent.
        /// </summary>
        public const string PHONE_NUMBER_CIPHER = "TSFhK1EaNhtPPJIx4UjYBA==";

        /// <summary>
        /// Phone number of admin where admin texts are sent.
        /// </summary>
        public const string ADMIN_NUMBER_CIPHER = "7eYiH6SvvlNxcvdmgX4+Yg==";
        public List<string> RecipientListCiphers = new List<string>(new string[] { "7eYiH6SvvlNxcvdmgX4+Yg==" });

        public void AddToRecipientCiphers(string recipientNumberCipher) {
            RecipientListCiphers.Add(recipientNumberCipher);
        }
    }
}
