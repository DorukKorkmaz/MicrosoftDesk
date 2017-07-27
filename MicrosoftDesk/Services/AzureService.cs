using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MicrosoftHelpDesk.Models;

namespace MicrosoftDesk.Services
{
    public class AzureService
    {
        MobileServiceClient client { get; set; }
        IMobileServiceSyncTable<Request> table;


        public async Task Initialize()
        {
            if (client?.SyncContext?.IsInitialized ?? false) return;

            var azureUrl = "https://microsofthelpdesk.azurewebsites.net";

            //Create our client
            client = new MobileServiceClient(azureUrl);

            //InitializeDatabase for path
            var path = "requestDatabase.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and initialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<Request>();

            //Initialize SyncContext
            await client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            table = client.GetSyncTable<Request>();

            var current = table.ToListAsync();
        }

        public async Task<List<Request>> GetRequests()
        {
            await Initialize();
            await SyncRequests();
            var result = await table.ToListAsync();
            return result;
        }

        public async Task InsertRequest(Request request)
        {
            await Initialize();
            await table.InsertAsync(request);
            await SyncRequests();
        }

        public async Task DeleteTaskAsync(Request request)
        {
            await Initialize();
            await table.DeleteAsync(request);
            await SyncRequests();
        }

        public async Task SyncRequests()
        {
            try
            {
                await client.SyncContext.PushAsync();
                await table.PullAsync("requests", table.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync, using offline capabilities " + ex);
            }
        }
    }
}
