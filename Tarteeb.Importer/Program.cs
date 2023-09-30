//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.importer.Services.Clients;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.importer;

public class Program
{
    static async Task Main(string[] args)
    {
        var loggingBroker = new LoggingBroker();

        try
        {
            using (var storageBroker = new StorageBroker())
            {
                var client = new Client
                {
                    FirstName = "John",
                    Id = Guid.NewGuid(),
                    Email = "something",
                    GroupId = Guid.NewGuid(),
                };

                var clientServices = new ClientServices(storageBroker);
                var persistedClient = await clientServices.AddClientAsync(client);
                Console.WriteLine(persistedClient.FirstName);

            }
        }
        catch (NullClientException exception)
        {
            loggingBroker.LoggingError(exception);
        }
        catch(InvalidClientException invalidClientException)
        {
            loggingBroker.LoggingError(invalidClientException);

            foreach (DictionaryEntry entry in invalidClientException.Data)
            {
                string errorSummery = ((List<string>)entry.Value)
                    .Select((string value) => value)
                    .Aggregate((string current, string next) => current + "," + next);

                Console.WriteLine(entry.Key + "-" + errorSummery);
            }
        }
    }
}