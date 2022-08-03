﻿
using Xrpl.Client.Models.Enums;

namespace Xrpl.Client.Models.Transactions
{
    public enum AccountSetFlags
    {
        asfRequireDest = 1,
        asfRequireAuth = 2,
        asfDisallowXRP = 3,
        asfDisableMaster = 4,
        asfAccountTxnID = 5,
        asfNoFreeze = 6,
        asfGlobalFreeze = 7,
        asfDefaultRipple = 8
    }
    public class AccountSetTransaction : TransactionCommon, IAccountSetTransaction
    {
        public AccountSetTransaction()
        {
            TransactionType = TransactionType.AccountSet;
        }

        public AccountSetTransaction(string account) : this()
        {
            Account = account;
        }

        public uint? ClearFlag { get; set; }

        public string Domain { get; set; }

        public string EmailHash { get; set; }

        public string MessageKey { get; set; }

        public uint? SetFlag { get; set; }

        public uint? TransferRate { get; set; }

        public uint? TickSize { get; set; }
    }

    public interface IAccountSetTransaction : ITransactionCommon
    {
        uint? ClearFlag { get; set; }

        string Domain { get; set; }

        string EmailHash { get; set; }

        string MessageKey { get; set; }

        uint? SetFlag { get; set; }

        uint? TransferRate { get; set; }

        uint? TickSize { get; set; }
    }

    public class AccountSetTransactionResponse : TransactionResponseCommon, IAccountSetTransaction
    {
        public uint? ClearFlag { get; set; }
        public string Domain { get; set; }
        public string EmailHash { get; set; }
        public string MessageKey { get; set; }
        public uint? SetFlag { get; set; }
        public uint? TransferRate { get; set; }
        public uint? TickSize { get; set; }        
    }
}
