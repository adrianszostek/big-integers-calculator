using BICViewNamespace;
using BICOperationsNamespace;

string operationSign;
string firstNum;
string secondNum;

while(true)
{
    Console.WriteLine("First number:");
    firstNum = Console.ReadLine();
    Console.WriteLine("\nOperation (+ - * /):");
    operationSign = Console.ReadLine();
    Console.WriteLine("\nSecond number:");
    secondNum = Console.ReadLine();
    Console.WriteLine("\n");

    switch (operationSign) {
        case "+":
            sbyte[] additionResult = BICOperations.Addition(firstNum, secondNum);
            BICView.DisplayResult(additionResult);
            break;
        case "-":
            sbyte[] subtractionResult;
            // checking which number is bigger
            if (firstNum.Length > secondNum.Length)
                subtractionResult = BICOperations.Subtraction(firstNum, secondNum);
            else if (firstNum.Length < secondNum.Length)
                subtractionResult = BICOperations.Subtraction(secondNum, firstNum, true);
            else
            {
                int differentDigitIndex = 0;
                while (firstNum[differentDigitIndex] == secondNum[differentDigitIndex])
                    differentDigitIndex++;
                if (firstNum[differentDigitIndex] > secondNum[differentDigitIndex])
                    subtractionResult = BICOperations.Subtraction(firstNum, secondNum);
                else
                    subtractionResult = BICOperations.Subtraction(secondNum, firstNum, true);
            }
            BICView.DisplayResult(subtractionResult);
            break;
        case "*":
            int[] multiplicationResult = BICOperations.Multiplication(firstNum, secondNum);
            BICView.DisplayResult(multiplicationResult);
            break;
        case "/":
            Console.WriteLine("Maximal number of significant digits:");
            byte significantDigits = byte.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            int[] divisionResult = BICOperations.Division(firstNum, secondNum, significantDigits);
            BICView.DisplayResultFloatingPoint(divisionResult, firstNum.Length-secondNum.Length);
            break;
        default:
            Console.WriteLine("Wrong operation sign");
            break;
    }
    Console.WriteLine("\n\n");


}
