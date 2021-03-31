namespace AdventOfCode2020.Day25
{
    internal class Solution
    {
        private readonly EncryptionSubject _card;
        private readonly EncryptionSubject _door;

        public Solution(long publicKey1, long publicKey2)
        {
            _card = new EncryptionSubject(publicKey1);
            _door = new EncryptionSubject(publicKey2);
        }

        public long PartOne() => _card.Handshake(_door);
    }

    internal class EncryptionSubject
    {
        private readonly long _loopSize;
        
        public EncryptionSubject(long publicKey)
        {
            PublicKey = publicKey;
            
            _loopSize = 0;
            var result = 1L;

            while (result != PublicKey)
            {
                result = Transform(result, 7L);
                _loopSize++;
            }
        }

        public long PublicKey { get; }

        public long Handshake(EncryptionSubject other)
        {
            var key = 1L;
            
            for (var i = 0; i < _loopSize; i++)
            {
                key = Transform(key, other.PublicKey);
            }

            return key;
        }

        private static long Transform(long a, long b) => a * b % 20201227L;
    }
}