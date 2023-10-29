using CreditCardApi.DataAccess;
using CreditCardApi.DataAccess.Repositories;
using CreditCardApi.Models;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBCreditCardMock>(opt =>
    opt.UseInMemoryDatabase("CreditCardMock")
);

//DI
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();

builder.Services.AddRateLimiter(options => {
    options.RejectionStatusCode = 429;
    options.OnRejected = async (context, token) =>
    {
        await context.HttpContext.Response.WriteAsync("muchas llamadas, por favor prueba mas tarde ");
    };
    options.AddFixedWindowLimiter("Web", options =>
    {
        options.AutoReplenishment = true;
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromMinutes(1);
    });
    options.AddConcurrencyLimiter(policyName: "concurrency", options => {
        options.PermitLimit = 10;
        options.QueueLimit = 0;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    
});

var app = builder.Build();
app.UseRateLimiter();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
