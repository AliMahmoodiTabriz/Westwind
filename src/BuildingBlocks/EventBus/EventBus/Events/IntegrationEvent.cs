using Newtonsoft.Json;
using System;

namespace EventBus.Events
{
    public record IntegrationEvent
    {

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonConstructor]
        public IntegrationEvent(string userId, string prdoductId, long transactionId)
        {
            UserId = userId;
            PrdoductId = prdoductId;
            TransactionId = transactionId;
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        [JsonConstructor]
        public IntegrationEvent(string prdoductId, long transactionId)
        {
            PrdoductId = prdoductId;
            TransactionId = transactionId;
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        [JsonConstructor]
        public IntegrationEvent(string userId, string prdoductId)
        {
            UserId = userId;
            PrdoductId = prdoductId;
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(string prdoductId)
        {
            PrdoductId = prdoductId;
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonProperty]
        public Guid Id { get; private init; }
        [JsonProperty]
        public DateTime CreationDate { get; private init; }
        [JsonProperty]
        public string UserId { get; private init; }
        [JsonProperty]
        public string PrdoductId { get; private init; }
        [JsonProperty]
        public long TransactionId { get; private init; }
    }
}
