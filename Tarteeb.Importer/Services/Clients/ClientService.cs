//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.importer.Services.Clients;

public class ClientServices
{
    public readonly StorageBroker storageBroker;

    public ClientServices(StorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public async Task<Client> AddClientAsync(Client client)
    {
        if (client is null)
        {
            throw new NullClientException();
        }
        return await storageBroker.InsertClientAsync(client);
    }
}