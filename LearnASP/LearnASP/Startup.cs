using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnASP.Web;

public class Startup
{
    public void ConfigureServices(IServiceCollection env)
    {

    }

    public void Configure(
        IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseDefaultFiles(); //index.html

        app.UseStaticFiles();

        app.UseEndpoints(enpoints =>
        {
            enpoints.MapGet("/hello", () => "Hello World!");
        });
    }        
}
