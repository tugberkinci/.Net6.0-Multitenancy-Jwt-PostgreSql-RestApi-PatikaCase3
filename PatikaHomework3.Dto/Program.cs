using PatikaHomework3.Dto.Dto;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
