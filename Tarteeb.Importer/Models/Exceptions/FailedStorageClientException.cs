//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions;

public class FailedStorageClientException : Xeption
{
    public FailedStorageClientException(Exception innerException)
        :base(message: "Failed client storage error occered, contact support.", innerException)
    { }
}

