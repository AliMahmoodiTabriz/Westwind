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

        [JsonProperty]
        public Guid Id { get; private init; }
        [JsonProperty]
        public DateTime CreationDate { get; private init; }

    }
}
