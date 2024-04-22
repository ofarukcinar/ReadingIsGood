using Newtonsoft.Json;
using Serilog;

namespace ReadingIsGood.Services;

public class BaseService
{
    public void SaveError(Exception err, string message)
    {
        Log.Error(err, message);
    }

    public void SaveError(string message)
    {
        Log.Error(message);
    }

    public void SaveLogWithData(string message, object data)
    {
        Log.Warning(message + $" - params: + {JsonConvert.SerializeObject(data)}");
    }

    public void SaveErrorWithData(string message, object data)
    {
        Log.Error(message + $" - params: + {JsonConvert.SerializeObject(data)}");
    }

    public void SaveLog(string message)
    {
        Log.Warning(message);
    }
}