using OneOf;
namespace NgNetCore.Domain.Core;

public static class TryCatchGuardExceptions
{

    public static bool ThrowsException(Action tryAction)
    {
        try
        {
            tryAction();
            return false;
        }
        catch (Exception)
        {
            return true;
        }
    }

}
