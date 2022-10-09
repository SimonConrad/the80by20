using Microsoft.Extensions.Configuration;

namespace the80by20.Sandbox.Azure
{
    public class AzureServiceBusTests
    {
        [Fact]
        public async Task QueueSendTest()
        {
            var config = InitConfiguration();
            var azureServiceConnectionString = config.GetSection("azureServiceConnectionString").Value;
            MessagesSender.ConnectionString = azureServiceConnectionString;

            await MessagesSender.Send();
        }

        [Fact]
        public async Task QueueReceiveTest()
        {
            var config = InitConfiguration();
            var azureServiceConnectionString = config.GetSection("azureServiceConnectionString").Value;
            MessagesReceiver.ConnectionString = azureServiceConnectionString;

            await MessagesReceiver.Receive();
        }


        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets("fe5caf95-024a-4333-ac92-6a3941d49e67")
                .Build();
            return config;
        }

        //public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        //{
        //    var r =  new ConfigurationBuilder()
        //        .SetBasePath(outputPath)
        //        .AddJsonFile("appsettings.json", optional: true)
        //        .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
        //        .AddEnvironmentVariables()
        //        .Build();
        //}

        //public static KavaDocsConfiguration GetApplicationConfiguration(string outputPath)
        //{
        //    var configuration = new KavaDocsConfiguration();

        //    var iConfig = GetIConfigurationRoot(outputPath);

        //    iConfig
        //        .GetSection("KavaDocs")
        //        .Bind(configuration);

        //    return configuration;
        //}
    }
}
