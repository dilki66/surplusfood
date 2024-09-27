using System.Net;

namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
