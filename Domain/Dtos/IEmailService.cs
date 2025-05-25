namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}