## Sample 1: Basic Usage

The `Text` property is binding value of input.

The `Mask` property expects some string input, which defines the data for the control.

The `Placeholder` property works as placeholder on input element.

### Default Patterns

If you do not set the `Patterns` property, like in this example, MaskedTextBox uses the default patterns:

 - **0** - Digit. Accepts any digit between 0 and 9.
 - **9** - Digit or space. Accepts any digit between 0 and 9 or space.
 - **#** - Digit or space. Identical to Rule 9. In addition, allows the + (plus) and - (minus) signs.
 - **L** - Letter. Restricts the input to a-z and A-Z letters. This rule is equivalent to [a-zA-Z] in regular expressions.
 - **?** - Letter or space. Restricts the input to letters a-z and A-Z. This rule is equivalent to [a-zA-Z]|\s in regular expressions.
 - **&** - Character. Accepts any character except a space. The rule is equivalent to \S in regular expressions.
 - **C** - Character or space. Accepts any character. The rule is equivalent to . in regular expressions.
 - **A** - Alphanumeric. Accepts letters and digits only.
 - **a** - Alphanumeric or space. Accepts only letters, digits, and space.