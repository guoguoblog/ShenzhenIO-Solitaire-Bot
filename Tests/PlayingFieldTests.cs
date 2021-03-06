﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SHENZENSolitaire.Game;

namespace Tests
{
    [TestClass]
    public class PlayingFieldTests
    {
        [TestMethod]
        public void MovableTest()
        {
            PlayingField pf = new PlayingField();
            pf.SetSlot(false, 0, new Card(2, SuitEnum.RED));
            pf.SetSlot(false, 0, new Card(1, SuitEnum.BLACK));
            pf.SetSlot(false, 0, new Card(0, SuitEnum.BLACK));
            pf.SetSlot(false, 0, new Card(3, SuitEnum.RED));
            pf.SetSlot(false, 0, new Card(8, SuitEnum.GREEN));

            pf.SetSlot(false, 1, new Card(9, SuitEnum.RED));
            pf.SetSlot(false, 1, new Card(8, SuitEnum.BLACK));
            pf.SetSlot(false, 1, new Card(7, SuitEnum.GREEN));
            pf.SetSlot(false, 1, new Card(6, SuitEnum.RED));
            pf.SetSlot(false, 1, new Card(5, SuitEnum.GREEN));

            pf.SetSlot(false, 2, new Card(6, SuitEnum.RED));
            pf.SetSlot(false, 2, new Card(5, SuitEnum.RED));
            pf.SetSlot(false, 2, new Card(4, SuitEnum.GREEN));
            pf.SetSlot(false, 2, new Card(3, SuitEnum.BLACK));

            pf.IsMovable(0, 0).Should().BeFalse("a dragon is blocking the way.");
            pf.IsMovable(0, 1).Should().BeFalse("a dragon is blocking the way.");
            pf.IsMovable(0, 2).Should().BeFalse("this card is a dragon.");
            pf.IsMovable(0, 3).Should().BeFalse("8 > 3");
            pf.IsMovable(0, 4).Should().BeTrue("it's the last card.");

            pf.IsMovable(1, 0).Should().BeTrue();
            pf.IsMovable(1, 1).Should().BeTrue();
            pf.IsMovable(1, 2).Should().BeTrue();
            pf.IsMovable(1, 3).Should().BeTrue();
            pf.IsMovable(1, 4).Should().BeTrue();

            pf.IsMovable(2, 0).Should().BeFalse("two reds");
            pf.IsMovable(2, 1).Should().BeTrue();
            pf.IsMovable(2, 2).Should().BeTrue();
            pf.IsMovable(2, 3).Should().BeTrue();
        }

        [TestMethod]
        public void Package()
        {
            byte[] pack = PlayingFields.A1.MakeFingerprint();
            PlayingField p = new PlayingField(pack);
            p.MakeFingerprint().Should().BeEquivalentTo(pack);
        }

        [TestMethod]
        public void CanStackOn()
        {
            PlayingField field = PlayingFields.A4;
            field.CanStackAnythingOn(new Card(5, SuitEnum.GREEN)).Should().BeFalse();
            field.CanStackAnythingOn(new Card(6, SuitEnum.BLACK)).Should().BeTrue();
            field.CanStackAnythingOn(Card.DRAGON_BLACK).Should().BeFalse();
            field.CanStackAnythingOn(Card.EMPTY).Should().BeFalse();
            field.CanStackAnythingOn(new Card(9, SuitEnum.RED)).Should().BeTrue();
            field.CanStackAnythingOn(new Card(7, SuitEnum.BLACK)).Should().BeFalse();

            field = PlayingFields.A5;
            field.CanStackAnythingOn(new Card(2, SuitEnum.BLACK)).Should().BeFalse();

            field = PlayingFields.A6;
            field.CanStackAnythingOn(new Card(5, SuitEnum.RED)).Should().BeTrue();
        }
    }
}
