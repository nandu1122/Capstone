using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwoFactorAuth.SmsHelper
{
    public class SmsService
    {
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = "ACfa54391d3505064e254dfebc9ff75d0c";
            // Your Auth Token from twilio.com/console
            var authToken = "2d25b4200b4792f91b87b65583a6eea3";

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber("+12484793562"),
              body: message);
        }
    }
}
