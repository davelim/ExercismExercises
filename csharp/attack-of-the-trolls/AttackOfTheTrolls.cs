using System;

// TODO: define the 'AccountType' enum
[Flags]
enum AccountType: byte
{
    Guest     = 0b00000001,
    User      = 0b00000010,
    Moderator = 0b00000100
}

// TODO: define the 'Permission' enum
[Flags]
enum Permission: byte
{
    Read   = 0b00000001,
    Write  = 0b00000010,
    Delete = 0b00000100,
    All    = Read | Write | Delete,
    None   = 0b00000000
}

static class Permissions
{
    public static Permission Default(AccountType accountType)
    {
        Permission perm = Permission.None;
        switch (accountType)
        {
            case AccountType.Guest:
                perm = Permission.Read;
                break;
            case AccountType.User:
                perm = Permission.Read | Permission.Write;
                break;
            case AccountType.Moderator:
                perm = Permission.All;
                break;
            default:
                break;
        }
        return perm;
    }

    public static Permission Grant(Permission current, Permission grant)
    {
        return current | grant;
    }

    public static Permission Revoke(Permission current, Permission revoke)
    {
        throw new NotImplementedException("Please implement the (static) Permissions.Revoke() method");
    }

    public static bool Check(Permission current, Permission check)
    {
        throw new NotImplementedException("Please implement the (static) Permissions.Check() method");
    }
}
