﻿using NodaTime;
using NUnit.Framework;
using System;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class TimedLayersTests
    {
        [Test]
        public void SecondHand_Is_90Degs_At_00_00_15()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 0, 0, 15));
            var layer = new SecondsLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.Rotate);
        }

        [Test]
        public void SecondHand_Is_180Degs_At_00_00_30()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 0, 0, 30));
            var layer = new SecondsLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.Rotate);
        }

        [Test]
        public void MinutesHand_Is_90Degs_At_00_15_00()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 0, 15, 0));
            var layer = new MinutesLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.Rotate);
        }

        [Test]
        public void MinutesHand_Is_180Degs_At_00_30_00()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 0, 30, 0));
            var layer = new MinutesLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.Rotate);
        }

        [Test]
        public void HoursHand_Is_90Degs_At_00_03_00()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 3, 0, 0));
            var layer = new HoursLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.Rotate);
        }

        [Test]
        public void HoursHand_Is_180Degs_At_06_00_00()
        {
            Instant time = Instant.FromDateTimeOffset(new DateTime(2019, 1, 1, 6, 0, 0));
            var layer = new HoursLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.Rotate);
        }

        [Test]
        public void HoursHand_Is_30_Degs_At_00_00_UTC_In_Paris()
        {
            Instant time = Instant.FromDateTimeUtc(new DateTime(2020, 01, 01, 00, 00, 00, DateTimeKind.Utc));
            var layer = new HoursLayer { TimeZone = "Europe/Paris" };
            layer.Update(time);
            Assert.AreEqual(30, layer.Rotate);
        }

        [Test]
        public void HoursHand_Is_15_Degs_At_00_00_UTC_In_Paris_On_A_24h_Clock()
        {
            Instant time = Instant.FromDateTimeUtc(new DateTime(2020, 01, 01, 00, 00, 00, DateTimeKind.Utc));
            var layer = new HoursLayer { TimeZone = "Europe/Paris", Is24Hours = true };
            layer.Update(time);
            Assert.AreEqual(15, layer.Rotate);
        }
    }
}
