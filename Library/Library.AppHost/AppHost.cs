var builder = DistributedApplication.CreateBuilder(args);

var sqlDb = builder.AddSqlServer("library-sql-server")
                 .AddDatabase("LibraryDb");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
                      .WithManagementPlugin();

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
       .WithReference(sqlDb, "Connection")
       .WithReference(rabbitmq)
       .WaitFor(sqlDb)
       .WaitFor(rabbitmq);

builder.AddProject<Projects.Library_DataGenerator>("library-data-generator")
       .WithReference(rabbitmq)
       .WaitFor(rabbitmq)
       .WithEnvironment("GeneratorOptions__IntervalMs", "2000")
       .WithEnvironment("GeneratorOptions__MaxBookId", "20")
       .WithEnvironment("GeneratorOptions__MaxReaderId", "20")
       .WithEnvironment("GeneratorOptions__MinDays", "7")
       .WithEnvironment("GeneratorOptions__MaxDays", "30")
       .WithEnvironment("GeneratorOptions__MessagesPerIteration", "1")
       .WithEnvironment("GeneratorOptions__QueueName", "book-issues-queue");

builder.Build().Run();