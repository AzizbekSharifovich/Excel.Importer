//=================================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===============================
using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;
using Tarteeb.Importer.Models.Exceptions.Categories;
using Xeptions;

namespace Tarteeb.Importer.Services.Clients;

public partial class ClientService
{
    private delegate Task<Client> ReturningClientFunction();

    private Task<Client> TryCatch(ReturningClientFunction returningClientFunction)
    {
		try
		{
            return returningClientFunction();
		}
        catch (NullClientException nullClientException)
        {
            throw CreateValidationException(nullClientException);
        }
        catch (InvalidClientException invalidClientException)
        {
            throw CreateValidationException(invalidClientException);
        }
        catch(DuplicateKeyException duplicateKeyException)
        {
            var alreadyExistsClientExceptions =
                new AlreadyExistsClientException(duplicateKeyException);

            throw CreateDependecyValidationException(alreadyExistsClientExceptions);
        }
        catch(DbUpdateConcurrencyException dbUpdateConcurrencyException)
        {
            var lockedClientException = 
                new LockedClientException(dbUpdateConcurrencyException);
            
            throw CreateDependecyException(lockedClientException);
        }
        catch(DbUpdateException dbUpdateException)
        {
            var failedStorageClientException =
                new FailedStorageClientException(dbUpdateException);

            throw CreateDependecyException(failedStorageClientException);
        }
        catch(Exception exception)
        {
            var failedServiceClientException = new FailedServiceClientException(exception);

            throw CreateServiceException(failedServiceClientException);
        }

    }

    private ClientServiceException CreateServiceException(Xeption exception)
    {
        var clientServiceException = new ClientServiceException(exception);

        return clientServiceException;
    }

    private ClientDependecyException CreateDependecyException(Xeption exception)
    {
        var clientDependecyException = new ClientDependecyException(exception);

        return clientDependecyException;
    }

    private ClientValidationException CreateValidationException(Xeption exception)
    {
        var clientValidationException = new ClientValidationException(exception);

        return clientValidationException;
    }

    private ClientDependecyValidationException CreateDependecyValidationException(Xeption exception)
    {
        var clientDependecyValidationExceptions =
                new ClientDependecyValidationException(exception);

        return clientDependecyValidationExceptions;
    }
}

