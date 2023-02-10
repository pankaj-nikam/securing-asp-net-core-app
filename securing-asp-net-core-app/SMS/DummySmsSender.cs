namespace securing_asp_net_core_app.SMS
{
    //NOTE: 1B
    public class DummySmsSender : ISmsSender
    {
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            Console.WriteLine($"Sending message to {phoneNumber} with message {message}");
            return Task.CompletedTask;
        }
    }
}
