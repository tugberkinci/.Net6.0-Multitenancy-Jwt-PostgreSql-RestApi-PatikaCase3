using PatikaHomework3.Dto.Dto;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
