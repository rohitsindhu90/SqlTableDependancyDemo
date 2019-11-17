using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRCore.Web.Hubs;
using EmployeeService.Domain.Model;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using Microsoft.Extensions.Logging;

namespace SignalRCore.Web.Hubs
{
    public class InventoryDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        //private readonly IInventoryRepository _repository;
        //private readonly IHubContext<Inventory> _hubContext;
        private SqlTableDependency<Employee> _tableDependency;
        private readonly ILogger<InventoryDatabaseSubscription> _logger;
        //public InventoryDatabaseSubscription(IInventoryRepository repository, IHubContext<Inventory> hubContext)
        //{
        //    _repository = repository;
        //    _hubContext = hubContext;            
        //}
        public InventoryDatabaseSubscription(ILogger<InventoryDatabaseSubscription> logger)
        {
            _logger = logger;
        }
        public void Configure(string connectionString)
        {
            var mapper = new ModelToTableMapper<Employee>();
            mapper.AddMapping(c => c.EmployeeId, "EmployeeId");
            mapper.AddMapping(c => c.FirstName, "FirstName");

            _tableDependency = new SqlTableDependency<Employee>(connectionString, "EmployeeDB", notifyOn: DmlTriggerType.All,mapper:mapper);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
            _logger.LogInformation("Waiting for receiving notifications...");
            Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(
            object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity

                var changedEntity = e.Entity;
                //_hubContext.Clients.All.InvokeAsync("UpdateCatalog", _repository.Products);
            }
        }

        #region IDisposable

        ~InventoryDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
