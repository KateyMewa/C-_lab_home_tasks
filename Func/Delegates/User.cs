namespace Delegates
{
    public class User
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name}, age: {this.Age}, gender: {this.Gender}";
        }
    }
}
