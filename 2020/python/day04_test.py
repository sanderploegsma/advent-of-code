import pytest
from day04 import Passport, parse_passport


@pytest.mark.parametrize('data', [
    "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\r\nbyr:1937 iyr:2017 cid:147 hgt:183cm",
    "hcl:#ae17e1 iyr:2013\r\neyr:2024\r\necl:brn pid:760753108 byr:1931\r\nhgt:179cm",
])
def test_required_fields_present(data):
    sut = parse_passport(data)
    assert sut.has_required_fields is True


@pytest.mark.parametrize('data', [
    "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884\r\nhcl:#cfa07d byr:1929",
    "hcl:#cfa07d eyr:2025 pid:166559648\r\niyr:2011 ecl:brn hgt:59in",
])
def test_required_fields_missing(data):
    sut = parse_passport(data)
    assert sut.has_required_fields is False


@pytest.mark.parametrize('data', ['1920', '1988', '2002'])
def test_valid_birth_year(data):
    sut = Passport({'byr': data})
    assert sut.has_valid_birth_year is True


@pytest.mark.parametrize('data', ['1800', '1919', '2003', '2010', 'NaN', '', '-100'])
def test_invalid_birth_year(data):
    sut = Passport({'byr': data})
    assert sut.has_valid_birth_year is False


@pytest.mark.parametrize('data', ['2010', '2015', '2020'])
def test_valid_issue_year(data):
    sut = Passport({'iyr': data})
    assert sut.has_valid_issue_year is True


@pytest.mark.parametrize('data', ['1999', '2003', '2021', 'NaN', '', '-100'])
def test_invalid_issue_year(data):
    sut = Passport({'iyr': data})
    assert sut.has_valid_issue_year is False


@pytest.mark.parametrize('data', ['2020', '2025', '2030'])
def test_valid_expiration_year(data):
    sut = Passport({'eyr': data})
    assert sut.has_valid_expiration_year is True


@pytest.mark.parametrize('data', ['1999', '2019', '2031', 'NaN', '', '-100'])
def test_invalid_expiration_year(data):
    sut = Passport({'eyr': data})
    assert sut.has_valid_expiration_year is False


@pytest.mark.parametrize('data', ['160cm', '190cm', '60in', '76in'])
def test_valid_height(data):
    sut = Passport({'hgt': data})
    assert sut.has_valid_height is True


@pytest.mark.parametrize('data', ['145cm', '200cm', '40in', '80in', '160', 'NaN', '', '-100'])
def test_invalid_height(data):
    sut = Passport({'hgt': data})
    assert sut.has_valid_height is False


@pytest.mark.parametrize('data', ['#000000', '#123456', '#ffffff', '#0e8a3b'])
def test_valid_hair_color(data):
    sut = Passport({'hcl': data})
    assert sut.has_valid_hair_color is True


@pytest.mark.parametrize('data', ['000000', '#fff', 'green', '#00zzef', ''])
def test_invalid_hair_color(data):
    sut = Passport({'hcl': data})
    assert sut.has_valid_hair_color is False


@pytest.mark.parametrize('data', ['amb', 'blu', 'brn', 'gry', 'grn', 'hzl', 'oth'])
def test_valid_eye_color(data):
    sut = Passport({'ecl': data})
    assert sut.has_valid_eye_color is True


@pytest.mark.parametrize('data', ['red', 'blk', 'green', 'blue', ''])
def test_invalid_eye_color(data):
    sut = Passport({'ecl': data})
    assert sut.has_valid_eye_color is False


@pytest.mark.parametrize('data', ['000000000', '123456789', '000123456', '999999999'])
def test_valid_passport_id(data):
    sut = Passport({'pid': data})
    assert sut.has_valid_passport_id is True


@pytest.mark.parametrize('data', ['0', '123456', '-000000000', '-00000000', '1234567890'])
def test_invalid_passport_id(data):
    sut = Passport({'pid': data})
    assert sut.has_valid_passport_id is False


@pytest.mark.parametrize('data', [
    "eyr:1972 cid:100\r\nhcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
    "iyr:2019\r\nhcl:#602927 eyr:1967 hgt:170cm\r\necl:grn pid:012533040 byr:1946",
    "hcl:dab227 iyr:2012\r\necl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
    "hgt:59cm ecl:zzz\r\neyr:2038 hcl:74454a iyr:2023\r\npid:3556412378 byr:2007",
])
def test_invalid_passport_values(data):
    sut = parse_passport(data)
    assert sut.has_valid_fields is False


@pytest.mark.parametrize('data', [
    "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980\r\nhcl:#623a2f",
    "eyr:2029 ecl:blu cid:129 byr:1989\r\niyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
    "hcl:#888785\r\nhgt:164cm byr:2001 iyr:2015 cid:88\r\npid:545766238 ecl:hzl\r\neyr:2022",
    "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719",
])
def test_valid_passport_values(data):
    sut = parse_passport(data)
    assert sut.has_valid_fields is True