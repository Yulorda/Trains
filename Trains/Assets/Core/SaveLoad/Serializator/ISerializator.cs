public interface ISerializator
{
    bool TryDeserialize(byte[] message, out object obj);
    byte[] Serialize(object obj);
}
