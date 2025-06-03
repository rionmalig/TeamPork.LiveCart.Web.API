using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using TeamPork.API.LiveCart.Configuration;
using TeamPork.LiveCart.Core.Configuration;
using TeamPork.LiveCart.Infrastructure.Data.Configuration;
using TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddOData(o => o.Expand()
    .Select()
    .Filter()
    .Count()
    .OrderBy()
    .SetMaxTop(null)
    .SkipToken()
    .AddRouteComponents("odata", ODataConfiguration.GetModelBuilder().GetEdmModel()));

builder.Services.AddAutoMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataServices();
builder.Services.AddCoreServices();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddAuthorization();
// builder.Services.ConfigureOrgins(builder.Configuration); 

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();
app.UseODataRouteDebug();

app.Run();
