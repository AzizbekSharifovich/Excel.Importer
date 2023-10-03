using System.Threading.Tasks;
using Tarteeb.importer.Models.Exceptions;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

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
            var clientValidationException =
                new ClientValidationException(nullClientException);

            throw clientValidationException;
        }
        catch (InvalidClientException invalidClientException)
        {
            var clientValidationException =
                new ClientValidationException(invalidClientException);

            throw clientValidationException;
        }

    }
}

