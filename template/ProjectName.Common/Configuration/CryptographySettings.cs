using System;

namespace ProjectName.Common.Configuration
{
    public sealed class CryptographySettings
    {
        public bool InteractiveLoginEnabled { get; set; }

        public Uri KeyUri { get; set; }
    }
}
