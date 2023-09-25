using SeaCadetsWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("recaptchaClient", r => r.BaseAddress = new System.Uri("https://www.google.com"));
builder.Services.AddTransient<IRecaptchaService, RecaptchaService>();

var DevelopmentOrigins = "_developmentOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DevelopmentOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost", "https://localhost:7122");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(DevelopmentOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
