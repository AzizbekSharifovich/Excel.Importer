//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//=================================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions;

public class LockedClientException : Xeption
{
    public LockedClientException(Exception innerException)
        : base(message:"Client is locked, try again later.", innerException)
    { } 
}

