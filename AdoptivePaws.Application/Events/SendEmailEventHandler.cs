using MediatR;
using Microsoft.Extensions.Logging;


namespace AdoptivePaws.Application.Events
{
    public class SendEmailEventHandler
        (ILogger<SendEmailEventHandler> logger)
        : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("user created");
        }
    }
}
