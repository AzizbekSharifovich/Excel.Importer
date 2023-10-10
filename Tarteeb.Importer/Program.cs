//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.Importer.Brokers.DataTimeBroker;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;
using Tarteeb.Importer.Models.Exceptions.Categories;
using Tarteeb.Importer.Services.Clients;

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
            
            Faker faker = new Faker();

            for(int index = 0; index < 2000; index++)
            {
                var client = new Client
                {
                    Id = faker.Random.Guid(),
                    FirstName = faker.Name.FindName(),
                    LastName = faker.Name.LastName(),
                    BrithDate = faker.Date.PastOffset(20, DateTime.Now.AddYears(-18)),
                    Email = faker.Internet.Email(),
                    PhoneNumber = "+" + faker.Phone.PhoneNumber(),
                    GroupId = faker.Random.Guid()
                };

                var persistedClient = await clientService.AddClientAsync(client);
                Console.WriteLine($"Added user: {persistedClient}");
            }
            
        }
        catch(ClientValidationException clientValidationException)
            when (clientValidationException.InnerException is InvalidClientException)
        {
            foreach (DictionaryEntry entry in clientValidationException.Data)
            {
                string errorSummary = string.Join(", ", (List<string>)entry.Value);

                Console.WriteLine(entry.Key + " - " + errorSummary);
            }
            Console.WriteLine($"Message: {clientValidationException.Message}");
        }
        catch (ClientValidationException clientValidationException)

        {
            Console.WriteLine(clientValidationException.Message);
        }

        catch (ClientDependecyValidationException clientDependecyValidationException)
        {
            Console.WriteLine(clientDependecyValidationException.Message);
        }
        catch (ClientDependecyException clientDependencyException)
        {
            Console.WriteLine(clientDependencyException.Message);
        }

        catch (ClientServiceException clientServiceException)
        {
            Console.WriteLine(clientServiceException.Message);
        }

    }
}