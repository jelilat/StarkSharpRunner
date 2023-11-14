using System;
using System.Numerics;
using AdronCore.Connectors.Components;
using AdronCore.Rpc;
using AdronCore.Tools.Notification;

namespace AdronCore.Platforms
{
    public class Platform 
    {
        public enum PlatformName { CryEngine, Unity, Godot, Dotnet }
        public enum PlatformConnectorType { WebGL, Sharpion, HTML5,RPC }
        public virtual void ConnectWallet(string walletType, int id) { }
        public virtual void SendTransaction(string walletType, int id, string contractAddress, string entryPoint, string callData) { }

        public virtual void SendTransaction(Platform platform, TransactionInteraction transactionInteraction,
            Action<JsonRpcResponse> successCallback, Action<JsonRpcResponse> errorCallback) { }
        public virtual void CallContract(ContractInteraction contractInteraction, Action<string> successCallback, Action<string> errorCallback) { }
        public virtual void WaitUntil(int id, Action<string> successCallback, Action<string> failCallback, Func<bool> predicate, Action<int, Action<string>, Action<string>> action) { }
        public virtual bool CheckWalletConnection() { return true; }
        public virtual string GetAccountInformation() { return string.Empty; }
        public virtual void DebugMessage(string message) { }
        public virtual void PlatformRequest(JsonRpc requestData,  Action<JsonRpcResponse> Callback){ }
        public virtual void PlatformLog(string LogMessage, NotificationType notitype) { }
    }
}