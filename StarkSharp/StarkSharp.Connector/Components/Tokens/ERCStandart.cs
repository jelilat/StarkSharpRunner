using AdronCore.Connectors.Components;
using Newtonsoft.Json;

namespace AdronCore.Components.Token
{
    public class ERCStandart
    {
        public static ContractInteraction GenerateStandartData(string contractAddress, string entryPoint, string[] callData)
        {
            string callDataString = JsonConvert.SerializeObject(callData);
            return new ContractInteraction(contractAddress, entryPoint, callDataString);
        }
    }
}
