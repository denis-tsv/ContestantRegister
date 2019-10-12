using System.Threading.Tasks;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Services.InfrastructureServices;

namespace ContestantRegister.Infrastructure.Implementation
{
    internal class EmailSender : IEmailSender
    {
        private readonly IRepository _repository;

        public EmailSender(IRepository repository)
        {
            _repository = repository;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new Models.Email
            {
                Address = email,
                Subject = subject,
                Message = message,
                CreateDate = DateTimeService.SfuServerNow,
            };
            _repository.Add(mail);
            await _repository.SaveChangesAsync();
        }
    }
}
