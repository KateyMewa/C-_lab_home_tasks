using System;
using System.Collections.Generic;
using System.Text;

namespace PlayRoom
{
    class Toy
    {
        private int _maxChildAge;
        private int _minChildAge;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public int MinChildAge { get => _minChildAge; set
            {
                if (value > MaxChildAge)
                    throw new Exception("This value cannot be more than maxChildAge");
                _minChildAge = value;
            }
        }
        public int MaxChildAge { get => _maxChildAge; set
            {
                if (value < MinChildAge)
                    throw new Exception("This value cannot be less than minChildAge");
                _maxChildAge = value;
            }
        }
        public ToyType Type { get; set; }

        public Toy(string name, decimal price, string size, int minChildAge, int maxChildAge, ToyType type)
        {
            Name = name;
            Price = price;
            Size = size;
            MaxChildAge = maxChildAge;
            MinChildAge = minChildAge;
            Type = type;
        }

        public override string ToString()
        {
            return string.Format($"Name - {Name}, Price - {Price}, Size - {Size}, Age from {MinChildAge} to {MaxChildAge}, Type - {Type}");
        }

        public bool SuitsTheAge(int age) => age >= this.MinChildAge && age <= this.MaxChildAge;

        public bool SuitsUserGender(ToyUserGender gender) => ToyUserResolver.SuitsTheGender(gender, this.Type);
    }
}
