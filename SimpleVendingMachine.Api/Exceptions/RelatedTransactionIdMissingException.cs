namespace SimpleVendingMachine.Api.Exceptions
{
    public class RelatedTransactionIdMissingException : Exception
    {
        public RelatedTransactionIdMissingException() { }

        public RelatedTransactionIdMissingException(string message) : base(message) { }

        public RelatedTransactionIdMissingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
