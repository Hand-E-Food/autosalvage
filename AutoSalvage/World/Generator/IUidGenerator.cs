namespace AutoSalvage.World.Generator
{
    internal interface IUidGenerator<T>
    {
        T Next();
    }
}