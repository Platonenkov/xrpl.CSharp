﻿using Newtonsoft.Json;

namespace Xrpl.Client.Requests.Account
{
    public class NFTBuyOffersRequest : BaseLedgerRequest
    {
        public NFTBuyOffersRequest(string tokenid)
        {
            TokenID = tokenid;
            Command = "nft_buy_offers";
        }

        [JsonProperty("tokenid")]
        public string TokenID { get; set; }
    }
}