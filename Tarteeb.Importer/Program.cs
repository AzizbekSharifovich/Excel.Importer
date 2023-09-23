using System;
using System.Linq;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer;

public class Program
{
    static async Task Main(string[] args)
    {
        var client = new Models.Clients.Client
        {
            Id = Guid.NewGuid(),
            FirstName = "Jonhatan",
            LastName = "Stricer",
            PhoneNumber = "1234567890",
            Email = "azizbek@gmail.com",
            BrithDate = DateTimeOffset.Now,
            GroupId = Guid.NewGuid(),
        };

        using (var storageBroker = new StorageBroker())
        {
            Client persistedClient = await storageBroker.InsertClientAsync(client);
            IQueryable<Client> dbClients = storageBroker.SelectAllClients();

            foreach (var dbClient in dbClients)
            {
                Console.WriteLine(dbClient.FirstName + " " + dbClient.LastName);
            }
        }
    }
}