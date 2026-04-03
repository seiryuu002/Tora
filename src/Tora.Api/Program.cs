using Tora.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddToraDb();

var app = builder.Build();
await app.MigrateDatabaseAsync();
app.Run();
