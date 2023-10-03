//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System;
using System.Text.RegularExpressions;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services.Clients;

public partial class ClientService
{
    private void ValidateClientOnAdd(Client client)
    {
        ValidateClientNotNull(client);

        Validate(
            (Rule: IsInvalid(client.Id), Parameter: nameof(client.Id)),
            (Rule: IsInvalid(client.FirstName), Parameter: nameof(client.FirstName)),
            (Rule: IsInvalid(client.Email), Parameter: nameof(client.Email)),
            (Rule: IsInvalid(client.BrithDate), Parameter: nameof(client.BrithDate)),
            (Rule: IsLessThan12(client.BrithDate), Parameter: nameof(client.BrithDate)),
            (Rule: IsInvalid(client.GroupId), Parameter: nameof(client.GroupId)));

        Validate(
            (Rule: IsInvalidEmail(client.Email), Parameter: nameof(client.Email)));
    }

    private static void ValidateClientNotNull(Client client)
    {
        if (client is null)
        {
            throw new NullClientException();
        }
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

    private dynamic IsInvalid(DateTimeOffset date) => new
    {
        Condition = date == default,
        Message = "Date is required"
    };

    private dynamic IsLessThan12(DateTimeOffset date) => new
    {
        Condition = IsAgeLessThan12(date),
        Message = "Age is less than 12"
    };

    private bool IsAgeLessThan12(DateTimeOffset date)
    {
        DateTimeOffset now = this.dataTimeBroker.GetCurrentDateTimeOffset();
        int age = (now - date).Days / 365;
        return age < 12;
    }

    private void Validate(params (dynamic Rule, string Parameter)[] validations)
    {
        var invalidClientException = new InvalidClientException();
        foreach ((dynamic rule, string parameter) in validations)
        {
            if (rule.Condition)
            {
                invalidClientException.UpsertDataList(
                    key: parameter,
                    value: rule.Message);
            }

            invalidClientException.ThrowIfContainsErrors();
        }
    }
}

