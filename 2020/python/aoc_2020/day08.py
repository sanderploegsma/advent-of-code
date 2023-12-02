"""Advent of Code 2020 - Day 8."""


def boot(code: list[str]):
    acc = 0
    pos = 0

    visited = set()
    while pos not in visited and pos < len(code):
        visited.add(pos)

        match code[pos].split(" "):
            case "nop", _:
                pos += 1
            case "acc", x:
                acc += int(x)
                pos += 1
            case "jmp", x:
                pos += int(x)

    return acc, pos == len(code)


with open("../input/08.txt", encoding="utf-8") as file:
    input = file.readlines()

acc, _ = boot(input)
print("Part one:", acc)


def fix_boot_code():
    for pos, instruction in enumerate(input):
        if "acc" in instruction:
            continue

        input_copy = input.copy()
        if "jmp" in instruction:
            input_copy[pos] = instruction.replace("jmp", "nop")
        if "nop" in instruction:
            input_copy[pos] = instruction.replace("nop", "jmp")

        yield input_copy


acc = next(x for x, ok in list(boot(code) for code in fix_boot_code()) if ok)
print("Part two:", acc)
