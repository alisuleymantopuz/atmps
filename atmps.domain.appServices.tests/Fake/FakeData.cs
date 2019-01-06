namespace atmps.domain.appServices.tests.Fake
{
    public static class FakeData
    {
        public static CardInfo FakeCardInfoMasterCardButNotOperated = new CardInfo
        {
            Number = "5569755825672968",
            BankIdentificationNumber = new BankIdentificationNumber { BIN = "556975", BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = "582567296",
            BankInfo = new BankInfo { Id = 1, IsOperated = false, VposUrl = "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo FakeCardInfoMasterCard = new CardInfo
        {
            Number = "5569755825672968",
            BankIdentificationNumber = new BankIdentificationNumber { BIN = "556975", BankInfo= new BankInfo { Id = 1 } },
            AccountNumber = "582567296",
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl= "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo FakeCardInfoVisa = new CardInfo
        {
            Number = "4532421174341278",
            BankIdentificationNumber = new BankIdentificationNumber { BIN = "453242", BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = "117434127",
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl = "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo FakeCardInfoAMEX = new CardInfo
        {
            Number = "371449635398431",
            BankIdentificationNumber = new BankIdentificationNumber { BIN = "371449", BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = "63539843",
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl = "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo WrongCardInfo = new CardInfo
        {
            Number = "1312312312313222",
            BankIdentificationNumber = new BankIdentificationNumber { BIN = "131231", BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = "231231322",
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl = "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo NullCardInfo = new CardInfo
        {
            Number = null,
            BankIdentificationNumber = new BankIdentificationNumber { BIN = null, BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = null,
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl = "https://localhost:4000/api/Payment/" }
        };

        public static CardInfo EmptyCardInfo = new CardInfo
        {
            Number = string.Empty,
            BankIdentificationNumber = new BankIdentificationNumber { BIN = string.Empty, BankInfo = new BankInfo { Id = 1 } },
            AccountNumber = string.Empty,
            BankInfo = new BankInfo { Id = 1, IsOperated = true, VposUrl = "https://localhost:4000/api/Payment/" }
        };
    }
}
