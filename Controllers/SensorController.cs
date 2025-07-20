using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeThingSpeak.Data;
using RealTimeThingSpeak.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class SensorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;

    public SensorController(ApplicationDbContext context)
    {
        _context = context;
        _httpClient = new HttpClient();
    }

    public async Task<IActionResult> Index()
    {
        string channelId = "2883041";  
        string apiKey = "9E4WL1H6VDC2HQHM"; 
        string url = $"https://api.thingspeak.com/channels/{channelId}/feeds.json?api_key={apiKey}&results=10";
        
        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var jsonData = JsonDocument.Parse(response);
            var feeds = jsonData.RootElement.GetProperty("feeds");

            foreach (var feed in feeds.EnumerateArray())
            {
#pragma warning disable CS8604 // Possible null reference argument.
                DateTime createdAt = DateTime.Parse(feed.GetProperty("created_at").GetString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null refe
                double field1 = double.Parse(feed.GetProperty("field1").GetString());
#pragma warning restore CS8604 // Possible null reference argument.

                // Check if the data already exists to avoid duplicates
                if (!_context.Feeds.Any(f => f.CreatedAt == createdAt))
                {
                    var newFeed = new Feed
                    {
                        CreatedAt = createdAt,
                        Field1 = field1
                    };

                    _context.Feeds.Add(newFeed);
                }
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching data: " + ex.Message);
        }

        var storedData = await _context.Feeds.OrderByDescending(f => f.CreatedAt).Take(10).ToListAsync();
        return View(storedData);
    }
}
