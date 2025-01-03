import Foundation

extension Bundle {
    public func readText(fromResource resource: String) -> String {
        let path = self.path(forResource: resource, ofType: "txt")!
        let data = FileManager.default.contents(atPath: path)!
        return String(data: data, encoding: String.Encoding.utf8)!
    }

    public func readLines(fromResource resource: String) -> [String] {
        let lines = self.readText(fromResource: resource).split(separator: "\n")
        return lines.map { String($0) }
    }
}
