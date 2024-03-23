namespace PokemonAPI.Wrappers;

public class Response<T>
{
    public Response()
    {
        //The default constructor is required for Newtonsoft.Json serialization
    }

    public Response(T data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }


    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public string Code { get; set; }
    public T Data { get; set; }
}
