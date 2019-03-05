namespace microservice1
{
    public class Employee
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var employee = obj as Employee;
            if (employee == null)
            {
                return false;
            }

            return Id.Equals(employee.Id);
        }
    }
}
