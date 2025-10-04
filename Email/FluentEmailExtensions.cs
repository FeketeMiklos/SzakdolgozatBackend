namespace SzakdolgozatBackend.Email
{
    public static class FluentEmailExtensions
    {
        public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager config)
        {
            var emailSettings = config.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("Port");

            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(host, port)
                .AddRazorRenderer();
        }
    }
}
