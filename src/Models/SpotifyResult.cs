using System.Net;
using System.Text.Json.Serialization;
using JuiceboxServer.Models.Responses;

namespace JuiceboxServer.Models
{
    public class SpotifyResult
    {
        public SpotifyResult(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        public SpotifyResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The Spotify error message.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SpotifyError? Error { get; set; }

        /// <summary>
        /// The contents of the Spotify response. Is set when the request is successful.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ISpotifyContent? Content { get; set; }

        /// <summary>
        /// The HTTP Status code returned.
        /// </summary>
        public int StatusCode { get; }

        public bool IsSuccessful { get => (200 <= StatusCode && StatusCode <= 299); }
    }

    public static class SpotifyResultFactory
    {
        public static async Task<SpotifyResult> CreateResultFromResponse<T>(HttpResponseMessage response)
            where T : ISpotifyContent
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var result = new SpotifyResult(response.StatusCode);

            if (result.IsSuccessful)
            {
                result.Content = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<SpotifyErrorResponse>();
                result.Error = errorResponse!.Error;
            }

            return result;
        }

        public static async Task<SpotifyResult> CreateResultFromResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var result = new SpotifyResult(response.StatusCode);

            if (!result.IsSuccessful)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<SpotifyErrorResponse>();
                result.Error = errorResponse!.Error;
            }

            return result;
        }
    }
}