using Microsoft.EntityFrameworkCore;
using SportLize.Profile.Api.Profile.Business;
using SportLize.Profile.Api.Profile.Business.Abstraction;
using SportLize.Profile.Api.Profile.Business.Profiles;
using SportLize.Profile.Api.Profile.Repository;
using SportLize.Profile.Api.Profile.Repository.Abstraction;
using SportLize.Profile.Api.Profile.Business.Kafka;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProfileDbContext>(options => options.UseSqlServer("name=ConnectionStrings:ProfileDbContext",
    b => b.MigrationsAssembly("SportLize.Profile.Api")));

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();

object value = builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddKafkaProducerService<KafkaTopicsOutput, ProducerService>(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
