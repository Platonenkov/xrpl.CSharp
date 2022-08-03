﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Xrpl.Client.Models.Methods
{

    public enum LedgerEntryRequestType
    {
        [EnumMember(Value = "index")]
        Index,
        [EnumMember(Value = "account_root")]
        AccountRoot,
        [EnumMember(Value = "directory")]
        Directory,
        [EnumMember(Value = "offer")]
        Offer,
        [EnumMember(Value = "ripple_state")]
        RippleState
    }
    
    public class LedgerEntryRequest : BaseLedgerRequest
    {
        public LedgerEntryRequest()
        {
            Command = "ledger_entry";
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LedgerEntryRequestType LedgerEntryRequestType { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("account_root")]
        public string AccountRoot { get; set; }

        [JsonProperty("ripple_state")]
        public RippleState RippleState { get; set; }

        [JsonProperty("binary")]
        public bool? Binary { get; set; }
    }

    public class RippleState
    {

        [JsonProperty("accounts")]
        public string[] Addresses { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }        
    }
}