//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions.Categories;

public class ClientDependecyValidationException : Xeption
{
    public ClientDependecyValidationException(Xeption innerException)
        : base(message: "Client dependecy validation error occered, fix the errors and try again.",
            innerException)
    { }
}

