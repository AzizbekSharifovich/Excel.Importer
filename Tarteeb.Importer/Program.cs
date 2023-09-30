﻿//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using System;
using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.importer.Services.Clients;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;
using System.Linq;
using Tarteeb.Importer.Brokers.Logging;
using System.Collections;
using System.Collections.Generic;

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
                    FirstName = "Azizbek",
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
            Console.WriteLine(invalidClientException.Message);

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