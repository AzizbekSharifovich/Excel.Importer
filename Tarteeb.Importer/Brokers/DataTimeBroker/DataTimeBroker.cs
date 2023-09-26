//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================

using System;

namespace Tarteeb.Importer.Brokers.DataTimeBroker;

public class DataTimeBroker
{
    DateTimeOffset GetCurrentDateTimeOffset() =>
        DateTimeOffset.UtcNow;
}

