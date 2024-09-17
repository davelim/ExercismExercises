using System;

[Flags]
enum AccountType: byte
{
    Guest     = 0b_0000_0001,
    User      = 0b_0000_0010,
    Moderator = 0b_0000_0100
}

[Flags]
enum Permission: byte
{
    Read   = 0b_0000_0001,
    Write  = 0b_0000_0010,
    Delete = 0b_0000_0100,
    All    = Read | Write | Delete,
    None   = 0b_0000_0000
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
        return current & ~revoke;
    }

    public static bool Check(Permission current, Permission check)
    {
        return current.HasFlag(check);
    }
}
