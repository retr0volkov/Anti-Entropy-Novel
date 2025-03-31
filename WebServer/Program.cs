using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

class Program
{
    static async Task Main()
    {
        string url = "http://localhost:80";
        string game = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            game = "\\Game\\";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            game = "/Game/";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            game = "/Game/";
        }
        string contentRoot = Directory.GetCurrentDirectory() + game;
        Console.WriteLine(contentRoot);
        
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddDirectoryBrowser();
        
        var app = builder.Build();

        app.UseDefaultFiles(new DefaultFilesOptions
        {
            DefaultFileNames = new[] { "index.html" },
            FileProvider = new PhysicalFileProvider(contentRoot),
        });
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(contentRoot),
            RequestPath = ""
        });
        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(contentRoot)
        });

        var serverTask = app.RunAsync(url);
        
        await Task.Delay(2000);
        OpenBrowser("http://localhost/");

        await serverTask;
    }

    static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to open browser: {ex.Message}");
        }
    }
}
