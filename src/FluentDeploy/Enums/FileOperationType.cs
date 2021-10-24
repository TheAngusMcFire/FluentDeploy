namespace FluentDeploy.Enums
{
    public enum FileOperationType
    {
        Unknown = 0,
        CreateDirectory = 1,
        CreateFile = 2,
        Delete = 3,
        Exists = 4,
        SymbolicLink = 5,
        CopyFromLocal = 6,
        SetAttributes = 7,
    }
}