using Framework.Extensions;
using Framework.Middleware;
using TiktokLocalAPI.Core.Helpers;
using TiktokLocalAPI.Data.Database;
using TiktokLocalAPI.Hubs;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer();

// Swagger XML documentation
services.AddXmlDocs();

// MySQL Database connection
services.AddMySQL();

// Repositories
services.AddIdentityRepos();
services.AddServiceRepos();
services.AddChatRepos();
services.AddOrderRepos();

// Services
services.AddIdentityServices();
services.AddServiceServices();
services.AddChatServices();
services.AddOrderServices();
services.AddFileServices();
services.AddStripeInfrastructure(builder.Configuration);

// Authentication
services.AddJwtAuthentication(builder.Configuration);

// Add CORS service
builder.Services.AddCustomCors();

// START THE APP
var app = builder.Build();

// Middleware and Endpoint Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

PrepareDb.PreparePopulation(app);

app.MapHub<ChatHub>("/api/chatHub");
app.Run();
