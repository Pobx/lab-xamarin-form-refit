using System;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefitLabWithXamarinForm.Models;

namespace RefitLabWithXamarinForm.Services
{
    public interface IIdentityClient
    {
        [Post("/connect/token")]
        Task<IApiResponse<ResponseToken>> Authentication([Body(BodySerializationMethod.UrlEncoded)] IDictionary<string, string> data, [HeaderCollection] IDictionary<string, string> headers);
    }
}

