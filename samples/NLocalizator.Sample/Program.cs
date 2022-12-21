using NLocalizator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddLocalizator<WeatherForecastLocalizationBook>(builder.Configuration);
// builder.Services.AddLocalizator<DaysLocalizationBook>(builder.Configuration);
// //TODO: birden fazla options objesi gerektiğinde appSettings'e nasıl yazılacak, nasıl okunacak?

builder.Services.AddLocalizator<WeatherForecastLocalizationBook>(options => 
    options.AddFolderPath(@"C:\Users\tcakir\Desktop\OKR\Localizator\samples\NLocalizator.Sample\Lang\Weather")
    .AddLanguage("tr"));

builder.Services.AddLocalizator<DaysLocalizationBook>(options => 
    options.AddFolderPath(@"C:\Users\tcakir\Desktop\OKR\Localizator\samples\NLocalizator.Sample\Lang\Days")
    .AddLanguage("tr"));


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
