namespace ThreeSixty.Common.Exceptions
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() : base("User was not found!")
        {

        }
    }
}
