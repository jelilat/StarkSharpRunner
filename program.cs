

using AdronCore;
using AdronCore.Connectors;
using AdronCore.Connectors.Components;
using AdronCore.Platforms;
using AdronCore.Platforms.Dotnet;
using AdronCore.Rpc;
using Nethereum.Web3.Accounts;

public class Program
{
    
    public static void Main()
    {
        Console.WriteLine("Hello world!");
        
        string senderAddress = "0x0680Bdc79d2A03a644B2aaB51AE84C44cA87bD283EAFa36aF48dac8151153D4e";

        string ethAddress = "0x049d36570d4e46f48e99674bd3fcc84644ddd6b96f7c741b1562b82f9e004dc7";
        
        string contractAddress = "myswap";
        string functionName = "approve";
        
        //string[] functionArgs = new[] { "" , ""};
        string[] functionArgsApprove = GetApproveArgs();

        string[] functionArgsSwap = GetMySwapArgs();
        
        CairoVersion cairoVersion = CairoVersion.Version2;
        string maxFee = "10000000000000000000";
        string chainIdBig = "23448594291968334";
        // account1
        string chainId = "0x534e5f4d41494e";
        //string privateKey = ""; //account1
        //string privateKey = ""; // account2
        string privateKey = ""; // bravvos
        string version = "1"; // https://docs.alchemy.com/reference/starknet-addinvoketransaction    0 or 1
        
        
        TransactionInteraction transactionInteractionApprove 
            = new TransactionInteraction(senderAddress, ethAddress, functionName, functionArgsApprove, cairoVersion,
                maxFee, chainId, privateKey, version);

        TransactionInteraction transactionInteractionSwap
            = new TransactionInteraction(senderAddress, contractAddress, functionName, functionArgsSwap, cairoVersion,
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
        string uint256 = "1000000000000000";
        return new[] { spender, uint256 };
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
        throw new NotImplementedException();
    }

    private static void OnSendTransactionSuccess(JsonRpcResponse response)
    {
        throw new NotImplementedException();
    }
}
