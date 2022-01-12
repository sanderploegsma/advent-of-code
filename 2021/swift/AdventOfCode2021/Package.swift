// swift-tools-version:5.5
import PackageDescription

let package = Package(
    name: "AdventOfCode2021",
    products: [],
    dependencies: [
        .package(url: "https://github.com/apple/swift-algorithms", from: "1.0.0"),
    ],
    targets: [
        .target(name: "Common"),
        .executableTarget(
            name: "Day01",
            dependencies: [
                .target(name: "Common"),
                .product(name: "Algorithms", package: "swift-algorithms"),
            ]),
        .executableTarget(
            name: "Day02",
            dependencies: [
                .target(name: "Common"),
            ]),
        .executableTarget(
            name: "Day03",
            dependencies: [
                .target(name: "Common"),
                .product(name: "Algorithms", package: "swift-algorithms"),
            ]),
    ])
