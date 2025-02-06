namespace Domain.ViewModels;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Suceeded { get; set; }
    public ApiError? Error { get; set; }
}

public class ApiError
{
    public string? Code { get; set; }
    public string? Message { get; set; }
}
