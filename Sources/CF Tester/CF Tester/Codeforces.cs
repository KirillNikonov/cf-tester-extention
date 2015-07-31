namespace NotACompany.CF_Tester
{
    using Exceptions;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

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

            if (response == null || ((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
            {
                throw new FailedRequestException();
            }

            var reader = new StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);

            try
            {
                string responseText = reader.ReadToEnd();

                List<Contest> contests = JsonConvert.DeserializeObject<ContestListResult>(responseText).result;

                for (int i = 0; i < contestNumber; i++) result.Add(contests[i]);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to retrieve and/or process recent contests data. Possibly, codeforces or codeforces api is unavailable at the moment.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            result.Sort();

            response.Close();

            return result;
        }

        /// <summary>
        /// Gets a list of tests for the specified problem.
        /// </summary>
        /// <param name="contestId">Contest Id.</param>
        /// <param name="problem">Problem Id ('A' - 'N').</param>
        /// <returns>A list of tests.</returns>
        public static List<Test> getTests(int contestId, char problem)
        {
            List<Test> tests = new List<Test>();

            WebRequest request = WebRequest.Create("http://codeforces.com/contest/" + contestId.ToString() + "/problem/" + problem);
            WebResponse response = request.GetResponse();

            if (response == null || ((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
            {
                throw new FailedRequestException();
            }

            var reader = new StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);

            string responseText = reader.ReadToEnd();
            
            string sampleTestsHTML = HTMLParser.getInnerHTML(responseText, "sample-test");

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

        /// <summary>
        /// Gets a list of problems in the contest.
        /// </summary>
        /// <param name="contestId">Contest id.</param>
        /// <returns>A list of problems.</returns>
        public static List<Problem> getProblems(int contestId)
        {
            List<Problem> problems = new List<Problem>();

            WebRequest request = WebRequest.Create("http://codeforces.com/contest/" + contestId.ToString());
            ((HttpWebRequest)request).Accept = "text/html";
            WebResponse response = request.GetResponse();

            if (response == null || ((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
            {
                throw new FailedRequestException();
            }

            var reader = new StreamReader(response.GetResponseStream(), UTF8Encoding.UTF8);

            string responseText = new Regex(@"([\s]*\r[\s]*)|([\s]*\n[\s]*)").Replace(reader.ReadToEnd(), "");
            responseText = new Regex(@"\<script.*?\<\/script\>").Replace(responseText, "");
            responseText = new Regex(@"\<[\s]*\/[\s]*table[\s]*\>.*$").Replace(new Regex(@"^.*?\<[\s]*table[\s]*class[=]\""problems\""[\s]*\>").Replace(responseText, ""), "");

            MatchCollection matches = new Regex(@"\<[\s]*a[\s]*href=\""/contest/" + contestId.ToString() + @"/problem/([\w]+?)\""[\s]*\>(\1)\<[\s]*\/[\s]*a[\s]*\>").Matches(responseText);

            foreach (Match match in matches)
            {
                problems.Add(new Problem(contestId, match.Groups[1].Value));
            }

            response.Close();

            return problems;
        }
    }
}
