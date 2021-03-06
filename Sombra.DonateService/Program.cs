﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Sombra.DonateService.DAL;
using Sombra.Infrastructure.DAL;
using Sombra.Infrastructure.Extensions;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    class Program
    {
        private static string _rabbitMqConnectionString;
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine("Donate Service Started");

            SetupConfiguration();

            var serviceProvider = ServiceInstaller.Run(
                Assembly.GetExecutingAssembly(),
                _rabbitMqConnectionString,
                services => services
                    .AddDbContext<DonationsContext>(_sqlConnectionString)
                    .AddAutoMapper(Assembly.GetExecutingAssembly()),
                DatabaseHelper.RunInstaller);

            Thread.Sleep(Timeout.Infinite);
        }
        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
                _sqlConnectionString = Environment.GetEnvironmentVariable("DONATIONS_DB_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
                _sqlConnectionString = config["DONATIONS_DB_CONNECTIONSTRING"];
            }
        }
    }
}