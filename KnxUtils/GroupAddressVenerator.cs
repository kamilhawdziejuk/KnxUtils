using System;
using System.Collections.Generic;

namespace KnxUtils
{
    /// <summary>
    /// Generates the KNX addresses
    /// </summary>
    public class GroupAddressGenerator
    {
        private ushort x = 1;
        private ushort y = 1;
        private ushort z = 1;

        public ushort X { get { return x; } }
        public ushort Y { get { return y; } }
        public ushort Z { get { return z; } }

        private readonly Dictionary<string, int> _reserved = new Dictionary<string, int>();


        public string CurrentAddressString
        {
            get { return String.Format("{0}/{1}/{2}", x, y, z); }
        }

        public void Reset()
        {
            x = y = z = 1;
        }

        public void ClearReserved()
        {
            _reserved.Clear();
        }

        public bool IsReserved(string address)
        {
            return _reserved.ContainsKey(address);
        }

        public void Setup(ushort a, ushort b, ushort c)
        {
            x = a;
            y = b;
            z = c;
        }

        public void Setup(string address)
        {
            var items = address.Split('/');
            x = (ushort)Int32.Parse(items[0]);
            y = (ushort)Int32.Parse(items[1]);
            z = (ushort)Int32.Parse(items[2]);
        }

        public void Reserve(string address = null, int type = 0)
        {
            var addToReserve = address ?? this.CurrentAddressString;
            if (!_reserved.ContainsKey(addToReserve))
            {
                _reserved.Add(addToReserve, type);
            }
        }

        public bool IsCurrentFree
        {
            get { return !_reserved.ContainsKey(this.CurrentAddressString); }
        }

        public void NextFree()
        {
            int loopEnd = 255 * 255 * 255;
            do
            {
                z = (ushort)((z + 1) % 256);
                if (z == 0)
                {
                    z = 1;
                    y = (ushort)((y + 1) % 256);
                    if (y == 0)
                    {
                        y = 1;
                        x = ((ushort)((x + 1) % 256));
                        if (x == 0)
                        {
                            z = 1;
                            throw new Exception("Overlap next address!");
                        }
                    }
                }
            } while (_reserved.ContainsKey(this.CurrentAddressString) && loopEnd-- > 0);
        }
    }
}
