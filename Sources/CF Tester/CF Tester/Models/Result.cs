namespace NotACompany.CF_Tester.Models
{
    using System;

    public class Result
    {
        public string output;
        public bool crashed;

        public Result(string output)
        {
            this.output = output.Replace("\r", "");
            this.crashed = false;
        }

        public Result(string output, bool crashed)
        {
            this.output = output.Replace("\r", "");
            this.crashed = crashed;
        }
    }
}
