namespace Example.MassTransit.Contracts;

public record SubmitOrder(Guid OrderId, string Message);
