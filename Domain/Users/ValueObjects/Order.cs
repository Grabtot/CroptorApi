namespace Croptor.Domain.Users.ValueObjects
{
    public record Order
    {
        private Order() { }

        private Order(Guid id, Guid userId, decimal amount)
        {
            Id = id;
            UserId = userId;
            Amount = amount;
        }

        public static Order Create(Guid userId, decimal amount)
        {
            return new(Guid.NewGuid(), userId, amount);
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
