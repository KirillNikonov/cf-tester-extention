namespace NotACompany.CF_Tester.Exceptions
{
    public class FailedRequestException : System.ApplicationException
    {
        public FailedRequestException() : base() { }
        public FailedRequestException(string message) : base(message) { }
        public FailedRequestException(string message, System.Exception inner) : base(message, inner) { }

        protected FailedRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
