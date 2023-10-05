//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.Importer.Brokers.DataTimeBroker;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions.Categories;
using Tarteeb.Importer.Services.Clients;
using Xeptions;

namespace Tarteeb.importer;

public class Program
{
    static async Task Main(string[] args)
    {
        var storageBroker = new StorageBroker();
        var loggingBroker = new LoggingBroker();
        var dataTimeBroker = new DataTimeBroker();

        try
        {
            var clientServices = new ClientService(
                new StorageBroker(),
                new DataTimeBroker(),
                new LoggingBroker());
            
            var client = new Client()
            {   
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "test@gmail.com",
                BrithDate = DateTimeOffset.UtcNow,
                GroupId = Guid.NewGuid(),
            };

            var persistedClient = await clientServices.AddClientAsync(client);

            Console.WriteLine(persistedClient);
        }
        catch(ClientValidationException clientValidationException)
        {
            var innerException = (Xeption)clientValidationException.InnerException;
            Console.WriteLine(innerException.Message);

            foreach (DictionaryEntry entry in innerException.Data)
            {
                string errorSummery = ((List<string>)entry.Value)
                    .Select((string value) => value)
                    .Aggregate((string current, string next) => current + "," + next);

                Console.WriteLine(entry.Key + "-" + errorSummery);
            }
        }
    }
}