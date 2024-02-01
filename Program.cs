using MineSweeperDemo.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<MineSweeperGameService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) =>
{
    if(builder.Environment.IsDevelopment())
    {
        options.AddPolicy("UIPolicy", builder =>
        {
            builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    }
    else
    {
        options.AddPolicy("UIPolicyProd", builder =>
        {
            builder
            .WithOrigins("https://minesweeperui.web.app")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    }

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("UIPolicy");
}
else
{
    app.UseCors("UIPolicyProd");
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
