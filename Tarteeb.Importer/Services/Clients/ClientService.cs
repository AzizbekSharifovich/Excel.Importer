//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tarteeb.importer.Brockers.Storages;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.importer.Services.Clients;

public class ClientServices
{
    private readonly StorageBroker storageBroker;

    public ClientServices(StorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }
   
    /// <exception cref="NullClientException"></exception>
    /// <exception cref="InvalidClientException"></exception>
    public async Task<Client> AddClientAsync(Client client)
    {
        if (client is null)
        {
            throw new NullClientException();
        }

        Validate(
            (Rule: IsInvalid(client.Id),Parameter: nameof(Client.Id)),
            (Rule: IsInvalid(client.FirstName), Parameter: nameof(Client.FirstName)),
            (Rule: IsInvalid(client.Email), Parameter: nameof(Client.Email)),
            (Rule: IsInvalid(client.GroupId), Parameter: nameof(Client.GroupId)));

        Validate(
            (Rule: IsInvalidEmail(client.Email), Parameter: nameof(Client.Email)));

        return await storageBroker.InsertClientAsync(client);
    }

    private dynamic IsInvalid(Guid id) => new
    {
        Condition = id == default,
        Message = "Id is required."
    };

    private dynamic IsInvalid(string text) => new
    {
        Condition = string.IsNullOrWhiteSpace(text),
        Message = "Text is required."
    };

    private dynamic IsInvalidEmail(string email) => new
    {
        Condition = !Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"),
        Message = "Email is invalid"
    };

    private void Validate(params(dynamic Rule, string Parameter)[] validations)
    {
        var invalidClientException = new InvalidClientException();
        foreach ((dynamic rule, string parameter) in validations) 
        { 
            if(rule.Condition)
            {
                invalidClientException.UpsertDataList(
                    key: parameter,
                    value: rule.Message);
            }

            invalidClientException.ThrowIfContainsErrors();
        }
    }
}