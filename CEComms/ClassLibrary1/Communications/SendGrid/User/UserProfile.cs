using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEComms.Communications.SendGrid.User {
    public class UserProfile {
        public UserProfile() {
            FromEmailCipher = DEFAULT_FROM_EMAIL_CIPHER;
            AdminEmailCipher = DEFAULT_ADMIN_EMAIL_CIPHER;
        }

        private const string API_KEY_CIPHER = "vZZswAyoKBvDmSIf8Uso4r14Yen35On52P2rybFzhAjQTyFvjUjxtxOtw5EezQVjSl9KJDu1scyVJeYRRUcltYFqud7wkgjftHFyKEkIYyk =";

        private const string DEFAULT_FROM_EMAIL_CIPHER = "U48Ul4Ozn6/k44sqAIL9hjh/YC/hFSi1okBtaKehUAA=";

        private const string DEFAULT_ADMIN_EMAIL_CIPHER = "T97nnD8qgDCMvR2xFTqBI0B+UYBqW7CQrZv2NGowfjA=";

        public string APIKeyCipher { get { return API_KEY_CIPHER; } }
        public string FromEmailCipher { get; set; }
        public string AdminEmailCipher { get; set; }
        

        public List<string> RecipientListCiphers = new List<string>(new string[] { "T97nnD8qgDCMvR2xFTqBI0B+UYBqW7CQrZv2NGowfjA=" });

        public void AddToRecipientCiphers(string recipientEmailCipher) {
            RecipientListCiphers.Add(recipientEmailCipher);
        }
    }
}
