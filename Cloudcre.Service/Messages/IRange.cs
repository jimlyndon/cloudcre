namespace Cloudcre.Service.Messages
{
    /// <summary>
    /// See Fowler's Range: http://martinfowler.com/eaaDev/Range.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
        bool IsValid { get; }
    }
}