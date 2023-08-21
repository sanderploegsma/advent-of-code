// swift-tools-version:5.8
import PackageDescription

let package = Package(
  name: "AdventOfCode2023",
  products: [],
  dependencies: [
    .package(url: "https://github.com/apple/swift-algorithms", from: "1.0.0")
  ],
  targets: [
    .target(name: "Common"),
    .executableTarget(
      name: "Day01",
      dependencies: [.target(name: "Common")],
      resources: [.process("input.txt")]
    ),
  ]
)
