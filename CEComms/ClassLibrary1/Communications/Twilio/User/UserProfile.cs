using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEComms.Communications.Twilio.User {
    public class UserProfile {
        public const string ACCOUNT_SID_CIPHER = "PKPBJJDXite9Ccsm1Skfj2J3zq9c0v7cD8fzljHVNZYvKemFKEhyihaNgP/swDvE";
        public const string AUTH_TOKEN_CIPHER = "pAP9/hFxQ4ruGvZl5naAihLsOyBcU/kzVtyZnZK7GlAJkDfyh5h49c2vKj/NiUsw";

        public const string PHONE_NUMBER_CIPHER = "TSFhK1EaNhtPPJIx4UjYBA==";

        public const string ADMIN_NUMBER_CIPHER = "7eYiH6SvvlNxcvdmgX4+Yg==";
        public static List<string> RecipientListCiphers = new List<string>(new string[] { "7eYiH6SvvlNxcvdmgX4+Yg==" });

        public static void AddToRecipientCiphers(string recipientNumberCipher) {
            RecipientListCiphers.Add(recipientNumberCipher);
        }
    }
}
