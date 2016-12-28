using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEComms.Communications.Indicators {
    public class SendFlag {
        public SendFlag() {
            AppSettingName = "SEND_NOTIFICATIONS";
        }

        public enum SendOption {
            ALWAYS,
            IF_ENV_FLAG_ON
        }

        public string AppSettingName { get; set; }

        public bool ShouldSend() {
            string appSettingValue = ConfigurationManager.AppSettings[AppSettingName];

            if (appSettingValue == null) {
                return true;
            } else if (string.Equals(appSettingValue, "{ENV_VAR}", StringComparison.InvariantCultureIgnoreCase)) {
                return bool.Parse(Environment.GetEnvironmentVariable(AppSettingName));
            } else {
                return bool.Parse(appSettingValue);
            }
        }
    }
}
