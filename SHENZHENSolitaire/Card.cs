﻿namespace SHENZENSolitaire
{
    public class Card
    {
        /// <summary>
        /// 0 - Dragon
        /// n - Playing Card
        /// </summary>
        public byte Value { get; private set; }

        public SuitEnum Suit { get; private set; }

        public Card(byte value = 0, SuitEnum suit = SuitEnum.EMPTY)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"Card: {Suit} {Value}";
        }

        public override bool Equals(object obj)
        {
            return obj is Card c && c.Suit == Suit && c.Value == Value;
        }

        /// <summary>
        /// Returns this card's value and suit packed into a byte.
        /// Special Values: 0xFF => Section change for the playing field
        /// </summary>
        /// <returns>The card's representation</returns>
        public byte GetFingerprint()
        {
            return (byte)(((int)this.Suit << 4) | this.Value);
        }

        public override int GetHashCode()
        {
            return ((int)this.Suit << 4) | this.Value;
        }

        public static Card Unpack(byte packedValue)
        {
            return new Card((byte)(packedValue & 0b1111), (SuitEnum)(packedValue >> 4));
        }
    }
}
