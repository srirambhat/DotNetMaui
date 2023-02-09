namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    private static bool start = false;
    private long number1 = 0;
    private long number2 = 0;
    private long multiplier = 1;
    private bool processNumber2Now = false;
    private char sOperator;

    public MainPage()
	{
		InitializeComponent();
	}

    private decimal ProcessCalulate(long number1, long number2, char lOper)
    {
        decimal retValue = 0;

        switch (lOper)
        {
            case '+':
                retValue = (decimal) number1 + number2;
                break;
            case '-':
                retValue = (decimal) number1 - number2;
                break;
            case '/':
                retValue = (decimal) number1 / number2;
                break;
            case '*':
                retValue = (decimal) number1 * number2;
                break;
            default:
                break;
        }
        return retValue;
    }

    private void ProcessNumbers(object sender, EventArgs e)
	{
        if (start)
        {
            start = false;
            multiplier = 1;
        }
        else
        {
            multiplier = 10;
        }

        if (!processNumber2Now)
        {
            // Processing Number 1
            String value = ((Button)sender).Text;
            number1 *= multiplier;
            number1 += Int32.Parse(value);
            Console.WriteLine("Number1: " + number1);
            CounterBtn.Text = $"{number1}";
        }
        else
        {
            // Processing Number 2
            String value = ((Button)sender).Text;
            number2 *= multiplier;
            number2 += Int32.Parse(value);
            Console.WriteLine("Number2: " + number2);
            CounterBtn.Text = $"{number2}";
        }

        SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void ProcessOperators(object sender, EventArgs e)
    {

        String value = ((Button)sender).Text;
        decimal lDisplayValue;

        if (value.Contains('='))
        {
            // Is this equal to 
            // Process the two number and print the result.
            lDisplayValue = ProcessCalulate(number1, number2, sOperator);
            CounterBtn.Text = $"{lDisplayValue:0000.0000}";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        else if (value.Contains('C'))
        {
            // Is this equal to 
            // Process the two number and print the result.
            number1 = 0;
            number2 = 0;
            start = false;
            processNumber2Now = false;
            multiplier = 1;
            
            CounterBtn.Text = $" ";
            SemanticScreenReader.Announce(CounterBtn.Text);

        }
        else if (!processNumber2Now)
        {
            CounterBtn.Text = $" ";
            SemanticScreenReader.Announce(CounterBtn.Text);
            switch (value[0])
            {
                case '+':
                case '*':
                case '/':
                case '-':
                    sOperator = value[0];
                    break; 
            }

            // it is either * or + so process number 2 now
            processNumber2Now = true;
            start = false;

        }

        /*
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
        */
    }
}

