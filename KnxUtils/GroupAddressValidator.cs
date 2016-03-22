using System;
using System.Text.RegularExpressions;

namespace KnxUtils
{
    /// <summary>
    /// Validator of the group addresses. The values should be in X/Y/Z format.
    /// </summary>
    public class GroupAddressValidator
    {
        private static readonly Regex GroupAddressExpression = new Regex(@"\b\d{1,3}\/\d{1,3}\/\d{1,3}\b");

        #region Public methods

        public static bool IsValidKnxAddress(string address)
        {
            if (GroupAddressExpression.IsMatch(address))
            {
                var items = address.Split('/');
                int x = Int32.Parse(items[0]);
                int y = Int32.Parse(items[1]);
                int z = Int32.Parse(items[2]);
                return (IsKnxRangeNumber(x) && IsKnxRangeNumber(y) && IsKnxRangeNumber(z));
            }
            return false;
        }

        /// <summary>
        /// Check if testAddress is between the addressFrom and addressTo
        /// </summary>
        /// <param name="addressFrom"></param>
        /// <param name="addressTo"></param>
        /// <param name="testAddress"></param>
        /// <returns></returns>
        public static bool IsBetween(string addressFrom, string addressTo, string testAddress)
        {
            return AreSequentKnxAddresses(addressFrom, testAddress) && AreSequentKnxAddresses(testAddress, addressTo);
        }

        /// <summary>
        /// Check if addressTo is larger than addressFrom 
        /// </summary>
        /// <param name="addressFrom"></param>
        /// <param name="addressTo"></param>
        /// <returns></returns>
        public static bool AreSequentKnxAddresses(string addressFrom, string addressTo)
        {
            int a = Convert(addressFrom);
            int b = Convert(addressTo);
            return a <= b;
        }

        #endregion

        #region Private methods

        private static bool IsKnxRangeNumber(int a)
        {
            return a >= 0 && a <= 255;
        }

        private static int Convert(string address)
        {
            var items = address.Split('/');
            int x = Int32.Parse(items[0]);
            int y = Int32.Parse(items[1]);
            int z = Int32.Parse(items[2]);

            int value = 255 * 255 * x + 255 * y + z;
            return value;
        }

        #endregion
    }
}
