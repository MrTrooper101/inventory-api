namespace inventory_api.Infastructure.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken cancellationToken);
    }
}
