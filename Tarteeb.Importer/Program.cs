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
        try 
        {
            StorageBroker storageBroker = new StorageBroker();
            LoggingBroker loggingBroker = new LoggingBroker();
            DataTimeBroker dataTimeBroker = new DataTimeBroker();

            ClientService clientService = new ClientService(
                storageBroker: new StorageBroker(),
                dataTimeBroker: new DataTimeBroker(),
                loggingBroker: new LoggingBroker());

            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test",
                BrithDate = DateTimeOffset.Parse("1/1.2000"),
                Email = "Test@gmail.com",
                GroupId = Guid.NewGuid()
            };

            var persistedClient = await clientService.AddClientAsync(client);
            Console.WriteLine(persistedClient.Id);
        }
        catch (Exception ex) 
        {
            
        }
    }
}