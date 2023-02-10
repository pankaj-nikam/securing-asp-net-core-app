namespace securing_asp_net_core_app.SMS
{
    //NOTE: 1A
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
