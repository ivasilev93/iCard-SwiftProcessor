using SwiftTransferProcessor.Common;
using SwiftTransferProcessor.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SwiftTransferProcessor
{
    public static class TransferParser
    {
        public static Transfer ParseToTransferModel(string input)
        {

            return new Transfer
            {
                SenderReference = ValidateFieldMatch(input, RegexPattern.SendersReference, nameof(RegexPattern.SendersReference)),
                BankOperationCode = ValidateFieldMatch(input, RegexPattern.BankOperationCode, nameof(RegexPattern.BankOperationCode)),
                ValueDate = DateTime.ParseExact(ValidateFieldMatch(input, RegexPattern.TransactionValueDate, nameof(RegexPattern.TransactionValueDate)), "yyMMdd", CultureInfo.InvariantCulture),
                Currency = ValidateFieldMatch(input, RegexPattern.TransactionCurrency, nameof(RegexPattern.TransactionCurrency)),
                InterbankSettledAmount = decimal.Parse(ValidateFieldMatch(input, RegexPattern.TransactionAmount, nameof(RegexPattern.TransactionAmount))),
                SenderAccount = ValidateFieldMatch(input, RegexPattern.SenderAccount, nameof(RegexPattern.SenderAccount)),
                SenderName = ValidateFieldMatch(input, RegexPattern.SenderName, nameof(RegexPattern.SenderName)),
                SenderAddress = ValidateFieldMatch(input, RegexPattern.SenderAddress, nameof(RegexPattern.SenderAddress)),
                SenderBIC = ValidateFieldMatch(input, RegexPattern.SenderBIC, nameof(RegexPattern.SenderBIC)),
                BeneficiaryAccount = ValidateFieldMatch(input, RegexPattern.BeneficiaryAccount, nameof(RegexPattern.BeneficiaryAccount)),
                BeneficiaryName = ValidateFieldMatch(input, RegexPattern.BeneficiaryName, nameof(RegexPattern.BeneficiaryName)),
                BeneficiaryAddress = ValidateFieldMatch(input, RegexPattern.BeneficiaryAddress, nameof(RegexPattern.BeneficiaryAddress)),
                BeneficiaryBIC = ValidateBeneficiaryBIC(input),
                DetailsOfCharges = ValidateFieldMatch(input, RegexPattern.DetailsOfCharge, nameof(RegexPattern.DetailsOfCharge)),
                Reason = ValidateFieldMatch(input, RegexPattern.Reason, nameof(RegexPattern.SendersReference), false),
            };

        }

        private static string ValidateFieldMatch(string input, string pattern, string field, bool required = true)
        {
            var match = Regex.Match(input, pattern);
            string result = null;
            if (match.Success)
            {
                result = match.Groups[1].Value.TrimEnd();
            }

            if (required && string.IsNullOrEmpty(result))
            {
                throw new ArgumentException($"{field} is missing or invalid!");
            }

            return result;
        }

        private static string ValidateBeneficiaryBIC(string input)
        {
            if (input.IndexOf("{2:O") > 0)
            {
                return ValidateFieldMatch(input, RegexPattern.BeneficiaryBIC_OutputAppHeader, nameof(RegexPattern.BeneficiaryBIC_OutputAppHeader));
            }

            return ValidateFieldMatch(input, RegexPattern.BeneficiaryBIC_InputAppHeader, nameof(RegexPattern.BeneficiaryBIC_InputAppHeader));
        }
    }
}
