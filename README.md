### Big Numbers Calculator
The CLI App enabling addition, subtraction, multiplication and division (with floating point) of two integers.

The original project was created as an assignment on my studies in C++. I decided to rewrite it, developing my C# / .NET skills.

In comparison with the old version:
- the limit of the integers' length was increased from 1000 to the practical limit of an array length which depends on the available memory; in the best case scenario the limit is defined by the Microsoft CLR restriction - 2GB per structure.
- arrays' types were optimized for memory consumption,
- a simple MVC pattern was adopted,
