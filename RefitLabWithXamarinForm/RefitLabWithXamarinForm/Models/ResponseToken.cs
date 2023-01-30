using System;
using System.Text.Json.Serialization;

namespace RefitLabWithXamarinForm.Models
{
    public class ResponseToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool Result { get; set; }
        public string ResultID { get; set; }
        public string ResultValue { get; set; }
        public string ErrorCode { get; set; }
        public bool IsError { get; set; }
        public bool IsUnsuccessPage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

