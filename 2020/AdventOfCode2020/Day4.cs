using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day4 : IDay<Passport>
    {
        public List<Passport> SetupInputs(string[] inputs)
        {
            var inputGroups = inputs.GroupsUntilWhiteSpace().Select(x => x.Split(new[] { ' ', '\n' }).Where(x => !string.IsNullOrEmpty(x)));

            List<Passport> passports = new List<Passport>(inputGroups.Count());

            foreach(var passportInput in inputGroups)
            {
                Passport passport = new Passport();
                foreach(var item in passportInput)
                {
                    var parts = item.Split(':');
                    switch(parts[0])
                    {
                        case nameof(Passport.byr):
                            passport.byr = parts[1];
                            break;
                        case nameof(Passport.iyr):
                            passport.iyr = parts[1];
                            break;
                        case nameof(Passport.eyr):
                            passport.eyr = parts[1];
                            break;
                        case nameof(Passport.hgt):
                            passport.hgt = parts[1];
                            break;
                        case nameof(Passport.hcl):
                            passport.hcl = parts[1];
                            break;
                        case nameof(Passport.ecl):
                            passport.ecl = parts[1];
                            break;
                        case nameof(Passport.pid):
                            passport.pid = parts[1];
                            break;
                        case nameof(Passport.cid):
                            passport.cid = parts[1];
                            break;
                        default:
                            break;
                    }
                }
                passports.Add(passport);
            }
            return passports;
        }

        Func<Passport, bool> FieldsPresentFunc = (p) =>
        {
            return !string.IsNullOrWhiteSpace(p.byr)
            && !string.IsNullOrWhiteSpace(p.iyr)
            && !string.IsNullOrWhiteSpace(p.eyr)
            && !string.IsNullOrWhiteSpace(p.hgt)
            && !string.IsNullOrWhiteSpace(p.hcl)
            && !string.IsNullOrWhiteSpace(p.ecl)
            && !string.IsNullOrWhiteSpace(p.pid);
        };

        public long A(List<Passport> inputs)
        {
            return inputs.Count(FieldsPresentFunc);
        }

        public static Func<Passport, bool> BirthYearValid = (p) => YearValid(p.byr, 1920, 2002);
        public static Func<Passport, bool> IssueYearValid = (p) => YearValid(p.iyr, 2010, 2020);
        public static Func<Passport, bool> ExpiryYearValid = (p) => YearValid(p.eyr, 2020, 2030);

        public static Func<string, int, int, bool> YearValid = (yearString, minYear, maxYear) =>
        {
            if (int.TryParse(yearString, out int year))
            {
                return year >= minYear && year <= maxYear;
            }
            return false;
        };

        public static Func<Passport, bool> HeightValid = (p) =>
        {
            var unit = p.hgt.Substring(p.hgt.Length - 2);
            var number = p.hgt.Substring(0, p.hgt.Length - 2);

            if(int.TryParse(number, out int height))
            {
                if (unit == "cm")
                {
                    return height >= 150 && height <= 193;
                }
                if (unit == "in")
                {
                    return height >= 59 && height <= 76;
                }
            }
            return false;
        };

        public static Func<Passport, bool> HairColourValid = (p) =>
        {
            var hairColour = p.hcl;

            if (hairColour.Length == 7)
            {
                if (hairColour[0] == '#')
                {
                    return int.TryParse(hairColour.Substring(1), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out var _);
                }
            }
            return false;
        };

        public static Func<Passport, bool> EyeColourValid = (p) =>
        {
            var eyeColour = p.ecl;
            switch(eyeColour)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    return true;
                default:
                    return false;
            }
        };

        public static Func<Passport, bool> PassportIdValid = (p) =>
        {
            if (p.pid.Length == 9)
            {
                return int.TryParse(p.pid, out int _);
            }
            return false;
        };

        public long B(List<Passport> inputs)
        {
            return inputs.Where(FieldsPresentFunc)
                .Where(PassportIdValid)
                .Where(EyeColourValid)
                .Where(HairColourValid)
                .Where(HeightValid)
                .Where(BirthYearValid)
                .Where(IssueYearValid)
                .Where(ExpiryYearValid)
                .Count();
        }
    }

    public class Passport
    {
        public string byr; //Birth Year
        public string iyr; //Issue Year
        public string eyr; //Expiration Year
        public string hgt; //Height
        public string hcl; //Hair Colour
        public string ecl; //Eye Colour
        public string pid; //Passport Id
        public string cid; //Country Id
    }
}
