use std::fs;

fn main() {
    let input = fs::read_to_string("input/day01.txt").expect("Failed to read input file");
    let part_one = input.chars().fold(0, |acc, c| match c {
        '(' => acc + 1,
        ')' => acc - 1,
        _ => acc,
    });
    println!("Part one: {}", part_one);

    let part_two = input
        .chars()
        .scan(0, |floor, c| {
            *floor += match c {
                '(' => 1,
                ')' => -1,
                _ => 0,
            };
            if *floor == -1 { None } else { Some(*floor) }
        })
        .count();
    println!("Part two: {}", part_two);
}
