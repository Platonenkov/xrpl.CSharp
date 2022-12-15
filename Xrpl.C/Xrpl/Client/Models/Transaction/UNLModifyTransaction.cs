using Xrpl.Client.Models.Enums;
using Xrpl.Client.Models.Transactions;

namespace xrpl_c.Xrpl.Client.Models.Transaction
{
    public class UNLModifyTransaction : TransactionCommon, IUNLModifyTransaction
    {
        public UNLModifyTransaction()
        {
            TransactionType = TransactionType.UNLModify;
        }


        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public uint UNLModifyDisabling { get; set; }
        /// <summary>
        /// The ledger index where this pseudo-transaction appears. This distinguishes the pseudo-transaction from other occurrences of the same change.
        /// </summary>
        public uint LedgerSequence { get; set; }
        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public string UNLModifyValidator { get; set; }
    }

    public interface IUNLModifyTransaction : ITransactionCommon
    {
        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public uint UNLModifyDisabling { get; set; }
        /// <summary>
        /// The ledger index where this pseudo-transaction appears. This distinguishes the pseudo-transaction from other occurrences of the same change.
        /// </summary>
        public uint LedgerSequence { get; set; }
        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public string UNLModifyValidator { get; set; }
    }

    public class UNLModifyTransactionResponse : TransactionResponseCommon, IUNLModifyTransaction
    {
        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public uint UNLModifyDisabling { get; set; }
        /// <summary>
        /// The ledger index where this pseudo-transaction appears. This distinguishes the pseudo-transaction from other occurrences of the same change.
        /// </summary>
        public uint LedgerSequence { get; set; }
        /// <summary>
        /// If 1, this change represents adding a validator to the Negative UNL.
        /// If 0, this change represents removing a validator from the Negative UNL. (No other values are allowed.)
        /// </summary>
        public string UNLModifyValidator { get; set; }
    }
}
