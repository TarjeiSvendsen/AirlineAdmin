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


var financeApi = builder.AddProject<Projects.AirlineAdmin_Finance_API>("financeApi")
    .WithReference(financeDb)
    .WaitFor(financeDb)
    .WithReference(garnetCache)
    .WaitFor(garnetCache)
    .WithReference(messaging)
    .WaitFor(messaging);


var userDbContainer = builder.AddMongoDB("financeDb")
    .WithDataVolume();
var userDb = userDbContainer.AddDatabase("AirlineAdmin");


var userApi = builder.AddProject<Projects.AirlineAdmin_User_API>("userApi")
    .WithReference(userDb)
    .WaitFor(userDb);


builder.Build().Run();
