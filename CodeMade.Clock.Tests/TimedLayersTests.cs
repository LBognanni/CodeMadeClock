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
            DateTime time = new DateTime(2019, 1, 1, 0, 0, 15);
            var layer = new SecondsLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.TransformRotate);
        }

        [Test]
        public void SecondHand_Is_180Degs_At_00_00_30()
        {
            DateTime time = new DateTime(2019, 1, 1, 0, 0, 30);
            var layer = new SecondsLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.TransformRotate);
        }

        [Test]
        public void MinutesHand_Is_90Degs_At_00_15_00()
        {
            DateTime time = new DateTime(2019, 1, 1, 0, 15, 0);
            var layer = new MinutesLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.TransformRotate);
        }

        [Test]
        public void MinutesHand_Is_180Degs_At_00_30_00()
        {
            DateTime time = new DateTime(2019, 1, 1, 0, 30, 0);
            var layer = new MinutesLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.TransformRotate);
        }

        [Test]
        public void HoursHand_Is_90Degs_At_00_03_00()
        {
            DateTime time = new DateTime(2019, 1, 1, 3, 0, 0);
            var layer = new HoursLayer();
            layer.Update(time);
            Assert.AreEqual(90, layer.TransformRotate);
        }

        [Test]
        public void HoursHand_Is_180Degs_At_06_00_00()
        {
            DateTime time = new DateTime(2019, 1, 1, 6, 0, 0);
            var layer = new HoursLayer();
            layer.Update(time);
            Assert.AreEqual(180, layer.TransformRotate);
        }

    }
}
