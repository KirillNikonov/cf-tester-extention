namespace NotACompany.CF_Tester.Models
{
    using System;

    public class Contest : IComparable
    {
        public int id;
        public string name;

        public int CompareTo(object obj)
        {
            if (obj is Contest)
            {
                return ((Contest)obj).id - this.id;
            }
            else throw new ArgumentException();
        }
    }
}
