//using StarkSharp.Fusion.Sharpion.Manager.IonPlatforms.Unity;
using System;
using System.Numerics;
using AdronCore.Connectors.Components;
using AdronCore.Fusion.Sharpion.Manager.IonPlatforms;
using AdronCore.Fusion.Sharpion.Manager.IonPlatforms.Dotnet;
using AdronCore.Platforms;
using static AdronCore.Platforms.Platform;

namespace AdronCore.Fusion.Sharpion.Manager
{
    public class SharpionManager
    {

        IonPlatform IonPlatform;
        public SharpionManager(IonPlatform platform) {this.IonPlatform = platform;}

        public static SharpionManager New(Platform.PlatformName name)
        {
            IonPlatform platform;

            switch (name)
            {
                case Platform.PlatformName.Dotnet:
                    platform = new IonDotnet();
                    break;
                // case PlatformName.Unity:
                //     platform = new IonUnity();
                //     break;
                default:
                    throw new NotSupportedException($"Platform '{name}' is not supported.");
            }

            return new SharpionManager(platform);
        }


        public virtual void ConnectToServer() { IonPlatform.ConnectToServer(); }
        public virtual void DisconnectToServer() { IonPlatform.DisconnectToServer(); }
        public virtual bool ConnectionStatus() { return IonPlatform.ConnectionStatus(); }
        public virtual void ConnectWallet() { IonPlatform.ConnectWallet(); }
        public virtual void DisconnectWallet() { IonPlatform.DisconnectWallet(); }
        public virtual void BalanceOf(string walletadress) { IonPlatform.BalanceOf(walletadress); }
        public virtual void SendTransaction(TransactionInteraction transactionInteraction) { IonPlatform.SendTransaction(transactionInteraction); }

    }
}
