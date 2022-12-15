using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Xrpl.Client.Models.Enums;
using Xrpl.Client.Models.Ledger;

using xrpl_c.Xrpl.Client.Models.Ledger;

namespace Xrpl.Client.Json.Converters
{
    public class LOConverter : JsonConverter
    {
        public static BaseRippleLO GetBaseRippleLO(LedgerEntryType type, object field) =>
            type switch
            {
                LedgerEntryType.None => null,
                LedgerEntryType.AccountRoot => JsonConvert.DeserializeObject<LOAccountRoot>($"{field}"),
                LedgerEntryType.Amendments => JsonConvert.DeserializeObject<LOAmendments>($"{field}"),
                LedgerEntryType.DirectoryNode => JsonConvert.DeserializeObject<LODirectoryNode>($"{field}"),
                LedgerEntryType.Escrow => JsonConvert.DeserializeObject<LOEscrow>($"{field}"),
                LedgerEntryType.FeeSettings => JsonConvert.DeserializeObject<LOFeeSettings>($"{field}"),
                LedgerEntryType.LedgerHashes => JsonConvert.DeserializeObject<LOLedgerHashes>($"{field}"),
                LedgerEntryType.Offer => JsonConvert.DeserializeObject<LOOffer>($"{field}"),
                LedgerEntryType.PayChannel => JsonConvert.DeserializeObject<LOPayChannel>($"{field}"),
                LedgerEntryType.RippleState => JsonConvert.DeserializeObject<LORippleState>($"{field}"),
                LedgerEntryType.SignerList => JsonConvert.DeserializeObject<LOSignerList>($"{field}"),
                LedgerEntryType.NFTokenOffer => JsonConvert.DeserializeObject<LONFTokenOffer>($"{field}"),
                //LedgerEntryType.NegativeUNL => expr,
                //LedgerEntryType.NFTokenOffer => expr,
                //LedgerEntryType.NFTokenPage => expr,
                //LedgerEntryType.Ticket => expr,
                //LedgerEntryType.Check => expr,
                //LedgerEntryType.DepositPreauth => expr,
                _ => throw new ArgumentOutOfRangeException()
            };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        public BaseRippleLO Create(Type objectType, JObject jObject)
        {

            switch (objectType.Name)
            {
                case "LOAccountRoot":
                    return new LOAccountRoot();
                case "LOAmendments":
                    return new LOAmendments();
                case "LODirectoryNode":
                    return new LODirectoryNode();
                case "LOEscrow":
                    return new LOEscrow();
                case "LOFeeSettings":
                    return new LOFeeSettings();
                case "LOLedgerHashes":
                    return new LOLedgerHashes();
                case "LOOffer":
                    return new LOOffer();
                case "LOPayChannel":
                    return new LOPayChannel();
                case "LORippleState":
                    return new LORippleState();
                case "LOSignerList":
                    return new LOSignerList();
                case "LONFTokenOffer":
                    return new LONFTokenOffer();
                    // case "Ticket":
                    //     return new LOTicket();
            }
            //switch (_Type)
            //{
            //    case LedgerEntryType.None: break;
            //    case LedgerEntryType.AccountRoot: return new LOAccountRoot();
            //    case LedgerEntryType.Amendments: return new LOAmendments();
            //    case LedgerEntryType.DirectoryNode: return new LODirectoryNode();
            //    case LedgerEntryType.Escrow: return new LOEscrow();
            //    case LedgerEntryType.FeeSettings: return new LOFeeSettings();
            //    case LedgerEntryType.LedgerHashes: return new LOLedgerHashes();
            //    case LedgerEntryType.Offer: return new LOOffer();
            //    case LedgerEntryType.PayChannel: return new LOPayChannel();
            //    case LedgerEntryType.RippleState: return new LORippleState();
            //    case LedgerEntryType.SignerList: return new LOSignerList();
            //    //case LedgerEntryType.NegativeUNL: return new LOPayChannel();
            //    //case LedgerEntryType.NFTokenOffer: return new LOPayChannel();
            //    //case LedgerEntryType.NFTokenPage: return new LOPayChannel();
            //    //case LedgerEntryType.Ticket: return new LOPayChannel();
            //    //case LedgerEntryType.Check: return new LOPayChannel();
            //    //case LedgerEntryType.DepositPreauth: return new LOPayChannel();
            //    default: throw new ArgumentOutOfRangeException();
            //}
            string ledgerEntryType = jObject.Property("LedgerEntryType")?.Value.ToString();

            switch (ledgerEntryType)
            {
                case "AccountRoot":
                    return new LOAccountRoot();
                case "Amendments":
                    return new LOAmendments();
                case "DirectoryNode":
                    return new LODirectoryNode();
                case "Escrow":
                    return new LOEscrow();
                case "FeeSettings":
                    return new LOFeeSettings();
                case "LedgerHashes":
                    return new LOLedgerHashes();
                case "Offer":
                    return new LOOffer();
                case "PayChannel":
                    return new LOPayChannel();
                case "RippleState":
                    return new LORippleState();
                case "SignerList":
                    return new LOSignerList();
                case "NFTokenOffer":
                    return new LONFTokenOffer(); //todo new type
                case "Ticket":
                    return new LOTicket();
            }
            throw new Exception("Can't create ledger type" + ledgerEntryType);
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}
