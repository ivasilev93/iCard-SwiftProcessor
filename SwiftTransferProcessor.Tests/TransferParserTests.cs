using System;
using Xunit;

namespace SwiftTransferProcessor.Tests
{
    public class TransferParserTests
    {

        [Fact]
        public void ParseToTransferModel_ShouldThrowExceptionWithCorrectMessage_WhenMandatoryFieldIsInvalid()
        {
            var input = GetDummyTransferDataWithInvalidSenderAccount();
            var exception = Assert.Throws<ArgumentException>(() => TransferParser.ParseToTransferModel(input));
            string correctFormat = "SenderAccount is missing or invalid!";
            Assert.Equal(correctFormat, exception.Message);
        }

        [Fact]
        public void ParseToTransferModel_ShouldThrowExceptionWithCorrectMessage_WhenMandatoryFieldIsEmpty()
        {
            var input = GetDummyTransferDataWithEmptySendersReference();
            var exception = Assert.Throws<ArgumentException>(() => TransferParser.ParseToTransferModel(input));
            string correctFormat = "SendersReference is missing or invalid!";
            Assert.Equal(correctFormat, exception.Message);
        }

        [Fact]
        public void ParseToTransferModel_ShouldAcceptInput_WhenNonMandatoryFieldIsEmpty()
        {
            var input = GetDummyTransferDataWithEmptyNonMandatoryField();
            var transferModel = TransferParser.ParseToTransferModel(input);
            Assert.NotNull(transferModel);
            Assert.Null(transferModel.Reason);
        }


        private string GetDummyTransferDataWithInvalidSenderAccount()
        {
            return @"{1:F21STBAMTMTAXXX0176109919}{4:{177:1705191627}{451:0}}{1:F01STBAMTMTAXXX0176109919}{2:O1031535170519BAPPIT22AXXX46901391011705191536N}{3:{103:TGT}{113:NYBI}{108:HO17051900169751}{115:153551153551IT0000000774956506}}{4:
                    :20:MB0T40989881
                    :23B:CRED
                    :32A:170519EUR50000,
                    :33B:EUR50000,
                    :50K:/#@!$#%#
                    NAME Georgi Georgiev
                    ADDRESS Str 'random street' #4 VARNA BG
                    CITY 3
                    :52A:BCNAPYPA
                    :59:/MT75STBA19116000000001041918031
                    NAME Minko Minkov
                    ADDRESS Str 'random street' #2 SOFIA BG
                    :70:REASON3
                    :71A:SHA
                    -}{5:{MAC:00000000}{PAC:00000000}{CHK:9AA6EB78DF36}}{S:{SAC:}{FAC:}{COP:S}}";
        }

        private string GetDummyTransferDataWithEmptySendersReference()
        {
            return @"{1:F21STBAMTMTAXXX0176109919}{4:{177:1705191627}{451:0}}{1:F01STBAMTMTAXXX0176109919}{2:O1031535170519BAPPIT22AXXX46901391011705191536N}{3:{103:TGT}{113:NYBI}{108:HO17051900169751}{115:153551153551IT0000000774956506}}{4:
                    :20:   
                    :23B:CRED
                    :32A:170519EUR50000,
                    :33B:EUR50000,
                    :50K:/IT09J0503404603000000000451
                    NAME Georgi Georgiev
                    ADDRESS Str 'random street' #4 VARNA BG
                    CITY 3
                    :52A:BCNAPYPA
                    :59:/MT75STBA19116000000001041918031
                    NAME Minko Minkov
                    ADDRESS Str 'random street' #2 SOFIA BG
                    :70:REASON3
                    :71A:SHA
                    -}{5:{MAC:00000000}{PAC:00000000}{CHK:9AA6EB78DF36}}{S:{SAC:}{FAC:}{COP:S}}";
        }

        private string GetDummyTransferDataWithEmptyNonMandatoryField()
        {
            return @"{1:F21STBAMTMTAXXX0176109919}{4:{177:1705191627}{451:0}}{1:F01STBAMTMTAXXX0176109919}{2:O1031535170519BAPPIT22AXXX46901391011705191536N}{3:{103:TGT}{113:NYBI}{108:HO17051900169751}{115:153551153551IT0000000774956506}}{4:
                    :20:MB0T40989881
                    :23B:CRED
                    :32A:170519EUR50000,
                    :33B:EUR50000,
                    :50K:/IT09J0503404603000000000451
                    NAME Georgi Georgiev
                    ADDRESS Str 'random street' #4 VARNA BG
                    CITY 3
                    :52A:BCNAPYPA
                    :59:/MT75STBA19116000000001041918031
                    NAME Minko Minkov
                    ADDRESS Str 'random street' #2 SOFIA BG
                    :70:   
                    :71A:SHA
                    -}{5:{MAC:00000000}{PAC:00000000}{CHK:9AA6EB78DF36}}{S:{SAC:}{FAC:}{COP:S}}";
        }
    }
}
