namespace CopitosWebApi.Services.DateProvider;

public class DateProvider : IDateProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}