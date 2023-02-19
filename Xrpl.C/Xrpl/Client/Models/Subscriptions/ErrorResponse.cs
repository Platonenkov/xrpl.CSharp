using System;
using Newtonsoft.Json;

namespace xrpl_c.Xrpl.Client.Models.Subscriptions;

public class ErrorResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }


    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
    [JsonProperty("error_code")]
    public string ErrorCode { get; set; }
    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }
    [JsonProperty("request")]
    public object Request { get; set; }



    [JsonProperty("api_version")]
    public string ApiVersion { get; set; }

}
public class ErrorResponse2
{
    [JsonProperty("id")]
    public Guid Id { get; set; }


    [JsonProperty("result")]
    public ErrorMessage Result { get; set; }

}
public class ErrorMessage
{
    [JsonProperty("account")]
    public string Account { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
    [JsonProperty("error_code")]
    public string ErrorCode { get; set; }
    [JsonProperty("error_message")]
    public string Message { get; set; }
    [JsonProperty("ledger_current_index")]
    public uint LedgerCurrentIndex { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("validated")]
    public bool Validated { get; set; }
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("request")]
    public object Request { get; set; }
}