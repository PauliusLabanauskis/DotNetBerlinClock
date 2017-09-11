using System;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string ConvertTime(string aTime)
        {
            var time = ParseTime(aTime);

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

            var hours = int.Parse(timeComponents[0]);
            var parsedTime = TimeSpan.FromHours(hours);

            var minutes = int.Parse(timeComponents[1]);
            parsedTime = parsedTime.Add(TimeSpan.FromMinutes(minutes));

            var seconds = int.Parse(timeComponents[2]);
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
