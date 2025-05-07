using LinqToDB;
using LinqToDB.Data;

public class AppDataConnection : DataConnection {
    public AppDataConnection(string connectionString)
        : base(LinqToDB.ProviderName.PostgreSQL, connectionString) { }
}
