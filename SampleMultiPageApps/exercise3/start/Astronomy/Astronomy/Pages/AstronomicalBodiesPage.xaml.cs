namespace Astronomy.Pages;

public partial class AstronomicalBodiesPage : ContentPage
{
    public AstronomicalBodiesPage()
	{
		InitializeComponent();

		btnComet.Clicked += async(s, e) => await Shell.Current
                            .GoToAsync($"{nameof(AstronomicalBodyPage)}?astroName=comet");
        btnEarth.Clicked += async (s, e) => await Shell.Current
                            .GoToAsync($"{nameof(AstronomicalBodyPage)}?astroName=earth");
        btnMoon.Clicked += async (s, e) => await Shell.Current
                            .GoToAsync($"{nameof(AstronomicalBodyPage)}?astroName=moon");
        btnSun.Clicked += async (s, e) => await Shell.Current
                            .GoToAsync($"{nameof(AstronomicalBodyPage)}?astroName=sun");
        
    }
}