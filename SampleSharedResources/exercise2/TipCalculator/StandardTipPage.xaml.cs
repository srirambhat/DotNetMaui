namespace TipCalculator;

public partial class StandardTipPage : ContentPage
{
    private Color colorNavy = Colors.Navy;
    private Color colorSilver = Colors.Silver;

    public StandardTipPage()
    {
        InitializeComponent();
        billInput.TextChanged += (s, e) => CalculateTip();
    }

    void CalculateTip()
    {
        double bill;

        if (Double.TryParse(billInput.Text, out bill) && bill > 0)
        {
            double tip = Math.Round(bill * 0.15, 2);
            double final = bill + tip;

            tipOutput.Text = tip.ToString("C");
            totalOutput.Text = final.ToString("C");
        }
    }

    void OnLight(object sender, EventArgs e)
    {
        Resources["fgColor"] = Colors.Navy;
        Resources["bgColor"] = Colors.Silver;
    }

    void OnDark(object sender, EventArgs e)
    {
        Resources["fgColor"] = Colors.Silver;
        Resources["bgColor"] = Colors.Navy;
    }

    async void GotoCustom(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CustomTipPage));
    }
}