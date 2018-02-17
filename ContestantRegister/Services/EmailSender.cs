﻿using System.Threading.Tasks;
using ContestantRegister.Data;

namespace ContestantRegister.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IEmailSender
    {
        private readonly ApplicationDbContext _context;

        public EmailSender(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new Models.Email
            {
                Address = email,
                Subject = subject,
                Message = message
            };
            _context.Emails.Add(mail);
            await _context.SaveChangesAsync();
        }
    }
}
