//=================================
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

namespace Tarteeb.importer;

public class Program
{
    static async Task Main(string[] args)
    {
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
            Console.WriteLine(exception.Message);
        }
        catch(InvalidClientException invalidClientException)
        {
            Console.WriteLine(invalidClientException.Message);
        }
    }
}