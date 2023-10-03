//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions;

public partial class ClientValidationException : Xeption
{
    public ClientValidationException(Xeption innerException)
        : base(message: "Client validation error occurred. Fix the errors and try again",
            innerException)
    { }  
}

