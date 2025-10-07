using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Ion.Numeral;

public static partial class Number
{
    /// <summary>
    /// The prime number <b>2</b>.
    /// </summary>
    public const int Prime2 = 2;

    /// <summary>
    /// The prime number <b>3</b>.
    /// </summary>
    public const int Prime3 = 3;

    /// <summary>
    /// The prime number <b>5</b>.
    /// </summary>
    public const int Prime5 = 5;

    /// <summary>
    /// The prime number <b>7</b>.
    /// </summary>
    public const int Prime7 = 7;

    /// <summary>
    /// The prime number <b>11</b>.
    /// </summary>
    public const int Prime11 = 11;

    /// <summary>
    /// The prime number <b>13</b>.
    /// </summary>
    public const int Prime13 = 13;

    /// <summary>
    /// The prime number <b>17</b>.
    /// </summary>
    public const int Prime17 = 17;

    /// <summary>
    /// The prime number <b>19</b>.
    /// </summary>
    public const int Prime19 = 19;

    /// <summary>
    /// The prime number <b>23</b>.
    /// </summary>
    public const int Prime23 = 23;

    /// <summary>
    /// The prime number <b>29</b>.
    /// </summary>
    public const int Prime29 = 29;

    /// <summary>
    /// The prime number <b>31</b>.
    /// </summary>
    public const int Prime31 = 31;

    /// <summary>
    /// The prime number <b>37</b>.
    /// </summary>
    public const int Prime37 = 37;

    /// <summary>
    /// The prime number <b>41</b>.
    /// </summary>
    public const int Prime41 = 41;

    /// <summary>
    /// The prime number <b>43</b>.
    /// </summary>
    public const int Prime43 = 43;

    /// <summary>
    /// The prime number <b>47</b>.
    /// </summary>
    public const int Prime47 = 47;

    /// <summary>
    /// The prime number <b>53</b>.
    /// </summary>
    public const int Prime53 = 53;

    /// <summary>
    /// The prime number <b>59</b>.
    /// </summary>
    public const int Prime59 = 59;

    /// <summary>
    /// The prime number <b>61</b>.
    /// </summary>
    public const int Prime61 = 61;

    /// <summary>
    /// The prime number <b>67</b>.
    /// </summary>
    public const int Prime67 = 67;

    /// <summary>
    /// The prime number <b>71</b>.
    /// </summary>
    public const int Prime71 = 71;

    /// <summary>
    /// The prime number <b>73</b>.
    /// </summary>
    public const int Prime73 = 73;

    /// <summary>
    /// The prime number <b>79</b>.
    /// </summary>
    public const int Prime79 = 79;

    /// <summary>
    /// The prime number <b>83</b>.
    /// </summary>
    public const int Prime83 = 83;

    /// <summary>
    /// The prime number <b>89</b>.
    /// </summary>
    public const int Prime89 = 89;

    /// <summary>
    /// The prime number <b>97</b>.
    /// </summary>
    public const int Prime97 = 97;

    /// <summary>
    /// Get all <see cref="NumberProperty.Prime"/> numbers up to given <b>upperLimit</b> using <i>Sieve of Eratosthenes</i> algorithm.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="BitArray"/> to keep track of composite numbers. 
    /// Iterates through numbers, marking multiples of each prime number as composite. 
    /// The outer loop runs up to the square root of the upper limit, and the inner loop 
    /// starts marking multiples from the square of the prime number, as smaller multiples 
    /// would have already been marked by smaller primes.
    /// <para><b>See <see cref="GetPrimeOptimized(int)"/> for optimized version.</b></para>
    /// </remarks>
    /// <returns>
    ///  All numbers not marked as <see cref="NumberProperty.Composite"/>.
    /// </returns>
    public static IEnumerable<int> GetPrime(int upperLimit)
    {
        BitArray composite = new BitArray(upperLimit);

        int sqrt = (int)Math.Sqrt(upperLimit);
        for (int p = 2; p <= sqrt; ++p)
        {
            if (composite[p]) continue;

            yield return p;

            for (int i = p * p; i < upperLimit; i += p)
                composite[i] = true;
        }

        for (int p = sqrt + 1; p < upperLimit; ++p)
        {
            if (!composite[p]) yield return p;
        }
    }

    /// <summary>
    /// Get all <see cref="NumberProperty.Prime"/> numbers up to given <b>upperLimit</b> using (an optimized version of) <i>Sieve of Eratosthenes</i> algorithm.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="HashSet{T}"/> to store prime numbers and <see cref="bool"/>[] to mark composite numbers. 
    /// Iterates through numbers, marking multiples of each prime as composite, and collects primes in <see cref="HashSet{T}"/>. 
    /// This approach minimizes number of operations and improves performance.
    /// <para><b>See <see cref="GetPrime(int)"/> for unoptimized version.</b></para>
    /// </remarks>
    /// <returns>
    ///  All numbers not marked as <see cref="NumberProperty.Composite"/>.
    /// </returns>
    public static HashSet<int> GetPrimeOptimized(int upperLimit)
    {
        HashSet<int> primes = new HashSet<int>();
        bool[] isComposite = new bool[upperLimit + 1];
        for (int p = 2; p * p <= upperLimit; ++p)
        {
            if (!isComposite[p])
            {
                primes.Add(p);
                for (int i = p * p; i <= upperLimit; i += p)
                    isComposite[i] = true;
            }
        }
        for (int p = (int)Math.Sqrt(upperLimit) + 1; p <= upperLimit; ++p)
        {
            if (!isComposite[p])
                primes.Add(p);
        }
        return primes;
    }

    /// <summary>
    /// Get if <b>n</b> is “probably prime” with <b>k</b> iterations of <i>Miller-Rabin</i> test (more iterations increases confidence in result).
    /// </summary>
    /// <remarks>
    /// To test, you can use known prime and composite numbers, including Carmichael numbers, which are composite numbers that can pass
    /// the Fermat primality test for all bases that are relatively prime to the number. Carmichael numbers are particularly useful for 
    /// testing because they can pass the Fermat test but fail the Miller-Rabin test with a nontrivial square root of 1.
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if <b>n</b> is “probably prime”. <see langword="false"/> if composite.
    /// </returns>
    public static bool IsPrime(BigInteger n, int k)
    {
        if (n < 2) return false;
        if (n != 2 && n % 2 == 0) return false;

        BigInteger s = n - 1;
        while (s % 2 == 0)
        {
            s >>= 1;
        }

        Random r = new Random();
        for (int i = 0; i < k; i++)
        {
            BigInteger a = new BigInteger(r.Next((int)n - 1) + 1);
            BigInteger temp = s;
            BigInteger mod = BigInteger.ModPow(a, temp, n);

            while (temp != n - 1 && mod != 1 && mod != n - 1)
            {
                mod = BigInteger.ModPow(mod, 2, n);
                temp *= 2;
            }

            if (mod != n - 1 && temp % 2 == 0)
            {
                return false;
            }
        }
        return true;
    }
}
