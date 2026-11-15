var builder = DistributedApplication.CreateBuilder(args);


var messaging = builder.AddKafka("messaging")
    .WithDataVolume();

var garnetCache = builder.AddGarnet("garnetCache")
    .WithDataVolume();


var financeDbUsername = builder.AddParameter("financeDbUsername", true);
var financeDbPassword = builder.AddParameter("financeDbPassword", true);

var financeDbContainer = builder.AddPostgres("financeDb",financeDbUsername,financeDbPassword)
    .WithDataVolume();
var financeDb = financeDbContainer.AddDatabase("AirlineAdmin");


var financeApi = builder.AddProject<Projects.AirlineAdmin_FinanceAPI>("financeApi")
    .WithReference(financeDb)
    .WaitFor(financeDb)
    .WithHttpHealthCheck("/health");




builder.Build().Run();
