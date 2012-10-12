using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace TestApp
{
    public class Hash
    {
        string _sSourceData;
        byte[] _tmpSource;
        byte[] _tmpHash;

        public Hash()
        {
            Init();
        }

        private void Init()
        {
            _sSourceData = "MySourceData";
            
            _tmpSource = Encoding.ASCII.GetBytes(_sSourceData);

            _tmpHash = new MD5CryptoServiceProvider().ComputeHash(_tmpSource);
            Console.WriteLine(ByteArrayToString(_tmpHash));

            _sSourceData = "NotMySourceData";
            _tmpSource = Encoding.ASCII.GetBytes(_sSourceData);

            byte[] tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(_tmpSource);

            bool bEqual = false;

            if (tmpNewHash.Length == _tmpHash.Length)
            {
                int i = 0;
                while ((i < tmpNewHash.Length) && (tmpNewHash[i] == _tmpHash[i]))
                {
                    i += 1;
                }
                if (i == tmpNewHash.Length)
                {
                    bEqual = true;
                }
            }

            var hashString = ByteArrayToString(_tmpHash);
            //
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            var sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
