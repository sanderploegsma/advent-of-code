module Tests

open Day04
open FsUnit.Xunit
open Xunit

[<Theory>]
[<InlineData("byr:2002", true)>]
[<InlineData("byr:2003", false)>]
[<InlineData("hgt:60in", true)>]
[<InlineData("hgt:190cm", true)>]
[<InlineData("hgt:190in", false)>]
[<InlineData("hgt:190", false)>]
[<InlineData("hcl:#123abc", true)>]
[<InlineData("hcl:#123abz", false)>]
[<InlineData("hcl:123abc", false)>]
[<InlineData("ecl:brn", true)>]
[<InlineData("ecl:wat", false)>]
[<InlineData("pid:000000001", true)>]
[<InlineData("pid:0123456789", false)>]
let ``Passport field validation`` field expected =
    isValidField field |> should equal expected

[<Theory>]
[<InlineData("eyr:1972 cid:100\r\nhcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926")>]
[<InlineData("iyr:2019\r\nhcl:#602927 eyr:1967 hgt:170cm\r\necl:grn pid:012533040 byr:1946")>]
[<InlineData("hcl:dab227 iyr:2012\r\necl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277")>]
[<InlineData("hgt:59cm ecl:zzz\r\neyr:2038 hcl:74454a iyr:2023\r\npid:3556412378 byr:2007")>]
let ``Invalid passport values`` passport =
    hasValidFields passport |> should be False

[<Theory>]
[<InlineData("pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980\r\nhcl:#623a2f")>]
[<InlineData("eyr:2029 ecl:blu cid:129 byr:1989\r\niyr:2014 pid:896056539 hcl:#a97842 hgt:165cm")>]
[<InlineData("hcl:#888785\r\nhgt:164cm byr:2001 iyr:2015 cid:88\r\npid:545766238 ecl:hzl\r\neyr:2022")>]
[<InlineData("iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719")>]
let ``Valid passport values`` passport =
    hasValidFields passport |> should be True
