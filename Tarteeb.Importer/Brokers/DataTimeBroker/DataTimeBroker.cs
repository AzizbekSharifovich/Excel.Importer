//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using System;

namespace Tarteeb.Importer.Brokers.DataTimeBroker;

public class DataTimeBroker
{
    public DateTimeOffset GetCurrentDateTimeOffset() =>
        DateTimeOffset.UtcNow;
}

