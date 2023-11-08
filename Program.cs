using CreditCardApi.DataAccess;
using CreditCardApi.DataAccess.Repositories;
using CreditCardApi.Models;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBCreditCardMock>(opt =>
    opt.UseInMemoryDatabase("CreditCardMock")
);
builder.Services.AddOutputCache(options => {
    options.AddBasePolicy(policy => policy
        .Expire(TimeSpan.FromMinutes(10)));
    options.AddPolicy("PicturePolicy", policy => policy
        .Expire(TimeSpan.FromMinutes(5)));

    
}
    
);

//DI
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();

builder.Services.AddRateLimiter(options => {
    options.RejectionStatusCode = 429;
    options.AddFixedWindowLimiter("FixedWindow", options =>
    {
        options.AutoReplenishment = true;
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromMinutes(1);
    });
    options.AddConcurrencyLimiter("Concurrency", options => {
        options.PermitLimit = 1;
        options.QueueLimit = 0;
        options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;    
    });
    options.AddSlidingWindowLimiter("SlidingWindow", limiterOptions =>
    {
        limiterOptions.PermitLimit = 2;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
        limiterOptions.QueueLimit = 1;
        limiterOptions.SegmentsPerWindow = 5;
        limiterOptions.AutoReplenishment = true;
        limiterOptions.Window = TimeSpan.FromSeconds(15);
    });
    options.AddTokenBucketLimiter("TokenBucket", limiterOptions =>
    {
        limiterOptions.TokensPerPeriod = 2;
        limiterOptions.TokenLimit = 10;
        limiterOptions.QueueLimit = 0;
        limiterOptions.AutoReplenishment = true;    
        limiterOptions.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
    });
});

/* using(var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger())
{
    log.Information("Hello, Serilog");
    log.Warning("Goodbye");
} */

var app = builder.Build();

app.UseRateLimiter();

app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

