using Travel.Domain.Enums;

namespace Travel.Domain.Tools
{
    public class Tools
    {
        public static ApiResponse<T> CreateResponse<T>(T data,Result result, int? DataCount)
        {
            return new ApiResponse<T>
            {
                Data = data,
                DataCount = DataCount,
                Result = result.GetDescription(),
            };
        }
    }

}
