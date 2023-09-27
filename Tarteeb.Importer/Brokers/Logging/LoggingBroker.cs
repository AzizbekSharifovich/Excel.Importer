//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System;
using Tarteeb.importer.Models.Exceptions;

namespace Tarteeb.Importer.Brokers.Logging;

public class LoggingBroker
{
    public void LoggingError(NullClientException nullClientException) =>
        Console.WriteLine(nullClientException.Message);
}