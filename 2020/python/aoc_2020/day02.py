def parse_line(line):
    policy, password = line.split(": ")
    rule, target = policy.split(' ')
    a, b = [int(x) for x in rule.split('-')]

    return a, b, target, password

input = open('2020/input/day02.txt').read()
passwords = [parse_line(x) for x in input.splitlines()]

def validate_part_one(min, max, target, password):
    """
    Validate whether the given password contains the target character at least `min` times, and no more than `max`.
    """
    count = len([c for c in password if c == target])
    return count >= min and count <= max

def validate_part_two(a, b, target, password):
    """
    Validate whether the given password contains the target character in position `a` or `b`, but not both.
    """
    return (password[a-1] == target) ^ (password[b-1] == target)

print('part one: %d' % len([p for p in passwords if validate_part_one(*p)]))
print('part two: %d' % len([p for p in passwords if validate_part_two(*p)]))
