namespace NotACompany.CF_Tester.Models
{
    public class Problem
    {
        public int contestId;
        public string index;

        public Problem(int contestId, string index)
        {
            this.contestId = contestId;
            this.index = index;
        }
    }
}
