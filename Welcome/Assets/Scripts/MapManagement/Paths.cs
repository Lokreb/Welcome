using System.Collections.Generic;

public enum Localisation { Start, S1, S2, S3, S4, Exit };

[System.Serializable]
public class Paths
{
    public static Localisation[] Start = {Localisation.S1};
}
