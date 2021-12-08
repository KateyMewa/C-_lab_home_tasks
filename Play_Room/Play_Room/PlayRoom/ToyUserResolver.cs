using System;
using System.Collections.Generic;
using System.Text;

namespace PlayRoom
{
    class ToyUserResolver
    {
        public static ToyUserGender GetUserForToyType(ToyType toyType)
        {
            switch (toyType)
            {
                case ToyType.Car:
                    return ToyUserGender.Boy;
                case ToyType.Ball:
                    return ToyUserGender.Unisex;
                case ToyType.Constructor:
                    return ToyUserGender.Boy;
                case ToyType.Doll:
                    return ToyUserGender.Girl;
                case ToyType.Mechanism:
                    return ToyUserGender.Unisex;
                case ToyType.Soft:
                    return ToyUserGender.Girl;
                default:
                    return ToyUserGender.Unisex;
            }
        }

        public static bool SuitsTheGender(ToyUserGender gender, ToyType type)
        {
            var toyUserGender = ToyUserResolver.GetUserForToyType(type);
            if (gender == ToyUserGender.Unisex || toyUserGender == ToyUserGender.Unisex)
            {
                return true;
            }
            return toyUserGender == gender;
        }
    }
}
