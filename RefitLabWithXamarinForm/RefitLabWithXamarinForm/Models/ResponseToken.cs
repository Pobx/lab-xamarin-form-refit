using System;
namespace RefitLabWithXamarinForm.Models
{
    public class ResponseToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public Data data { get; set; }
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

