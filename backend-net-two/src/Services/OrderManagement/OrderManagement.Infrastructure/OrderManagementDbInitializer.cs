﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class OrderManagementDbInitializer
    {
        private readonly OrderManagementContext _context; 
        private readonly ILogger<OrderManagementDbInitializer> _logger;

        public OrderManagementDbInitializer(OrderManagementContext context, ILogger<OrderManagementDbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void MigrateDatabase()
        {
            _logger.LogInformation("Migrating database associated with HumanResourcesContext");

            try
            {
                //if the sql server container is not created on run docker compose this migration can't fail for network related exception.
                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(new TimeSpan[]
                    {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(8),
                    });
                retry.Execute(() => _context.Database.Migrate());

                _logger.LogInformation("Migrated database associated with HumanResourcesContext");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating the database used on HumanResourcesContext");
            }
        }
    }
}
