namespace NotACompany.CF_Tester.Exceptions
{
    public class FailedRequestException : System.ApplicationException
    {
        public FailedRequestException() { }
        public FailedRequestException(string message) { }
        public FailedRequestException(string message, System.Exception inner) { }

        protected FailedRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
