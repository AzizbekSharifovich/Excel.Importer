//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================

using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions.Categories;

public class ClientDependecyException : Xeption
{
    public ClientDependecyException(Xeption innerException)
        : base(message: "Client dependecy error occered, contact support", innerException)
    { }
}

