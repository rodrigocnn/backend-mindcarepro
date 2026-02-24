namespace MindCarePro.API.Common;

public class ApiResponse<T>
{
    public ApiResponse(T data, bool success = true)
    {
        Data = data;
        Success = success;
    }

    public ApiResponse(T data, IEnumerable<string> notifications, bool success = true)
    {
        Data = data;
        Notifications = notifications.ToList();
        Success = success;
    }

    public bool Success { get; set; }
    public T Data { get; set; }
    public List<string> Notifications { get; set; } = new();
}