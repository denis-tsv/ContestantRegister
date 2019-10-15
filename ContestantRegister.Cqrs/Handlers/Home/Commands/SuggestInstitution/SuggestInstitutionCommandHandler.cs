using System.Threading.Tasks;
using ContestantRegister.Application.Handlers.Frontend.Handlers.Home.Commands;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Services.InfrastructureServices;
using ContestantRegister.Utils;
using Microsoft.Extensions.Options;

namespace ContestantRegister.Application.Handlers.Frontend.Handlers.Home.CommansHandlers
{
    public class SuggestInstitutionCommandHandler : ICommandHandler<SuggestInstitutionCommand>
    {
        private readonly IEmailSender _emailSender;
        private readonly SuggestStudyPlaceOptions _options;

        public SuggestInstitutionCommandHandler(IEmailSender emailSender, IOptions<SuggestStudyPlaceOptions> options)
        {
            _emailSender = emailSender;
            _options = options.Value;
        }

        public async Task HandleAsync(SuggestInstitutionCommand command)
        {
            var body = $"Предложил {command.ViewModel.Email}<br>" +
                       $"Краткое название {command.ViewModel.ShortName}<br>" +
                       $"Полное название {command.ViewModel.FullName}<br>" +
                       $"Регион {command.ViewModel.Region}<br>" +
                       $"Город {command.ViewModel.City}<br>" +
                       $"Краткое название англ {command.ViewModel.ShortNameEn}<br>" +
                       $"Полное название англ {command.ViewModel.FullNameEn}<br>" +
                       $"Сайт {command.ViewModel.Site}<br>";

            await _emailSender.SendEmailAsync(_options.Email, "Новый вуз", body);
        }
    }
}
