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
            var inputGroups = GroupsUntilWhiteSpace(inputs).Select(x => x.Split(new[] { ' ', '\n' }).Where(x => !string.IsNullOrEmpty(x)));

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

        public long A(List<Passport> inputs)
        {
            return inputs.Count(p => p.IsValid);
        }

        public long B(List<Passport> inputs)
        {
            return -1;
        }

        private IEnumerable<string> GroupsUntilWhiteSpace(string[] inputs)
        {
            string output = "";
            for(int i = 0; i < inputs.Count(); i++)
            {
                if(string.IsNullOrWhiteSpace(inputs[i]))
                {
                    yield return output;
                    output = "";
                }
                else
                {
                    output += inputs[i] + "\n";
                }
            }
            yield return output;
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

        public bool IsValid => !string.IsNullOrWhiteSpace(byr)
            && !string.IsNullOrWhiteSpace(iyr)
            && !string.IsNullOrWhiteSpace(eyr)
            && !string.IsNullOrWhiteSpace(hgt)
            && !string.IsNullOrWhiteSpace(hcl)
            && !string.IsNullOrWhiteSpace(ecl)
            && !string.IsNullOrWhiteSpace(pid)
            //&& !string.IsNullOrWhiteSpace(cid)
            ;
    }
}
