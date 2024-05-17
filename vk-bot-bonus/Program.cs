using vk_bot_bonus.Service;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

var builder = WebApplication.CreateBuilder(args);

// Добавление и настройка VkApi
builder.Services.AddSingleton<IVkApi>(sp => 
{
    var api = new VkApi();
    api.Authorize(new ApiAuthParams
    {
        AccessToken = "YOUR_ACCESS_TOKEN"
    });
    return api;
});

// Добавление и настройка ExcelService
builder.Services.AddSingleton(new ExcelService("path/to/your/excel/file.xlsx"));

// Add services to the container.
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints?.MapControllers();
});

app.Run();