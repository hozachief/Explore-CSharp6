/** 
 * Method AllCaps() has the undesirable side effect of modifying the property values
 * along with returning the uppercase string. The author of the Person class
 * intended the strings for FirstName and LastName to be read-only. With C# 6, you
 * can make that intent clear. Remove the private set from both properties to create
 * a read-only auto property. The compiler spots the two locations where the
 * FirstName and LastName properties are changed when they should not have been.
*/

/** 
 * If you repeatedly use one class with static methods throughout your code,
 * including the class name each time obscures the meaning of your code.
 * 
 * 'using static' statement becomes more useful in larger programs that make extensive
 * use of a single class with many static methods, like the string class or
 * the System.Math class.
 * JOSE: So too generic...
*/
using System;
using static System.Console;
using System.Linq;

public class Person
{
    //public string FirstName { get; private set; }
    //public string LastName { get; private set; }
    public string FirstName { get; }
    public string LastName { get; }
    /** 
     * New syntax in C# 6 enables you to use initializers for auto-properties.
     * 
     * The assignment on the MiddleName property is an initializer. It initializes 
     * the compiler-generated backing field for the middle name.
    */
    public string MiddleName { get; } = "";

    public Person(string first, string middle, string last)
    {
        FirstName = first;
        MiddleName = middle;
        LastName = last;
    }

    /** 
     * Expression-bodied members provide a lightweight syntax for lightweight methods.
     *    
    */
    /*
    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
    */
    // Expression-bodied member
    //public override string ToString() => FirstName + " " + LastName;
    /** 
     * C# 6 contains new syntax for composing strings from a string and embedded
     * expressions that are evaluated to produce other string values.
    */
    public override string ToString() => $"{FirstName} {LastName}";

    /*
    public string AllCaps()
    {
        // Compiler Error: Property or indexer 'Person.LastName' cannot be assigned
        // to -- it is read only
        //FirstName = FirstName.ToUpper();
        //LastName = LastName.ToUpper();
        //return ToString();
        return ToString().ToUpper();
    }
    */
    // Expression-bodied member
    public string AllCaps() => ToString().ToUpper();
}

public class Program
{
    public static void Main()
    {
        //var p = new Person("Jose", "Fraga");
        var p = new Person("Jose", "N/A", "Fraga");
        //Console.WriteLine("The name, in all caps: " + p.AllCaps());
        //Console.WriteLine("The name: " + p);
        /** 
         * C# 6 contains new syntax for composing strings from a string and embedded
         * expressions that are evaluated to produce other string values.
        */
        WriteLine($"The name, in all caps: {p.AllCaps()}");
        WriteLine($"The name is: {p}");

        /** 
         * ...      
        */
        var phrase = "the quick brown fox jumps over the lazy dog";
        var wordLength = from word in phrase.Split(" ") select word.Length;
        var average = wordLength.Average();
        // You can remove the local variable average and perform that calculation
        // as part of the interpolated string expression.
        //WriteLine(average);
        WriteLine($"The average word length is: {wordLength.Average()}");
        /** 
         * String interpolation syntax supports all the format strings available
         * using earlier formatting methods. You can specify the format string inside
         * the braces. Add a : following the expression to format.       
        */
        WriteLine($"The average word length is: {wordLength.Average():F2}");

        string s = null;
        // System.NullReferenceException thrown
        //WriteLine(s.Length);
        /** 
         * The ?. (null conditional) operator makes it easier to write logic that
         * takes null values into account seamlessly, without extra if checks.
         * There is no output because the result of s?.Length is an int? when the
         * result of s.Length is an int. In this example, s?.Length is null.
         * The ?. returns null if its left operand is null. If the type of the right
         * operand is a value type, the ?. operator return a nullable type for that
         * type.       
        */
        WriteLine(s?.Length);

        // Use ?[] for array or indexer access.
        char? c = s?[0];
        WriteLine(c.HasValue);

        /** 
         * Multiple conditional operators can be combined into a single expression.
         * A null left operand produces a null result, making it easy to avoid
         * nested if clauses to access members of members.       
        */
        string ss = null;
        /*
        bool? hasMore = ss?.ToCharArray()?.GetEnumerator()?.MoveNext();
        WriteLine(hasMore.HasValue);
        */
        // Preceding example simplified by using the null coalescing operator to
        // provide a default value.
        // JOSE: "Simplified"...sure...
        bool hasMore = ss?.ToCharArray()?.GetEnumerator()?.MoveNext() ?? false;
        WriteLine(hasMore);

        /** 
         * Exception filters
         * Exception filters enable you to catch an exception based on some condition.
         * A typical use is to create a filter method that logs exceptions, but
         * never handles those exceptions. An exception filter is a boolean expression
         * that is true when the catch clause should be executed, and false when
         * the exception should not be caught by the catch clause.
        */
        try
        {
            string sss = null;
            WriteLine(sss.Length);
        } catch (Exception e) when (LogException(e))
        {
        }
        WriteLine("Exception must have been handled");
    }

    private static bool LogException(Exception e)
    {
        WriteLine($"\tIn the log routine. Caught {e.GetType()}");
        WriteLine($"\tMessage: {e.Message}");
        // Returns false, which indicates that the exception cannot be handled.
        return false;
        // Return true...the exception is caught and the program runs to completion.
        //return true;
    }
}