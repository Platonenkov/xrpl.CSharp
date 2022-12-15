using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading.Tasks;

using Xrpl.Client.Exceptions;
using Xrpl.Client.Models.Ledger;
using Xrpl.Client.Models.Methods;
using Xrpl.Client.Models.Transactions;

using xrpl_c.Xrpl.Client.Models.Subscriptions;

using BookOffers = Xrpl.Client.Models.Transactions.BookOffers;
using ChannelAuthorize = Xrpl.Client.Models.Transactions.ChannelAuthorize;
using ChannelVerify = Xrpl.Client.Models.Transactions.ChannelVerify;
using Submit = Xrpl.Client.Models.Transactions.Submit;

namespace Xrpl.Client
{
    public delegate Task OnDisconnected(WebSocketClient client);
    public delegate Task OnRippleResponse(string response);
    public delegate Task OnLedgerStreamResponse(LedgerStreamResponseResult response);
    public delegate Task OnValidationsStreamResponse(ValidationsStreamResponseResult response);
    public delegate Task OnTransactionStreamResponse(TransactionStreamResponseResult response);
    public delegate Task OnPeerStatusStreamResponse(PeerStatusStreamResponseResult response);
    public delegate Task OnConsensusStreamResponse(ConsensusStreamResponseResult response);
    public delegate Task OnPathFindResponse(PathFindStreamResult response);
    public delegate Task OnErrorResponse(ErrorResponse response);
    public delegate Task OnClientError(Exception e, WebSocketClient client);
    public delegate Task OnClientReconnect(IRippleClient client);

    public interface IRippleClient : IDisposable
    {
        event OnDisconnected OnDisconnected;
        event OnLedgerStreamResponse OnLedgerClosed;
        event OnValidationsStreamResponse OnValidation;
        event OnTransactionStreamResponse OnTransaction;
        event OnPeerStatusStreamResponse OnPeerStatusChange;
        event OnConsensusStreamResponse OnConsensusPhase;
        event OnPathFindResponse OnPathFind;
        event OnErrorResponse OnError;
        event OnRippleResponse OnResponse;
        event OnClientError OnClientError;
        event OnClientReconnect OnReconnect;

        public WebSocketState ClientState { get; }
        #region Server

        /// <summary> connect to the server </summary>
        void Connect();
        /// <summary> Disconnect from server </summary>
        void Disconnect();
        /// <summary> The subscribe method requests periodic notifications from the server when certain events happen. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.SubscribeRequest"/> request.</param>
        /// <returns></returns>
        Task<object> Subscribe(SubscribeRequest request);
        /// <summary> The unsubscribe command tells the server to stop sending messages for a particular subscription or set of subscriptions.</summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.UnSubscribeRequest"/> request.</param>
        /// <returns></returns>
        Task<object> UnSubscribe(UnSubscribeRequest request);

        /// <summary> The ping command returns an acknowledgement,
        /// so that clients can test the connection status and latency </summary>
        /// <returns></returns>
        Task Ping();

        /// <summary> The server_info command asks the server for a human-readable version of various information about the rippled server being queried. </summary>
        /// <returns></returns>
        Task<ServerInfo> ServerInfo();
        /// <summary> The fee command reports the current state of the open-ledger requirements for the transaction cost. </summary>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.Fee"/> response.</returns>
        Task<Fee> Fees();


        #endregion

        #region Account

        #region Info

        /// <summary> The account_info command retrieves information about an account, its activity, and its XRP balance. </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountInfo"/> response.</returns>
        Task<AccountInfo> AccountInfo(string account);

        /// <summary> The account_info command retrieves information about an account, its activity, and its XRP balance. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountInfoRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountInfo"/> response.</returns>
        Task<AccountInfo> AccountInfo(AccountInfoRequest request);



        #endregion

        #region Offers

        /// <summary> The account_offers method retrieves a list of offers made by a given account that are outstanding as of a particular ledger version </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountOffers"/> response.</returns>
        Task<AccountOffers> AccountOffers(string account);

        /// <summary> The account_offers method retrieves a list of offers made by a given account that are outstanding as of a particular ledger version </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountOffersRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountOffers"/> response.</returns>
        Task<AccountOffers> AccountOffers(AccountOffersRequest request);

        #endregion

        #region Currencies

