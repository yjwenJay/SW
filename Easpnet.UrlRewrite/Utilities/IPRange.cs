namespace Easpnet.UrlRewrite.Utilities
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    public sealed class IPRange
    {
        private IPAddress _maximumAddress;
        private IPAddress _minimumAddress;

        public IPRange(IPAddress address)
        {
            this._minimumAddress = address;
            this._maximumAddress = address;
        }

        public IPRange(IPAddress minimumAddress, IPAddress maximumAddress)
        {
            if (Compare(minimumAddress, maximumAddress) == -1)
            {
                this._minimumAddress = minimumAddress;
                this._maximumAddress = maximumAddress;
            }
            else
            {
                this._minimumAddress = maximumAddress;
                this._maximumAddress = minimumAddress;
            }
        }

        public static int Compare(IPAddress left, IPAddress right)
        {
            if (left == null)
            {
                throw new ArgumentNullException("left");
            }
            if (right == null)
            {
                throw new ArgumentNullException("right");
            }
            byte[] addressBytes = left.GetAddressBytes();
            byte[] buffer2 = right.GetAddressBytes();
            if (addressBytes.Length != buffer2.Length)
            {
                throw new ArgumentOutOfRangeException(MessageProvider.FormatString(Message.AddressesNotOfSameType, new object[0]));
            }
            for (int i = 0; i < addressBytes.Length; i++)
            {
                if (addressBytes[i] < buffer2[i])
                {
                    return -1;
                }
                if (addressBytes[i] > buffer2[i])
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool InRange(IPAddress address)
        {
            return ((Compare(this.MinimumAddress, address) <= 0) && (Compare(address, this.MaximumAddress) <= 0));
        }

        public static IPRange Parse(string pattern)
        {
            pattern = Regex.Replace(pattern, @"([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})\.\*", "$1.0-$1.255");
            pattern = Regex.Replace(pattern, @"([0-9]{1,3}\.[0-9]{1,3})\.\*", "$1.0.0-$1.255.255");
            pattern = Regex.Replace(pattern, @"([0-9]{1,3})\.\*", "$1.0.0.0-$1.255.255.255");
            string[] strArray = pattern.Split(new char[] { '-' });
            if (strArray.Length > 1)
            {
                return new IPRange(IPAddress.Parse(strArray[0].Trim()), IPAddress.Parse(strArray[1].Trim()));
            }
            return new IPRange(IPAddress.Parse(pattern.Trim()));
        }

        public IPAddress MaximumAddress
        {
            get
            {
                return this._maximumAddress;
            }
        }

        public IPAddress MinimumAddress
        {
            get
            {
                return this._minimumAddress;
            }
        }
    }
}
