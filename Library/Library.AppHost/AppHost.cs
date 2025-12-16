var builder = DistributedApplication.CreateBuilder(args);

var sqlDb = builder.AddSqlServer("library-sql-server")
                 .AddDatabase("LibraryDb");

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
       .WithReference(sqlDb, "Connection")
       .WaitFor(sqlDb);

builder.Build().Run();