namespace CSharpAppPlayground.Classes.DataGen
{
    public interface IDataGenerator<T>
    {
        List<T> GenerateData(int count);
    }
}
