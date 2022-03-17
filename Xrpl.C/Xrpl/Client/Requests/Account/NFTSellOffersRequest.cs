﻿using Newtonsoft.Json;

namespace Xrpl.Client.Requests.Account
{
    public class NFTSellOffersRequest : BaseLedgerRequest
    {
        public NFTSellOffersRequest(string tokenid)
        {
            TokenID = tokenid;
            Command = "nft_sell_offers";
        }

        [JsonProperty("tokenid")]
        public string TokenID { get; set; }
    }
}