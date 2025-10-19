namespace ApiConsumerProduct.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class ConsumerProducts
    {
        public static string GetList(string version = "v1") => $"{Base}/{version}/consumerProducts";
        public static string GetAll(string version = "v1") => $"{Base}/{version}/consumerProducts/all";
        public static string GetRecord(Guid id, string version = "v1") => $"{Base}/{version}/consumerProducts/{id}";
        public static string Delete(Guid id, string version = "v1") => $"{Base}/{version}/consumerProducts/{id}";
        public static string Put(Guid id, string version = "v1") => $"{Base}/{version}/consumerProducts/{id}";
        public static string Create(string version = "v1") => $"{Base}/{version}/consumerProducts";
        public static string CreateBatch(string version = "v1") => $"{Base}/{version}/consumerProducts/batch";
    }

    public static class Users
    {
        public static string GetList(string version = "v1")  => $"{Base}/{version}/users";
        public static string GetRecord(Guid id, string version = "v1") => $"{Base}/{version}/users/{id}";
        public static string Delete(Guid id, string version = "v1") => $"{Base}/{version}/users/{id}";
        public static string Put(Guid id, string version = "v1") => $"{Base}/{version}/users/{id}";
        public static string Create(string version = "v1")  => $"{Base}/{version}/users";
        public static string CreateBatch(string version = "v1")  => $"{Base}/{version}/users/batch";
        public static string AddRole(Guid id, string version = "v1") => $"{Base}/{version}/users/{id}/addRole";
        public static string RemoveRole(Guid id, string version = "v1") => $"{Base}/{version}/users/{id}/removeRole";
    }

    public static class RolePermissions
    {
        public static string GetList(string version = "v1") => $"{Base}/{version}/rolePermissions";
        public static string GetAll(string version = "v1") => $"{Base}/{version}/rolePermissions/all";
        public static string GetRecord(Guid id, string version = "v1") => $"{Base}/{version}/rolePermissions/{id}";
        public static string Delete(Guid id, string version = "v1") => $"{Base}/{version}/rolePermissions/{id}";
        public static string Put(Guid id, string version = "v1") => $"{Base}/{version}/rolePermissions/{id}";
        public static string Create(string version = "v1") => $"{Base}/{version}/rolePermissions";
        public static string CreateBatch(string version = "v1") => $"{Base}/{version}/rolePermissions/batch";
    }
}
