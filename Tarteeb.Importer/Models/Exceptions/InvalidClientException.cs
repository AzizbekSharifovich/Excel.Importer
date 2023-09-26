//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions;
public class InvalidClientException : Xeption
{
    public InvalidClientException()
        : base(message: "Client is invalid. Fix the errors and try again.") { }
}

