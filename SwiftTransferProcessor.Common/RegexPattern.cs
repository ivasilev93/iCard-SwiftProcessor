using System;
using System.Collections.Generic;
using System.Text;

namespace SwiftTransferProcessor.Common
{
    public static class RegexPattern
    {
        public const string SenderBIC = @"{1:F01([a-zA-Z0-9]{8})[a-zA-Z]{4}[0-9]{4}[0-9]{6}}";

        public const string BankOperationCode = @":23B:([A-Z]{4})";

        public const string BeneficiaryBIC_InputAppHeader = @"{2:I[0-9]{3}([a-zA-Z0-9]{8})[a-zA-Z0-9]{4}[U|S|N][1|2|3][0-9]{3}}";

        // to be specified...
        public const string BeneficiaryBIC_OutputAppHeader = @"{2:O[0-9]{3}[0-9]{4}[0-9]{6}([a-zA-Z0-9]{8})[a-zA-Z0-9]{14}([0-9]{6})([0-9]{4})[S|N|U]}";

        public const string SendersReference = @"{4:[\r\n ]+:20:([a-zA-Z0-9]+)";

        public const string SenderAccount = @":50[A|F|K]:\/([a-zA-Z0-9]+)[\r\n]+";

        public const string SenderName = @":50[A|F|K]:\/[a-zA-Z0-9]+[\r\n ]+NAME ([a-zA-Z' ]+)";

        public const string SenderAddress = @":50[A|F|K]:\/[a-zA-Z0-9]+[\r\n ]+NAME [a-zA-Z' ]+[\r\n ]+ADDRESS ([a-zA-Z' 0-9.,#]+)";

        public const string BeneficiaryAccount = @":59:\/([a-zA-Z0-9]+)[\r\n ]+";

        public const string BeneficiaryName = @":59:\/[a-zA-Z0-9]+[\r\n ]+NAME ([a-zA-Z' ]+)";

        public const string BeneficiaryAddress = @":59:\/[a-zA-Z0-9]+[\r\n ]+NAME [a-zA-Z' ]+[\r\n ]+ADDRESS ([a-zA-Z' 0-9.,#""']+)";

        public const string TransactionValueDate = @":32A:([0-9]{6})[A-Z]{3}[0-9]{1,12},[0-9]{0,2}";

        public const string TransactionCurrency = @":32A:[0-9]{6}([A-Z]{3})[0-9]{1,12},[0-9]{0,2}";

        public const string TransactionAmount = @":32A:[0-9]{6}[A-Z]{3}([0-9]{1,12},[0-9]{0,2})";

        public const string Reason = @":70:([a-zA-Z0-9!@#$%^&*,]+)";

        public const string DetailsOfCharge = @":71A:([A-Z]{3})";
    }
}
