using Library.DataGenerator.Options;
using Library.DataGenerator.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddRabbitMQClient("rabbitmq", configureConnectionFactory: factory =>
{
    factory.AutomaticRecoveryEnabled = true;
    factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
});

builder.Services.Configure<GeneratorOptions>(
    builder.Configuration.GetSection(GeneratorOptions.SectionName));

builder.Services.AddSingleton<BookIssueGenerator>();

var host = builder.Build();
host.Run();