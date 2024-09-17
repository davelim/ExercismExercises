using System;
using System.Collections.Generic; // HashSet<>


public class FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }
    public override bool Equals(Object other)
    {
        return other is FacialFeatures f
            && (f.EyeColor, f.PhiltrumWidth).Equals((EyeColor, PhiltrumWidth));
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(EyeColor, PhiltrumWidth);
    }
}

public class Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }
    public override bool Equals(Object other)
    {
        return other is Identity i
            && (i.Email, i.FacialFeatures).Equals((Email, FacialFeatures));
    }
    public override int GetHashCode()
    {
        return HashCode.Combine<string, FacialFeatures>(Email, FacialFeatures);
    }
}

public class Authenticator
{
    private HashSet<Identity> _registeredIdentities = new();

    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {
        return faceA.Equals(faceB);
    }

    public bool IsAdmin(Identity identity)
    {
        return identity.Equals(new Identity("admin@exerc.ism", new FacialFeatures("green", 0.9m)));
    }

    public bool Register(Identity identity)
    {
        return _registeredIdentities.Add(identity);
    }

    public bool IsRegistered(Identity identity)
    {
        return _registeredIdentities.Contains(identity);
    }

    public static bool AreSameObject(Identity identityA, Identity identityB)
    {
        throw new NotImplementedException("Please implement the Authenticator.AreSameObject() method");
    }
}