        /// <summary> The account_currencies command retrieves a list of currencies that an account can send or receive, based on its trust lines. </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountCurrencies"/> response.</returns>
        Task<AccountCurrencies> AccountCurrencies(string account);

        /// <summary> The account_currencies command retrieves a list of currencies that an account can send or receive, based on its trust lines. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountCurrenciesRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountCurrencies"/> response.</returns>
        Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request);

        #endregion

        #region Trustlines

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="account">The account number to query.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(string account);

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountLinesRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(AccountLinesRequest request);

        #endregion

        #region Objects

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(string account);

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountObjectsRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(AccountObjectsRequest request);


        #endregion

        #region NoRipple

        /// <summary>
        /// The noripple_check command provides a quick way to check the status of the Default Ripple field
        /// for an account and the No Ripple flag of its trust lines, compared with the recommended settings
        /// </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NoRippleCheck"/> response.</returns>
        Task<NoRippleCheck> NoRippleCheck(string account);

        /// <summary>
        /// The noripple_check command provides a quick way to check the status of the Default Ripple field
        /// for an account and the No Ripple flag of its trust lines, compared with the recommended settings
        /// </summary>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NoRippleCheckRequest"/> response.</returns>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NoRippleCheck"/> response.</returns>
        Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request);


        #endregion

        #region Balance

        /// <summary> The gateway_balances command calculates the total balances issued by a given account,
        /// optionally excluding amounts held by operational addresses. </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.GatewayBalances"/> response.</returns>
        Task<GatewayBalances> GatewayBalances(string account);

        /// <summary> The gateway_balances command calculates the total balances issued by a given account,
        /// optionally excluding amounts held by operational addresses. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.GatewayBalancesRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.GatewayBalances"/> response.</returns>
        Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request);


        #endregion

        #endregion

        #region NFT


        /// <summary> The nft_buy_offers method returns a list of buy offers for a given NFToken object. </summary>
        /// <param name="nft_id">The unique identifier of a NFToken object.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NFTBuyOffers"/> response.</returns>
        Task<NFTBuyOffers> NftBuyOffers(string nft_id);

        /// <summary> The nft_buy_offers method returns a list of buy offers for a given NFToken object. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.NFTBuyOffersRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NFTBuyOffers"/> response.</returns>
        Task<NFTBuyOffers> NftBuyOffers(NFTBuyOffersRequest request);

        /// <summary> The nft_sell_offers method returns a list of sell offers for a given NFToken object</summary>
        /// <param name="nft_id">The unique identifier of a NFToken object.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NFTSellOffers"/> response.</returns>
        Task<NFTSellOffers> NftSellOffers(string nft_id);

        /// <summary> The nft_sell_offers method returns a list of sell offers for a given NFToken object</summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.NFTSellOffersRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.NFTSellOffers"/> response.</returns>
        Task<NFTSellOffers> NftSellOffers(NFTSellOffersRequest request);

        /// <summary> The account_nfts method returns a list of NFToken objects for the specified account.</summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountNFTs"/> response.</returns>
        Task<AccountNFTs> AccountNfTs(string account);

        /// <summary> The account_nfts method returns a list of NFToken objects for the specified account.</summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountNFTsRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountNFTs"/> response.</returns>
        Task<AccountNFTs> AccountNfTs(AccountNFTsRequest request);


        #endregion

        #region Transactions

        /// <summary> The account_tx method retrieves a list of transactions that involved the specified account </summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountTransactions"/> response.</returns>
        Task<AccountTransactions> AccountTransactions(string account);

        /// <summary> The account_tx method retrieves a list of transactions that involved the specified account </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountTransactionsRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountTransactions"/> response.</returns>
        Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request);
        /// <summary> The tx method retrieves information on a single transaction, by its identifying hash </summary>
        /// <param name="transaction">The 256-bit hash of the transaction, as hex.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.ITransactionResponseCommon"/> response.</returns>
        Task<ITransactionResponseCommon> Transaction(string transaction);
        /// <summary> The tx method retrieves information on a single transaction, by its identifying hash </summary>
        /// <param name="transaction">The 256-bit hash of the transaction, as hex.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.IBaseTransactionResponse"/> response.</returns>
        Task<IBaseTransactionResponse> TransactionAsBinary(string transaction);

        /// <summary> The submit method applies a transaction and sends it to the network to be confirmed and included in future ledgers. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.SubmitBlobRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.Submit"/> response.</returns>
        Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request);

        /// <summary> The submit method applies a transaction and sends it to the network to be confirmed and included in future ledgers. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.SubmitRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.Submit"/> response.</returns>
        Task<Submit> SubmitTransaction(SubmitRequest request);

        #endregion

        #region Channels

        /// <summary> The account_channels method returns information about an account's Payment Channels.
        /// This includes only channels where the specified account is the channel's source, not the destination.</summary>
        /// <param name="account">The unique identifier of an account, typically the account's Address.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountChannels"/> response.</returns>
        Task<AccountChannels> AccountChannels(string account);

        /// <summary> The account_channels method returns information about an account's Payment Channels.
        /// This includes only channels where the specified account is the channel's source, not the destination. </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.AccountChannelsRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Methods.AccountChannels"/> response.</returns>
        Task<AccountChannels> AccountChannels(AccountChannelsRequest request);

        /// <summary> The channel_authorize method creates a signature that can be used to redeem a specific amount of XRP from a payment channel </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.ChannelAuthorizeRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.ChannelAuthorize"/> response.</returns>
        Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request);

        /// <summary>The channel_verify method checks the validity of a signature that can be used to redeem a specific amount of XRP from a payment channel</summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.ChannelVerifyRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.ChannelAuthorize"/> response.</returns>
        Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request);

        #endregion

        #region Ledger

        /// <summary>
        /// The ledger_request command tells server to fetch a specific ledger version from its connected peers.
        /// This only works if one of the server's immediately-connected peers has that ledger.
        /// You may need to run the command several times to completely fetch a ledger
        /// </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.LedgerRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Ledger.LOLedger"/> response.</returns>
        Task<LOLedger> Ledger(LedgerRequest request);
        /// <summary> The ledger_closed method returns the unique identifiers of the most recently closed ledger. </summary>
        /// <returns>An <see cref="Xrpl.Client.Models.Ledger.LOBaseLedger"/> response.</returns>
        Task<LOBaseLedger> ClosedLedger();

        /// <summary>
        /// The ledger_current method returns the unique identifiers of the current in-progress ledger.
        /// This command is mostly useful for testing, because the ledger returned is still in flux.
        /// </summary>
        /// <returns>An <see cref="Xrpl.Client.Models.Ledger.LOLedgerCurrentIndex"/> response.</returns>
        Task<LOLedgerCurrentIndex> CurrentLedger();

        /// <summary>
        /// The ledger_data method retrieves contents of the specified ledger.
        /// You can iterate through several calls to retrieve the entire contents of a single ledger version.
        /// </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.LedgerDataRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Ledger.LOLedgerData"/> response.</returns>
        Task<LOLedgerData> LedgerData(LedgerDataRequest request);


        #endregion


        /// <summary>
        /// The book_offers method retrieves a list of offers, also known as the order book , between two currencies
        /// </summary>
        /// <param name="request">An <see cref="Xrpl.Client.Models.Methods.BookOffersRequest"/> request.</param>
        /// <returns>An <see cref="Xrpl.Client.Models.Transactions.BookOffers"/> response.</returns>
        Task<BookOffers> BookOffers(BookOffersRequest request);

    }

    public class RippleClient : IRippleClient
    {
        public event OnDisconnected OnDisconnected;
        public event OnLedgerStreamResponse OnLedgerClosed;
        public event OnValidationsStreamResponse OnValidation;
        public event OnTransactionStreamResponse OnTransaction;
        public event OnPeerStatusStreamResponse OnPeerStatusChange;
        public event OnConsensusStreamResponse OnConsensusPhase;
        public event OnPathFindResponse OnPathFind;
        public event OnErrorResponse OnError;
        public event OnClientError OnClientError;
        public event OnRippleResponse OnResponse;
        public event OnClientReconnect OnReconnect;

        private WebSocketClient _Client;
        private readonly ConcurrentDictionary<Guid, TaskInfo> _Tasks;
        private readonly JsonSerializerSettings _SerializerSettings;

        private readonly string Url;

        public WebSocketState ClientState => _Client.State;

        public RippleClient(string url)
        {
            Url = url;
            _SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            OnResponse += OnMessageReceived;
            _Tasks = new ConcurrentDictionary<Guid, TaskInfo>();

            Ininitialize();
        }

        void Ininitialize()
        {
            Debug.WriteLine("Initialize socket client");

            _Client = WebSocketClient.Create(Url);
            _Client.OnMessageReceived(MessageReceived);

            _Client.OnConnectionError(
                async (Exception, Client) =>
                {
                    await Error(Exception, Client);
                    await OnClientReconnect();
                });
            _Client.OnClosed(_ => OnClientReconnect());

            _Client.OnDisconnect(Client => OnDisconnected?.Invoke(Client));

            Debug.WriteLine("websocket client fully initialized and ready to work");
        }

        Task OnClientReconnect()
        {
            Debug.WriteLine("Reconnecting the client");
            Ininitialize();
            Connect();
            OnReconnect?.Invoke(this);
            Debug.WriteLine("Client reconnected");
            return Task.CompletedTask;
        }
        #region Server

        /// <inheritdoc />
        public void Connect()
        {
            _Client.OnMessageReceived(MessageReceived);

            _Client.Connect();
            do
            {
                if (_Client.IsDisposed)
                    break;
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (_Client.State != WebSocketState.Open);
        }
        /// <inheritdoc />
        public void Disconnect()
        {
            OnResponse -= OnMessageReceived;
            _Client.Cancel();
            _Client.Disconnect();
        }

        /// <inheritdoc />
        public Task<object> Subscribe(SubscribeRequest request)
        {

            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.RemoveUponCompletion = false;
            task_info.Type = typeof(object);

            _Tasks.TryAdd(request.Id, task_info);
            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<object> UnSubscribe(UnSubscribeRequest request)
        {

            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.RemoveUponCompletion = false;
            task_info.Type = typeof(object);

            _Tasks.TryAdd(request.Id, task_info);
            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task Ping()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "ping";

            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(object);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        /// <inheritdoc />
        public Task<ServerInfo> ServerInfo()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "server_info";

            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<ServerInfo> task = new TaskCompletionSource<ServerInfo>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(ServerInfo);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<Fee> Fees()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "fee";

            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<Fee> task = new TaskCompletionSource<Fee>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(Fee);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }


        #endregion

        #region Account

        #region Info

        /// <inheritdoc />
        public Task<AccountInfo> AccountInfo(string account)
        {
            AccountInfoRequest request = new AccountInfoRequest(account);
            return AccountInfo(request);
        }
        /// <inheritdoc />
        public Task<AccountInfo> AccountInfo(AccountInfoRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountInfo> task = new TaskCompletionSource<AccountInfo>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountInfo);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Offers

        /// <inheritdoc />
        public Task<AccountOffers> AccountOffers(string account)
        {
            AccountOffersRequest request = new AccountOffersRequest(account);
            return AccountOffers(request);
        }
        /// <inheritdoc />
        public Task<AccountOffers> AccountOffers(AccountOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountOffers> task = new TaskCompletionSource<AccountOffers>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountOffers);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Currencies

        /// <inheritdoc />
        public Task<AccountCurrencies> AccountCurrencies(string account)
        {
            AccountCurrenciesRequest request = new AccountCurrenciesRequest(account);
            return AccountCurrencies(request);
        }
        /// <inheritdoc />
        public Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountCurrencies> task = new TaskCompletionSource<AccountCurrencies>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountCurrencies);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Trustlines

        /// <inheritdoc />
        public Task<AccountLines> AccountLines(string account)
        {
            AccountLinesRequest request = new AccountLinesRequest(account);
            return AccountLines(request);
        }
        /// <inheritdoc />
        public Task<AccountLines> AccountLines(AccountLinesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountLines> task = new TaskCompletionSource<AccountLines>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountLines);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Objects

        /// <inheritdoc />
        public Task<AccountObjects> AccountObjects(string account)
        {
            AccountObjectsRequest request = new AccountObjectsRequest(account);
            return AccountObjects(request);
        }
        /// <inheritdoc />
        public Task<AccountObjects> AccountObjects(AccountObjectsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountObjects> task = new TaskCompletionSource<AccountObjects>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountObjects);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region NoRipple

        /// <inheritdoc />
        public Task<NoRippleCheck> NoRippleCheck(string account)
        {
            NoRippleCheckRequest request = new NoRippleCheckRequest(account);
            return NoRippleCheck(request);
        }
        /// <inheritdoc />
        public Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<NoRippleCheck> task = new TaskCompletionSource<NoRippleCheck>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(NoRippleCheck);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Balance

        /// <inheritdoc />
        public Task<GatewayBalances> GatewayBalances(string account)
        {
            GatewayBalancesRequest request = new GatewayBalancesRequest(account);
            return GatewayBalances(request);
        }
        /// <inheritdoc />
        public Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<GatewayBalances> task = new TaskCompletionSource<GatewayBalances>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(GatewayBalances);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #endregion

        #region NFT

        /// <inheritdoc />
        public Task<NFTBuyOffers> NftBuyOffers(string nft_id)
        {
            NFTBuyOffersRequest request = new NFTBuyOffersRequest(nft_id);
            return NftBuyOffers(request);
        }
        /// <inheritdoc />
        public Task<NFTBuyOffers> NftBuyOffers(NFTBuyOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<NFTBuyOffers> task = new TaskCompletionSource<NFTBuyOffers>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(NFTBuyOffers);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<NFTSellOffers> NftSellOffers(string nft_id)
        {
            NFTSellOffersRequest request = new NFTSellOffersRequest(nft_id);
            return NftSellOffers(request);
        }
        /// <inheritdoc />
        public Task<NFTSellOffers> NftSellOffers(NFTSellOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<NFTSellOffers> task = new TaskCompletionSource<NFTSellOffers>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(NFTSellOffers);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<AccountNFTs> AccountNfTs(string account)
        {
            AccountNFTsRequest request = new AccountNFTsRequest(account);
            return AccountNfTs(request);
        }
        /// <inheritdoc />
        public Task<AccountNFTs> AccountNfTs(AccountNFTsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountNFTs> task = new TaskCompletionSource<AccountNFTs>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountNFTs);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Transactions

        /// <inheritdoc />
        public Task<AccountTransactions> AccountTransactions(string account)
        {
            AccountTransactionsRequest request = new AccountTransactionsRequest(account);
            return AccountTransactions(request);
        }
        /// <inheritdoc />
        public Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountTransactions> task = new TaskCompletionSource<AccountTransactions>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountTransactions);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<ITransactionResponseCommon> Transaction(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<ITransactionResponseCommon> task = new TaskCompletionSource<ITransactionResponseCommon>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(TransactionResponseCommon);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<IBaseTransactionResponse> TransactionAsBinary(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            request.Binary = true;
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<IBaseTransactionResponse> task = new TaskCompletionSource<IBaseTransactionResponse>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(BinaryTransactionResponse);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;

        }

        /// <inheritdoc />
        public Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(Submit);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<Submit> SubmitTransaction(SubmitRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(Submit);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Channels

        /// <inheritdoc />
        public Task<AccountChannels> AccountChannels(string account)
        {
            AccountChannelsRequest request = new AccountChannelsRequest(account);
            return AccountChannels(request);
        }
        /// <inheritdoc />
        public Task<AccountChannels> AccountChannels(AccountChannelsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<AccountChannels> task = new TaskCompletionSource<AccountChannels>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(AccountChannels);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<ChannelAuthorize> task = new TaskCompletionSource<ChannelAuthorize>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(ChannelAuthorize);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<ChannelVerify> task = new TaskCompletionSource<ChannelVerify>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(ChannelVerify);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        #region Ledger

        /// <inheritdoc />
        public Task<LOLedger> Ledger(LedgerRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<LOLedger> task = new TaskCompletionSource<LOLedger>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = Guid.Parse("1A3B944E-3632-467B-A53A-206305310BAE");
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(LOLedger);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<LOBaseLedger> ClosedLedger()
        {
            ClosedLedgerRequest request = new ClosedLedgerRequest();
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<LOBaseLedger> task = new TaskCompletionSource<LOBaseLedger>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(LOBaseLedger);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;

        }
        /// <inheritdoc />
        public Task<LOLedgerCurrentIndex> CurrentLedger()
        {
            CurrentLedgerRequest request = new CurrentLedgerRequest();
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<LOLedgerCurrentIndex> task = new TaskCompletionSource<LOLedgerCurrentIndex>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(LOLedgerCurrentIndex);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }
        /// <inheritdoc />
        public Task<LOLedgerData> LedgerData(LedgerDataRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<LOLedgerData> task = new TaskCompletionSource<LOLedgerData>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(LOLedgerData);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        #endregion

        /// <inheritdoc />
        public Task<BookOffers> BookOffers(BookOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, _SerializerSettings);
            TaskCompletionSource<BookOffers> task = new TaskCompletionSource<BookOffers>();

            TaskInfo task_info = new TaskInfo();
            task_info.TaskId = request.Id;
            task_info.TaskCompletionResult = task;
            task_info.Type = typeof(BookOffers);

            _Tasks.TryAdd(request.Id, task_info);

            _Client.SendMessage(command);
            return task.Task;
        }

        private async Task Error(Exception ex, WebSocketClient client)
        {
            if (OnClientError is not null)
                await OnClientError.Invoke(ex, client);
            //throw new Exception(ex.Message, ex);
        }

        private Task MessageReceived(string s, WebSocketClient client)
        {
            if (string.IsNullOrWhiteSpace(s))
                return Task.CompletedTask;

            var json = JObject.Parse(s);
            var can_get_type = json.TryGetValue("type", out var response_type);
            if (!can_get_type)
                throw new ArgumentNullException("type", "Unknown response type");
            Enum.TryParse(response_type.ToString(), out ResponseStreamType type);

            switch (type)
            {
                case ResponseStreamType.response:
                    {
                        OnResponse?.Invoke(s);
                        break;
                    }
                case ResponseStreamType.connected:
                    {
                        break;
                    }
                case ResponseStreamType.disconnected:
                    {
                        break;
                    }
                case ResponseStreamType.ledgerClosed:
                    {
                        var response = JsonConvert.DeserializeObject<LedgerStreamResponseResult>(s);
                        OnLedgerClosed?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.validationReceived:
                    {
                        var response = JsonConvert.DeserializeObject<ValidationsStreamResponseResult>(s);
                        OnValidation?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.transaction:
                    {
                        var response = JsonConvert.DeserializeObject<TransactionStreamResponseResult>(s);
                        OnTransaction?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.peerStatusChange:
                    {
                        var response = JsonConvert.DeserializeObject<PeerStatusStreamResponseResult>(s);
                        OnPeerStatusChange?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.consensusPhase:
                    {
                        var response = JsonConvert.DeserializeObject<ConsensusStreamResponseResult>(s);
                        OnConsensusPhase?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.path_find:
                    {
                        var response = JsonConvert.DeserializeObject<PathFindStreamResult>(s);
                        OnPathFind?.Invoke(response);
                        break;
                    }
                case ResponseStreamType.error:
                    {
                        var response = JsonConvert.DeserializeObject<ErrorResponse>(s);
                        OnError?.Invoke(response);
                        break;
                    }
                default: throw new ArgumentOutOfRangeException();
            }
            return Task.CompletedTask;
        }

        private Task OnMessageReceived(string s)
        {
            var response = JsonConvert.DeserializeObject<RippleResponse>(s);

            try
            {
                var task_info_result = _Tasks.TryGetValue(response.Id, out var task_info);
                if (task_info_result == false) throw new Exception("Task not found");

                if (response.Status == "success")
                {
                    Debug.WriteLine($"RESPONSE {response.Id} : {response.Result.ToString()}");
                    var deserialized = JsonConvert.DeserializeObject(response.Result.ToString(), task_info.Type, _SerializerSettings);
                    var set_result = task_info.TaskCompletionResult.GetType().GetMethod("SetResult");
                    set_result.Invoke(task_info.TaskCompletionResult, new[] { deserialized });

                    if (task_info.RemoveUponCompletion)
                    {
                        _Tasks.TryRemove(response.Id, out task_info);
                    }
                }
                else if (response.Status == "error")
                {
                    var set_exception = task_info.TaskCompletionResult.GetType().GetMethod("SetException", new Type[] { typeof(Exception) }, null);

                    RippleException exception = new RippleException(response.Error);
                    set_exception.Invoke(task_info.TaskCompletionResult, new[] { exception });

                    _Tasks.TryRemove(response.Id, out task_info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var task_info_result = _Tasks.TryGetValue(response.Id, out var task_info);
                var set_exception = task_info.TaskCompletionResult.GetType().GetMethod("SetException", new Type[] { typeof(Exception) }, null);

                RippleException exception = new RippleException(response.Error ?? e.Message, e);
                set_exception.Invoke(task_info.TaskCompletionResult, new[] { exception });

                _Tasks.TryRemove(response.Id, out task_info);
            }

            return Task.CompletedTask;
        }

        #region IDisposable

        public void Dispose()
        {
            _Client?.Disconnect();
            _Client?.Dispose();
        }

        #endregion
    }
}
