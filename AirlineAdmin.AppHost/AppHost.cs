var builder = DistributedApplication.CreateBuilder(args);



var messaging = builder.AddKafka("messaging")
    .WithDataVolume();

var garnetCache = builder.AddGarnet("garnetCache")
    .WithDataVolume();


var financeDbUsername = builder.AddParameter("financeDbUsername", true);
var financeDbPassword = builder.AddParameter("financeDbPassword", true);

var financeDb = builder.AddPostgres("financeDb",financeDbUsername,financeDbPassword)
    .WithDataVolume();


var financeApi = builder.AddProject<Projects.AirlineAdmin_Finance_API>("financeApi")
    .WithReference(financeDb)
    .WaitFor(financeDb)
    .WithReference(garnetCache)
    .WaitFor(garnetCache)
    .WithReference(messaging)
    .WaitFor(messaging);


var userDbUsername = builder.AddParameter("userDbUsername", true);
var userDbPassword = builder.AddParameter("userDbPassword", true);
var userDb = builder.AddPostgres("userDb",userDbUsername,userDbPassword)
    .WithDataVolume();


var userApi = builder.AddProject<Projects.AirlineAdmin_User_API>("userApi")
    .WithReference(userDb)
    .WaitFor(userDb);

var aircraftDb = builder.AddMongoDB("aircraftDb")
    .WithDataVolume();

var aircraftApi = builder.AddProject<Projects.AirlineAdmin_Aircraft_API>("aircraftApi")
    .WithReference(aircraftDb)
    .WaitFor(aircraftDb);


builder.Build().Run();
