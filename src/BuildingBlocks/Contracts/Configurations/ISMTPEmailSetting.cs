using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Configurations
{
    public interface ISMTPEmailSetting
    {
        string Host { get; set; }

        int Port { get; set; }

        string SenderName { get; set; }

        string SenderEmail { get; set; }

        bool EnableSSL { get; set; }

        bool EnableVerification { get; set; }

        string Username { get; set; }

        string Password { get; set; }
    }
}
