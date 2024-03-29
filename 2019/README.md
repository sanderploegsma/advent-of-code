# Advent of Code 2019

This is my first year participating in Advent of Code! To make matters worse, I decided to brush up my Go skills, which means that things like finding the sum, maximum or distinct set of values in a collection have to be done manually. We'll see how long this choice lasts.

Anyway, if anyone is reading this, feel free to let me know if I did something horribly wrong (or stupid). I am always eager to learn!

## Notes

I will try and maintain notes for particular challenges if I think they are worth sharing.

### Day 01

Initially, I went down the same path as a lot of other solutions by using an imperative way of calculating the fuel needed in part two. However, using recursion makes a lot more sense, while also improving the readability of the code.

### Day 03

I guess there must be a less naive way of doing this, because my current setup takes a whopping _24 seconds_ on average to calculate the result. However, it is pretty readable IMO. Curious to see if a more performant solution is as easy to follow.

### Day 04

Hmm, I have a range, but should it be inclusive? Is `000001` also possible? For my specific input (`254032-789860`) I guess it doesn't matter. Still, this is confusing.

Also, it took me a while to get the second part working. What didn't help is that my unit test had a bug (expecting `false` instead of `true` for input `111122`), so I had a hard time figuring out what was wrong with my code.

I'm still pondering whether there is a nice way of doing this challenge without converting the input to strings and looping over each digit. Something like this might work, but I'm not sure if I like it:

```go
func toDigits(password int) []int {
  digits := make([]int, 0)
  remainder := password

  // idea: calculate the digit using this formula:
  //
  // password % 10^(n+1)
  // -------------------
  //        10^n
  //
  // This only works if we start with the smallest value, so after each iteration the remainder will be:
  //
  // i=0: 123456
  // i=1: 123450
  // i=2: 123400
  // etc.
  for i := 0; i < 6; i++ {
    // Not sure if this even compiles because math.Pow10(i) returns a float.
    // For sake of argument, let's pretend it returns an int
    val := remainder % math.Pow10(i+1)
    digits = append(digits, val / math.Pow10(i))

    remainder = remainder - val
  }

  // digits contains digits from right-to-left, so we have to reverse them
  for i := len(digits)/2-1; i >= 0; i-- {
    opp := len(digits)-1-i
    digits[i], digits[opp] = digits[opp], digits[i]
  }

  return digits
}
```

**Update:** my colleagues pointed out that because digits have to be ascending, checking if a password contains two adjacent digits is as easy as checking if they occur two times. This makes the code much easier to reason about, which is why I rewrote it.

Note that this solution aims for readability and not performance, because it loops many more times than actually needed. Specifically the `isAscending` and `countDigits` functions could have been combined, but what would have been the signature? I don't really like this:

```go
func countDigitsAndCheckIfAscending(digits []int) (map[int]int, bool) {}
```

### Day 05

"Oh cool, I can use channels for the input and output!" - Me

I kind of liked this challenge. Granted, there was a bit of copying and pasting going on to get all of the opcodes in there, and for a minute I was trying to find a way to repeat myself less. The only real headache I got from this puzzle was figuring out when I had to use the read modes w.r.t. the third parameters of some opcodes (the one telling you which index to write to).
