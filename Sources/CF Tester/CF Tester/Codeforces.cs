namespace NotACompany.CF_Tester
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    public static class Codeforces
    {
        /// <summary>
        /// Gets a list (simplified) of most recent contests.
        /// </summary>
        /// <param name="contestNumber">Number of contests to return.</param>
        /// <returns>A list of contests.</returns>
        public static List<Contest> getRecentContests(int contestNumber = 6)
        {
            List<Contest> result = new List<Contest>();

            WebRequest request = WebRequest.Create("http://codeforces.com/api/contest.list?gym=false");
            WebResponse response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);

            string responseText = reader.ReadToEnd();

            List<Contest> contests = JsonConvert.DeserializeObject<ContestListResult>(responseText).result;

            for (int i = 0; i < contestNumber; i++) result.Add(contests[i]);

            response.Close();

            return result;
        }
        
        /// <summary>
        /// Gets a list of tests for the specified problem.
        /// </summary>
        /// <param name="contestId">Contest Id.</param>
        /// <param name="problem">Problem Id ('A' - 'G').</param>
        /// <returns>A list of tests.</returns>
        public static List<Test> getTests(int contestId, char problem)
        {
            List<Test> tests = new List<Test>();

            WebRequest request = WebRequest.Create("http://codeforces.com/contest/" + contestId.ToString() + "/problem/" + problem);
            WebResponse response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);

            string responseText = reader.ReadToEnd();
            string sampleTestsHTML = HTMLParser.getInnerHTML(HTMLParser.getInnerHTML(responseText, "sample-tests"), "sample-test");

            List<string> input = HTMLParser.getInnerHTMLList(sampleTestsHTML, "input");
            List<string> output = HTMLParser.getInnerHTMLList(sampleTestsHTML, "output");

            
            for (int i = 0; i < input.Count; i++)
                tests.Add(new Test(
                    HTMLParser.replaceBrTag(HTMLParser.getInnerHTML(input[i], "pre")),
                    HTMLParser.replaceBrTag(HTMLParser.getInnerHTML(output[i], "pre"))
                    ));

            response.Close();

            return tests;
        }
    }
}
