using System.Linq;

namespace urlshorter.Interfaces
{
    public interface IHashingService
    {
        string ToBase62(int value);
    }

    public class HashingService : IHashingService
    {
        public string ToBase62(int value)
        {
            var digits = new List<int>();
            var dividend = value;
            while (dividend > 0)
            {
                int remainder = (int) (dividend % 62);
                dividend /= 62;
                digits.Insert(0, remainder);
            }

            const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var count = digits.Count;
            var hashedValue = string.Empty;
            var i = 0;
            while (count > i)
            {
                hashedValue += alphabet[digits[i]];
                i++;
            }

            return hashedValue;
        }
    }
}
