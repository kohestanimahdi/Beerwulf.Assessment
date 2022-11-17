namespace Review.Domain.Common
{
    public enum ApiResultStatusCode
    {
        Success = 200,

        ServerError = 500,

        BadRequest = 400,

        NotFound = 404,

        ListEmpty = 404,

        LogicError = 500,

        UnAuthorized = 401,

        Forbidden = 401,

        ToManyRequest = 429,

        Other = 400
    }
}
