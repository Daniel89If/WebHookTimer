using WebHookTimer.Interfaces;
using WebHookTimer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<TimerRequestManagment>();
builder.Services.AddSingleton<IDbManager, SqlDbManager>();
builder.Services.AddSingleton<IExpiredTime, ExpiredTimeHandler>();
builder.Services.AddSingleton<IJobManagment, JobManagment>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var jobManagment = scope.ServiceProvider.GetRequiredService<IJobManagment>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
