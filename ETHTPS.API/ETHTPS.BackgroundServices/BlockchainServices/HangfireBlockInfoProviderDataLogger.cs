﻿using ETHTPS.Data.Database;
using ETHTPS.Services.BlockchainServices.Extensions;

using Hangfire;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHTPS.Services.BlockchainServices
{
    public class HangfireBlockInfoProviderDataLogger<T> : HangfireBackgroundService
        where T : IBlockInfoProvider
    {
        private readonly T _instance;
        private readonly string _provider;
        private readonly int _providerID;

        public HangfireBlockInfoProviderDataLogger(T instance, ILogger<HangfireBackgroundService> logger, ETHTPSContext context) : base(logger, context)
        {
            _instance = instance;
            _provider = _instance.GetProviderName();
            _providerID = _context.Providers.First(x => x.Name == _provider).Id;
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public override async Task RunAsync()
        {
            try
            {
                var delta = await CalculateTPSGPSAsync();
                UpdateMaxEntry(delta);

                AddOrUpdateHourTPSEntry(delta);
                AddOrUpdateDayTPSEntry(delta);
                AddOrUpdateWeekTPSEntry(delta);
                AddOrUpdateMonthTPSEntry(delta);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"{_provider}: {delta.TPS}TPS {delta.GPS}GPS");
            }
            catch (Exception e)
            {
                _logger.LogError("TPSDataUpdaterBase", e);
                throw;
            }
        }

        private async Task<TPSGPSInfo> CalculateTPSGPSAsync()
        {
            var latestBlock = await _instance.GetLatestBlockInfoAsync();
            if (_instance.BlockTimeSeconds > 0)
            {
                return new TPSGPSInfo()
                {
                    BlockNumber = latestBlock.BlockNumber,
                    Date = latestBlock.Date,
                    GPS = latestBlock.GasUsed / _instance.BlockTimeSeconds,
                    TPS = latestBlock.TransactionCount / _instance.BlockTimeSeconds
                };
            }
            else //Add up all blocks submitted at the same time
            {
                var result = new TPSGPSInfo() 
                {
                    Date = latestBlock.Date
                };
                BlockInfo secondToLatestBlock;
                int count = 0;
                do
                {
                    result.TPS += latestBlock.TransactionCount;
                    result.GPS += latestBlock.GasUsed;

                    secondToLatestBlock = await _instance.GetBlockInfoAsync(latestBlock.BlockNumber - 1);
                    if (secondToLatestBlock.Date.Subtract(latestBlock.Date).TotalSeconds != 0)
                    {
                        result.TPS /= Math.Abs(secondToLatestBlock.Date.Subtract(result.Date).TotalSeconds);
                        result.GPS /= Math.Abs(secondToLatestBlock.Date.Subtract(result.Date).TotalSeconds);
                        break;
                    }
                    latestBlock = secondToLatestBlock;

                    if (++count == 10)
                    {
                        throw new Exception("Possible infinite loop");
                    }
                }
                while (true);
                return result;
            }
        }

        public void UpdateMaxEntry(TPSGPSInfo entry)
        {
            Func<TpsandGasDataMax, bool> selector = x => x.ProviderNavigation.Name == _provider && x.NetworkNavigation.Name == "Mainnet";
            if (!_context.TpsandGasDataMaxes.Any(selector))
            {
                _context.TpsandGasDataMaxes.Add(new TpsandGasDataMax()
                {
                    Date = entry.Date,
                    MaxGps = entry.GPS,
                    MaxTps = entry.TPS,
                    Network = 1,
                    Provider = _providerID
                });
            }
            else
            {
                var targetEntry = _context.TpsandGasDataMaxes.First(selector);
                if (entry.TPS > targetEntry.MaxTps)
                {
                    targetEntry.MaxTps = entry.TPS;
                }
                if (entry.GPS > targetEntry.MaxGps)
                {
                    targetEntry.MaxGps = entry.GPS;
                }
                _context.TpsandGasDataMaxes.Update(targetEntry);
            }
        }

        public void AddOrUpdateHourTPSEntry(TPSGPSInfo entry)
        {
            var targetDate = entry.Date
                .Subtract(TimeSpan.FromSeconds(entry.Date.Second))
                .Subtract(TimeSpan.FromMilliseconds(entry.Date.Millisecond));
            Func<TpsandGasDataHour, bool> selector = x => x.NetworkNavigation.Name == "Mainnet" && x.Provider == _providerID && x.StartDate.Minute == targetDate.Minute;
            if (!_context.TpsandGasDataHours.Any(selector))
            {
                _context.TpsandGasDataHours.Add(new TpsandGasDataHour()
                {
                    Network = 1,
                    AverageTps = entry.TPS,
                    AverageGps = entry.GPS,
                    Provider = _providerID,
                    StartDate = targetDate,
                    ReadingsCount = 1
                });
            }
            else
            {
                var x = _context.TpsandGasDataHours.First(selector);
                if (x.StartDate.Hour == targetDate.Hour)
                {
                    x.AverageTps = ((x.AverageTps * x.ReadingsCount) + entry.TPS) / ++x.ReadingsCount;
                    x.AverageGps = ((x.AverageGps * x.ReadingsCount) + entry.GPS) / ++x.ReadingsCount;
                }
                else
                {
                    x.AverageTps = entry.TPS;
                    x.AverageGps = entry.GPS;
                    x.ReadingsCount = 1;
                    x.StartDate = entry.Date;
                }
                _context.TpsandGasDataHours.Update(x);
            }
        }
        public void AddOrUpdateDayTPSEntry(TPSGPSInfo entry)
        {
            var targetDate = entry.Date
                .Subtract(TimeSpan.FromSeconds(entry.Date.Second))
                .Subtract(TimeSpan.FromMilliseconds(entry.Date.Millisecond))
                .Subtract(TimeSpan.FromMinutes(entry.Date.Minute));
            Func<TpsandGasDataDay, bool> selector = x => x.NetworkNavigation.Name == "Mainnet" && x.Provider == _providerID && x.StartDate.Hour == targetDate.Hour;
            if (!_context.TpsandGasDataDays.Any(selector))
            {
                _context.TpsandGasDataDays.Add(new TpsandGasDataDay()
                {
                    Network = 1,
                    AverageTps = entry.TPS,
                    AverageGps = entry.GPS,
                    Provider = _providerID,
                    StartDate = targetDate,
                    ReadingsCount = 1
                });
            }
            else
            {
                var x = _context.TpsandGasDataDays.First(selector);
                if (x.StartDate.Day == targetDate.Day)
                {
                    x.AverageTps = ((x.AverageTps * x.ReadingsCount) + entry.TPS) / ++x.ReadingsCount;
                    x.AverageGps = ((x.AverageGps * x.ReadingsCount) + entry.GPS) / ++x.ReadingsCount;
                }
                else
                {
                    x.AverageTps = entry.TPS;
                    x.AverageGps = entry.GPS;
                    x.ReadingsCount = 1;
                    x.StartDate = entry.Date;
                }
                _context.TpsandGasDataDays.Update(x);
            }
        }
        public void AddOrUpdateWeekTPSEntry(TPSGPSInfo entry)
        {
            var targetDate = entry.Date
                .Subtract(TimeSpan.FromSeconds(entry.Date.Second))
                .Subtract(TimeSpan.FromMilliseconds(entry.Date.Millisecond))
                .Subtract(TimeSpan.FromMinutes(entry.Date.Minute));
            Func<TpsandGasDataWeek, bool> selector = x => x.NetworkNavigation.Name == "Mainnet" && x.Provider == _providerID && x.StartDate.Hour == targetDate.Hour && x.StartDate.DayOfWeek == targetDate.DayOfWeek;
            if (!_context.TpsandGasDataWeeks.Any(selector))
            {
                _context.TpsandGasDataWeeks.Add(new TpsandGasDataWeek()
                {
                    Network = 1,
                    AverageTps = entry.TPS,
                    AverageGps = entry.GPS,
                    Provider = _providerID,
                    StartDate = targetDate,
                    ReadingsCount = 1
                });
            }
            else
            {
                var x = _context.TpsandGasDataWeeks.First(selector);
                if (x.StartDate.Day == targetDate.Day)
                {
                    x.AverageTps = ((x.AverageTps * x.ReadingsCount) + entry.TPS) / ++x.ReadingsCount;
                    x.AverageGps = ((x.AverageGps * x.ReadingsCount) + entry.GPS) / ++x.ReadingsCount;
                }
                else
                {
                    x.AverageTps = entry.TPS;
                    x.AverageGps = entry.GPS;
                    x.ReadingsCount = 1;
                    x.StartDate = entry.Date;
                }
                _context.TpsandGasDataWeeks.Update(x);
            }
        }
        public void AddOrUpdateMonthTPSEntry(TPSGPSInfo entry)
        {
            var targetDate = entry.Date
                .Subtract(TimeSpan.FromSeconds(entry.Date.Second))
                .Subtract(TimeSpan.FromMilliseconds(entry.Date.Millisecond))
                .Subtract(TimeSpan.FromMinutes(entry.Date.Minute))
                .Subtract(TimeSpan.FromHours(entry.Date.Hour));
            Func<TpsandGasDataMonth, bool> selector = x => x.NetworkNavigation.Name == "Mainnet" && x.Provider == _providerID && x.StartDate.Day == targetDate.Day;
            if (!_context.TpsandGasDataMonths.Any(selector))
            {
                _context.TpsandGasDataMonths.Add(new TpsandGasDataMonth()
                {
                    Network = 1,
                    AverageTps = entry.TPS,
                    AverageGps = entry.GPS,
                    Provider = _providerID,
                    StartDate = targetDate,
                    ReadingsCount = 1
                });
            }
            else
            {
                var x = _context.TpsandGasDataMonths.First(selector);
                if (x.StartDate.Month == targetDate.Month)
                {
                    x.AverageTps = ((x.AverageTps * x.ReadingsCount) + entry.TPS) / ++x.ReadingsCount;
                    x.AverageGps = ((x.AverageGps * x.ReadingsCount) + entry.GPS) / ++x.ReadingsCount;
                }
                else
                {
                    x.AverageTps = entry.TPS;
                    x.AverageGps = entry.GPS;
                    x.ReadingsCount = 1;
                    x.StartDate = entry.Date;
                }
                _context.TpsandGasDataMonths.Update(x);
            }
        }
    }
}
