//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================

using Xeptions;

namespace Tarteeb.importer.Models.Exceptions;

public class NullClientException : Xeption
{
    public NullClientException()
        : base("Client is Null"){ }
}