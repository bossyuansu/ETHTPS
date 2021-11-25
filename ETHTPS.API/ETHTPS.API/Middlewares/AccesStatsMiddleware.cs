﻿

using ETHTPS.Data.Database;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ETHTPS.API.Middlewares
{
    public class AccesStatsMiddleware
    {
        private readonly RequestDelegate _next;

        public AccesStatsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ETHTPSContext dbContext, ILogger<AccesStatsMiddleware> logger, IConfiguration configuration)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();

            logger.LogInformation($"{context.Connection.Id}: {context.Request.Path}{context.Request.QueryString} ({stopwatch.Elapsed.TotalMilliseconds}ms)");

            try
            {
                var section = configuration.GetSection("Telegram");
                if (section != null)
                {
                    if (context.Request.Method == "GET")
                    {
                        if (context.Request.Path.ToString().Contains("API/v2/Providers"))
                        {
                            var bot = new NetTelegramBotApi.TelegramBot(section.GetValue<string>("Token"), new System.Net.Http.HttpClient());
                            var message = new NetTelegramBotApi.Requests.SendMessage(section.GetValue<long>("ChatID"), $"[{context.Connection.RemoteIpAddress.MapToIPv4()}] - New user");
                            await bot.MakeRequestAsync(message);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                logger.LogError("Error sending Telegram message", e);
            }

            var entry = new AccesStat()
            {
                Count = 1,
                Path = context.Request.PathBase + context.Request.Path + context.Request.QueryString,
                Project = Assembly.GetEntryAssembly().FullName
            };
            if (!dbContext.AccesStats.Any(x=>x.Path == entry.Path && x.Project == entry.Project))
            {
                dbContext.AccesStats.Add(entry);
            }
            else
            {
                var target = dbContext.AccesStats.First(x => x.Path == entry.Path && x.Project == entry.Project);
                target.Count++;

                target.AverageRequestTimeMs = (target.AverageRequestTimeMs * target.Count + stopwatch.Elapsed.TotalMilliseconds) / (target.Count + 1);
                dbContext.AccesStats.Update(target);
            }
            dbContext.SaveChanges();
        }
    }
}
