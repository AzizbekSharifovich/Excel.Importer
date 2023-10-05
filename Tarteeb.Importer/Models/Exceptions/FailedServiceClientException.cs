//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions;

public class FailedServiceClientException : Xeption
{
    public FailedServiceClientException(Exception innerException)
        : base(message:"Failed client service error occured,  contact support.", innerException)
    { }    
}

