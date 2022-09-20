using System;
using Newtonsoft.Json;
using Xrpl.Client.Json.Converters;
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
    }

    public interface INFTokenAcceptOfferTransaction : ITransactionCommon
    {
        string NFTokenID { get; set; }
    }

    public class NFTokenAcceptOfferTransactionResponse : TransactionResponseCommon, INFTokenAcceptOfferTransaction
    {
        public string NFTokenID { get; set; }

    }
}
