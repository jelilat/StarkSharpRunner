

using AdronCore;
using AdronCore.Connectors;
using AdronCore.Connectors.Components;
using AdronCore.Platforms;
using AdronCore.Platforms.Dotnet;
using AdronCore.Rpc;
using AdronCore.Settings;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;

public class Program
{
    public static void Main()
    {
        Settings.apiurl = "REPLACE_ME";

        string senderAddress = "REPLACE_ME";
        string myswapAddress = "0x10884171baf1914edc28d7afb619b40a4051cfae78a094a55d230f19e944a28";
        string tokenAddress = "0x49d36570d4e46f48e99674bd3fcc84644ddd6b96f7c741b1562b82f9e004dc7";
        string functionName = "approve";

        string[] functionArgsApprove = GetApproveArgs();
        string[] functionArgsSwap = GetMySwapArgs();

        CairoVersion cairoVersion = CairoVersion.Version0; // StarknetEth Contract is using CairoVersion.Version0
        string maxFee = "0xa6608711978c"; // Flexible
        // account1
        string chainId = "0x534e5f4d41494e"; // SN_MAIN
        string privateKey = "REPLACE_ME"; // bravvos
        string version = "1"; // https://docs.alchemy.com/reference/starknet-addinvoketransaction    0 or 1

        TransactionInteraction transactionInteractionApprove
            = new TransactionInteraction(senderAddress, tokenAddress, functionName, functionArgsApprove, cairoVersion,
                maxFee, chainId, privateKey, version);

        functionName = "swap";
        TransactionInteraction transactionInteractionSwap
            = new TransactionInteraction(senderAddress, myswapAddress, functionName, functionArgsSwap, cairoVersion,
                maxFee, chainId, privateKey, version);
        Platform platform = DotnetPlatform.New(Platform.PlatformConnectorType.RPC);
        Connector connector = new Connector(platform);

        connector.SendTransaction(transactionInteractionApprove, response => OnSendTransactionSuccess(response),
            errorMessage => OnSendTransactionError(errorMessage));
        connector.SendTransaction(transactionInteractionSwap, response => OnSendTransactionSuccess(response),
            errorMessage => OnSendTransactionError(errorMessage));
    }

    private static string[] GetApproveArgs()
    {
        string spender = "0x10884171baf1914edc28d7afb619b40a4051cfae78a094a55d230f19e944a28";
        string uint256 = "0x1";
        string padding = "0x0"; // whenever we have a uint256 parameter, we need to add "0x0"
        return new[] { spender, uint256, padding };
    }

    private static string[] GetMySwapArgs()
    {
        string pool_id = "2";
        string token_from_addr = "0x049d36570d4e46f48e99674bd3fcc84644ddd6b96f7c741b1562b82f9e004dc7";
        string amount_from = "1000000000000000";
        string amount_to_min = "1779276970132355228";
        return new[] { pool_id, token_from_addr, amount_from, amount_to_min };
    }
    private static void OnSendTransactionError(JsonRpcResponse errorMessage)
    {
        Console.WriteLine("Error: ");
        Console.WriteLine(errorMessage);
    }

    private static void OnSendTransactionSuccess(JsonRpcResponse response)
    {
        Console.WriteLine("Success: ");
        Console.WriteLine(response);
    }
}
