using System;
using System.Text;

namespace ASNA
{
    public class PasswordManager
    {
        public string EncodeString(string inputf)
        {
            string EncodeMe = Base64Encode(inputf);
            EncodeMe = HexEncode(EncodeMe);
            return EncodeMe;
        }

        public string DecodeString(string inputf)
        {
            string DecodeMe = HexDecode(inputf);
            DecodeMe = Base64Decode(DecodeMe);
            return DecodeMe;
        }

        private string Base64Encode(string inputf)
        {
            byte[] byt = Encoding.UTF8.GetBytes(inputf);
            return Convert.ToBase64String(byt);
        }
        private string Base64Decode(string inputf)
        {
            byte[] byt = Convert.FromBase64String(inputf);
            return Encoding.UTF8.GetString(byt);
        }

        private string HexEncode(string inputf)
        {
            byte[] byt = Encoding.Default.GetBytes(inputf);
            string hexstring = BitConverter.ToString(byt);
            hexstring = hexstring.Replace("-", "");
            return hexstring;
        }
        private string HexDecode(string inputf)
        {
            string stringvalue = "";
            for(int i = 0; i < inputf.Length / 2; i++)
            {
                string hexchar = inputf.Substring(i * 2, 2);
                int hexvalue = Convert.ToInt32(hexchar, 16);
                stringvalue += Char.ConvertFromUtf32(hexvalue);
            }
            return stringvalue;
        }
    }
}
