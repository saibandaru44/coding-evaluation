using System.Linq;
using System.Text;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int numberOfEmployees = 0;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            //your code here
            var hiredPosition = GetPositionByTitle(root, title);

            if (hiredPosition is null || hiredPosition.IsFilled())
                return null;

            hiredPosition.SetEmployee(new Employee(++numberOfEmployees, person));
            return hiredPosition;
        }

        private Position? GetPositionByTitle(Position position, string title)
        {
            if (string.Equals(position.GetTitle(), title, System.StringComparison.OrdinalIgnoreCase))
                return position;
            return position.GetDirectReports().Select(directReport => GetPositionByTitle(directReport, title)).FirstOrDefault(p => p is not null);
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}