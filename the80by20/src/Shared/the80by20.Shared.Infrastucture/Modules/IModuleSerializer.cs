namespace the80by20.Shared.Infrastucture.Modules
{
    public interface IModuleSerializer
    {
        byte[] Serialize<T>(T value);
        T Deserialize<T>(byte[] value);
        object Deserialize(byte[] value, Type type);
    }
}
