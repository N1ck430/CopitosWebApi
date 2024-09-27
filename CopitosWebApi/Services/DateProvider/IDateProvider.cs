namespace CopitosWebApi.Services.DateProvider;

public interface IDateProvider
{
    public DateTime UtcNow { get; }
}