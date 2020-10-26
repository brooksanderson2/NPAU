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
