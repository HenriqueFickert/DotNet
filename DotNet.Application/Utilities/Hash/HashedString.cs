namespace DotNet.Application.Utilities.Hash
{
    public class HashedString
    {
        public string String { get; private set; }

        public void ChangeString(string _string)
        {
            String = _string;
        }
    }
}