using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter();
        private String theTime;


        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }

        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.That(berlinClock.ConvertTime(theTime), Is.EqualTo(theExpectedBerlinClockOutput));
        }

    }
}
