namespace RC5;

public class Rc5
{
    private const int BlockLength = 64;
    private const int Rounds = 16;
    private const ulong Pw = 0xB7E151628AED2A6B;
    private const ulong Qw = 0x9E3779B97F4A7C15;

    private readonly ulong[] _expandedKeys;

    public Rc5(IReadOnlyList<byte> key)
    {
        ulong x, y;
        int i, j, n;
        
        var bytePerWord = BlockLength >> 3;
        var keyLength = key.Count;
        var lLength = keyLength % bytePerWord > 0 ? keyLength / bytePerWord + 1 : keyLength / bytePerWord;
        var secretKey = new ulong[lLength];

        for (i = keyLength - 1; i >= 0; i--) secretKey[i / bytePerWord] = LeftShift(secretKey[i / bytePerWord], 8) + key[i];
        
        var tableSize = 2 * (Rounds + 1);
        _expandedKeys = new ulong[tableSize];
        _expandedKeys[0] = Pw;
        for (i = 1; i < tableSize; i++) _expandedKeys[i] = _expandedKeys[i - 1] + Qw;
        
        x = y = 0;
        i = j = 0;
        n = 3 * Math.Max(tableSize, lLength);

        for (var k = 0; k < n; k++)
        {
            x = _expandedKeys[i] = LeftShift(_expandedKeys[i] + x + y, 3);
            y = secretKey[j] = LeftShift(secretKey[j] + x + y, (int)(x + y));
            i = (i + 1) % tableSize;
            j = (j + 1) % lLength;
        }
    }


    private static ulong LeftShift(ulong a, int offset)
    {
        var r1 = a << offset;
        var r2 = a >> (BlockLength - offset);
        return r1 | r2;
    }
    
    private ulong RightShift(ulong a, int offset)
    {
        var r1 = a >> offset;
        var r2 = a << (BlockLength - offset);
        return r1 | r2;
    }
    
    private static ulong BytesToUInt64(IReadOnlyList<byte> b, int p)
    {
        ulong r = 0;
        for (var i = p + 7; i > p; i--)
        {
            r |= b[i];
            r <<= 8;
        }

        r |= b[p];
        return r;
    }
    
    private static void UInt64ToBytes(ulong a, IList<byte> b, int p)
    {
        for (var i = 0; i < 7; i++)
        {
            b[p + i] = (byte)(a & 0xFF);
            a >>= 8;
        }

        b[p + 7] = (byte)(a & 0xFF);
    }
    
    public void Cipher(byte[] inBuf, byte[] outBuf)
    {
        var a = BytesToUInt64(inBuf, 0);
        var b = BytesToUInt64(inBuf, 8);

        a += _expandedKeys[0];
        b += _expandedKeys[1];

        for (var i = 1; i < Rounds + 1; i++)
        {
            a = LeftShift(a ^ b, (int)b) + _expandedKeys[2 * i];
            b = LeftShift(b ^ a, (int)a) + _expandedKeys[2 * i + 1];
        }

        UInt64ToBytes(a, outBuf, 0);
        UInt64ToBytes(b, outBuf, 8);
    }
    
    public void Decipher(byte[] inBuf, byte[] outBuf)
    {
        var a = BytesToUInt64(inBuf, 0);
        var b = BytesToUInt64(inBuf, 8);

        for (var i = Rounds; i > 0; i--)
        {
            b = RightShift(b - _expandedKeys[2 * i + 1], (int)a) ^ a;
            a = RightShift(a - _expandedKeys[2 * i], (int)b) ^ b;
        }

        b -= _expandedKeys[1];
        a -= _expandedKeys[0];

        UInt64ToBytes(a, outBuf, 0);
        UInt64ToBytes(b, outBuf, 8);
    }
}