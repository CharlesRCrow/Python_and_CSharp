using LearnASP.Web;

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");

//app.Run();

//if (!app.Environment.IsDevelopment())
//{
 //   app.UseHsts();
//}

Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
}).Build().Run();

Console.WriteLine("This executes after the web server has stopped!");