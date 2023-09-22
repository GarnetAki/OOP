namespace MPSExceptions;

public class ServiceExceptions : Exception
{
    private ServiceExceptions(string message)
        : base(message)
    {
    }

    public static ServiceExceptions LoginException(string login)
        => new ServiceExceptions($"User with the same login \"{login}\" already exists.");

    public static ServiceExceptions StringFormatException(string str)
        => new ServiceExceptions($"Line \"{str}\" with the wrong format.");

    public static ServiceExceptions IdException(Guid id)
        => new ServiceExceptions($"User with this id \"{id.ToString()}\" does not exist.");

    public static ServiceExceptions SubordinateIdException(Guid id)
        => new ServiceExceptions($"User with this id \"{id.ToString()}\" already in subordinates list.");

    public static ServiceExceptions UserInAccountException(Guid uid, Guid aid)
        => new ServiceExceptions($"User with this id \"{uid.ToString()}\" does not exist in list of users in account \"{aid.ToString()}\".");

    public static ServiceExceptions AccountInUserException(Guid uid, Guid aid)
        => new ServiceExceptions($"Account with this id \"{aid.ToString()}\" does not exist in list of accounts in user \"{uid.ToString()}\".");

    public static ServiceExceptions PhoneIdException(Guid id)
        => new ServiceExceptions($"Phone with this id \"{id.ToString()}\" does not exist.");

    public static ServiceExceptions TypesException()
        => new ServiceExceptions($"Different types of message and message source.");

    public static ServiceExceptions AuthorizedException()
        => new ServiceExceptions($"You does not authorized.");

    public static ServiceExceptions AuthorizedAccountException()
        => new ServiceExceptions($"You does not authorized in account.");
}