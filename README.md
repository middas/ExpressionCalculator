# ExpressionCalculator

## Specification
Your calculator program must accept, as input, a single arithmetic expression. This arithmetic expression
will be encoded as a string using the format specified below.
The calculator must produce, as output, the result of evaluating the input arithmetic expression.

## Grammar

### Program Input
The calculator input is a single arithmetic expression, encoded as a string, in the following format.
Expression
An expression may be one of the following:
* number
* variable-name
* add ( expression , expression )
* sub ( expression , expression )
* mul ( expression , expression )
* div ( expression , expression )
* let ( variable-name , expression , expression )

#### Where number is:
* the zero digit (0), or
* an optional minus sign (-), followed by one non-zero digit, and then any number of digits

#### variable-name is:
* a sequence of underscores, digits, lowercase letters, and uppercase letters
* which does not begin with a digit
* and which is not equal to “add”, “sub”, “mul”, “div”, or “let”

Whitespace characters may not appear inside numbers, variable-names, or keywords (i.e. “add”, “sub”,
“mul”, “div”, “let”). Otherwise, whitespace characters are ignored.
Variable names and keywords are case-sensitive.
All other inputs are illegal.

### Semantics
#### Numeric Value
The result of evaluating any expression, including the main input expression and any intervening
recursively specified expressions, is a numeric value.
A numeric value is an arithmetic data type which can represent any signed integer between -32,768 and
+32,767, inclusive. Compliant calculator programs must support at least this range of values.
Implementations may optionally support a larger range of values. However, it is not required, and this
document intentionally does not define how the implementation should handle values or expression
results outside the required range. (i.e. We will never test your program with values less than -32,768,
or greater than +32,767.)
### Evaluation
Expressions shall be evaluated according to the following rules.
#### Number Expressions
A textual number expression shall be converted into a numeric value by interpreting that text as a
human-readable base 10 encoding of that number.
The program behavior is implementation-defined if the number expression describes a value which is
less than -32,768, or greater than +32,767.
#### Add Expressions
Given the following expression:
* add ( augend-expression , addend-expression )

The numeric value is equal to the numeric value of augend-expression plus addend-expression.
The program behavior is implementation-defined if the result would be less than -32,768, or greater
than +32,767.

#### Sub Expressions
Given the following expression:
* sub ( minuend-expression , subtrahend-expression )

The numeric value is equal to the numeric value of minuend-expression minus subtrahend-expression.
The program behavior is implementation-defined if the result would be less than -32,768, or greater
than +32,767.
#### Mul Expressions
Given the following expression:
* mul ( multiplicand-expression , multiplier-expression )

The numeric value is equal to the numeric value of multiplicand-expression times multiplier-expression.
The program behavior is implementation-defined if the result would be less than -32,768, or greater
than +32,767.
#### Div Expressions
Given the following expression:
* div ( dividend-expression , divisor-expression )

The numeric value is equal to the numeric value of dividend-expression divided by divisor-expression,
rounded toward zero.
It is an error if divisor-expression has a numeric value of 0.
#### Let Expressions
Given the following expression:
* let ( assigned-variable-name, value-expression, result-expression )

The numeric value is equal to the numeric value of result-expression, where all enclosed instances of
assigned-variable-name are evaluated as if they are equal to value-expression.

It is an error if assigned-variable-name is ever used inside value-expression. It is also an error if assigned-
variable-name is assigned a value by any let expression contained within result-expression or value-
expression.

##### Variable Name Expressions
The numeric value of a variable name expression is equal to the value previously assigned to this name
by a containing let expression.
It is an error to use a variable name which has not been assigned a value by a containing let expression.