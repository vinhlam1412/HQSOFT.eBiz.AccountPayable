namespace HQSOFT.eBiz.AccountPayable;

public static class AccountPayableDbProperties
{
    public static string DbTablePrefix { get; set; } = "AP";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AccountPayable";
}
