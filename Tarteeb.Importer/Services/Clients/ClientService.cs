//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.Importer.Brokers.DataTimeBroker;
using Tarteeb.Importer.Brokers.Logging;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services.Clients;

public partial class ClientService
{
    private readonly StorageBroker storageBroker;
    private readonly DataTimeBroker dataTimeBroker;
    private readonly LoggingBroker loggingBroker;

    public ClientService(
        StorageBroker storageBroker,
        DataTimeBroker dataTimeBroker,
        LoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.dataTimeBroker = dataTimeBroker;
        this.loggingBroker = loggingBroker;
    }

    /// <exception cref="ClientValidationException"></exception>
    public Task<Client> AddClientAsync(Client client) =>
    TryCatch(() =>
    {
        ValidateClientOnAdd(client);

        return  this.storageBroker.InsertClientAsync(client);
    });
}

