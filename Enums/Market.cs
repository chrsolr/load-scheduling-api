using System.Runtime.Serialization;

public enum Market
{
    [EnumMember(Value = "ERCOT")]
    ERCOT,

    [EnumMember(Value = "PJM")]
    PJM,

    [EnumMember(Value = "NEISO")]
    NEISO,

    [EnumMember(Value = "ISONE")]
    ISONE,

    [EnumMember(Value = "NYISO")]
    NYISO,

    [EnumMember(Value = "MISO")]
    MISO,

    [EnumMember(Value = "CAISO")]
    CAISO,
}
