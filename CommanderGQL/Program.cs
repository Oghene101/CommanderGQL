using CommanderGQL.Data;
using CommanderGQL.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
        .UseSnakeCaseNamingConvention());

builder.Services.AddGraphQLServer()
    .AddTypes()
    .AddInMemorySubscriptions()
    .ModifyPagingOptions(options =>
    {
        options.DefaultPageSize = 10;
        options.IncludeTotalCount = true;
        options.MaxPageSize = 10;
    })
    .AddProjections()
    .AddFiltering()
    .AddSorting();


var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();


app.Run();