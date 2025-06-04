namespace Nexus.Sdk.Token.Responses;
public class CustomResultHolder<T>
{
    public string? Message { get; set; }
    public string[]? Errors { get; set; }
    public T? Values { get; set; }
}

public class CustomResultHolder : CustomResultHolder<object> { }