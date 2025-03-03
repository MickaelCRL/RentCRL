using AutoFixture;
using AutoFixture.Dsl;
using RentCRL.Domain;
using System.Net.Mail;

namespace RentCRL.Tests.Utils
{
    public static class FixtureExtensions
    {
        public static string CreateEmail(this Fixture fixture)
        {
            return fixture.Create<MailAddress>().Address;
        }

        public static string CreatePhoneNumber(this Fixture fixture)
        {
            var random = fixture.Create<Random>();
            var numberList = Enumerable.Range(0, 12).Select(i => random.Next(0, 10).ToString());
            var phoneNumber = $"+{string.Join("", numberList)}";
            return phoneNumber;
        }
    }
}
