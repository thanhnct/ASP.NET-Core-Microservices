using Contracts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class SMTPEmailSetting : ISMTPEmailSetting
    {
        public required string Host { get; set; }

        public int Port { get; set; }

        public required string SenderName { get; set; }

        public required string SenderEmail { get; set; }

        public bool EnableSSL { get; set; }

        public bool EnableVerification { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
