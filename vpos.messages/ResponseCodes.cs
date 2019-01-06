using System.Collections.Generic;

namespace vpos.messages
{
    /// <summary>
    /// Response codes.
    /// </summary>
    public enum ResponseCodes
    {
        G1001,
        G1002,
        A1001,
        A1002,
        A1003,
        D1001,
        D1002,
        W1001,
        W1002,
        W1003
    }

    /// <summary>
    /// Response code messages.
    /// </summary>
    public static class ResponseCodeMessages
    {
        public static Dictionary<string, string> List = new Dictionary<string, string>
        {
            {"G1001","System Error."},
            {"G1002","Internal Error."},
            {"A1001","Account is not found!"},
            {"A1002","Account informations is not correct."},
            {"A1003","Account informations can't be empty."},
            {"D1001","Deposit amount can't be empty"},
            {"D1002","Deposit amount can't be less than or equal to zero!"},
            {"W1001","Withdraw amount can't be empty"},
            {"W1002","Withdraw amount can't be less than or equal to zero!"},
            {"W1003","Balance is not enough for this withdraw."}

        };
    }
}