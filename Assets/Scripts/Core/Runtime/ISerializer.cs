namespace Huntag.Core
{
    public interface ISerializer
    {
        void Serialize<T>(T obj, string path);

        T Deserialize<T>(string text, ref T obj);
    }
}
