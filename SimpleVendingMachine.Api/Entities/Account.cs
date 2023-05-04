namespace SimpleVendingMachine.Api.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
