using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelExp.Common.Models;

namespace TravelExp.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<bool> CheckConnectionAsync(string url);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);


        Task<Response> AddTripAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, TripRequest request);

        Task<Response> AddTripDetailAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, TripDetailRequest request);
    }

}
