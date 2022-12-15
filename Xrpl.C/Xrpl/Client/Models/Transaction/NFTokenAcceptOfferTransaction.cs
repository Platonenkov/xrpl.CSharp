using System;
using Newtonsoft.Json;
using Xrpl.Client.Json.Converters;
using Xrpl.Client.Models.Common;
using Xrpl.Client.Models.Enums;

//https://xrpl.org/nftoken-tester-tutorial.html#the-nftokenacceptoffer-transaction

namespace Xrpl.Client.Models.Transactions
{
    public class NFTokenAcceptOfferTransaction : TransactionCommon, INFTokenAcceptOfferTransaction
    {
        public NFTokenAcceptOfferTransaction()
        {
            TransactionType = TransactionType.NFTokenAcceptOffer;
        }

        public string NFTokenID { get; set; }

        public string NFTokenSellOffer { get; set; }
        public string NFTokenBuyOffer { get; set; }
        /// <inheritdoc />
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency NFTokenBrokerFee { get; set; }
    }

    public interface INFTokenAcceptOfferTransaction : ITransactionCommon
    {
        string NFTokenID { get; set; } //todo unknown field
        /// <summary>
        /// Identifies the NFTokenOffer that offers to sell the NFToken.<br/>
        /// In direct mode this field is optional, but either NFTokenSellOffer or  NFTokenBuyOffer must be specified.<br/>
        /// In brokered mode, both NFTokenSellOffer  and NFTokenBuyOffer must be specified.
        /// </summary>
        public string NFTokenSellOffer { get; set; }
        /// <summary>
        /// Identifies the NFTokenOffer that offers to buy the NFToken.<br/>
        /// In direct mode this field is optional, but either NFTokenSellOffer or NFTokenBuyOffer must be specified.<br/>
        /// In brokered mode, both NFTokenSellOffer and NFTokenBuyOffer must be specified.
        /// </summary>
        public string NFTokenBuyOffer { get; set; }
        /// <summary>
        /// This field is only valid in brokered mode.<br/>
        /// It specifies the amount that the broker will keep as part of their fee for bringing the two offers together; the remaining amount will be sent to the seller of the NFToken being bought.<br/>
        /// If specified, the fee must be such that, prior to accounting for the transfer fee charged by the issuer, the amount that the seller would receive is at least as much as the amount indicated in the sell offer.<br/>
        /// This functionality is intended to allow the owner of an NFToken to offer their token for sale to a third party broker, who may then attempt to sell the NFToken on for a larger amount, without the broker having to own the NFToken or custody funds.<br/>
        /// Note: in brokered mode, the offers referenced by NFTokenBuyOffer and NFTokenSellOffer must both specify the same NFTokenID; that is, both must be for the same NFToken.
        /// </summary>
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency NFTokenBrokerFee { get; set; }
    }

    public class NFTokenAcceptOfferTransactionResponse : TransactionResponseCommon, INFTokenAcceptOfferTransaction
    {
        public string NFTokenID { get; set; } //todo unknown field
        /// <summary>
        /// Identifies the NFTokenOffer that offers to sell the NFToken.<br/>
        /// In direct mode this field is optional, but either NFTokenSellOffer or  NFTokenBuyOffer must be specified.<br/>
        /// In brokered mode, both NFTokenSellOffer  and NFTokenBuyOffer must be specified.
        /// </summary>
        public string NFTokenSellOffer { get; set; }
        /// <summary>
        /// Identifies the NFTokenOffer that offers to buy the NFToken.<br/>
        /// In direct mode this field is optional, but either NFTokenSellOffer or NFTokenBuyOffer must be specified.<br/>
        /// In brokered mode, both NFTokenSellOffer and NFTokenBuyOffer must be specified.
        /// </summary>
        public string NFTokenBuyOffer { get; set; }
        /// <summary>
        /// This field is only valid in brokered mode.<br/>
        /// It specifies the amount that the broker will keep as part of their fee for bringing the two offers together; the remaining amount will be sent to the seller of the NFToken being bought.<br/>
        /// If specified, the fee must be such that, prior to accounting for the transfer fee charged by the issuer, the amount that the seller would receive is at least as much as the amount indicated in the sell offer.<br/>
        /// This functionality is intended to allow the owner of an NFToken to offer their token for sale to a third party broker, who may then attempt to sell the NFToken on for a larger amount, without the broker having to own the NFToken or custody funds.<br/>
        /// Note: in brokered mode, the offers referenced by NFTokenBuyOffer and NFTokenSellOffer must both specify the same NFTokenID; that is, both must be for the same NFToken.
        /// </summary>
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency NFTokenBrokerFee { get; set; }

    }
}
