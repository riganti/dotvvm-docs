## Sample 3: Custom Patterns

The `Patterns` property expects `Dictionary<char, MaskPattern>`, where the key is text character and `MaskPattern` defines regular expressions for client and server.

Unlike the `AdditionalPatterns` property, this one will replace the default patterns.