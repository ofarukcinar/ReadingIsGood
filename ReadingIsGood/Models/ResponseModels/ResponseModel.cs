namespace ReadingIsGood.Models.ResponseModels;

public record ResponseModel<T>(bool Success, string Message, T Data)
{
    public ResponseModel(T data) : this(true, null, data)
    {
    }

    public ResponseModel(T data, string message) : this(false, message, data)
    {
    }
}