using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class AuthSenderOptions
    {
        private string user = "Devyn Woodruff"; // The name you want to show up on your email
                                                // Make sure the string passed in below matches your API Key
        private string key = "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";
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }
    }
}
