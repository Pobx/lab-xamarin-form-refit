using System;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefitLabWithXamarinForm.Services
{
    public interface IUserClient
    {
        [Get("/users")]
        Task<IApiResponse<List<object>>> GetUsers();
    }
}

