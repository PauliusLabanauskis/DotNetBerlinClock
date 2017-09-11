using System;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string ConvertTime(string aTime)
        {
            var time = ParseTime(aTime);

            if (time.TotalSeconds > TimeSpan.FromDays(1).TotalSeconds)
            {
                throw new InvalidOperationException("Time exceeds 24 hours");
            }

            var topLightOn = time.Seconds % 2 == 0;

            var hours = time.Days == 1 && TimeSpan.FromDays(1) - time == TimeSpan.Zero ? 24 : time.Hours;
            var fiveHourLights = hours / 5;
            var oneHourLights = hours % 5;

            var fiveMinuteLights = time.Minutes / 5;
            var oneMinuteLights = time.Minutes % 5;

            var representation = GetClockRepresentation(topLightOn, fiveHourLights, oneHourLights,
                fiveMinuteLights, oneMinuteLights);

            return representation;
        }

        private TimeSpan ParseTime(string time)
        {
            var timeComponents = time.Split(':');

            if (timeComponents.Length != 3 || timeComponents.Any(c => c.Except("0123456789").Any()))
            {
                throw new InvalidOperationException("Unsupported time format");
            }

            var hours = uint.Parse(timeComponents[0]);
            if (hours > 24)
            {
                throw new InvalidOperationException("Hour range exceeded");
            }
            var parsedTime = TimeSpan.FromHours(hours);

            var minutes = uint.Parse(timeComponents[1]);
            if (minutes > 59)
            {
                throw new InvalidOperationException("Minute range exceeded");
            }
            parsedTime = parsedTime.Add(TimeSpan.FromMinutes(minutes));

            var seconds = uint.Parse(timeComponents[2]);
            if (seconds > 59)
            {
                throw new InvalidOperationException("Second range exceeded");
            }
            parsedTime = parsedTime.Add(TimeSpan.FromSeconds(seconds));

            return parsedTime;
        }

        private string GetClockRepresentation(bool topLightOn, int fiveHourLights,
            int oneHourLights, int fiveMinuteLights, int oneMinuteLights)
        {
            var representationBuilder = new StringBuilder();

            representationBuilder.AppendLine(topLightOn ? "Y" : "O");

            representationBuilder.Append(new string('R', fiveHourLights))
                .AppendLine(new string('O', 4 - fiveHourLights));

            representationBuilder.Append(new string('R', oneHourLights))
                .AppendLine(new string('O', 4 - oneHourLights));

            for (var lights = 1; lights <= fiveMinuteLights; lights++)
            {
                representationBuilder.Append(lights % 3 == 0 ? 'R' : 'Y');
            }
            representationBuilder.AppendLine(new string('O', 11 - fiveMinuteLights));

            representationBuilder.Append(new string('Y', oneMinuteLights))
                .Append(new string('O', 4 - oneMinuteLights));

            return representationBuilder.ToString();
        }
    }
}
