using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace AQA.helpers
{
    public class ResponseParser
    {
        public static List<Response> Parse(string stringResponses)
        {
            var strings = Regex.Split(stringResponses, "----------------")
                .Where(o => !string.IsNullOrEmpty(o)).ToArray();
            var responses = strings.Select(s => ParseOneResponse(s)).ToList();
            return responses;
        }


        private static Response ParseOneResponse(string stringResponse)
        {
            if (string.IsNullOrEmpty(stringResponse))
            {
                return null;
            }

            var strings = Regex.Split(stringResponse, "\r\n")
                .Where(o => !string.IsNullOrEmpty(o)).ToArray();

            var input = strings[0].Replace("Trade parsed from ", "")
                .Replace("\'", "");

            if (stringResponse.Contains("Several errors occured"))
            {
                return new Response
                {
                    Input = input,
                    Errors = ParseErrors(stringResponse)
                };
            }

            return new Response
            {
                Commission = decimal.Parse(strings[2].Replace("Commission = ", "")
                    .Replace("\'", "")),
                Input = input,
                Margin = decimal.Parse(strings[3].Replace("Margin     = ", "")
                    .Replace("\'", "")),
                Profit = decimal.Parse(strings[1].Replace("Profit     = ", "")
                    .Replace("\'", "")),
                Errors = new List<string>()
            };
        }

        private static List<string> ParseErrors(string stringResponse)
        {
            var onlyErrors = Regex.Split(stringResponse, "Several errors occured:\r\n")
                .Last(o => !string.IsNullOrEmpty(o)).Trim();
            ;
            return Regex.Split(onlyErrors, "\r\n")
                .Where(o => !string.IsNullOrEmpty(o)).Select(o => o.Trim()).ToList();
        }
    }
}