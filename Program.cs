var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowOrigin"); // Agrega esta línea para habilitar CORS
app.UseAuthorization();
app.MapControllers();
app.Run();