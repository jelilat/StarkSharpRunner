﻿using System.Numerics;
using AdronCore.Connectors.Components;

namespace AdronCore.Fusion.Sharpion.Manager.IonPlatforms
{
    public class IonPlatform
    {
        public virtual void ConnectToServer() { }
        public virtual void DisconnectToServer() { }
        public virtual bool ConnectionStatus() => false;
        public virtual void ConnectWallet() { }
        public virtual void DisconnectWallet() { }
        public virtual void BalanceOf(string walletadress) { }
        public virtual void SendTransaction(TransactionInteraction transactionInteraction) { }

    }
}
