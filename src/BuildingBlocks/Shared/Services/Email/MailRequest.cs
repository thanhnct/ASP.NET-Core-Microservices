using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Email
{
    public class MailRequest
    {
        [EmailAddress]
        public required string FromEmail { get; set; }

        [EmailAddress]
        public required string ToEmail { get; set; }

        public IEnumerable<string> ToEmails { get; set; } = new List<string>();

        public required string Subject { get; set; }

        public string? Body { get; set; }

        public bool IsBodyHtml { get; set; } = true;

        public IFormCollection? Attachments { get; set; }
    }
}
