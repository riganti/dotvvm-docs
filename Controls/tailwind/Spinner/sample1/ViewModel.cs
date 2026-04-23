public class SpinnersViewModel
{
    public bool IsLoading { get; set; } = false;

    public void ToggleLoading()
    {
        IsLoading = !IsLoading;
    }
}